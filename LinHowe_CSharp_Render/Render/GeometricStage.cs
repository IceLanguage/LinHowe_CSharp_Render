using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHowe_CSharp_Render.Render
{
    
    /// <summary>
    /// 几何阶段
    /// </summary>
    partial class GeometricStage
    {
        public static SmallStage _smallStage = Model_View_Transformation_Stage.instance;

        public override void ChangeState()
        {
            _smallStage.ChangeState();
        }
    }

    /// <summary>
    /// 模型视图变换阶段
    /// </summary>
    partial class Model_View_Transformation_Stage:SmallStage
    {
        public static readonly Model_View_Transformation_Stage instance = new Model_View_Transformation_Stage();
        
    }

    /// <summary>
    /// 顶点着色阶段
    /// </summary>
    partial class Vertex_Coloring_Stage : SmallStage
    {
        public static readonly Vertex_Coloring_Stage instance = new Vertex_Coloring_Stage();
        public override void ChangeState()
        {
            GeometricStage._smallStage = Projection_Stage.instance;
        }
    }

    /// <summary>
    /// 投影阶段
    /// </summary>
    partial class Projection_Stage:SmallStage
    {
        public static readonly Projection_Stage instance = new Projection_Stage();
        
    }

    /// <summary>
    /// 裁剪阶段
    /// </summary>
    partial class CutOut_Stage:SmallStage
    {
        public static readonly CutOut_Stage instance = new CutOut_Stage();
        public override void ChangeState()
        {
            GeometricStage._smallStage = Screen_Mapping_Stage.instance;
        }
    }

    /// <summary>
    /// 屏幕映射阶段
    /// </summary>
    partial class Screen_Mapping_Stage:SmallStage
    {
        public static readonly Screen_Mapping_Stage instance = new Screen_Mapping_Stage();

        public override void ChangeState()
        {
            GeometricStage._smallStage = Model_View_Transformation_Stage.instance;
            _stage = RasterizationStage.instance;
            Console.WriteLine("进入光栅化阶段");
        }
    }
}
