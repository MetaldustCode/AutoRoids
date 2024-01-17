using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoRoids
{
    internal class EngineShip
    {
        internal List<BlockReference> lstBlkRefShip = new List<BlockReference>();
        internal List<BlockReference> lstBlkRefShield = new List<BlockReference>();

        internal List<BlockReference> lstBlkRefBullet = new List<BlockReference>();
        internal List<Point2d> lstOriginBullet = new List<Point2d>();

        // Constant Values
        internal double dblScale;

        internal double dblDistance;

        internal List<List<Point2d>> lstLstOriginalShip = new List<List<Point2d>>();
        internal List<List<Point2d>> lstLstOriginalShield = new List<List<Point2d>>();

        // Dynamic Values

        internal Point2d ptBlockOrigin;
        internal double dblBlockRotation;

        internal double dblRotation;
        internal double dblAngle;

        internal List<List<Point2d>> lstLstMatrix3dShip = new List<List<Point2d>>();
        internal List<List<Point2d>> lstLstMatrix3dShield = new List<List<Point2d>>();

        internal List<List<Point2d>> lstLstBoundingBoxShip = new List<List<Point2d>>();
        internal List<List<Point2d>> lstLstBoundingBoxShield = new List<List<Point2d>>();

        internal Boolean bolVisibleThrust;
        internal Boolean bolVisibleShield;
        internal List<double> lstRotationShield = new List<double>();

        internal Boolean bolExplode = false;
        internal int intExplode = 0;

        internal EngineShip(List<BlockReference> lstBlkRefShip,
                            List<BlockReference> lstBlkRefShield,
                            List<List<Point2d>> lstLstOriginalShip,
                            List<List<Point2d>> lstLstOriginalShield,
                            List<double> lstRotationShield,
                            double dblScale, double dblDistance)
        {
            this.lstBlkRefShip = lstBlkRefShip;
            this.lstBlkRefShield = lstBlkRefShield;
            this.dblScale = dblScale;
            this.dblDistance = dblDistance;
            this.lstLstOriginalShip = lstLstOriginalShip;
            this.lstLstOriginalShield = lstLstOriginalShield;
            this.lstRotationShield = lstRotationShield;
        }
    }
}