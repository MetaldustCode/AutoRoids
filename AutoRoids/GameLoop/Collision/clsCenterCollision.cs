using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRoids
{
    internal class clsCenterCollision
    {
        internal Boolean CheckIsCollision()
        {
            if (StaticRock.EngineScore.lstBlkRefShip.Count > 0)
            {
                double minSpacing = 200;
                for (int k = StaticRock.lstEngineRock.Count - 1; k >= 0; k--)
                {
                    EngineRock engineRock = StaticRock.lstEngineRock[k];

                    if (!engineRock.bolExploded)
                    {
                        List<Point2d> lstPoints = engineRock.lstMatrix3d;
                        for (int i = 0; i < lstPoints.Count; i++)
                        {
                            double pointDistance = lstPoints[i].GetDistanceTo(new Point2d());
                            if (pointDistance < minSpacing)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
