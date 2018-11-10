using LinHowe_CSharp_Render.Math;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinHowe_CSharp_Render.Render
{
    /// <summary>
    /// 像素着色器
    /// </summary>
    static partial class PixelShader
    {

        public static void Lighting(GameObject go, Vector3 worldEyePositon, ref Vertex v)
        {
            ProgrammableShader(go, worldEyePositon, ref v);
            
        }
    }

    partial class PixelShader
    {
        private static void ProgrammableShader(GameObject go, Vector3 worldEyePositon, ref Vertex v)
        {
            Mesh mesh = go.mesh;
            Matrix4x4 m = go.ObjectToWorldMatrix;

            Vector3 worldPoint = v.position * m;//世界空间顶点位置

            //模型空间法线乘以世界矩阵的逆转置得到世界空间法线
            //原因 https://blog.csdn.net/christina123y/article/details/5963679
            Vector3 normal = (v.normal * m.Inverse().Transpose()).Normalize();

            Color emissiveColor = mesh.Mat.emissive;// v.color;//自发光
            Color ambientColor = Rendering_pipeline._ambientColor * mesh.Mat.ka;//环境光 

            foreach (Light light in Rendering_pipeline._lights)
            {


            }
        }
    }
}
