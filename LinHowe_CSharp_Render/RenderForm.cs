﻿using LinHowe_CSharp_Render.Math;
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
        float rot = 0;
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
            Draw.Clear();
            RenderStage.Render();
            RenderStage _stage = Rendering_pipeline._stage;
            ApplicationStage Astage = (ApplicationStage)_stage;

            //Init Mesh
            Mesh cubeMesh = new Mesh(
                CubeData.pointList,
                CubeData.indexs,
                CubeData.norlmas,
                CubeData.vertColors,
                CubeData.mat,
                CubeData.uvs);
            Astage.AddMesh(cubeMesh);

            //Init Camera
            Camera _camera = new Camera();
            _camera.pos = new Vector3(0, 0, 0, 1);
            _camera.lookAt = new Vector3(0, 0, 1, 1);
            _camera.up = new Vector3(0, 1, 0, 0);
            _camera.fov = (float)System.Math.PI / 4f;
            _camera.aspect = this.MaximumSize.Width / (float)this.MaximumSize.Height;
            _camera.zn = 1f;
            _camera.zf = 500f;
            Astage.AddCamera(_camera);

            //Init Light
            Light light = new Light(new Vector3(50, 1, 1), new Math.Color(1, 1, 1));
            Astage.AddLight(light);
           
            rot += 0.2f;
            Rendering_pipeline.m = 
                Matrix4x4.GetRotateX(rot) * 
                Matrix4x4.GetRotateY(rot) *
                Matrix4x4.GetRotateZ(rot) *
                Matrix4x4.GetTranslate(0, 0, 8);
        }
        private void Tick(object sender, EventArgs e)
        {
            lock (Draw._frameBuff)
            {
                Init();

                
                //渲染流水线
                while (!Rendering_pipeline.RenderEnd)
                    RenderStage.Render();

                if (g == null)
                {
                    g = this.CreateGraphics();
                }

                
                //渲染
                g.DrawImage(Draw._frameBuff, 0, 0);
            }
        }

        

    }
}
