using System.Collections.Generic;
using LinHowe_CSharp_Render.Math;

namespace LinHowe_CSharp_Render.Render
{
    /// <summary>
    /// 顶点着色器
    /// </summary>
    static partial class VertexShader
    {
        public static void Lighting(GameObject go, Vector3 worldEyePositon, ref Vertex v)
        {
            ProgrammableShader(go, worldEyePositon, ref v);
        }
        public static void Init(List<GameObject> models)
        {
            int count = models.Count;

            
            for (int i = 0; i < count; ++i)
            {
                int size = models[i].mesh.Vertices.Length;
          
                
                for (int j = 0; j < size; ++j)
                {
                    models[i].mesh.Vertices[j].v_shader.lightingColor = Color.Black;
                }
               
            }
        }
    }

    partial class VertexShader
    {
        private static void ProgrammableShader(GameObject go, Vector3 worldEyePositon, ref Vertex v)
        {
            Mesh mesh = go.mesh;
            Matrix4x4 m = go.ObjectToWorldMatrix;

            Vector3 worldPoint = v.position * m;//世界空间顶点位置

            //模型空间法线乘以世界矩阵的逆转置得到世界空间法线
            //原因 https://blog.csdn.net/christina123y/article/details/5963679
            Vector3 normal = (v.normal * m.Inverse().Transpose()).Normalize();

            Color emissiveColor = mesh.Mat.emissive * v.color;//自发光
            Color ambientColor = Rendering_pipeline._ambientColor * mesh.Mat.ka;//环境光 

            foreach (Light light in Rendering_pipeline._lights)
            {
                Vector3 LightDir = Light.GetDirection(light, worldPoint);

                Color lightColor = Light.GetLightColor(light, worldPoint);
                //漫反射
                float diffuse = System.Math.Max(Vector3.Dot(normal, LightDir),0);
                float halftemp = Vector3.Dot(normal, LightDir) * 0.5f + 0.5f;
                Color diffuseColor = mesh.Mat.diffuse * diffuse * lightColor;

                Vector3 ViewDir = (worldEyePositon - worldPoint).Normalize();
                Vector3 h = (ViewDir + LightDir).Normalize();
                float specular = (float)System.Math.Pow(System.Math.Max(Vector3.Dot(h, normal), 0), mesh.Mat.shininess);
                Color specularColor = mesh.Mat.specular * specular * lightColor;//镜面高光

                v.lightingColor = v.lightingColor + emissiveColor + ambientColor + diffuseColor + specularColor;

            }
        }

    }
}
