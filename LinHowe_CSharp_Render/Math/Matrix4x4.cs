using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Math
{
	/// <summary>
	/// 4x4矩阵
	/// </summary>
	class Matrix4x4
	{
		private float[,] _m = new float[4, 4];
		public readonly static Matrix4x4 Identity = new Matrix4x4
		(
			1,0,0,0,
			0,1,0,0,
			0,0,1,0,
			0,0,0,1
		);

		public Matrix4x4()
		{
			_m = new float[4, 4] 
			{ 
				{ 0, 0, 0, 0 },
				{ 0, 0, 0, 0 },
				{ 0, 0, 0, 0 },
				{ 0, 0, 0, 0 }
			};

		}
		public Matrix4x4(
			float a1, float a2, float a3, float a4,
			float b1, float b2, float b3, float b4,
			float c1, float c2, float c3, float c4,
			float d1, float d2, float d3, float d4)
		{
			_m = new float[4, 4]
			{
				{ a1, a2, a3, a4 },
				{ b1, b2, b3, b4 },
				{ c1, c2, c3, c4 },
				{ d1, d2, d3, d4 }
			};

		}

		public float this[int i, int j]
		{
			get { return _m[i, j]; }
			set { _m[i, j] = value; }
		}

		public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			Matrix4x4 res = new Matrix4x4();
			
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					for (int k = 0; k < 4; k++)
					{
						res[i, j] += lhs[i, k] * rhs[k, j];
					}
				}
			}
			return res;
		}

		/// <summary>
		/// 获取平移矩阵
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <returns></returns>
		public static Matrix4x4 GetTranslate(float x, float y, float z)
		{
			return new Matrix4x4
			(
				1, 0, 0, 0,
				0, 1, 0, 0,
				0, 0, 1, 0,
				x, y, z, 1
			);
		}
	}
}
