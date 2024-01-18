using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AutoRoids
{
    internal class clsReg
    {
        internal static string strBase = "SOFTWARE";
        internal static string strGPKey = "AutoRoids";
        internal static string strGPProgramName = "AutoRoids";

        internal String GetAutoRoids()
        {
            return getGuid("AutoRoidsPalette");
        }

        internal void SetIdleDelay(string strValue)
        {
            SaveData("IdleDelay", strValue);
        }

        internal string GetIdleDelay(out int intIdleDelay)
        {
            string strValue = GetData("IdleDelay");

            if (int.TryParse(strValue, out intIdleDelay))
                return strValue;
            else
            {
                intIdleDelay = 0;
                return "0";
            }
        }

        internal void SetGameScale(string strValue)
        {
            SaveData("GameScale", strValue);
        }

        internal string GetGameScale(out double dblSpeed)
        {
            string strValue = GetData("GameScale");

            if (double.TryParse(strValue, out dblSpeed))
                return strValue;
            else
            {
                dblSpeed = 1;
                return "1";
            }
        }

        internal void SetShipRotation(string strValue)
        {
            SaveData("ShipRotation", strValue);
        }

        internal string GetShipRotation(out double dblShipRotation)
        {
            string strValue = GetData("ShipRotation");

            if (double.TryParse(strValue, out dblShipRotation))
                return strValue;
            else
            {
                dblShipRotation = 2.8;
                return "2.8";
            }
        }

        internal void SetShipThrust(string strValue)
        {
            SaveData("ShipThrust", strValue);
        }

        internal string GetShipThrust(out double dblShipThrust)
        {
            string strValue = GetData("ShipThrust");

            if (double.TryParse(strValue, out dblShipThrust))
                return strValue;
            else
            {
                dblShipThrust = 16;
                return "16";
            }
        }

        internal void SetRockCount(string strValue)
        {
            SaveData("RockCount", strValue);
        }

        internal string GetRockCount(out int intRockCount)
        {
            string strValue = GetData("RockCount");

            if (int.TryParse(strValue, out intRockCount))
                return strValue;
            else
            {
                intRockCount = 12;
                return "12";
            }
        }

        internal void SetRockAngle(string strValue)
        {
            SaveData("RockAngle", strValue);
        }

        internal string GetRockAngle(out double dblRockDistance)
        {
            string strValue = GetData("RockAngle");

            if (double.TryParse(strValue, out dblRockDistance))
                return strValue;
            else
            {
                dblRockDistance = 0;
                return "0";
            }
        }

        internal void SetRockMinDistance(string strValue)
        {
            SaveData("RockMinDistance", strValue);
        }

        internal string GetRockMinDistance(out double dblRockDistance)
        {
            string strValue = GetData("RockMinDistance");

            if (double.TryParse(strValue, out dblRockDistance))
                return strValue;
            else
            {
                dblRockDistance = 1;
                return "1";
            }
        }

        internal void SetRockMaxDistance(string strValue)
        {
            SaveData("RockMaxDistance", strValue);
        }

        internal string GetRockMaxDistance(out double dblRockDistance)
        {
            string strValue = GetData("RockMaxDistance");

            if (double.TryParse(strValue, out dblRockDistance))
                return strValue;
            else
            {
                dblRockDistance = 3;
                return "3";
            }
        }

        internal void SetRockMinRotation(string strValue)
        {
            SaveData("RockMinRotation", strValue);
        }

        internal string GetRockMinRotation(out double dblRockRotation)
        {
            string strValue = GetData("RockMinRotation");

            if (double.TryParse(strValue, out dblRockRotation))
                return strValue;
            else
            {
                dblRockRotation = 0;
                return "0";
            }
        }

        internal void SetRockMaxRotation(string strValue)
        {
            SaveData("RockMaxRotation", strValue);
        }

        internal string GetRockMaxRotation(out double dblRockRotation)
        {
            string strValue = GetData("RockMaxRotation");

            if (double.TryParse(strValue, out dblRockRotation))
                return strValue;
            else
            {
                dblRockRotation = 1;
                return "1";
            }
        }

        internal void SetBulletDelay(string strValue)
        {
            SaveData("RockBulletDelay", strValue);
        }

        internal string GetBulletDelay(out int dblBulletDelay)
        {
            string strValue = GetData("RockBulletDelay");

            if (int.TryParse(strValue, out dblBulletDelay))
                return strValue;
            else
            {
                dblBulletDelay = 150;
                return "150";
            }
        }

        internal void SetDeathBlossomDelay(string strValue)
        {
            SaveData("RockDeathBlossomDelay", strValue);
        }

        internal string GetDeathBlossomDelay(out int dblBulletDelay)
        {
            string strValue = GetData("RockDeathBlossomDelay");

            if (int.TryParse(strValue, out dblBulletDelay))
                return strValue;
            else
            {
                dblBulletDelay = 50;
                return "50";
            }
        }

        internal void SetZoomToShip(Boolean strValue)
        {
            SaveData("ZoomToShip", strValue.ToString());
        }

        internal Boolean GetZoomToShip()
        {
            string strValue = GetData("ZoomToShip");

            if (strValue.Length == 0 ||
                strValue == "False") return false;
            return true;
        }

        internal void SetShowOverlay(Boolean strValue)
        {
            SaveData("ShowOverlay", strValue.ToString());
        }

        internal Boolean GetShowOverlay()
        {
            string strValue = GetData("ShowOverlay");

            if (strValue.Length == 0 ||
                strValue == "True") return true;
            return false;
        }

        internal void SetShowBlocks(Boolean strValue)
        {
            SaveData("ShowBlocks", strValue.ToString());
        }

        internal Boolean GetShowBlocks()
        {
            string strValue = GetData("ShowBlocks");

            if (strValue.Length == 0 ||
                strValue == "True") return true;
            return false;
        }

        internal void SetShowBoundingBox(Boolean strValue)
        {
            SaveData("ShowBoundingBox", strValue.ToString());
        }

        internal Boolean GetShowBoundingBox()
        {
            string strValue = GetData("ShowBoundingBox");

            if (strValue.Length == 0 ||
                strValue == "True") return true;
            return false;
        }

        #region "Basic Code"

        private bool getGPkey(ref RegistryKey gpKey)
        {
            bool rtnValue = false;
            RegistryKey regBaseKey = null;
            try
            {
                // get the base software key
                regBaseKey = Registry.CurrentUser.OpenSubKey(strBase, true);
                if (getSubKey(ref regBaseKey, strGPKey, ref gpKey))
                {
                    rtnValue = true;
                }
                else
                {
                    rtnValue = false;
                }
            }
            catch (System.Exception)
            {
                rtnValue = false;
            }
            finally
            {
                regBaseKey.Close();
            }

            return rtnValue;
        }

        // Get GUID of palette or create a new one
        private string getGuid(string strKey)
        {
            string strGuid = "";
            try
            {
                RegistryKey gpKey = null;
                // get Default key
                if (getGPkey(ref gpKey))
                {
                    RegistryKey guidKey = null;
                    // Get sub key
                    if (getSubKey(ref gpKey, strKey, ref guidKey))
                    {
                        // Get Guid
                        if (hasValue(ref guidKey, "GUID"))
                        {
                            strGuid = guidKey.GetValue("GUID", "").ToString();
                        }
                        else
                        {
                            // Create new Guid
                            strGuid = "{" + System.Guid.NewGuid().ToString() + "}";
                            guidKey.SetValue("GUID", strGuid);
                        }
                        guidKey.Close();
                    }
                }
                gpKey.Close();
                return strGuid;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error Saving GUID" + "\n" + ex.ToString());
            }
            return "";
        }

        // Check if the Registry Key contains a subkey
        private bool getSubKeyList(ref RegistryKey subkey, ref List<string> lstKeys)
        {
            foreach (string item in subkey.GetSubKeyNames())
            {
                lstKeys.Add(item);
            }

            return true;
        }

        // Check if the Registry Key contains a subkey
        private bool getSubKeyValueList(ref RegistryKey subkey, ref List<string> lstKeys)
        {
            foreach (string item in subkey.GetValueNames())
            {
                if (!string.IsNullOrEmpty(item))
                {
                    lstKeys.Add(item);
                }
            }
            return true;
        }

        // Check if the Registry Key contains a subkey
        private bool hasValue(ref RegistryKey subkey, string value)
        {
            foreach (string item in subkey.GetValueNames())
            {
                if (item.ToString() == value)
                {
                    return true;
                }
            }
            return false;
        }

        //// Delete Key
        //private bool DeleteSubKey(string strKey)
        //{
        //    try
        //    {
        //        RegistryKey gpKey = null;
        //        // get Default key
        //        if (getGPkey(ref gpKey))
        //        {
        //            if (hasSubKey(ref gpKey, strKey))
        //            {
        //                //RegistryKey subkey = null;
        //                // Delete Sub Key
        //                gpKey.DeleteSubKey(strKey, true);
        //            }
        //            gpKey.Close();
        //        }

        //        return true;
        //    }
        //    catch (System.Exception)
        //    {
        //        return false;
        //    }
        //}

        // Delete the name in the SubKey Given
        private bool DeleteSubKeyName(string strKey, string name)
        {
            bool rtnValue = false;
            try
            {
                RegistryKey gpKey = null;
                // get Default key
                if (getGPkey(ref gpKey))
                {
                    if (hasSubKey(ref gpKey, strKey))
                    {
                        RegistryKey subkey = null;
                        // get subkey
                        if (getSubKey(ref gpKey, strKey, ref subkey))
                        {
                            // Delete the name of the Key
                            foreach (string item in subkey.GetSubKeyNames())
                            {
                                if (item == name)
                                {
                                    subkey.DeleteValue(name);
                                    rtnValue = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                            subkey.DeleteValue(name);
                            subkey.Close();
                        }
                    }
                    gpKey.Close();
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            return rtnValue;
        }

        // Check if the Registry Key contains a subkey
        private bool hasSubKey(ref RegistryKey subkey, string value)
        {
            foreach (string item in subkey.GetSubKeyNames())
            {
                if (item.ToString() == value)
                {
                    return true;
                }
            }
            return false;
        }

        // get SubKey and or create a new one and return the new key
        private bool getSubKey(ref RegistryKey key, string strkey, ref RegistryKey rtnKey)
        {
            try
            {
                if (!hasSubKey(ref key, strkey))
                {
                    key.CreateSubKey(strkey);
                    rtnKey = key.OpenSubKey(strkey, true);
                    return true;
                }
                else
                {
                    rtnKey = key.OpenSubKey(strkey, true);
                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        internal void setproject(string Value)
        {
            SaveValue(strGPProgramName, "LSTPROJECT", Value);
        }

        internal string getproject()
        {
            string strValue = GetValue(strGPProgramName, "LSTPROJECT");
            return strValue;
        }

        internal void SaveData(string strName, string Value)
        {
            SaveValue(strGPProgramName, strName, Value);
        }

        internal string GetData(string strName)
        {
            string strValue = GetValue(strGPProgramName, strName);
            return strValue;
        }

        internal void SaveValue(string strKey, string myName, string myData)
        {
            RegistryKey gpKey = null;
            if (getGPkey(ref gpKey))
            {
                RegistryKey subkey = null;
                if (getSubKey(ref gpKey, strKey, ref subkey))
                {
                    subkey.SetValue(myName, myData);
                }
            }
        }

        internal string GetValue(string strKey, string myName)
        {
            RegistryKey gpKey = null;
            if (getGPkey(ref gpKey))
            {
                RegistryKey subkey = null;
                if (getSubKey(ref gpKey, strKey, ref subkey))
                {
                    return (string)subkey.GetValue(myName, "");
                }
            }
            return "";
        }

        internal string getLastProject()
        {
            string strValue = GetValue(strGPProgramName, "LASTPROJECT");
            return strValue;
        }

        internal void setLastProject(string Value)
        {
            SaveValue(strGPProgramName, "LASTPROJECT", Value.ToString());
        }

        internal void SaveProjectDefaults(List<string> lstProjects, List<string> lstDefault)
        {
            string strCombined = "";
            for (int i = 0; i <= lstProjects.Count - 1; i++)
            {
                string strValues = "";
                for (int k = 0; k <= lstDefault.Count - 1; k++)
                {
                    if (lstDefault.Count - 1 == k)
                    {
                        strValues += lstDefault[k];
                    }
                    else
                    {
                        strValues += lstDefault[k] + "|";
                    }
                }
                strCombined += lstProjects[i] + "|" + strValues + "$";
            }
        }

        #endregion "Basic Code"
    }
}