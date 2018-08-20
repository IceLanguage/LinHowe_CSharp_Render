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
            Rendering_pipeline._models.Clear();
            Rendering_pipeline._lights.Clear();
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
                    DrawTriangle(mesh.Vertices[i], mesh.Vertices[i + 1], mesh.Vertices[i + 2],mesh);
                }
            }

        }

        private static void DrawTriangle(Vertex2 p1, Vertex2 p2, Vertex2 p3,Mesh mesh)
        {
            RasterizationTriangle(p1, p2, p3,mesh);
            //BresenhamDrawLine(p1, p2);
            //BresenhamDrawLine(p2, p3);
            //BresenhamDrawLine(p3, p1);
        }

        /// <summary>
        /// 光栅化三角形
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        private static void RasterizationTriangle(Vertex2 p1, Vertex2 p2, Vertex2 p3,Mesh mesh)
        {
            if (p1.v.position.y == p2.v.position.y)
            {
                if (p1.v.position.y < p3.v.position.y)
                {//平顶
                    DrawTriangleTop(p1, p2, p3,mesh);
                }
                else
                {//平底
                    DrawTriangleBottom(p3, p1, p2, mesh);
                }
            }
            else if (p1.v.position.y == p3.v.position.y)
            {
                if (p1.v.position.y < p2.v.position.y)
                {//平顶
                    DrawTriangleTop(p1, p3, p2, mesh);
                }
                else
                {//平底
                    DrawTriangleBottom(p2, p1, p3, mesh);
                }
            }
            else if (p2.v.position.y == p3.v.position.y)
            {
                if (p2.v.position.y < p1.v.position.y)
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
                Vertex2 top;

                Vertex2 bottom;
                Vertex2 middle;
                if (p1.v.position.y > p2.v.position.y && p2.v.position.y > p3.v.position.y)
                {
                    top = p3;
                    middle = p2;
                    bottom = p1;
                }
                else if (p3.v.position.y > p2.v.position.y && p2.v.position.y > p1.v.position.y)
                {
                    top = p1;
                    middle = p2;
                    bottom = p3;
                }
                else if (p2.v.position.y > p1.v.position.y && p1.v.position.y > p3.v.position.y)
                {
                    top = p3;
                    middle = p1;
                    bottom = p2;
                }
                else if (p3.v.position.y > p1.v.position.y && p1.v.position.y > p2.v.position.y)
                {
                    top = p2;
                    middle = p1;
                    bottom = p3;
                }
                else if (p1.v.position.y > p3.v.position.y && p3.v.position.y > p2.v.position.y)
                {
                    top = p2;
                    middle = p3;
                    bottom = p1;
                }
                else if (p2.v.position.y > p3.v.position.y && p3.v.position.y > p1.v.position.y)
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
                    (middle.v.position.y - top.v.position.y) * 
                    (bottom.v.position.x - top.v.position.x) /
                    (bottom.v.position.y - top.v.position.y) + top.v.position.x;

                
                float dy = middle.v.position.y - top.v.position.y;
                float t = dy / (bottom.v.position.y - top.v.position.y);
                //插值生成左右顶点
                Vertex2 newMiddle = new Vertex2();
              
                newMiddle.v.position.x = middlex;
                newMiddle.v.position.y = middle.v.position.y;
                
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
        private static void DrawTriangleTop(Vertex2 p1, Vertex2 p2, Vertex2 p3,Mesh mesh)
        {
            for (float y = p1.v.position.y; y <= p3.v.position.y; y += 0.5f)
            {
                int yIndex = (int)(System.Math.Round(y, MidpointRounding.AwayFromZero));
                if (yIndex >= 0 && yIndex < _frameBuff.Height)
                {
                    float xl = 
                        (y - p1.v.position.y) * 
                        (p3.v.position.x - p1.v.position.x) / 
                        (p3.v.position.y - p1.v.position.y) + p1.v.position.x;
                    float xr = 
                        (y - p2.v.position.y) *
                        (p3.v.position.x - p2.v.position.x) /
                        (p3.v.position.y - p2.v.position.y) + p2.v.position.x;

                    float dy = y - p1.v.position.y;
                    float t = dy / (p3.v.position.y - p1.v.position.y);
                    //插值生成左右顶点
                    Vertex2 new1 = new Vertex2();
                    new1.v.position.x = xl;
                    new1.v.position.y = y;
                    ScreenSpaceLerpVertex(ref new1, p1, p3, t);
                    //
                    Vertex2 new2 = new Vertex2();
                    new2.v.position.x = xr;
                    new2.v.position.y = y;
                    ScreenSpaceLerpVertex(ref new2, p2, p3, t);
                    //扫描线填充
                    if (new1.v.position.x < new2.v.position.x)
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

        private static void DrawTriangleBottom(Vertex2 p1, Vertex2 p2, Vertex2 p3, Mesh mesh)
        {
            for (float y = p1.v.position.y; y <= p2.v.position.y; y += 0.5f)
            {
                int yIndex = (int)(System.Math.Round(y, MidpointRounding.AwayFromZero));
                if (yIndex >= 0 && yIndex < _frameBuff.Height)
                {
                    float xl = 
                        (y - p1.v.position.y) * 
                        (p2.v.position.x - p1.v.position.x) / 
                        (p2.v.position.y - p1.v.position.y) + p1.v.position.x;
                    float xr = 
                        (y - p1.v.position.y) * 
                        (p3.v.position.x - p1.v.position.x) / 
                        (p3.v.position.y - p1.v.position.y) + p1.v.position.x;

                    float dy = y - p1.v.position.y;
                    float t = dy / (p2.v.position.y - p1.v.position.y);
                    //插值生成左右顶点
                    Vertex2 new1 = new Vertex2();
                    new1.v.position.x = xl;
                    new1.v.position.y = y;
                    ScreenSpaceLerpVertex(ref new1, p1, p2, t);
                    //
                    Vertex2 new2 = new Vertex2();
                    new2.v.position.x = xr;
                    new2.v.position.y = y;
                    ScreenSpaceLerpVertex(ref new2, p1, p3, t);
                    //扫描线填充
                    if (new1.v.position.x < new2.v.position.x)
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
        private static void ScanlineFill(Vertex2 left, Vertex2 right, int yIndex,Mesh mesh)
        {
            PixelShader.Lighting(mesh, Rendering_pipeline._camera.pos, ref left.save);
            left.v.lightingColor = left.save.lightingColor;
            PixelShader.Lighting(mesh, Rendering_pipeline._camera.pos, ref right.save);
            right.v.lightingColor = right.save.lightingColor;

            float dx = right.v.position.x - left.v.position.x;
            float step = 1;
            if (dx != 0)
            {
                step = 1 / dx;
            }
            for (float x = left.v.position.x; x <= right.v.position.x; x += 0.5f)
            {
                int xIndex = (int)(x + 0.5f);
                if (xIndex >= 0 && xIndex < _frameBuff.Width)
                {
                    float lerpFactor = 0;
                    if (dx != 0)
                    {
                        lerpFactor = (x - left.v.position.x) / dx;
                    }
                    //1/z’与x’和y'是线性关系的
                    float onePreZ = MathHelp.Lerp(left.v.onePerZ, right.v.onePerZ, lerpFactor);
                    float w = 1 / onePreZ;

                    if (yIndex < 0 || yIndex >= _frameBuff.Height || xIndex < 0 || xIndex >= _frameBuff.Width)
                        continue;

                    if (onePreZ >= _zBuff[xIndex, yIndex])//使用1/z进行深度测试
                    {
                        _zBuff[xIndex, yIndex] = onePreZ;
                        //插值顶点颜色
                        Math.Color vertColor = MathHelp.Lerp(left.v.color, right.v.color, lerpFactor);
                        Math.Color lightColor = MathHelp.Lerp(left.v.lightingColor, right.v.lightingColor, lerpFactor);
                        _frameBuff.SetPixel(xIndex, yIndex, (lightColor * vertColor).TransFormToSystemColor());
                    }
                   
                }
            }

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
        public static void ScreenSpaceLerpVertex(ref Vertex2 v, Vertex2 v1, Vertex2 v2, float t)
        {
            
            v.v.onePerZ = MathHelp.Lerp(v1.v.onePerZ, v2.v.onePerZ, t);
            //
            //v.u = MathHelp.Lerp(v1.u, v2.u, t);
            //v.v = MathHelp.Lerp(v1.v, v2.v, t);
            //
            v.v.color = MathHelp.Lerp(v1.v.color, v2.v.color, t);
            //
            v.v.lightingColor = MathHelp.Lerp(v1.v.lightingColor, v2.v.lightingColor, t);
            v.save = v.v;
            v.save.position.x = MathHelp.Lerp(v1.save.position.x, v2.save.position.x, t);
            v.save.position.y = MathHelp.Lerp(v1.save.position.y, v2.save.position.y, t);
            v.save.position.z = MathHelp.Lerp(v1.save.position.z, v2.save.position.z, t);
        }

      
    }
}
