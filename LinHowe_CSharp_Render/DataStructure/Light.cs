using LinHowe_CSharp_Render.Math;

namespace LinHowe_CSharp_Render
{
    struct Light
    {
        /// <summary>
        /// 灯光颜色
        /// </summary>
        public Color lightColor;

        /// <summary>
        /// 灯光世界坐标
        /// </summary>
        public Vector3 worldPosition;
        
        public Light(Vector3 worldPosition, Color lightColor)
        {
            this.worldPosition = worldPosition;
            this.lightColor = lightColor;
        }
    }
}
