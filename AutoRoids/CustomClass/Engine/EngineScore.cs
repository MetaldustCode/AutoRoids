using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;

namespace AutoRoids
{
    internal class EngineScore
    {
        internal List<BlockReference> lstBlkRefShip = new List<BlockReference>();
        internal Boolean bolReset = false;

        internal EngineScore(List<BlockReference> lstBlkRefShip)
        {
            this.lstBlkRefShip = lstBlkRefShip;
        }
    }
}