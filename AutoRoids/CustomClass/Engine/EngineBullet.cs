using Autodesk.AutoCAD.Geometry;

namespace AutoRoids
{
    internal class EngineBullet
    {
        internal Point2d ptOrigin;
        internal double dblAngle;
        internal double dblDistance;
        internal double dblTraveled;
        internal double dblMaxTravel;
        internal int intColor = 3;

        internal EngineBullet(Point2d ptOrigin,
                              double dblAngle)
        {
            this.ptOrigin = ptOrigin;
            this.dblAngle = dblAngle;
            this.dblMaxTravel = StaticRock.dblBulletMaxTravel;
        }
    }
}