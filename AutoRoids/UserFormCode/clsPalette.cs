using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using AutoRoids.UserForm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;

namespace AutoRoids
{
    public class clsPalette
    {
        internal static PaletteSet _ps = null;
        internal static bool _IsInInit = false;

        internal static List<Control> _lstControls = new List<Control>();

        private int intWidth = 220;
        private int intHeight = 200;

        internal static string strCurrentPalette = "";

        [CommandMethod("AutoRoids")]
        public void ShowWPFPalette()
        {
            if (_ps == null)
            {
                // AddTimer();
                _IsInInit = true;
                // Create the palette set
                clsReg clsReg = new clsReg();
                _ps = new PaletteSet("AutoRoids", new Guid(clsReg.GetAutoRoids()));

                _ps.DockEnabled = DockSides.Right | DockSides.Left;

                _ps.Style = Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowPropertiesMenu
                            | Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowAutoHideButton
                            | Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowCloseButton
                            | Autodesk.AutoCAD.Windows.PaletteSetStyles.Snappable;

                xmlAutoRoids xmlAutoRoids = new xmlAutoRoids();

                _ps.MinimumSize = new Size(intWidth, intHeight);

                _ps.AddVisual("AutoRoids", xmlAutoRoids);

                _ps.RecalculateDockSiteLayout();

                _ps.StateChanged += new PaletteSetStateEventHandler(PaletteSet_StateChanged);

                _ps.PaletteActivated += _ps_PaletteActivated;
                // Display our palette set
                _ps.KeepFocus = true;
                _ps.Visible = true;
                _IsInInit = false;
            }

            _ps.KeepFocus = true;
            _ps.Visible = true;
        }

        public void _ps_PaletteActivated(object sender, PaletteActivatedEventArgs e)
        {
            strCurrentPalette = e.Activated.Name;
        }

        public void PaletteSet_StateChanged(object sender, PaletteSetStateEventArgs e)
        {
            if (e.NewState.ToString().ToUpper() == "SHOW")
            {
            }
        }
    }
}