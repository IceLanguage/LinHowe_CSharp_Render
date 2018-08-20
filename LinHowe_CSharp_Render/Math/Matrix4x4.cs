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
		public Matrix4x4(Matrix4x4 m)
		{
			_m = new float[4, 4]
			{
				{ m[0,0],m[0,1],m[0,2],m[0,3] },
				{ m[1,0],m[1,1],m[1,2],m[1,3] },
				{ m[2,0],m[2,1],m[2,2],m[2,3] },
				{ m[3,0],m[3,1],m[3,2],m[3,3] }
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

		public static Matrix4x4 GetRotateY(float r)
		{
			Matrix4x4 rm = new Matrix4x4(Identity);
			rm[0, 0] = (float)(System.Math.Cos(r));
			rm[0, 2] = (float)(-System.Math.Sin(r));
			rm[2, 0] = (float)(System.Math.Sin(r));
			rm[2, 2] = (float)(System.Math.Cos(r));
			return rm;
		}

		public static Matrix4x4 GetRotateX(float r)
		{
			Matrix4x4 rm = new Matrix4x4(Identity);		
			rm[1, 1] = (float)(System.Math.Cos(r));
			rm[1, 2] = (float)(System.Math.Sin(r));
			rm[2, 1] = (float)(-System.Math.Sin(r));
			rm[2, 2] = (float)(System.Math.Cos(r));
			return rm;
		}

		public static Matrix4x4 GetRotateZ(float r)
		{
			Matrix4x4 rm = new Matrix4x4(Identity);
			rm[0, 0] = (float)(System.Math.Cos(r));
			rm[0, 1] = (float)(System.Math.Sin(r));
			rm[1, 0] = (float)(-System.Math.Sin(r));
			rm[1, 1] = (float)(System.Math.Cos(r));
			return rm;
		}

		/// <summary>
		/// 求转置
		/// </summary>
		/// <returns></returns>
		public Matrix4x4 Transpose()
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = i + 1; j < 4; j++)
				{

					float temp = _m[i, j];
					_m[i, j] = _m[j, i];
					_m[j, i] = temp;
				}
			}
			return this;
		}

		/// <summary>
		/// 求矩阵行列式
		/// </summary>
		/// <param name="m"></param>
		/// <param name="n"></param>
		/// <returns></returns>
		public float Determinate()
		{
			return Determinate(_m, 4);
		}

		private float Determinate(float[,] m, int n)
		{
			if (n == 1)
			{//递归出口
				return m[0, 0];
			}
			else
			{
				float result = 0;
				float[,] tempM = new float[n - 1, n - 1];
				for (int i = 0; i < n; i++)
				{
					//求代数余子式
					for (int j = 0; j < n - 1; j++)//行
					{
						for (int k = 0; k < n - 1; k++)//列
						{
							int x = j + 1;//原矩阵行
							int y = k >= i ? k + 1 : k;//原矩阵列
							tempM[j, k] = m[x, y];
						}
					}

					result += (float)System.Math.Pow(-1,2 + i) * m[0, i] * Determinate(tempM, n - 1);
				}
				return result;
			}
		}
		/// <summary>
		/// 获取当前矩阵的伴随矩阵
		/// </summary>
		/// <returns></returns>
		public Matrix4x4 GetAdjoint()
		{
			int x, y;
			float[,] tempM = new float[3, 3];
			Matrix4x4 result = new Matrix4x4();
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					for (int k = 0; k < 3; k++)
					{
						for (int t = 0; t < 3; ++t)
						{
							x = k >= i ? k + 1 : k;
							y = t >= j ? t + 1 : t;

							tempM[k, t] = _m[x, y];
						}
					}
					result._m[i, j] = (float)System.Math.Pow(-1, (1 + j) + (1 + i)) * Determinate(tempM, 3);
				}
			}
			return result.Transpose();
		}
		/// <summary>
		/// 求当前矩阵的逆矩阵
		/// </summary>
		/// <returns></returns>
		public Matrix4x4 Inverse()
		{
			float a = Determinate();
			if (a == 0)
			{
				Console.WriteLine("矩阵不可逆");
				return null;
			}
			Matrix4x4 adj = GetAdjoint();//伴随矩阵
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					adj._m[i, j] = adj._m[i, j] / a;
				}
			}
			return adj;
		}
	}
}
