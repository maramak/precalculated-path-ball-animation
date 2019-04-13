Shader "MK/Util/Color (NoLit)"
{
	Properties {
		_Color ("Main Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}

	SubShader {
		Pass {
			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			// User defined variables
			uniform fixed4 _Color;

			// Vertex function
			float4  vert(float4  v : POSITION) : SV_POSITION {
				return UnityObjectToClipPos(v);
			}

			// Fragment function
			fixed4 frag() : COLOR {
				return _Color;
			}

			ENDCG
		}
	}
	
	Fallback "Mobile/Diffuse"

}
