using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System.Collections.Generic;
using AutoCAD;
using System.Runtime.InteropServices;
using System;


[assembly: CommandClass(typeof(AutoRoids.SelectionSetExample))]

namespace AutoRoids
{
    public class SelectionSetExample
    {
        [CommandMethod("ss1")]
        public void ListSelectionSets()
        {
            run();




            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;
            Editor ed = acDoc.Editor;

 

            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock @lock = acDoc.LockDocument())
                {
                    DBDictionary namedObjectsDict = acTrans.GetObject(acDb.NamedObjectsDictionaryId, OpenMode.ForRead) as DBDictionary;

                    RegAppTable regAppTable = acTrans.GetObject(acDb.RegAppTableId, OpenMode.ForRead) as RegAppTable;

                    List<string> lstValue = new List<string>();

                    foreach (DBDictionaryEntry entry in namedObjectsDict)
                    {
                        lstValue.Add(entry.Key + " " + entry.Value.ToString());
                        ObjectId namedObjectId = (ObjectId)entry.Value;


                        // Open the object by its ObjectId
                        DBObject obj = namedObjectId.GetObject(OpenMode.ForRead);

                        // Check if the object is a RegAppTableRecord
                        if (obj is RegAppTableRecord)
                        {
                            RegAppTableRecord regAppRecord = (RegAppTableRecord)obj;

                            // Check if the RegAppTableRecord is a named selection set
                            if (regAppTable.Has(regAppRecord.Name))
                            {
                                // Print the name of the selection set
                                ed.WriteMessage($"Selection Set Name: {regAppRecord.Name}\n");
                            }
                        }

                    }




                }

                acTrans.Commit();
            }
            Document doc = Application.DocumentManager.MdiActiveDocument;
            //Editor ed = doc.Editor;
            //Database db = doc.Database;

            //// Get the named objects dictionary
            //DBDictionary namedObjectsDict = db.NamedObjectsDictionaryId.GetObject(OpenMode.ForRead) as DBDictionary;

            //if (namedObjectsDict == null)
            //{
            //    ed.WriteMessage("No named objects found.\n");
            //    return;
            //}

            //// Get the RegAppTable
            //RegAppTable regAppTable = (RegAppTable)db.RegAppTableId.GetObject(OpenMode.ForRead);

            //// Iterate through the named objects dictionary
            //foreach (DBDictionaryEntry entry in namedObjectsDict)
            //{

            //    ObjectId namedObjectId = (ObjectId)entry.Value;

            //    // Open the object by its ObjectId
            //    DBObject obj = namedObjectId.GetObject(OpenMode.ForRead);

            //    // Check if the object is a RegAppTableRecord
            //    if (obj is RegAppTableRecord)
            //    {
            //        RegAppTableRecord regAppRecord = (RegAppTableRecord)obj;

            //        // Check if the RegAppTableRecord is a named selection set
            //        if (regAppTable.Has(regAppRecord.Name))
            //        {
            //            // Print the name of the selection set
            //            ed.WriteMessage($"Selection Set Name: {regAppRecord.Name}\n");
            //        }
            //    }

            //}
        }
        [CommandMethod("MergeSelectionSets")]
        public void MergeSelectionSets()
        {
            // Get the current document editor
            Editor acDocEd = Application.DocumentManager.MdiActiveDocument.Editor;

            // Request for objects to be selected in the drawing area
            PromptSelectionResult acSSPrompt;
            acSSPrompt = acDocEd.GetSelection();

            SelectionSet acSSet1;
            ObjectIdCollection acObjIdColl = new ObjectIdCollection();

            // If the prompt status is OK, objects were selected
            if (acSSPrompt.Status == PromptStatus.OK)
            {
                // Get the selected objects
                acSSet1 = acSSPrompt.Value;

                // Append the selected objects to the ObjectIdCollection
                acObjIdColl = new ObjectIdCollection(acSSet1.GetObjectIds());
            }

            // Request for objects to be selected in the drawing area
            acSSPrompt = acDocEd.GetSelection();

            SelectionSet acSSet2;

            // If the prompt status is OK, objects were selected
            if (acSSPrompt.Status == PromptStatus.OK)
            {
                acSSet2 = acSSPrompt.Value;

                // Check the size of the ObjectIdCollection, if zero, then initialize it
                if (acObjIdColl.Count == 0)
                {
                    acObjIdColl = new ObjectIdCollection(acSSet2.GetObjectIds());
                }
                else
                {
                    // Step through the second selection set
                    foreach (ObjectId acObjId in acSSet2.GetObjectIds())
                    {
                        // Add each object id to the ObjectIdCollection
                        acObjIdColl.Add(acObjId);
                    }
                }
            }

            Application.ShowAlertDialog("Number of objects selected: " +
                                        acObjIdColl.Count.ToString());
        }


        [CommandMethod("SelectObjectsByCrossingWindow")]
        public void SelectObjectsByCrossingWindow()
        {
            // Get the current document editor
            Editor acDocEd = Application.DocumentManager.MdiActiveDocument.Editor;

            // Create a crossing window from (2,2,0) to (10,8,0)
            PromptSelectionResult acSSPrompt;
            acSSPrompt = acDocEd.SelectCrossingWindow(new Point3d(2, 2, 0),
                                                        new Point3d(10, 8, 0));

            // If the prompt status is OK, objects were selected
            if (acSSPrompt.Status == PromptStatus.OK)
            {
                SelectionSet acSSet = acSSPrompt.Value;



                Application.ShowAlertDialog("Number of objects selected: " +
                                            acSSet.Count.ToString());
            }
            else
            {
                Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }

        public void run()
        {
            AcadApplication acadApp = (AcadApplication)Marshal.GetActiveObject("AutoCAD.Application");

            // Start AutoCAD

            acadApp.Visible = true; // Optional, to make AutoCAD visible

            // Get the active document
            AcadDocument doc = acadApp.ActiveDocument;

            // Create a new selection set
            AcadSelectionSets selectionSets = doc.SelectionSets;
            AcadSelectionSet selectionSet = selectionSets.Add("MySelectionSet");

            // Add entities to the selection set (replace "MyEntityName" with the name of the entity)
            selectionSet.Select(AcSelect.acSelectionSetAll, null, null, null);

            // Example: Add a circle to the selection set
            selectionSet.Select(AcSelect.acSelectionSetWindowPolygon, new double[] { 0, 0, 0 }, new double[] { 5, 5, 0 }, null);

            // Perform operations on the selection set
            // For example, count the number of entities in the selection set
            int count = selectionSet.Count;

            // Release COM objects
            System.Runtime.InteropServices.Marshal.ReleaseComObject(selectionSet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(selectionSets);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(acadApp);
        }

 

    }
}

