using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;

namespace AutoRoids
{
    internal class EngineBackGround
    {
        internal List<BlockReference> lstBlkRef;

        internal List<Point2d> lstPointOutside;
        internal List<Point2d> lstPointInside;

        internal EngineBackGround(List<BlockReference> lstBlkRefStar,
                                  List<BlockReference> lstBlkRefBorder,
                                  List<Point2d> lstPointOutside,
                                  List<Point2d> lstPointInside)

        {
            lstBlkRef = new List<BlockReference>();

            for (int i = 0; i < lstBlkRefStar.Count; i++)
                lstBlkRef.Add(lstBlkRefStar[i]);

            for (int i = 0; i < lstBlkRefBorder.Count; i++)
                lstBlkRef.Add(lstBlkRefBorder[i]);

            this.lstPointOutside = lstPointOutside;
            this.lstPointInside = lstPointInside;
        }
    }
}