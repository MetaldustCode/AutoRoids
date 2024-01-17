using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;

namespace AutoRoids
{
    internal class clsZoom
    {
        public void ZoomToPoint(Point3d pt)
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            Editor acEd = acDoc.Editor;

            using (var view = acEd.GetCurrentView())
            {
                var UCS2DCS =
                    (Matrix3d.Rotation(-view.ViewTwist, view.ViewDirection, view.Target) *
                    Matrix3d.Displacement(view.Target - Point3d.Origin) *
                    Matrix3d.PlaneToWorld(view.ViewDirection)).Inverse() *
                    acEd.CurrentUserCoordinateSystem;
                var center = pt.TransformBy(UCS2DCS);
                view.CenterPoint = new Point2d(center.X, center.Y);
                acEd.SetCurrentView(view);
            }
        }

        public void ZoomScale()
        {
            Double dblScale = 3;
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            Editor acEd = acDoc.Editor;

            using (ViewTableRecord view = acEd.GetCurrentView())
            {
                view.Width /= dblScale;
                view.Height /= dblScale;
                acEd.SetCurrentView(view);
            }
        }
    }
}