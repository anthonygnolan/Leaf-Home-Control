using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Windows.Common.Brush
{
    public class AcrylicHostBrush : AcrylicBrushBase
    {
        public AcrylicHostBrush()
        {
        }

        protected override BackdropBrushType GetBrushType()
        {
            return BackdropBrushType.HostBackdrop;
        }
    }
}
