using LinHowe_CSharp_Render.Math;
using System;

namespace LinHowe_CSharp_Render
{
    class GameObject
    {
        public float max_radius = 0;//最大半径
        public Mesh mesh;//网格
        public Vector3 position = Vector3.zero;//坐标
        public Vector3 rotation = Vector3.zero;
        public Vector3 scale = new Vector3(1, 1, 1);
        private Matrix4x4 _ObjectToWorldMatrix;
        public Matrix4x4 ObjectToWorldMatrix
        {
            get
            {
                if(_ObjectToWorldMatrix == null)
                {
                    _ObjectToWorldMatrix = 
                        Matrix4x4.GetRotateX(rotation.x) *
                        Matrix4x4.GetRotateY(rotation.y) *
                        Matrix4x4.GetRotateZ(rotation.z) *
                        Matrix4x4.GetTranslate(position);
                }
                return _ObjectToWorldMatrix;
            }
            set
            {
                _ObjectToWorldMatrix = value;
                _WorldInverseTranspose = ObjectToWorldMatrix.Inverse().Transpose();
            }
        }
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

        
        /// <summary>
        /// 每帧的更新
        /// </summary>
        public Action<GameObject> UpdateFunction;
        /// <summary>
        /// 触发事件的执行
        /// </summary>
        public Action<GameObject> EventFunction;

        private Matrix4x4 _WorldInverseTranspose;

        private Matrix4x4 WorldInverseTranspose
        {
            get
            {
                if (_WorldInverseTranspose == null)
                {
                    _WorldInverseTranspose = ObjectToWorldMatrix.Inverse().Transpose();
                }
                return _WorldInverseTranspose;
            }
        }
        //模型空间法线乘以世界矩阵的逆转置得到世界空间法线
        //原因 https://blog.csdn.net/christina123y/article/details/5963679
        public Vector3 GetWorldNormal(Vector3 normal)
        {
            return (normal * WorldInverseTranspose).Normalize();
        }
    }
}
