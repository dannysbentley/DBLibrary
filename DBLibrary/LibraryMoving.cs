#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;
#endregion

namespace DBLibrary
{
    public class LibraryMoving
    {
        ///  Author:   Danny Bentley
        ///  Date  :   09/23/2016
        ///  Objective : My Revit Library. Reusable Code. 
        
        #region MOVING ELEMENTS IN REVIT

        public bool MoveUsingLocationCurve(Wall wall)
        {
            LocationCurve wallLine = wall.Location as LocationCurve;
            XYZ translationVec = new XYZ(10, 20, 0);
            return (wallLine.Move(translationVec));
        }

        public void MoveUsingCurveParam(Document doc, Wall wall, Line newWallLine)
        {
            LocationCurve wallLine = wall.Location as LocationCurve;

            // Change the wall line to a new line.
            Transaction t = new Transaction(doc);
            t.Start("Move");
            wallLine.Curve = newWallLine;
            t.Commit();
        }

        public void LocationMove(FamilyInstance familyInstance)
        {
            LocationPoint columnPoint = familyInstance.Location as LocationPoint;
            if (null != columnPoint)
            {
                XYZ newLocation = new XYZ(10, 20, 0);
                // Move the column to the new location
                columnPoint.Point = newLocation;
            }
        }

        public void MoveColumn(Document document, FamilyInstance familyInstance)
        {
            // get the column current location
            LocationPoint columnLocation = familyInstance.Location as LocationPoint;

            XYZ oldPlace = columnLocation.Point;

            // Move the column to new location.
            XYZ newPlace = new XYZ(10, 20, 30);
            ElementTransformUtils.MoveElement(document, familyInstance.Id, newPlace);

            // now get the column's new location
            columnLocation = familyInstance.Location as LocationPoint;
            XYZ newActual = columnLocation.Point;

            string info = "Original Z location: " + oldPlace.Z +
                            "\nNew Z location: " + newActual.Z;

            TaskDialog.Show("Revit", info);
        }
        #endregion 
    }
}