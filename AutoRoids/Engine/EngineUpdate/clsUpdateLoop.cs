using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;

namespace AutoRoids
{
    internal class clsUpdateLoop
    {

        internal void GameLoop(int intElapsed, int intIdleDelay)
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            if (acDoc != null)
            {
                Database acDb = acDoc.Database;

                using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
                {
                    using (DocumentLock @lock = acDoc.LockDocument())
                    {
                        BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                        ResetCacheCounter(acTrans);

                        clsUpdateRock clsUpdateRock = new clsUpdateRock();

                        if (intElapsed >= intIdleDelay)
                        {
                            clsFireBullets clsFireBullets = new clsFireBullets();

                            // for (int i = 0; i < 5; i++)
                            {
                                clsFireCollision clsFireCollision = new clsFireCollision();

                                clsUpdateRock.UpdateRockDataFrame();

                                if (!StaticRock.EngineShip.bolExplode)
                                {
                                    clsUpdateShip clsUpdateShip = new clsUpdateShip();
                                    List<enumDirection> lstDirection = clsUpdateShip.MoveShip();
                                    clsFireBullets.FireBullet(lstDirection);

                                    if (!lstDirection.Contains(enumDirection.Shield))
                                        if (clsFireCollision.ShipCollision(acTrans, acDb, acBlkTbl))
                                            StaticRock.EngineShip.bolExplode = true;
                                }
                                else
                                {
                                    // Transaction to Update Player Block
                                    clsExplodeShip clsExplodeShip = new clsExplodeShip();
                                    clsExplodeShip.ExplodeShip(acTrans);
                                }

                                clsFireBullets.OffsetBullets();
                                clsFireBullets.WrapBullet();

                                clsFireCollision.FireCollision(acTrans, acDb, acBlkTbl);
                                clsFireCollision.OffsetExplode();

                                // Engine Objects (Debug)
                                List<EngineRock> lstEngineRock = StaticRock.lstEngineRock;
                                List<EngineBullet> lstBullets = StaticRock.lstBullets;
                                List<EngineExplode> lstExplode = StaticRock.lstExplode;
                                EngineShip EngineShip = StaticRock.EngineShip;
                            }

                            SetBoundingBox(acTrans, acDb);

                            clsUpdateRock.UpdateGraphics(acTrans, acDb, acBlkTbl);
                        }

                        acTrans.Commit();
                    }
                }

                Autodesk.AutoCAD.ApplicationServices.Application.UpdateScreen();
            }
        }

        private void SetBoundingBox(Transaction acTrans, Database acDb)
        {
            if (!StaticRock.bolBoundingBox)
            {
                clsCacheGetBoundingBox clsCacheGetBoundingBox = new clsCacheGetBoundingBox();
                clsCacheGetBoundingBox.HideBoundingBox(acTrans);
            }
            else
            {
                clsGetBoundingBox clsGetBoundingBox = new clsGetBoundingBox();
                clsGetBoundingBox.DrawingBoundingBox(acTrans, acDb);
            }
        }

        private void ResetCacheCounter(Transaction acTrans)
        {
            if (StaticRock.lstShipDebris == null)
                StaticRock.lstShipDebris = new List<EngineShipDebris>();
            else
                StaticRock.lstShipDebris.Clear();

            clsCacheGetDebris.intPolyline = 0;

            if (StaticRock.lstBoundingBox == null)
                StaticRock.lstBoundingBox = new List<EngineBoundingBox>();
            else
                StaticRock.lstBoundingBox.Clear();

            clsCacheGetBoundingBox.intPolyline = 0;

            clsCacheGetPoint.intPoint = 0;
            clsCacheGetBullet.intBlkRef = 0;
            clsCacheGetExplode.intBlkRef = 0;
        }
    }
}