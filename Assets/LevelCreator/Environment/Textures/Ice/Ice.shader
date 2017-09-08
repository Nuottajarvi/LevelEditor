// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Seinelake/Ice" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo", 2D) = "white" {}
		_BumpMap("Normal map", 2D) = "bump" {}
		_Magnitude ("Magnitude", Range(0,1)) = 0.05
		_BumpDepth ("Bump Depth", Range(0, 5.0)) = 1
		
	}
	SubShader{
		GrabPass {"_GrabTexture"}
		
		Tags {"Queue"="Transparent" "RenderType"="Opaque" "LightMode" = "ForwardBase"}
		Blend One Zero
		
		Pass{
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#pragma multi_compile_fwdbase
			#include "AutoLight.cginc"
 
			uniform sampler2D _GrabTexture;
			uniform sampler2D _MainTex;
			uniform fixed4 _Color;
			
			uniform sampler2D _BumpMap;
			uniform float4 _BumpMap_ST;
			uniform float _Magnitude;
			uniform float _BumpDepth;
			uniform float4 _LightColor0;

			struct vertexInput{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;				
			};
			
			struct vertexOutput{
				float4 pos : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				float4 uvgrab : TEXCOORD1;
				float3 normalWorld : TEXCOORD2;
				float3 tangentWorld : TEXCOORD3;
				float3 binormalWorld : TEXCOORD4;
				LIGHTING_COORDS(5,6)
			};
			
			vertexOutput vert(vertexInput v){
				vertexOutput o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.texcoord = v.texcoord;
				
				o.normalWorld = normalize(mul( float4(v.normal, 0.0), unity_WorldToObject).xyz);
				
				o.tangentWorld = normalize(mul(unity_ObjectToWorld, v.tangent).xyz);
				o.binormalWorld = normalize(cross( o.normalWorld, o.tangentWorld));
				
				o.uvgrab = ComputeGrabScreenPos(o.pos);
				
				TRANSFER_VERTEX_TO_FRAGMENT(o);
				
				return o;
			}
			
			half4 frag(vertexOutput i) : COLOR{
				half4 mainColour = tex2D(_MainTex, i.texcoord);
				
				//DISTORTION
				half4 bump = tex2D(_BumpMap, i.texcoord);
				half2 distortion = UnpackNormal(bump).rg;
				
				i.uvgrab.xy += distortion * _Magnitude;
				
				//LIGHTING
				float attenuation = LIGHT_ATTENUATION(i);
				float3 lightDirection;
				
				if(_WorldSpaceLightPos0.w == 0.0){
					//attenuation = 1.0;
					lightDirection = normalize( _WorldSpaceLightPos0.xyz );
				}else{
					float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.pos.xyz;
					float distance = length(fragmentToLightSource);
					//attenuation = 1.0 / distance;
					lightDirection = normalize(fragmentToLightSource);
				}
				
				
			//BUMPMAP
				float4 normalTexture = tex2D(_BumpMap, i.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw);
				float3 localCoords = float3(2.0 * normalTexture.ag - float2(1.0, 1.0), 0.0);
				localCoords.z = _BumpDepth - 0.5 * dot(localCoords, localCoords); //can also be just BumpDepth. Check difference
				
				//normal transpose matrix
				float3x3 local2WorldTranspose = float3x3(i.tangentWorld, i.binormalWorld, i.normalWorld);
				
				//calculate normal direction
				float normalDirection = normalize( mul( localCoords, local2WorldTranspose));
				
				float3 diffuseReflection = attenuation * _LightColor0 * saturate(dot(normalDirection, lightDirection));
				
				float3 lightFinal = diffuseReflection + UNITY_LIGHTMODEL_AMBIENT + float3(1,1,1) + float3(0.2,0.2,0.2) * mainColour.w;
			
				return float4(mainColour * lightFinal * _Color.rgb, 1.0) * tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(i.uvgrab));
			}
		
			ENDCG
		} 
	}	
}
