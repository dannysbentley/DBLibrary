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
    public class LibraryGeometry
    {
        ///  Author:   Danny Bentley
        ///  Date  :   02/15/2017
        ///  Objective : My Revit Library. Reusable Code. 
        ///  Geometry and simple math formulas. 

        public const double _eps = 1.0e-9;
        /// <summary>
        /// Find the midpoint between two points. 
        /// </summary>
        /// <param name="p">XYZ</param>
        /// <param name="q">XYZ</param>
        /// <returns>XYZ midpoint</returns>
        public XYZ Midpoint(XYZ p, XYZ q)
        {
            return 0.5 * (p + q);
        }

        /// <summary>
        /// Find the midpoint between two lines. 
        /// </summary>
        /// <param name="line">Line</param>
        /// <param name="line">Line</param>
        /// <returns>XYZ midpoint</returns>
        public XYZ Midpoint(Line line)
        {
            return Midpoint(line.GetEndPoint(0),
              line.GetEndPoint(1));
        }

        /// <summary>
        /// Test if value is zero. With tolerance  
        /// </summary>
        /// <param name="a">double</param>
        /// <param name="tolerance">double</param>
        /// <returns>bool</returns>
        public bool IsZero(double a, double tolerance)
        {
            return tolerance > Math.Abs(a);
        }

        /// <summary>
        /// Test if value is zero.  
        /// </summary>
        /// <param name="a">double</param>
        /// <returns>bool</returns>
        public bool IsZero(double a)
        {
            return IsZero(a, _eps);
        }

        /// <summary>
        /// Chech if two double values are equal.   
        /// </summary>
        /// <param name="a">double</param>
        /// <param name="b">double</param>
        /// <returns>bool</returns>
        public bool IsEqual(double a, double b)
        {
            return IsZero(b - a);
        }

        /// <summary>
        /// compare two double values.    
        /// </summary>
        /// <param name="a">double</param>
        /// <param name="b">double</param>
        /// <returns>int</returns>
        public int Compare(double a, double b)
        {
            return IsEqual(a, b) ? 0 : (a < b ? -1 : 1);
        }

        /// <summary>
        /// compare two XYZ values.    
        /// </summary>
        /// <param name="p">XYZ</param>
        /// <param name="q">XYZ</param>
        /// <returns>int</returns>
        public int Compare(XYZ p, XYZ q)
        {
            int d = Compare(p.X, q.X);

            if (0 == d)
            {
                d = Compare(p.Y, q.Y);

                if (0 == d)
                {
                    d = Compare(p.Z, q.Z);
                }
            }
            return d;
        }

        /// <summary>
        /// Check if point is inside a bounding box.    
        /// </summary>
        /// <param name="bb">BoundingBoxXYZ</param>
        /// <param name="p">XYZ</param>
        /// <returns>bool</returns>
        public bool BoundingBoxXyzContains(BoundingBoxXYZ bb, XYZ p)
        {
            return 0 < Compare(bb.Min, p)
              && 0 < Compare(p, bb.Max);
        }

    }
}
