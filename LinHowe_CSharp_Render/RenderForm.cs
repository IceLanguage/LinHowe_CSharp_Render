using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using LinHowe_CSharp_Render.Math;
using LinHowe_CSharp_Render.Render;
using LinHowe_CSharp_Render.Test;

namespace LinHowe_CSharp_Render
{
    /// <summary>
    /// 渲染器的窗口
    /// </summary>
    public partial class RenderForm : Form
    {
        
        Graphics g = null;
        Graphics G
        {
            get
            {
                if(null == g) g = CreateGraphics();
                return g;
            }
        }
        Bitmap texture;
        bool isInit = false;
        public RenderForm()
        {
            InitializeComponent();

            //Init buffer
            Draw.Init(MaximumSize.Width, MaximumSize.Height);

            System.Timers.Timer mainTimer = new System.Timers.Timer(1000/60);
            mainTimer.Elapsed += new ElapsedEventHandler(Tick);
            mainTimer.AutoReset = true;
            mainTimer.Enabled = true;
            mainTimer.Start();
        }

        /// <summary>
        /// 场景更新
        /// </summary>
        private void UpdateScene()
        {
            Draw.Clear();
            Rendering_pipeline.Render();

            foreach (GameObject go in Rendering_pipeline._models)
            {
                go.mesh.Reset();               
            }

            if (!isInit)
            {
                isInit = true;
                Init();
            }
            
            //旋转
            foreach(GameObject go in Rendering_pipeline._models)
            {
                go.rotation.x += 0.5f;
                go.rotation.y += 0.5f;
                go.rotation.z += 0.5f;
                go.ObjectToWorldMatrix = 
                    Matrix4x4.GetRotateX(go.rotation.x) *
                    Matrix4x4.GetRotateY(go.rotation.y) *
                    Matrix4x4.GetRotateZ(go.rotation.z) *
                    Matrix4x4.GetTranslate(go.position);
            }

        }
        private void Init()
        {
            //Init Texture
            try
            {
                
                int texSize = 64;
             
                texture = new Bitmap(texSize, texSize);
                for (var i = 0; i < texSize; i++)
                    for (var j = 0; j < texSize; j++)
                    {
                        bool c = (((i & 0x8) == 0) ^ ((j & 0x8) == 0));
                        if(c)
                        {
                            texture.SetPixel(i, j, Color.White.TransFormToSystemColor());
                        }
                        else
                        {
                            texture.SetPixel(i, j, Color.Black.TransFormToSystemColor());
                        }
                    }

                //texture = (Bitmap)Image.FromFile(@"../../Resource/Lh.png", true);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("There was an error opening the bitmap." + "Please check the path.");
            }

            RenderStage _stage = Rendering_pipeline._stage;
            ApplicationStage Astage = (ApplicationStage)_stage;

            //Init GameObject
            Mesh cubeMesh = new Mesh(
                CubeData.pointList,
                CubeData.indexs,
                CubeData.norlmas,
                CubeData.vertColors,
                CubeData.mat,
                CubeData.uvs);
            cubeMesh.Texture = texture;
            cubeMesh.IsRenderTexture = true;
           GameObject cubeGameObject = new GameObject(cubeMesh, new Vector3(-3, 0, 10));
            Astage.AddGameObject(cubeGameObject);


            Mesh sphereMesh = new Mesh(
                SphereData.pointList,
                SphereData.indexs,
                SphereData.norlmas,
                SphereData.vertColors,
                SphereData.mat,
                SphereData.uvs);
            GameObject sphereGameObject = new GameObject(sphereMesh, new Vector3(3, 0, 8));
            Astage.AddGameObject(sphereGameObject);

            //Init Camera
            Camera MainCamera = new Camera
            {
                pos = new Vector3(0, 0, 0, 1),
                lookAt = new Vector3(0, 0, 1, 1),
                up = new Vector3(0, 1, 0, 0),
                fov = (float)System.Math.PI / 4f,
                aspect = MaximumSize.Width / (float)MaximumSize.Height,
                zn = 1f,
                zf = 500f,
                ScreenHeight = MaximumSize.Height
            };
            Astage.SetMainCamera(MainCamera);

            //Init Light
            Light light = new Light(new Vector3(0, 3, 0), new Vector3(0.6f, 1, 0), new Color(1, 1, 1));
            light.SetPointLight(0.3f, 0.5f);
            Astage.AddLight(light);

            


        }

        
        private void Tick(object sender, EventArgs e)
        {
            lock (Draw._frameBuff)
            {
                UpdateScene();

                //渲染流水线
                while (!Rendering_pipeline.RenderEnd)
                    Rendering_pipeline.Render();

                //渲染
                G.DrawImage(Draw._frameBuff, 0, 0);
            }
        }

        

    }
}
