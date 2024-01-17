using SharpDX.DirectInput;

namespace AutoRoids
{
    internal static class clsSharpDX
    {
        //[STAThread]
        internal static void Main()
        {
            MainForKeyboard();
        }

        internal static Keyboard keyboard = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        private static void MainForKeyboard()
        {
            // Initialize DirectInput
            var directInput = new DirectInput();

            // Instantiate the joystick
            keyboard = new Keyboard(directInput);

            // Acquire the joystick
            keyboard.Properties.BufferSize = 128;
            keyboard.Acquire();
        }
    }
}