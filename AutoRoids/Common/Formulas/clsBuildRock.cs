using Autodesk.AutoCAD.Geometry;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoRoids
{
    public class clsBuildRock
    {
        //internal void TrimRock(Transaction acTrans, Database acDb,
        //                 BlockReference acBlkRefRock, int intPos)
        //{
        //    clsTrim clsTrim = new clsTrim();
        //    List<Line> lstLine = new List<Line>();
        //    if (acBlkRefRock != null && acBlkRefRock.ObjectId.IsValid && !acBlkRefRock.IsErased)
        //    {
        //        List<Point2d> lstRockPoints = GetRockPoints(acTrans, acDb, acBlkRefRock, intPos);
        //        List<List<Point2d>> lstRockGroup = GroupIntoLines(lstRockPoints);

        //        List<Point2d> lstBorderPoints = GetRock(5, 100, true);
        //        List<List<Point2d>> lstBorderGroup = GroupIntoLines(lstBorderPoints);

        //        if (clsTrim.PointToLine(lstRockGroup, out List<MyLine> lstRockLine) &&
        //            clsTrim.PointToLine(lstBorderGroup, out List<MyLine> lstBorderLine))
        //        {
        //            List<List<MyLine>> lstLstShieldLine = GetShieldPolyline();
        //            clsTrim.ProcessRockTrim(acTrans, acDb, intPos, lstRockLine, lstBorderLine, lstLstShieldLine);
        //        }
        //    }
        //}

        //internal List<List<MyLine>> GetShieldPolyline()
        //{
        //    clsTrim clsTrim = new clsTrim();

        //    List<List<MyLine>> lstLstShieldLine = new List<List<MyLine>>();
        //    for (int i = 0; i < StaticRock.lstShield.Count; i++)
        //    {
        //        List<Point2d> lstShieldPoints = GetShieldPoints(StaticRock.lstShield[i], 6);
        //        List<List<Point2d>> lstShieldGroup = GroupIntoLines(lstShieldPoints);
        //        lstLstShieldLine.Add(clsTrim.PointToLine(lstShieldGroup));
        //    }

        //    return lstLstShieldLine;
        //}

        //internal void ShieldPolyline(Transaction acTrans, Database acDb)
        //{
        //    clsAppend clsAppend = new clsAppend();
        //    clsTrim clsTrim = new clsTrim();
        //    clsPolylineAdd clsPolylineAdd = new clsPolylineAdd();
        //    List<List<MyLine>> lstLstShieldLine = GetShieldPolyline();

        //    for (int i = 0; i < lstLstShieldLine.Count; i++)
        //    {
        //        for (int k = 0; k < lstLstShieldLine[i].Count; k++)
        //        {
        //            List<Point2d> lstPoint = new List<Point2d> { lstLstShieldLine[i][k].ptStart, lstLstShieldLine[i][k].ptEnd };
        //            clsAppend.AppendToEntity(clsPolylineAdd.AddPolyLineEntity(acTrans, acDb, lstPoint, 2, 3));
        //        }
        //    }

        //}

        //internal List<Point2d> GetRockPoints(Transaction acTrans, Database acDb, BlockReference acBlkRefRock, int intPos)
        //{
        //    List<Point2d> lstRockPoints = GetRock(intPos, 1, true);

        //    for (int i = 0; i < lstRockPoints.Count; i++)
        //    {
        //        Point3d pt = lstRockPoints[i].ToPoint3d();
        //        pt = pt.TransformBy(acBlkRefRock.BlockTransform);
        //        lstRockPoints[i] = pt.ToPoint2d();
        //    }

        //    return lstRockPoints;
        //}

        //internal List<Point2d> GetShipPoints(Transaction acTrans, Database acDb, BlockReference acBlkRefRock, int intPos)
        //{
        //    List<Point2d> lstRockPoints = GetRock(intPos, 1, false);

        //    for (int i = 0; i < lstRockPoints.Count; i++)
        //    {
        //        Point3d pt = lstRockPoints[i].ToPoint3d();
        //        pt = pt.TransformBy(acBlkRefRock.BlockTransform);
        //        lstRockPoints[i] = pt.ToPoint2d();
        //    }

        //    return lstRockPoints;
        //}

        //internal List<Point2d> GetShieldPoints(BlockReference acBlkRefShield, int intSides)
        //{
        //    double dblScale = acBlkRefShield.ScaleFactors.X;

        //    List<Point2d> lstShield = GetRock(8, 1, false, intSides);

        //    for (int i = 0; i < lstShield.Count; i++)
        //    {
        //        Point3d pt = lstShield[i].ToPoint3d();
        //        pt = pt.TransformBy(acBlkRefShield.BlockTransform);
        //        lstShield[i] = pt.ToPoint2d();
        //    }

        //    return lstShield;
        //}

        internal List<List<Point2d>> GroupIntoLines(List<Point2d> _lstPoint)
        {
            List<Point2d> lstPoint = _lstPoint.ToList();

            List<List<Point2d>> rtnValue = new List<List<Point2d>>();

            List<double> lstDistance = new List<double>();

            for (int i = 1; i < lstPoint.Count; i++)
            {
                Point2d pt1 = lstPoint[i - 1];
                Point2d pt2 = lstPoint[i];

                lstDistance.Add(pt1.GetDistanceTo(pt2));
            }

            for (int i = 1; i < lstPoint.Count; i++)
            {
                List<Point2d> lstLine = new List<Point2d>();

                lstLine.Add(lstPoint[i - 1]);
                lstLine.Add(lstPoint[i]);
                rtnValue.Add(lstLine);
            }

            return rtnValue;
        }

        internal List<Point2d> GetRock(int intPos, int intSides)
        {
            List<Point2d> lstRock1 = new List<Point2d>()
            {
               new Point2d(2,0),
               new Point2d(5,0),
               new Point2d(8,2),
               new Point2d(7,4),
               new Point2d(8,6),
               new Point2d(6,8),
               new Point2d(4,6),
               new Point2d(2,8),
               new Point2d(0,6),
               new Point2d(0,2),
               new Point2d(2,0)
            };

            List<Point2d> lstRock2 = new List<Point2d>()
            {
               new Point2d(0,3),
               new Point2d(2,4),
               new Point2d(0,5),
               new Point2d(3,8),
               new Point2d(6,8),
               new Point2d(8,5),
               new Point2d(8,3),
               new Point2d(6,0),
               new Point2d(4,0),
               new Point2d(4,3),
               new Point2d(2,0),
               new Point2d(0,3)
            };

            List<Point2d> lstRock3 = new List<Point2d>()
            {
               new Point2d(2,0),
               new Point2d(5,1),
               new Point2d(6,0),
               new Point2d(8,2),
               new Point2d(5,4),
               new Point2d(8,5),
               new Point2d(8,6),
               new Point2d(5,8),
               new Point2d(2,8),
               new Point2d(3,6),
               new Point2d(0,6),
               new Point2d(0,3),
               new Point2d(2,0)
            };

            List<Point2d> lstRock4 = new List<Point2d>()
            {
               new Point2d(2,0),
               new Point2d(3,1),
               new Point2d(6,0),
               new Point2d(8,3),
               new Point2d(6,5),
               new Point2d(8,6),
               new Point2d(6,8),
               new Point2d(4,7),
               new Point2d(2,8),
               new Point2d(0,6),
               new Point2d(1,4),
               new Point2d(0,2),
               new Point2d(2,0)
            };

            List<Point2d> lstRockBorder = new List<Point2d>()
            {
               new Point2d(0,0),
               new Point2d(36,0),
               new Point2d(36,24),
               new Point2d(0,24),
               new Point2d(0,0)
            };

            List<Point2d> lstShield = CalculatePolygonVertices(intSides, 8);

            List<Point2d> lstRockEdge = new List<Point2d>()
            {
               new Point2d(0,0),
               new Point2d(37,0),
               new Point2d(37,25),
               new Point2d(0,25),
               new Point2d(0,0)
            };

            double dblOffset = 0.166;

            List<Point2d> lstShip = new List<Point2d>()
            {
               new Point2d(-1, -1.5),
               new Point2d(0, 1.5),
               new Point2d(1,-1.5),
               new Point2d(0.5,-1),
               new Point2d(-0.5,-1),
               new Point2d(-1, -1.5)
            };

            for (int i = 0; i < lstShip.Count; i++)
            {
                lstShip[i] = new Point2d(lstShip[i].X, lstShip[i].Y + dblOffset);
            }

            List<Point2d> lstThrust = new List<Point2d>()
            {
               new Point2d(-0.5,-1),
               new Point2d(0,-2.0),
               new Point2d(0.5,-1)
            };

            for (int i = 0; i < lstThrust.Count; i++)
            {
                lstThrust[i] = new Point2d(lstThrust[i].X, lstThrust[i].Y + dblOffset);
            }

            switch (intPos)
            {
                case 0:
                    return lstRock1;

                case 1:
                    return lstRock2;

                case 2:
                    return lstRock3;

                case 3:
                    return lstRock4;

                case 4:
                    return lstRockEdge;

                case 5:
                    return lstRockBorder;

                case 6:
                    return lstShip;

                case 7:
                    return lstThrust;

                default:
                    return lstShield;
            }
        }

        internal List<Point2d> CalculatePolygonVertices(int sides, double inscribedCircleRadius)
        {
            List<Point2d> rtnValue = new List<Point2d>();

            // Calculate the angle between each vertex
            double angleIncrement = 2 * Math.PI / sides;

            // Calculate the initial angle based on whether sides is even or odd
            double startAngle = sides % 2 == 0 ? -Math.PI / 2 - angleIncrement / 2 : -Math.PI / 2;

            // Calculate the coordinates of each vertex
            for (int i = 0; i < sides; i++)
            {
                double angle = startAngle + i * angleIncrement;
                double x = inscribedCircleRadius * Math.Cos(angle);
                double y = inscribedCircleRadius * Math.Sin(angle);
                rtnValue.Add(new Point2d(x, y));
            }

            rtnValue.Add(rtnValue[0]); // Close the polygon by adding the first vertex at the end

            return rtnValue;
        }

        internal Point2d GetMiddleOffset(List<Point2d> lstPoint)
        {
            double minX = int.MaxValue;
            double minY = int.MaxValue;
            double maxX = int.MinValue;
            double maxY = int.MinValue;

            foreach (Point2d point in lstPoint)
            {
                minX = Math.Min(minX, point.X);
                minY = Math.Min(minY, point.Y);
                maxX = Math.Max(maxX, point.X);
                maxY = Math.Max(maxY, point.Y);
            }

            double width = maxX - minX;
            double height = maxY - minY;

            return new Point2d(width / 2.0, height / 2.0);
        }
    }
}