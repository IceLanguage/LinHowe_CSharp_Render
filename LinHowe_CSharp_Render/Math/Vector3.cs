

namespace LinHowe_CSharp_Render.Math
{
    /// <summary>
    /// 3d向量
    /// </summary>
    struct Vector3
    {
        public float x;
        public float y;
        public float z;
        public float w;
        public readonly static Vector3 zero = new Vector3(0, 0, 0);
        public Vector3(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector3(float x, float y, float z) : this(x, y, z, 0) { }

        public float Length
        {
            get
            {
                float sq = x * x + y * y + z * z;
                return (float)System.Math.Sqrt(sq);
            }
        }

        public Vector3 Normalize()
        {
            float length = Length;
            if (length != 0)
            {
                float s = 1 / length;
                x *= s;
                y *= s;
                z *= s;
            }
            return this;
        }


        public static Vector3 operator *(Vector3 lhs, Matrix4x4 rhs)
        {
            Vector3 v = new Vector3
            {
                x = lhs.x * rhs[0, 0] + lhs.y * rhs[1, 0] + lhs.z * rhs[2, 0] + lhs.w * rhs[3, 0],
                y = lhs.x * rhs[0, 1] + lhs.y * rhs[1, 1] + lhs.z * rhs[2, 1] + lhs.w * rhs[3, 1],
                z = lhs.x * rhs[0, 2] + lhs.y * rhs[1, 2] + lhs.z * rhs[2, 2] + lhs.w * rhs[3, 2],
                w = lhs.x * rhs[0, 3] + lhs.y * rhs[1, 3] + lhs.z * rhs[2, 3] + lhs.w * rhs[3, 3]
            };
            return v;
        }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            Vector3 v = new Vector3
            {
                x = lhs.x - rhs.x,
                y = lhs.y - rhs.y,
                z = lhs.z - rhs.z,
                w = lhs.w - rhs.w
            };
            return v;
        }

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            Vector3 v = new Vector3
            {
                x = lhs.x + rhs.x,
                y = lhs.y + rhs.y,
                z = lhs.z + rhs.z,
                w = lhs.w + rhs.w
            };
            return v;
        }

        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }

        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            float x = lhs.y * rhs.z - lhs.z * rhs.y;
            float y = lhs.z * rhs.x - lhs.x * rhs.z;
            float z = lhs.x * rhs.y - lhs.y * rhs.x;
            return new Vector3(x, y, z, 0);
        }
    }
}
