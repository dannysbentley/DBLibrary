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
    public class LibraryConvertUnits
    {
        ///  Author:   Danny Bentley
        ///  Date  :   09/23/2016
        ///  Objective : My Revit Library. Reusable Code. 
        ///  
        #region CONVERSION AND UNITS.

        /// <summary>
        /// Convert inches to feet
        /// </summary>
        /// <param name="inches">double</param>
        /// <returns>double</returns>
        public double InchesToFeet(double inches)
        {
            double feet = 0;
            feet = inches / 12;
            return feet;
        }

        const double _convertFootToMm = 12 * 25.4;

        /// <summary>
        /// Convert Feet to MM
        /// </summary>
        /// <param name="length">double</param>
        /// <returns>double</returns>
        public double FootToMm(double length)
        {
            return length * _convertFootToMm;
        }

        /// <summary>
        /// Convert MM to Feet
        /// </summary>
        /// <param name="length">double</param>
        /// <returns>double</returns>
        public double MmToFoot(double length)
        {
            return length / _convertFootToMm;
        }
        #endregion
    }
}
