using LinHowe_CSharp_Render.Math;
using System;
using System.Drawing;

namespace LinHowe_CSharp_Render.Render
{
    static class Draw
    {
        //帧缓冲
        public static Bitmap _frameBuff;

        public static void Init(int width, int height)
        {
            _frameBuff = new Bitmap(width, height);
        }
        
        /// <summary>
        /// 光栅化
        /// </summary>
        public static void Rasterization()
        {
            foreach(Mesh mesh in Rendering_pipeline._models)
            {
                for (int i = 0; i + 2 < mesh.Vertices.Length; i += 3)
                {
                    if (mesh.Cuts[i] || mesh.Cuts[i + 1] || mesh.Cuts[i + 2])
                        continue;
                    DrawTriangle(mesh.Vertices[i], mesh.Vertices[i + 1], mesh.Vertices[i + 2]);
                }
            }

        }

        private static void DrawTriangle(Vertex p1, Vertex p2, Vertex p3)
        {
            
            BresenhamDrawLine(p1, p2);
            BresenhamDrawLine(p2, p3);
            BresenhamDrawLine(p3, p1);
        }
        /// <summary>
        /// 绘制直线，使用bresenham算法
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        private static void BresenhamDrawLine(Vertex p1, Vertex p2)
        {
            int x = (int)(System.Math.Round(p1.position.x, MidpointRounding.AwayFromZero));
            int y = (int)(System.Math.Round(p1.position.y, MidpointRounding.AwayFromZero));
            int dx = (int)(System.Math.Round(p2.position.x - p1.position.x, MidpointRounding.AwayFromZero));
            int dy = (int)(System.Math.Round(p2.position.y - p1.position.y, MidpointRounding.AwayFromZero));
            int stepx = 1;
            int stepy = 1;

            if (dx >= 0)
            {
                stepx = 1;
            }
            else
            {
                stepx = -1;
                dx = System.Math.Abs(dx);
            }

            if (dy >= 0)
            {
                stepy = 1;
            }
            else
            {
                stepy = -1;
                dy = System.Math.Abs(dy);
            }

            int dx2 = 2 * dx;
            int dy2 = 2 * dy;

            if (dx > dy)
            {
                int error = dy2 - dx;
                for (int i = 0; i <= dx; i++)
                {
                    _frameBuff.SetPixel(x, y, System.Drawing.Color.White);
                    if (error >= 0)
                    {
                        error -= dx2;
                        y += stepy;
                    }
                    error += dy2;
                    x += stepx;

                }
            }
            else
            {
                int error = dx2 - dy;
                for (int i = 0; i <= dy; i++)
                {
                    _frameBuff.SetPixel(x, y, System.Drawing.Color.White);
                    if (error >= 0)
                    {
                        error -= dy2;
                        x += stepx;
                    }
                    error += dx2;
                    y += stepy;

                }
            }

        }
    }
}
