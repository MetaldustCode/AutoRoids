namespace AutoRoids
{
    internal class clsStars
    {
        //internal void DeleteStars(Transaction acTrans)
        //{
        //    if (StaticRock.lstStars != null)
        //    {
        //        for (int i = 0; i < StaticRock.lstStars.Count; i++)
        //        {
        //            if (StaticRock.lstStars[i].ObjectId.IsValid && !StaticRock.lstStars[i].IsErased)
        //            {
        //                BlockReference acBlkRef = acTrans.GetObject(StaticRock.lstStars[i].ObjectId, OpenMode.ForWrite) as BlockReference;
        //                try
        //                { acBlkRef.Erase(); }
        //                catch (Exception)
        //                { }
        //            }
        //        }

        //        StaticRock.lstStars.Clear();
        //    }
        //}

        //internal void AddStars(Transaction acTrans, Database acDb, BlockTable acBlkTbl)
        //{
        //    DeleteStars(acTrans);

        //    clsCreateStarfield clsCreateStarfield = new clsCreateStarfield();
        //    List<Point2d> lstPoints = clsCreateStarfield.GetStarField();
        //    clsAddBlock clsAddBlock = new clsAddBlock();
        //    clsAppend clsAppend = new clsAppend();

        //    for (int i = 0; i < lstPoints.Count; i++)
        //    {
        //        BlockReference acBlkRef = null;

        //        if (i % lstPoints.Count == 0)
        //        {
        //            acBlkRef = clsAddBlock.BuildBullet(acTrans, acDb, acBlkTbl, "Star-Large", 5, 5);
        //            acBlkRef.Position = lstPoints[i].ToPoint3d();
        //            clsAppend.AppendToStars(acBlkRef);
        //            continue;
        //        }

        //        acBlkRef = clsAddBlock.BuildStar(acTrans, acDb, acBlkTbl, "Star-Small", 7, 1.0);
        //        acBlkRef.Position = lstPoints[i].ToPoint3d();
        //        clsAppend.AppendToStars(acBlkRef);
        //    }
        //}
    }
}