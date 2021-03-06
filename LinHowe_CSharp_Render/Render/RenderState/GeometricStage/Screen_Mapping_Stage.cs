﻿
using System;

namespace LinHowe_CSharp_Render.Render
{
    partial class Screen_Mapping_Stage
    {
        public override void ChangeState()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            foreach (GameObject go in Rendering_pipeline._models)
            {
                Mesh mesh = go.mesh;
                if (mesh.CullFlag)
                    continue;
                int size = mesh.Vertices.Length;
                for (int i = 0; i < size; ++i)
                {
                    if (mesh.Cuts[i])
                        continue;
                    TransformToScreen(ref mesh.Vertices[i].v_trans);
                }
            }
            GeometricStage.CurStage = Model_View_Transformation_Stage.instance;
            Rendering_pipeline._stage = RasterizationStage.instance;

            watch.Stop();
            TimeSpan timespan = watch.Elapsed;
            System.Diagnostics.Debug.WriteLine("5-屏幕映射执行时间：{0}(毫秒)", timespan.TotalMilliseconds);
        }

        /// <summary>
        /// 从齐次剪裁坐标系转到屏幕坐标
        /// </summary>
        private void TransformToScreen(ref Vertex v)
        {
            if (v.position.w != 0)
            {
                //先进行透视除法，转到cvv
                v.position.x *= v.onePerZ;
                v.position.y *= v.onePerZ;
                v.position.z *= v.onePerZ;
                v.position.w = 1;
                //cvv到屏幕坐标
                v.position.x = (v.position.x + 1) * 0.5f * Draw.Width;
                v.position.y = (1 - v.position.y) * 0.5f * Draw.Height;
            }

        }
    }
}
