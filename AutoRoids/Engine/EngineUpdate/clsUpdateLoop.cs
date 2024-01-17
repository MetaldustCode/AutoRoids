using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;

namespace AutoRoids
{
    internal class clsUpdateLoop
    {
        internal void GameLoop(int intElapsed, int intIdleDelay)
        {
            // Reset Polyline Counter
            clsCacheGetPolyline.intPolyline = 0;
            clsCacheGetPoint.intPoint = 0;
            clsCacheGetBullet.intBlkRef = 0;
            clsCacheGetExplode.intBlkRef = 0;

            StaticRock.lstBoundingBox.Clear();

            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;

            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {
                    if (!StaticRock.bolBoundingBox)
                    {
                        clsCacheGetPolyline clsCache = new clsCacheGetPolyline();
                        clsCache.HidePolyline(acTrans);
                    }

                    BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;

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
                                clsExplodeShip clsExplodeShip = new clsExplodeShip();
                                clsExplodeShip.ExplodeShip(acTrans, acDb);
                            }

                            clsFireBullets.OffsetBullets();
                            clsFireBullets.WrapBullet();

                            clsFireCollision.FireCollision(acTrans, acDb, acBlkTbl);
                            clsFireCollision.OffsetExplode();

                            // Engine Objects
                            List<EngineRock> lstEngineRock = StaticRock.lstEngineRock;
                            List<EngineBullet> lstBullets = StaticRock.lstBullets;
                            List<EngineExplode> lstExplode = StaticRock.lstExplode;
                            EngineShip EngineShip = StaticRock.EngineShip;
                        }

                        clsGetBoundingBox clsGetBoundingBox = new clsGetBoundingBox();
                        clsGetBoundingBox.DrawingBoundingBox(acTrans, acDb);

                        clsUpdateRock.UpdateRockGraphics(acTrans, acDb, acBlkTbl);
                    }

                    acTrans.Commit();
                }
            }

            Autodesk.AutoCAD.ApplicationServices.Application.UpdateScreen();
        }
    }
}