using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;

namespace AutoRoids
{
    internal class clsAppend
    {
        internal void AppendToEntity(Polyline acEntity)
        {
            if (StaticRock.lstLines == null)
                StaticRock.lstLines = new List<Polyline>();

            StaticRock.lstLines.Add(acEntity);
        }

        internal void AppendToEntity(DBPoint acEntity)
        {
            if (StaticRock.lstPoint == null)
                StaticRock.lstPoint = new List<DBPoint>();

            StaticRock.lstPoint.Add(acEntity);
        }
    }
}