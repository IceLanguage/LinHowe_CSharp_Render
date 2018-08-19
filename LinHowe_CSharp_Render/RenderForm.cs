using LinHowe_CSharp_Render.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinHowe_CSharp_Render
{
    public partial class RenderForm : Form
    {
        private RenderStage stage;
        public RenderForm()
        {
            InitializeComponent();
            stage = new ApplicationStage();
        }
    }
}
