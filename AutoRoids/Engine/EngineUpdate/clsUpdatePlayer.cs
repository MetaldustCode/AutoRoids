using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoRoids
{
    internal class clsUpdatePlayer
    {
        internal void RemovePlayer()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {
                    RemovePlayer(acTrans);
                    acTrans.Commit();
                }
            }

            Autodesk.AutoCAD.ApplicationServices.Application.UpdateScreen();
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();
            dynamic acadApp = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication;
            acDoc.Editor.Regen();
        }

        internal Boolean RemovePlayer(Transaction acTrans)
        {
            EngineScore engineScore = StaticRock.EngineScore;
            if (engineScore != null)
            {
                if (engineScore.lstBlkRefShip != null)
                {
                    if (engineScore.lstBlkRefShip.Count > 0)
                    {
                        for (int i = engineScore.lstBlkRefShip.Count - 1; i >= 0; i--)
                        {
                            BlockReference acBlkRef = acTrans.GetObject(engineScore.lstBlkRefShip[i].ObjectId, OpenMode.ForWrite) as BlockReference;
                            acBlkRef.Erase();
                            engineScore.lstBlkRefShip.RemoveAt(i);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        internal void ErasePlayers(Transaction acTrans)
        {
            EngineScore engineScore = StaticRock.EngineScore;
            if (engineScore != null)
            {
                if (engineScore.lstBlkRefShip != null)
                {
                    for (int i = 0; i < engineScore.lstBlkRefShip.Count; i++)
                    {
                        if (engineScore.lstBlkRefShip[i].IsValid(acTrans))
                        {
                            BlockReference acBlkRef = acTrans.GetObject(engineScore.lstBlkRefShip[i].ObjectId, OpenMode.ForWrite) as BlockReference;
                            acBlkRef.Erase();
                        }
                    }
                }
            }
        }

        internal void AddDefaultPlayer()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {
                    BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    ErasePlayers(acTrans);

                    clsAddBlock clsAddBlock = new clsAddBlock();

                    List<BlockReference> lstBlockReference = new List<BlockReference>();

                    for (int i = 0; i < 3; i++)
                        AddPlayer(acTrans, acDb, acBlkTbl, ref lstBlockReference, i);

                    StaticRock.EngineScore = new EngineScore(lstBlockReference);
                    acTrans.Commit();
                }
            }

            Autodesk.AutoCAD.ApplicationServices.Application.UpdateScreen();
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();
            dynamic acadApp = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication;
            acDoc.Editor.Regen();
        }

        internal void AddNewPlayer()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {
                    // BlockTableRecord acBTR = (BlockTableRecord)acTrans.GetObject(acDb.CurrentSpaceId, OpenMode.ForWrite);
                    BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    AddNewPlayer(acTrans, acDb, acBlkTbl);

                    acTrans.Commit();
                }
            }

            Autodesk.AutoCAD.ApplicationServices.Application.UpdateScreen();
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();
            dynamic acadApp = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication;
            acDoc.Editor.Regen();
        }

        internal void AddNewPlayer(Transaction acTrans, Database acDb, BlockTable acBlkTbl)
        {
            if (StaticRock.EngineScore != null)
            {
                if (StaticRock.EngineScore.lstBlkRefShip != null)
                {
                    int intCount = StaticRock.EngineScore.lstBlkRefShip.Count;

                    List<BlockReference> lstBlockReference = StaticRock.EngineScore.lstBlkRefShip;

                    AddPlayer(acTrans, acDb, acBlkTbl, ref lstBlockReference, intCount);

                    StaticRock.EngineScore.lstBlkRefShip = lstBlockReference;
                }
            }
        }

        internal void AddPlayer(Transaction acTrans, Database acDb, BlockTable acBlkTbl, ref List<BlockReference> lstBlockReference, int i)
        {
            clsReg clsReg = new clsReg();
            clsReg.GetGameScale(out double dblGameScale);

            double dblTop = 1130 - (44.149 * dblGameScale);
            double dblLeft = -1700;

            double dblWidth = 0.003;
            int intShip = 3;
            clsAddBlock clsAddBlock = new clsAddBlock();
            BlockReference acBlkRefShip = clsAddBlock.BuildShip(acTrans, acDb, acBlkTbl, "Ship", intShip, 0, dblWidth, "Continuous", true, out List<Point2d> lstPointShip, true, out double dblScale);

            double dblRotation = 90;
            dblRotation = dblRotation.ToRadians();
            acBlkRefShip.Rotation = dblRotation;

            double dblOffset = 60 * i * StaticRock.dblGameScale;
            acBlkRefShip.Position = new Point3d(dblLeft + dblOffset, dblTop, 0);
            acBlkRefShip.ColorIndex = 3;

            lstBlockReference.Add(acBlkRefShip);
        }
    }
}