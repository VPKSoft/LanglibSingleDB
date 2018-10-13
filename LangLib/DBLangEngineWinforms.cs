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

namespace VPKSoft.LangLib
{
    /// <summary>
    /// Initializes a Windows Forms application wrapper.
    /// <para/>System.Windows.Form should inherit from this.
    /// </summary>
    public class DBLangEngineWinforms : System.Windows.Forms.Form
    {
        /// <summary>
        /// The actual localization engine (DBLangEngine) for
        /// <para/>wrapper class.
        /// </summary>
        public DBLangEngine DBLangEngine;

        /// <summary>
        /// The constructor. DBLangEngine is initialized.
        /// </summary>
        public DBLangEngineWinforms()
        {
            DBLangEngine = new DBLangEngine(this, AppType.Winforms);
        }
    }
}