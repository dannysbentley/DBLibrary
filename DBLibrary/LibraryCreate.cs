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
    
    public class LibraryCreate
    {
        ///  Author:   Danny Bentley
        ///  Date  :   02/16/2017
        ///  Objective : My Revit Library. Reusable Code. 
        ///  Create items.  

        /// <summary>
        /// Create a detail line
        /// </summary>
        /// <param name="doc">Document</param>
        /// <param name="p1">XYZ</param>
        /// <param name="p2">XYZ</param>
        public void CreateDetailLine(Document doc ,XYZ p1, XYZ p2)
        {
            using (Transaction t = new Transaction(doc, "Create detail Line"))
            {
                t.Start("Create Line");
                Line line = Line.CreateBound(p1, p2);
                doc.Create.NewDetailCurve(doc.ActiveView, line);
                t.Commit();
            }
        }
        
        /// <summary>
        /// Create a detail line from curve
        /// </summary>
        /// <param name="doc">Document</param>
        /// <param name="line">Line</param>
        public void CreateDetailLine(Document doc, Line line)
        {
            using (Transaction t = new Transaction(doc, "Create detail Line"))
            {
                t.Start("Create Line");
                doc.Create.NewDetailCurve(doc.ActiveView, line);
                t.Commit();
            }
        }
    }
}
