﻿using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;

namespace AutoRoids
{
    internal class clsExplodeShip
    {
        internal static List<List<Point2d>> lstLstMyLine = new List<List<Point2d>>();
        internal static List<Double> lstRotation = new List<double>();
        internal static List<Double> lstOffset = new List<double>();

        internal void ExplodeShip(Transaction acTrans)
        {
            EngineShip engineShip = StaticRock.EngineShip;

            ExplodeShip(engineShip, acTrans);
        }

        internal void ExplodeShip(EngineShip engineShip, Transaction acTrans)
        {
            clsCenterCollision clsCenterCollision = new clsCenterCollision();
            Point2d ptOrigin = engineShip.ptBlockOrigin;

            int intCount = engineShip.intExplode;

            if (intCount == 0)
                lstLstMyLine = SetRandomValues(engineShip);

            if (intCount < 255 || clsCenterCollision.CheckIsCollision())
            {
                for (int i = 0; i < lstLstMyLine.Count; i++)
                    lstLstMyLine[i] = Rotate(lstLstMyLine[i], ptOrigin, lstOffset[i], lstRotation[i]);

                CreateShipDebris(lstLstMyLine);

                engineShip.intExplode++;
            }
            else
            {
                engineShip.ptBlockOrigin = new Point2d();
                engineShip.bolExplode = false;
                engineShip.intExplode = 0;

                clsMoveShip.dblLocalAngle = 0.0;
                clsMoveShip.dblAccelerationAngle = 0.0;
                clsMoveShip.dblVelocity = 0.0;

                clsUpdatePlayer clsUpdatePlayer = new clsUpdatePlayer();

                if (clsUpdatePlayer.RemovePlayer(acTrans))
                    StaticRock.EngineScore.bolReset = true;
            }
        }

        internal void CreateShipDebris(List<List<Point2d>> lstLstMyLine)
        {
            List<GameLine> lstGameLine = new List<GameLine>();
            for (int i = 0; i < lstLstMyLine.Count; i++)
                lstGameLine.Add(new GameLine(lstLstMyLine[i][0], lstLstMyLine[i][1]));

            if (StaticRock.lstShipDebris == null)
                StaticRock.lstShipDebris = new List<EngineShipDebris>();

            StaticRock.lstShipDebris.Add(new EngineShipDebris(lstGameLine, 3, 1));
        }


        internal List<List<Point2d>> SetRandomValues(EngineShip engineShip)
        {
            clsCreatePoints clsGeneratePoints = new clsCreatePoints();
            Random random = clsGeneratePoints.GetRandom();

            List<Point2d> lstMatrix = engineShip.lstLstMatrix3dShip[0];
            List<List<Point2d>> lstLstMyLine = lstMatrix.GroupIntoLines();

            lstRotation.Clear();

            lstOffset.Clear();

            for (int i = 0; i < lstLstMyLine.Count; i++)
                lstRotation.Add(random.GetDouble(-1, 1));

            for (int i = 0; i < lstLstMyLine.Count; i++)
                lstOffset.Add(random.GetDouble(0.1, 0.5));

            return lstLstMyLine;
        }

        internal List<Point2d> Rotate(List<Point2d> lstMyLine, Point2d ptOrigin,
                                      double dblOffset, double dblRotation)
        {
            Point2d ptStart = lstMyLine[0];
            Point2d ptEnd = lstMyLine[1];

            Point2d ptMid = ptStart.GetMidPoint(ptEnd);
            double dblAngle = ptOrigin.GetAngle(ptMid);

            for (int k = 0; k < lstMyLine.Count; k++)
                lstMyLine[k] = lstMyLine[k].CalculatePoint(dblAngle, dblOffset);

            ptStart = lstMyLine[0];
            ptEnd = lstMyLine[1];

            ptMid = ptStart.GetMidPoint(ptEnd);

            RotatePoints(ref ptStart, ref ptEnd, ptMid, dblRotation);

            lstMyLine[0] = ptStart;
            lstMyLine[1] = ptEnd;

            return lstMyLine;
        }

        internal void RotatePoints(ref Point2d start, ref Point2d end, Point2d midPoint, double angleInDegrees)
        {
            angleInDegrees = angleInDegrees.ToRadians();
            Vector3d vec = new Vector3d(midPoint.X, midPoint.Y, 0);
            // Create a 2D rotation matrix around the midpoint
            Matrix3d rotationMatrix = Matrix3d.Rotation(angleInDegrees, Vector3d.ZAxis, midPoint.ToPoint3d());

            // Transform the start and end points using the rotation matrix
            Point3d rotatedStartPoint = start.ToPoint3d().TransformBy(rotationMatrix);
            Point3d rotatedEndPoint = end.ToPoint3d().TransformBy(rotationMatrix);

            start = rotatedStartPoint.ToPoint2d();
            end = rotatedEndPoint.ToPoint2d();
        }
    }
}