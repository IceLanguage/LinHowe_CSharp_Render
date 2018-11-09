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
        }
        public GameObject(Mesh mesh)
        {
            this.mesh = mesh;
        }
    }
}
