using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;

namespace AutoRoids
{
    internal static class StaticRock
    {
        // Number of Ships
        internal static int intNumberofPlayers = 5;
        // Starfield
        public static double dblOuterWidth = 3150;

        public static double dblOuterHeight = 1950;

        // Default Rock
        public static double dblInnerWidth = 1200;

        public static double dblInnerHeight = 2200;

        //Physics
        public static double dblLocalAngle = 0.0;

        public static double dblAccelerationAngle = 0.0;
        public static double dblVelocity = 0.0;

        // Block Line Width
        internal static double dblPolylineWidth = 0.03;

        // Bullet Times
        internal static double dblBulletOffset = 30;

        internal static double dblBulletMaxTravel = 2000;
        internal static int intBulletDelay = 150;
        internal static int intDeathBlossomDelay = 50;

        // Explode Rock Speed
        internal static double dblExplodeSpeed = 1.0;

        // Registry
        internal static int intRockCount;

        internal static double dblRockAngle;

        internal static double dblRockMinDistance;
        internal static double dblRockMaxDistance;

        internal static double dblRockMinRotation;
        internal static double dblRockMaxRotation;

        internal static double dblShipAngle;
        internal static double dblShipMaxSpeed;

        internal static double dblGameScale;
        internal static int intIdleDelay;

        internal static Boolean bolShowBlocks;
        internal static Boolean bolShowOverlay;
        internal static Boolean bolBoundingBox;
        internal static Boolean bolZoomToShip;

        // Engine Objects
        internal static List<EngineRock> lstEngineRock;
        internal static List<EngineBullet> lstBullets;
        internal static List<EngineExplode> lstExplode;

        // Temp Lines
        internal static List<EngineBoundingBox> lstBoundingBox;
        internal static List<EngineShipDebris> lstShipDebris;

        internal static EngineShip EngineShip;
        internal static EngineBackGround EngineBackGround;
        internal static EngineScore EngineScore;

        internal static List<Polyline> lstLines = new List<Polyline>();
        internal static List<DBPoint> lstPoint = new List<DBPoint>();

        internal static void InitStaticRock()
        {
            if (lstLines == null) lstLines = new List<Polyline>();
            if (lstPoint == null) lstPoint = new List<DBPoint>();
        }

        internal static void SetStaticRegistry(Boolean bolInit)
        {
            clsReg clsReg = new clsReg();
            if (bolInit)
                clsReg.GetRockCount(out StaticRock.intRockCount);

            clsReg.GetRockAngle(out StaticRock.dblRockAngle);

            clsReg.GetRockMaxDistance(out StaticRock.dblRockMaxDistance);
            clsReg.GetRockMinDistance(out StaticRock.dblRockMinDistance);

            clsReg.GetRockMaxRotation(out StaticRock.dblRockMaxRotation);
            clsReg.GetRockMinRotation(out StaticRock.dblRockMinRotation);

            clsReg.GetShipRotation(out StaticRock.dblShipAngle);
            clsReg.GetShipThrust(out StaticRock.dblShipMaxSpeed);

            clsReg.GetGameScale(out StaticRock.dblGameScale);

            clsReg.GetIdleDelay(out StaticRock.intIdleDelay);
            clsReg.GetDeathBlossomDelay(out StaticRock.intDeathBlossomDelay);

            clsReg.GetBulletDelay(out StaticRock.intBulletDelay);

            StaticRock.bolShowBlocks = clsReg.GetShowBlocks();
            StaticRock.bolBoundingBox = clsReg.GetShowBoundingBox();
            StaticRock.bolShowOverlay = clsReg.GetShowOverlay();
            StaticRock.bolZoomToShip = clsReg.GetZoomToShip();
            StaticRock.dblBulletOffset = dblShipMaxSpeed * 2;
        }
    }
}