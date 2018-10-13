#region License
/*
LangLib

A program and library for application localization.
Copyright (C) 2015 VPKSoft, Petteri Kautonen

Contact: vpksoft@vpksoft.net

This file is part of LangLib.

LangLib is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

LangLib is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with LangLib.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPKSoft
{
    namespace LangLib
    {
        /// <summary>
        /// A static class for database utilities.
        /// </summary>
        public static class DBUtils
        {
            /// <summary>
            /// Makes a string a database combatible.
            /// <para/> -Hyphens (') are replaced with double hyphens.
            /// <para/> -The returning string is surrounded with hyphens (').
            /// </summary>
            /// <param name="str">A string to make database compatible.</param>
            /// <returns>A database combatible string.</returns>
            public static string MkStr(string str)
            {
                return "'" + str.Replace("'", "''") + "'";
            }
        }
    }
}