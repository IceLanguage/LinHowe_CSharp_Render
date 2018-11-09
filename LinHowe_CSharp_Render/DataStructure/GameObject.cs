using LinHowe_CSharp_Render.Math;

namespace LinHowe_CSharp_Render
{
    class GameObject
    {
        public float max_radius = 0;//最大半径
        public Mesh mesh;//网格
        public Vector3 position = Vector3.zero;//坐标
        public Vector3 rotation = Vector3.zero;
        public Matrix4x4 ObjectToWorldMatrix;//模型-世界矩阵
        public GameObject(Mesh mesh,Vector3 position)
        {
            this.mesh = mesh;
            this.position = position;
            CalculateMaxRadius();
        }
        /// <summary>
        /// 计算半径
        /// </summary>
        private void CalculateMaxRadius()
        {
            int size = mesh.Vertices.Length;
            for (int i = 0; i < size; ++i)
            {
                //计算物体包围球的最大半径
                max_radius = System.Math.Max(max_radius,
                    Vector3.DistanceSquare(position, mesh.Vertices[i].m_vertex.position));
            }
            max_radius = (float)System.Math.Sqrt(max_radius);
        }


    }
}
