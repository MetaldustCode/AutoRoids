using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRoids 
{
    internal class EngineShipDebris
    {
        internal List<GameLine> lstLine = new List<GameLine>();
        internal int intColor;
        internal double dblLineWidth;

        internal EngineShipDebris(List<GameLine> lstLine, int intColor, double dblLineWidth)
        {
            this.lstLine = lstLine;
            this.intColor = intColor;
            this.dblLineWidth = dblLineWidth;
        }
    }
}
