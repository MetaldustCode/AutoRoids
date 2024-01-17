using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System.Collections.Generic;

namespace AutoRoids
{
    internal class clsUpdateRock
    {
        internal void UpdateRockDataFrame()
        {
            List<EngineRock> lstEngineRock = StaticRock.lstEngineRock;

            if (lstEngineRock != null)
            {
                for (int i = 0; i < lstEngineRock.Count; i++)
                {
                    EngineRock engineRock = lstEngineRock[i];

                    engineRock.ptBlockOrigin =
                        engineRock.ptBlockOrigin.CalculatePoint(engineRock.dblAngle, engineRock.dblDistance);

                    engineRock.dblBlockRotation =
                        (engineRock.dblBlockRotation += engineRock.dblRotation).NormalizeAngle();

                    clsWrap clsWrap = new clsWrap();
                    clsWrap.WrapRock(ref engineRock);

                    lstEngineRock[i] = engineRock;
                }
            }
        }

        internal void UpdateRockGraphics(Transaction acTrans, Database acDb, BlockTable acBlkTbl)
        {
            List<EngineRock> lstEngineRock = StaticRock.lstEngineRock;

            if (lstEngineRock != null)
            {
                for (int i = lstEngineRock.Count - 1; i >= 0; i--)
                {
                    EngineRock engineRock = lstEngineRock[i];

                    if (!engineRock.bolExploded)
                    {
                        BlockReference acBlkRef = acTrans.GetObject(engineRock.acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;
                        acBlkRef.Position = engineRock.ptBlockOrigin.ToPoint3d();
                        acBlkRef.Rotation = engineRock.dblBlockRotation.ToRadians();
                    }
                    else
                    {
                        BlockReference acBlkRef = acTrans.GetObject(engineRock.acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;
                        acBlkRef.Erase();
                        lstEngineRock.RemoveAt(i);
                    }
                }
            }

            EngineShip EngineShip = StaticRock.EngineShip;

            for (int i = 0; i < EngineShip.lstBlkRefShield.Count; i++)
            {
                BlockReference acBlkRef = EngineShip.lstBlkRefShield[i];
                acBlkRef = acTrans.GetObject(acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;

                if (EngineShip.bolVisibleShield)
                {
                    acBlkRef.Visible = true;
                    acBlkRef.Rotation = EngineShip.lstRotationShield[i].ToRadians();
                    acBlkRef.Position = EngineShip.ptBlockOrigin.ToPoint3d();
                }
                else
                {
                    if (acBlkRef.Visible == true)
                        acBlkRef.Visible = false;
                }
            }

            for (int i = 0; i < EngineShip.lstBlkRefShip.Count; i++)
            {
                BlockReference acBlkRef = EngineShip.lstBlkRefShip[i];
                acBlkRef = acTrans.GetObject(acBlkRef.ObjectId, OpenMode.ForWrite) as BlockReference;

                acBlkRef.Position = EngineShip.ptBlockOrigin.ToPoint3d();

                acBlkRef.Rotation = EngineShip.dblBlockRotation.ToRadians();

                if (i == 1)
                {
                    if (!EngineShip.bolExplode)
                        acBlkRef.Visible = EngineShip.bolVisibleThrust;
                    else
                        acBlkRef.Visible = false;
                }
                else
                {
                    if (!EngineShip.bolExplode)
                        acBlkRef.Visible = true;
                    else
                        acBlkRef.Visible = false;
                }
            }

            clsCacheGetBullet clsCacheGetBullet = new clsCacheGetBullet();
            clsCacheGetBullet.HideBullet(acTrans);

            if (StaticRock.lstBullets != null)
            {
                for (int i = 0; i < StaticRock.lstBullets.Count; i++)
                {
                    EngineBullet engineBullet = StaticRock.lstBullets[i];

                    clsCacheGetBullet.GetBullet(acTrans, acDb, acBlkTbl, engineBullet.ptOrigin, engineBullet.intColor);
                }
            }

            if (StaticRock.lstExplode != null)
            {
                clsCacheGetExplode clsCacheGetExplode = new clsCacheGetExplode();
                clsCacheGetExplode.HideExplode(acTrans);

                for (int i = 0; i < StaticRock.lstExplode.Count; i++)
                {
                    EngineExplode engineExplode = StaticRock.lstExplode[i];

                    clsCacheGetExplode.GetExplode(acTrans, acDb, acBlkTbl, engineExplode.ptOrigin, engineExplode.intColor);
                }
            }

         
        }
    }
}