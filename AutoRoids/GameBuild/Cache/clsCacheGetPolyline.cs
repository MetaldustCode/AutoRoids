using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoRoids
{
    internal class clsCacheGetPolyline
    {
        internal static List<Polyline> lstPolyline;
        internal static List<Boolean> lstVisible;
        internal static int intPolyline;

        internal Polyline GetPolyline(Transaction acTrans, Database acDb, List<Point2d> lstPoint,
                                       int intColor, double dblWidth)
        {
            if (lstPolyline == null)
                lstPolyline = new List<Polyline>();

            if (lstVisible == null)
                lstVisible = new List<Boolean>();

            Polyline acPline = null;

            if (intPolyline < lstPolyline.Count)
            {
                acPline = lstPolyline[intPolyline];
                ModifyPolyline(acTrans, acPline, lstPoint, intColor, dblWidth);
                lstVisible[intPolyline] = true;
            }
            else
            {
                clsAddGeometry clsPolylineAdd = new clsAddGeometry();
                acPline = clsPolylineAdd.AddPolyLine(acTrans, acDb, lstPoint, intColor, dblWidth);

                lstPolyline.Add(acPline);
                lstVisible.Add(true);
            }

            intPolyline++;

            return acPline;
        }

        internal void ModifyPolyline(Transaction acTrans, Polyline acPoly, List<Point2d> lstPoint,
                                     int intColor, double dblWidth)
        {
            if (acPoly.ObjectId.IsValid && !acPoly.ObjectId.IsErased)
            {
                acPoly = acTrans.GetObject(acPoly.ObjectId, OpenMode.ForWrite) as Polyline;

                acPoly.Color = Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, (Int16)intColor);

                //for (int i = 0; i < acPoly.NumberOfVertices +1; i++)
                //    acPoly.RemoveVertexAt(i);

                if (acPoly.NumberOfVertices == lstPoint.Count)
                {
                    for (int i = 0; i < lstPoint.Count; i++)
                        acPoly.SetPointAt(i, lstPoint[i]);
                }

                acPoly.ConstantWidth = dblWidth;

                //for (int i = 0; i < lstPoint.Count; i++)
                //    acPoly.AddVertexAt(i, new Point2d(lstPoint[i].X, lstPoint[i].Y), 0, dblWidth, dblWidth);

                acPoly.Visible = true;
            }
        }

        internal void HidePolyline(Transaction acTrans)
        {
            if (lstPolyline != null)
            {
                if (lstVisible.Contains(true))
                {
                    for (int i = 0; i < lstPolyline.Count; i++)
                    {
                        Polyline acPoly = lstPolyline[i];

                        if (lstVisible[i] == true)
                        {
                            if (acPoly.ObjectId.IsValid && !acPoly.ObjectId.IsErased)
                            {
                                acPoly = acTrans.GetObject(acPoly.ObjectId, OpenMode.ForWrite) as Polyline;
                                acPoly.Visible = false;
                                lstVisible[i] = false;
                            }
                        }
                    }
                }
            }
        }
    }
}