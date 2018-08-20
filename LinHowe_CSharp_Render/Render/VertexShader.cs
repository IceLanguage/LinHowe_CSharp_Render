using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinHowe_CSharp_Render.Math;

namespace LinHowe_CSharp_Render.Render
{
    /// <summary>
    /// 顶点着色器
    /// </summary>
    static partial class VertexShader
    {
        public static List<Vertex[]> Vertices = new List<Vertex[]>();
    }

    partial class VertexShader
    {
        public static void Lighting(Mesh mesh, Vector3 worldEyePositon, ref Vertex v)
        {
            Matrix4x4 m = Rendering_pipeline.m;

            Vector3 worldPoint = v.position * m;//世界空间顶点位置

            //模型空间法线乘以世界矩阵的逆转置得到世界空间法线
            //原因 https://blog.csdn.net/christina123y/article/details/5963679
            Vector3 normal = (v.normal * m.Inverse().Transpose()).Normalize();

            Math.Color emissiveColor = mesh.material.emissive;// v.color;//自发光
            Math.Color ambientColor = Rendering_pipeline._ambientColor * mesh.material.ka;//环境光 

            foreach (Light light in Rendering_pipeline._lights)
            {
                Vector3 inLightDir = (light.worldPosition - worldPoint).Normalize();

                //漫反射
                //float diffuse = System.Math.Max(Vector3.Dot(normal, inLightDir),0);
                float halftemp = Vector3.Dot(normal, inLightDir) * 0.5f + 0.5f;
                Color diffuseColor = mesh.material.diffuse * halftemp * light.lightColor;

                Vector3 inViewDir = (worldEyePositon - worldPoint).Normalize();
                Vector3 h = (inViewDir + inLightDir).Normalize();
                float specular = 0;
                if (halftemp != 0.5f)
                {
                    //防止出现光源在物体背面产生高光的情况
                    specular = (float)System.Math.Pow(System.Math.Max(Vector3.Dot(h, normal), 0), mesh.material.shininess);
                }
                Color specularColor = mesh.material.specular * specular * light.lightColor;//镜面高光

                v.lightingColor += emissiveColor + ambientColor + diffuseColor + specularColor;

            }


        }
    }
}
