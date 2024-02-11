using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Internal;
using Autodesk.AutoCAD.Runtime;

[assembly: CommandClass(typeof(AutoRoids.MyCommands))]

namespace AutoRoids
{

    public class MyCommands
    {
        [CommandMethod("GetViewSize")]
        public  void RunMyCommand()
        {
            Document dwg = Application.DocumentManager.MdiActiveDocument;
            Editor ed = dwg.Editor;

            clsViewUtil util = new clsViewUtil(dwg);

            try
            {
                //Get current view size
                Point2d vSize = util.GetCurrentViewSize();
                ed.WriteMessage("\nCurrent view size: " +
                    "H={0} W={1}", vSize.Y, vSize.X);

                //Draw a bound box of current view, which
                //is slightly smaller than current view
                util.GetCurrentViewBound(0.95, true);

                //Draw a bound box of current view, which
                //is exactly the same size as current view
                util.GetCurrentViewBound(1.0, true);
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("\nError: {0}", ex.ToString());
            }

            Autodesk.AutoCAD.Internal.Utils.PostCommandPrompt();
        }
    }
}

