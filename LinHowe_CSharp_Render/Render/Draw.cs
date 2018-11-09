using LinHowe_CSharp_Render.Math;
using System;
using System.Drawing;

namespace LinHowe_CSharp_Render.Render
{
    /// <summary>
    /// 绘制像素
    /// </summary>
    static class Draw
    {
        //帧缓冲
        public static Bitmap _frameBuff;
        public static float[,] _zBuff;

        public static void Clear()
        {
            Graphics.FromImage(Draw._frameBuff).Clear(System.Drawing.Color.Black);
            Array.Clear(Draw._zBuff, 0, Draw._zBuff.Length);
            //Rendering_pipeline._models.Clear();
            //Rendering_pipeline._lights.Clear();
        }
        public static void Init(int width, int height)
        {
            _frameBuff = new Bitmap(width, height);
            _zBuff = new float[width, height];
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
                    if (mesh.Blankings[i / 3])
                        continue;
                    DrawTriangle(mesh.Vertices[i], mesh.Vertices[i + 1], mesh.Vertices[i + 2],mesh);
                }
            }

        }

        private static void DrawTriangle(Point p1, Point p2, Point p3,Mesh mesh)
        {
            RasterizationTriangle(p1, p2, p3,mesh);
        }

        /// <summary>
        /// 光栅化三角形
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        private static void RasterizationTriangle(Point p1, Point p2, Point p3,Mesh mesh)
        {
            if (p1.v_trans.position.y == p2.v_trans.position.y)
            {
                if (p1.v_trans.position.y < p3.v_trans.position.y)
                {//平顶
                    DrawTriangleTop(p1, p2, p3,mesh);
                }
                else
                {//平底
                    DrawTriangleBottom(p3, p1, p2, mesh);
                }
            }
            else if (p1.v_trans.position.y == p3.v_trans.position.y)
            {
                if (p1.v_trans.position.y < p2.v_trans.position.y)
                {//平顶
                    DrawTriangleTop(p1, p3, p2, mesh);
                }
                else
                {//平底
                    DrawTriangleBottom(p2, p1, p3, mesh);
                }
            }
            else if (p2.v_trans.position.y == p3.v_trans.position.y)
            {
                if (p2.v_trans.position.y < p1.v_trans.position.y)
                {//平顶
                    DrawTriangleTop(p2, p3, p1, mesh);
                }
                else
                {//平底
                    DrawTriangleBottom(p1, p2, p3, mesh);
                }
            }
            else
            {//分割三角形
                Point top;

                Point bottom;
                Point middle;
                if (p1.v_trans.position.y > p2.v_trans.position.y && p2.v_trans.position.y > p3.v_trans.position.y)
                {
                    top = p3;
                    middle = p2;
                    bottom = p1;
                }
                else if (p3.v_trans.position.y > p2.v_trans.position.y && p2.v_trans.position.y > p1.v_trans.position.y)
                {
                    top = p1;
                    middle = p2;
                    bottom = p3;
                }
                else if (p2.v_trans.position.y > p1.v_trans.position.y && p1.v_trans.position.y > p3.v_trans.position.y)
                {
                    top = p3;
                    middle = p1;
                    bottom = p2;
                }
                else if (p3.v_trans.position.y > p1.v_trans.position.y && p1.v_trans.position.y > p2.v_trans.position.y)
                {
                    top = p2;
                    middle = p1;
                    bottom = p3;
                }
                else if (p1.v_trans.position.y > p3.v_trans.position.y && p3.v_trans.position.y > p2.v_trans.position.y)
                {
                    top = p2;
                    middle = p3;
                    bottom = p1;
                }
                else if (p2.v_trans.position.y > p3.v_trans.position.y && p3.v_trans.position.y > p1.v_trans.position.y)
                {
                    top = p1;
                    middle = p3;
                    bottom = p2;
                }
                else
                {
                    //三点共线
                    return;
                }
                //插值求中间点x
                float middlex = 
                    (middle.v_trans.position.y - top.v_trans.position.y) * 
                    (bottom.v_trans.position.x - top.v_trans.position.x) /
                    (bottom.v_trans.position.y - top.v_trans.position.y) + top.v_trans.position.x;

                
                float dy = middle.v_trans.position.y - top.v_trans.position.y;
                float t = dy / (bottom.v_trans.position.y - top.v_trans.position.y);
                //插值生成左右顶点
                Point newMiddle = new Point();
              
                newMiddle.v_trans.position.x = middlex;
                newMiddle.v_trans.position.y = middle.v_trans.position.y;
                
                ScreenSpaceLerpVertex(ref newMiddle, top, bottom, t);

                //平底
                DrawTriangleBottom(top, newMiddle, middle, mesh);
                //平顶
                DrawTriangleTop(newMiddle, middle, bottom, mesh);
            }
        }

        /// <summary>
        /// 平顶，p1,p2,p3为下顶点
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        private static void DrawTriangleTop(Point p1, Point p2, Point p3,Mesh mesh)
        {
            for (float y = p1.v_trans.position.y; y <= p3.v_trans.position.y; y += 0.5f)
            {
                int yIndex = (int)(System.Math.Round(y, MidpointRounding.AwayFromZero));
                if (yIndex >= 0 && yIndex < _frameBuff.Height)
                {
                    float xl = 
                        (y - p1.v_trans.position.y) * 
                        (p3.v_trans.position.x - p1.v_trans.position.x) / 
                        (p3.v_trans.position.y - p1.v_trans.position.y) + p1.v_trans.position.x;
                    float xr = 
                        (y - p2.v_trans.position.y) *
                        (p3.v_trans.position.x - p2.v_trans.position.x) /
                        (p3.v_trans.position.y - p2.v_trans.position.y) + p2.v_trans.position.x;

                    float dy = y - p1.v_trans.position.y;
                    float t = dy / (p3.v_trans.position.y - p1.v_trans.position.y);
                    //插值生成左右顶点
                    Point new1 = new Point();
                    new1.v_trans.position.x = xl;
                    new1.v_trans.position.y = y;
                    ScreenSpaceLerpVertex(ref new1, p1, p3, t);
                    //
                    Point new2 = new Point();
                    new2.v_trans.position.x = xr;
                    new2.v_trans.position.y = y;
                    ScreenSpaceLerpVertex(ref new2, p2, p3, t);
                    //扫描线填充
                    if (new1.v_trans.position.x < new2.v_trans.position.x)
                    {
                        ScanlineFill(new1, new2, yIndex, mesh);
                    }
                    else
                    {
                        ScanlineFill(new2, new1, yIndex, mesh);
                    }
                }
            }
        }
        /// <summary>
        /// 平底，p1为上顶点,p2，p3
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>

        private static void DrawTriangleBottom(Point p1, Point p2, Point p3, Mesh mesh)
        {
            for (float y = p1.v_trans.position.y; y <= p2.v_trans.position.y; y += 0.5f)
            {
                int yIndex = (int)(System.Math.Round(y, MidpointRounding.AwayFromZero));
                if (yIndex >= 0 && yIndex < _frameBuff.Height)
                {
                    float xl = 
                        (y - p1.v_trans.position.y) * 
                        (p2.v_trans.position.x - p1.v_trans.position.x) / 
                        (p2.v_trans.position.y - p1.v_trans.position.y) + p1.v_trans.position.x;
                    float xr = 
                        (y - p1.v_trans.position.y) * 
                        (p3.v_trans.position.x - p1.v_trans.position.x) / 
                        (p3.v_trans.position.y - p1.v_trans.position.y) + p1.v_trans.position.x;

                    float dy = y - p1.v_trans.position.y;
                    float t = dy / (p2.v_trans.position.y - p1.v_trans.position.y);
                    //插值生成左右顶点
                    Point new1 = new Point();
                    new1.v_trans.position.x = xl;
                    new1.v_trans.position.y = y;
                    ScreenSpaceLerpVertex(ref new1, p1, p2, t);
                    //
                    Point new2 = new Point();
                    new2.v_trans.position.x = xr;
                    new2.v_trans.position.y = y;
                    ScreenSpaceLerpVertex(ref new2, p1, p3, t);
                    //扫描线填充
                    if (new1.v_trans.position.x < new2.v_trans.position.x)
                    {
                        ScanlineFill(new1, new2, yIndex,mesh);
                    }
                    else
                    {
                        ScanlineFill(new2, new1, yIndex, mesh);
                    }
                }
            }
        }

        /// <summary>
        /// 扫描线填充
        /// </summary>
        /// <param name="left">左端点，值已经经过插值</param>
        /// <param name="right">右端点，值已经经过插值</param>
        private static void ScanlineFill(Point left, Point right, int yIndex,Mesh mesh)
        {

            Vertex leftV = left.v_trans;
            Vertex rightV = right.v_trans;

            float dx = rightV.position.x - leftV.position.x;
            float step = 1;
            if (dx != 0)
            {
                step = 1 / dx;
            }
            for (float x = leftV.position.x; x <= rightV.position.x; x += 0.5f)
            {
                int xIndex = (int)(x + 0.5f);
               
                if (xIndex >= 0 && xIndex < _frameBuff.Width)
                {
                    float lerpFactor = 0;
                    if (dx != 0)
                    {
                        lerpFactor = (x - leftV.position.x) / dx;
                    }
                    //1/z’与x’和y'是线性关系的
                    float onePreZ = MathHelp.Lerp(leftV.onePerZ, rightV.onePerZ, lerpFactor);
                    float w = 1 / onePreZ;

                    if (yIndex < 0 || yIndex >= _frameBuff.Height || xIndex < 0 || xIndex >= _frameBuff.Width)
                        continue;

                    if (onePreZ >= _zBuff[xIndex, yIndex])//使用1/z进行深度测试
                    {
                        _zBuff[xIndex, yIndex] = onePreZ;
                        //插值顶点颜色
                        Color vertColor = MathHelp.Lerp(leftV.color, rightV.color, lerpFactor);
                        
                        _frameBuff.SetPixel(xIndex, yIndex, vertColor.TransFormToSystemColor());
                    }
                   
                }
            }

        }

        private static void DrawPoint()
        {

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

        /// <summary>
        /// 屏幕空间插值生成新顶点，此时已近经过透视除法，z信息已经没有作用
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static void ScreenSpaceLerpVertex(ref Point v, Point v1, Point v2, float t)
        {
            
            v.v_trans.onePerZ = MathHelp.Lerp(v1.v_trans.onePerZ, v2.v_trans.onePerZ, t);

            v.v_trans.u = MathHelp.Lerp(v1.v_trans.u, v2.v_trans.u, t);
            v.v_trans.v = MathHelp.Lerp(v1.v_trans.v, v2.v_trans.v, t);

            v.v_trans.color = MathHelp.Lerp(v1.v_trans.color, v2.v_trans.color, t);
            
            v.v_trans.lightingColor = MathHelp.Lerp(v1.v_trans.lightingColor, v2.v_trans.lightingColor, t);
            v.v_shader = v.v_trans;
            v.v_shader.position = MathHelp.Lerp(v1.v_shader.position, v2.v_shader.position, t);
            v.v_shader.normal = MathHelp.Lerp(v1.v_shader.normal, v2.v_shader.normal, t);
            v.v_shader.color = v.v_trans.color;
            v.v_shader.lightingColor = v.v_trans.lightingColor;
            v.v_shader.u = v.v_trans.u;
            v.v_shader.v = v.v_trans.v;
        }

      
    }
}
