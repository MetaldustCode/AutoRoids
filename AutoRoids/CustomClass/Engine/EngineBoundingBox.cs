using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRoids
{
    internal class EngineBoundingBox
    {
        internal List<GameLine> lstLine = new List<GameLine>();
        internal int intColor;

        internal EngineBoundingBox(List<GameLine> lstLine, int intColor)
        {
            this.lstLine = lstLine;
            this.intColor = intColor;   
        }
    }
}
