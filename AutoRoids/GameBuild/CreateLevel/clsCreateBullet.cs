using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;

namespace AutoRoids
{
    internal class clsCreateBullet
    {
        internal void EraseGameBullets()
        {
            if (StaticRock.lstBullets != null)
                StaticRock.lstBullets.Clear();
        }

        internal void DeleteBullet(Transaction acTrans, BlockReference acBlkRef)
        {
            clsDeleteEntity clsDeleteEntity = new clsDeleteEntity();
            List<BlockReference> lstBlkRef = new List<BlockReference> { acBlkRef };
            clsDeleteEntity.DeleteEntity(acTrans, ref lstBlkRef);
        }
    }
}