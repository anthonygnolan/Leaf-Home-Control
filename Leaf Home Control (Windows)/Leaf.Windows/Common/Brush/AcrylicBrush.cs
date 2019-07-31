using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Windows.Common.Brush
{
    public class AcrylicBrush : AcrylicBrushBase
    {
        public AcrylicBrush()
        {
        }

        protected override BackdropBrushType GetBrushType()
        {
            return BackdropBrushType.Backdrop;
        }
    }
}
