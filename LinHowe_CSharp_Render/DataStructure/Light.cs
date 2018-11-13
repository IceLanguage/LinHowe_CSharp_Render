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
        //光强
        public float intensity = 1;
        public LightType type = LightType.Directional;
        /// <summary>
        /// 灯光颜色
        /// </summary>
        private Color lightColor;

        /// <summary>
        /// 灯光世界坐标
        /// </summary>
        private Vector3 worldPosition;

        private Vector3 direction;

        
        /// <summary>
        /// 衰减因子
        /// </summary>
        private float kc, kl, kq;
        /// <summary>
        /// 指数因子
        /// </summary>
        private float pr;
        public Light(Vector3 worldPosition, Vector3 direction, Color lightColor)
        {
            this.worldPosition = worldPosition;
            this.lightColor = lightColor;
            this.direction = direction.Normalize();
        }
        public void SetPointLight(float kc ,float kl)
        {
            this.kc = kc;
            this.kl = kl;
            type = LightType.Point;
        }
        public void SetSpotLight(float kc, float kl,float kq,float pr)
        {
            this.kc = kc;
            this.kl = kl;
            this.kq = kq;
            this.pr = pr;
            type = LightType.Spot;
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
            float d;
            switch (light.type)
            {
                case LightType.Directional:
                    color = light.lightColor;
                    break;
                case LightType.Point:
                    d = (float)System.Math.Sqrt(Vector3.DistanceSquare(worldPos, light.worldPosition));
                    color = light.lightColor / (light.kc + light.kl * d);
                    break;
                case LightType.Spot:
                    d = (float)System.Math.Sqrt(Vector3.DistanceSquare(worldPos, light.worldPosition));
                    float p = light.kc + light.kl * d + light.kq * d * d;
                    if (p == 0)
                        p = 1;
                    color = light.lightColor / p;
                    float cosAngle = Vector3.Dot(light.direction.Normalize(), (light.worldPosition - worldPos).Normalize());
                    if (cosAngle > 0)
                        color *= (float)System.Math.Pow(cosAngle,light.pr);
                    else
                        color = Color.Black;
                    break;
                default:
                    break;
            }
            return color * light.intensity;
        }
    }


    
}
