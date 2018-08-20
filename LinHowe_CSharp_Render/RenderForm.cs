using LinHowe_CSharp_Render.Math;
using LinHowe_CSharp_Render.Render;
using LinHowe_CSharp_Render.Test;
using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace LinHowe_CSharp_Render
{
    /// <summary>
    /// 渲染器的窗口
    /// </summary>
    public partial class RenderForm : Form
    {
        
        Graphics g = null;
        
        public RenderForm()
        {
            InitializeComponent();

            //Init buffer
            Draw.Init(MaximumSize.Width, MaximumSize.Height);

            

            System.Timers.Timer mainTimer = new System.Timers.Timer(1000);
            mainTimer.Elapsed += new ElapsedEventHandler(Tick);
            mainTimer.AutoReset = true;
            mainTimer.Enabled = true;
            mainTimer.Start();
        }
        private void Init()
        {
            RenderStage.Render();
            RenderStage _stage = RenderStage._stage;
            ApplicationStage Astage = (ApplicationStage)_stage;

            //Init Mesh
            Mesh cubeMesh = new Mesh(CubeData.pointList, CubeData.indexs, CubeData.norlmas);
            Astage.AddMesh(cubeMesh);

            //Init Camera
            Camera _camera = new Camera();
            _camera.pos = new Vector3(0, 0, 0, 1);
            _camera.lookAt = new Vector3(0, 0, 1, 1);
            _camera.up = new Vector3(0, 1, 0, 0);
            Astage.AddCamera(_camera);
        }
        private void Tick(object sender, EventArgs e)
        {
            lock (Draw._frameBuff)
            {
                Init();
                //渲染流水线
                while (!RenderStage.RenderEnd)
                    RenderStage.Render();

                if (g == null)
                {
                    g = this.CreateGraphics();
                }

                //清屏
                g.Clear(System.Drawing.Color.Black);

                //渲染
                g.DrawImage(Draw._frameBuff, 0, 0);
            }
        }

        

    }
}
