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
using System.Globalization;
using System.Diagnostics;
using System.IO;

namespace VPKSoft.LangLib
{
    /// <summary>
    /// Some utilities used in the LangLib
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Checks if a program was given a command line parameter
        /// <para/>"--dbLang" to notify that the program should localize
        /// <para/>it self.
        /// </summary>
        /// <returns>A CultureInfo if the program was given the command 
        /// <para/>line parameter "--dbLang", otherwise string.</returns>
        public static CultureInfo ShouldLocalize()
        {
            foreach (string arg in Environment.GetCommandLineArgs())
            {
                if (arg.ToUpper().StartsWith("--dbLang".ToUpper()))
                {
                    try
                    {
                        string cultureName = arg.Split('=')[1];
                        return CultureInfo.GetCultureInfo(cultureName);
                    }
                    catch
                    {
                        return CultureInfo.GetCultureInfo(1033);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Returns a value of the SQLite database file name if the application was started with a "--localize=file.sqlite" command line argument.
        /// </summary>
        /// <returns>A SQLite database file name if the application was started with a "--localize=file.sqlite" command line argument and the file exists; otherwise string.Empty.</returns>
        public static string ShouldRunLocalizationProgram()
        {
            foreach (string arg in Environment.GetCommandLineArgs())
            {
                if (arg.StartsWith("--localize="))
                {
                    try
                    {
                        // try to check if a SQLite database file exists with the given "--localize=file.sqlite" command line argument..
                        string localizationDatabaseName = arg.Split('=')[1];
                        if (File.Exists(localizationDatabaseName))
                        {
                            // the file exists, so return it..
                            return localizationDatabaseName;
                        }
                        else
                        {
                            // the file doesn't exist, so return string.Empty..
                            return string.Empty;
                        }
                    }
                    catch
                    {
                        // on an exception return string.Empty..
                        return string.Empty;
                    }
                }
            }
            // nothing was found in the command line arguments so return string.Empty..
            return string.Empty;
        }

        /// <summary>
        /// Returns a localization process to localize the current running application.
        /// </summary>
        /// <param name="applicationPath">The path where the application is located.</param>
        /// <returns>A Process class instance if a process can be created with the "--localize=file.sqlite" command line argument and the given <paramref name="applicationPath"/>; otherwise null.</returns>
        public static Process CreateDBLocalizeProcess(string applicationPath)
        {
            try
            {
                // create a path for the DBLocalization application..
                string dbLocalizationExecutable = Path.Combine(applicationPath, "DBLocalization.exe");

                // get the path for the language database..
                string dbLocalizationDatabase = ShouldRunLocalizationProgram();

                // if the both files exists..
                if (File.Exists(dbLocalizationExecutable) && File.Exists(dbLocalizationDatabase))
                {
                    // create a process..
                    Process process = new Process();
                    process.StartInfo = new ProcessStartInfo(dbLocalizationExecutable, "\"" + dbLocalizationDatabase + "\"");
                    return process; // ..and return it..
                }
                else
                {
                    // otherwise return null..
                    return null;
                }
            }
            catch
            {
                // on exception return null..
                return null;
            }
        }
    }
}
