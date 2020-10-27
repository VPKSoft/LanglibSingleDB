#region License
/*
LangLib

A program and library for application localization.
Copyright (C) 2020 VPKSoft, Petteri Kautonen

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
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VPKSoft.LangLib;

namespace LangLibTestWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (Utils.ShouldLocalize() != null) // Localize and exit.
            {
                new MainWindow();
                new AboutWindow();
                System.Diagnostics.Process.GetCurrentProcess().Kill(); // self kill after localization
                return;
            }
        }
    }
}
