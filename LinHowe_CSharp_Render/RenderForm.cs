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
        private RenderStage stage = new ApplicationStage();
        private Camera _camera;
        private Draw _draw;
        Graphics g = null;

        
        public RenderForm()
        {
            InitializeComponent();

            //Init buffer
            _draw = new Draw(MaximumSize.Width, MaximumSize.Height);

            //Init Mesh
            Mesh cubeMesh = new Mesh(CubeData.pointList, CubeData.indexs, CubeData.norlmas);

            //Init Camera
            _camera.pos = new Vector3(0, 0, 0, 1);
            _camera.lookAt = new Vector3(0, 0, 1, 1);
            _camera.up = new Vector3(0, 1, 0, 0);

            System.Timers.Timer mainTimer = new System.Timers.Timer(1000);
            mainTimer.Elapsed += new ElapsedEventHandler(Tick);
            mainTimer.AutoReset = true;
            mainTimer.Enabled = true;
            mainTimer.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            lock (_draw._frameBuff)
            {

                if (g == null)
                {
                    g = this.CreateGraphics();
                }
                g.Clear(System.Drawing.Color.Black);
                g.DrawImage(_draw._frameBuff, 0, 0);
            }
        }

        

    }
}
