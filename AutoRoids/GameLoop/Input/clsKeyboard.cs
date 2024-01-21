using SharpDX.DirectInput;
using System;
using System.Collections.Generic;

namespace AutoRoids
{
    internal class clsKeyboard
    {
        internal List<enumDirection> GetDirection()
        {
            return GetMovementKeyboard();
        }

        public List<enumDirection> GetMovementKeyboard()
        {
            List<enumDirection> lstDirection = new List<enumDirection>();

            var keyboardState = new KeyboardState();
            try
            {
                clsSharpDX.keyboard.GetCurrentState(ref keyboardState);
            }
            catch (Exception)
            {
                return new List<enumDirection> { };
            }

            if (keyboardState != null)
            {
                if (keyboardState.PressedKeys.Count > 0)
                {
                    List<Key> lstKeys = keyboardState.PressedKeys;

                    if (lstKeys.Count > 0)
                    {
                        for (int i = 0; i < lstKeys.Count; i++)
                        {
                            if (lstKeys[i] == Key.Up) lstDirection.Add(enumDirection.Up);                       
                            if (lstKeys[i] == Key.Right) lstDirection.Add(enumDirection.Right);
                            if (lstKeys[i] == Key.Left) lstDirection.Add(enumDirection.Left);

                            if (lstKeys[i] == Key.LeftControl) lstDirection.Add(enumDirection.Fire);
                            if (lstKeys[i] == Key.RightControl) lstDirection.Add(enumDirection.Fire);

                            if (lstKeys[i] == Key.LeftShift) lstDirection.Add(enumDirection.Shield);
                            if (lstKeys[i] == Key.RightShift) lstDirection.Add(enumDirection.Shield);

                            if (lstKeys[i] == Key.Delete) lstDirection.Add(enumDirection.Hyperspace);
                            if (lstKeys[i] == Key.End) lstDirection.Add(enumDirection.DeathBlossom);                      

                            //if (lstDirection.Contains(enumDirection.DeathBlossom))
                            //    lstDirection.Add(enumDirection.Left);

                            // Debug.Print(lstKeys[i].ToString());
                        }
                    }
                }
            }
            return lstDirection;
        }
    }
}