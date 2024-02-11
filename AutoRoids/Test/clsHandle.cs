using AutoCAD;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

[assembly: CommandClass(typeof(AutoRoids.clsHandle))]

namespace AutoRoids
{
    public class clsHandle
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [CommandMethod("ss2")]
        public void Main()
        {
            IntPtr mainWindowHandle = Autodesk.AutoCAD.ApplicationServices.Application.MainWindow.Handle;

            // Get the PID associated with the main window handle
            if (GetWindowThreadProcessId(mainWindowHandle, out uint processId) != 0)
            {
                Console.WriteLine($"Process ID (PID) associated with the window: {processId}");
                int signedValue = (int)processId;

                AcadApplication acadApp = GetAutoCADInstanceByPID(signedValue);

                if (acadApp != null)
                {
                    // Get the active document
                    AcadDocument doc = acadApp.ActiveDocument;

                    // Create a new selection set
                    AcadSelectionSets selectionSets = doc.SelectionSets;
                    AcadSelectionSet selectionSet = selectionSets.Add("MySelectionSet");
                }
            }

        }




        internal AcadApplication GetAutoCADInstanceByPID(int processId)
        {
            // Get all processes running on the system
            Process[] processes = Process.GetProcessesByName("acad");

            // Find the process with the matching PID
            foreach (Process process in processes)
            {
                if (process.Id == processId)
                {
                    // Match found, connect to the AutoCAD instance
                    try
                    {
                  
                        return (AcadApplication)System.Runtime.InteropServices.Marshal.GetActiveObject($"AutoCAD.Application.{process.Id}");
                    }
                    catch (System.Exception ex)
                    {
                        // Handle exceptions if unable to connect
                        Console.WriteLine($"Failed to connect to AutoCAD instance with PID {processId}: {ex.Message}");
                        return null;
                    }
                }
            }

            // AutoCAD instance not found for the given PID
            return null;
        }



    }
}
