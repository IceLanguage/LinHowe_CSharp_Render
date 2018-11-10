using LinHowe_CSharp_Render.Math;

namespace LinHowe_CSharp_Render
{
    enum LightType
    {
        Directional,
        Point,
        Spot
    }
    class Light
    {
        public LightType type = LightType.Directional;
        /// <summary>
        /// 灯光颜色
        /// </summary>
        public Color lightColor;

        /// <summary>
        /// 灯光世界坐标
        /// </summary>
        public Vector3 worldPosition;

        public Vector3 direction;
        public Light(Vector3 worldPosition, Vector3 direction, Color lightColor)
        {
            this.worldPosition = worldPosition;
            this.lightColor = lightColor;
            this.direction = direction.Normalize();
        }

        public static Vector3 GetDirection(Light light,Vector3 worldPos)
        {
            Vector3 dir = Vector3.zero;
            switch (light.type)
            {
                case LightType.Directional:
                    dir = light.direction;
                    break;
                default:
                    dir = light.worldPosition - worldPos;
                    break;
            }
            return light.direction.Normalize();
        }

        public static Color GetLightColor(Light light, Vector3 worldPos)
        {
            Color color = Color.Black;
            switch (light.type)
            {
                case LightType.Directional:
                    color = light.lightColor;
                    break;
                case LightType.Point:
                    break;
                case LightType.Spot:
                    break;
                default:
                    break;
            }
            return color;
        }
    }


    
}
