using LinHowe_CSharp_Render.Math;

namespace LinHowe_CSharp_Render
{
    class GameObject
    {
        public int id;
        public float avg_radius, max_radius;//平均半径，最大半径
        public string name;
        public Mesh mesh;//网格
        public Vector3 position;//坐标
        public Vector3 ux, uy, uz;//局部坐标轴
    }
}
