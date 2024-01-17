using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoRoids
{
    internal class clsCreateEngineBackGround
    {
        internal EngineBackGround CreateBackGround(Transaction acTrans, Database acDb, BlockTable acBlkTbl)
        {
            DeleteBackGround(acTrans);
            List<BlockReference> lstStars = GetStars(acTrans, acDb, acBlkTbl);
            List<BlockReference> lstBorder = GetBorder(acTrans, acDb, acBlkTbl, out List<List<Point2d>> lstLstPoint); 

            EngineBackGround EngineBackGround =
                new EngineBackGround(lstStars, lstBorder, lstLstPoint[0], lstLstPoint[1]);

            return EngineBackGround;
        }

        internal void DeleteBackGround(Transaction acTrans)
        {
            EngineBackGround EngineBackGround = StaticRock.EngineBackGround;

            if (EngineBackGround != null)
            {
                if (EngineBackGround.lstBlkRef != null)
                {
                    EngineBackGround.lstBlkRef.DeleteBlockReference(acTrans);
                }

                EngineBackGround.lstBlkRef.Clear();
            }
        }

        internal List<BlockReference> GetStars(Transaction acTrans, Database acDb, BlockTable acBlkTbl)
        {
            clsCreateStarfield clsCreateStarfield = new clsCreateStarfield();
            List<Point2d> lstPoints = clsCreateStarfield.GetStarField();
            clsAddBlock clsAddBlock = new clsAddBlock();

            List<BlockReference> lstBlkRef = new List<BlockReference>();

            for (int i = 0; i < lstPoints.Count; i++)
            {
                BlockReference acBlkRef = null;
                if (i % lstPoints.Count == 0)
                    acBlkRef = clsAddBlock.BuildBullet(acTrans, acDb, acBlkTbl, "Earth", 5, 5);
                else
                    acBlkRef = clsAddBlock.BuildStar(acTrans, acDb, acBlkTbl, "Star", 7, 1.0);

                acBlkRef.Position = lstPoints[i].ToPoint3d();
                lstBlkRef.Add(acBlkRef);
            }

            return lstBlkRef;
        }

        internal List<BlockReference> GetBorder(Transaction acTrans, Database acDb, BlockTable acBlkTbl, out List<List<Point2d>> lstLstPoint)
        {
            List<BlockReference> lstBlkRef = new List<BlockReference>();
            lstLstPoint = new List<List<Point2d>>();

            clsAddBlock clsAddBlock = new clsAddBlock();
        
            double dblScale = 100.0;
            double dblWidth = 0.003;

            List<int> lstColor = new List<int> { 3, 3 };
            List<String> lstLineType = new List<String> { "Continuous", "Dashed" };
            List<String> lstBlockName = new List<String> { "Outside", "Inside" };

            for (int i = 0; i < 2; i++)
            {
                int intPos = i;
                BlockReference acBlkRef =
                    clsAddBlock.BuildBorder(acTrans, acDb, acBlkTbl, String.Format("Border-{0}",
                                            lstBlockName[i]), lstColor[i], intPos, dblWidth, dblScale, lstLineType[i], out List<Point2d> lstPoint);

                lstBlkRef.Add(acBlkRef);
                lstLstPoint.Add(lstPoint);
            }

            return lstBlkRef;
        }
    }
}