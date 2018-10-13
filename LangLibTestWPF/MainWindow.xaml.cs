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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using VPKSoft.LangLib;

namespace LangLibTestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : VPKSoft.LangLib.DBLangEngineWPF
    {
        public MainWindow()
        {
            InitializeComponent();

            DBLangEngine.DBName = "LangLibTestWPF.sqlite";
            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitalizeLanguage("LangLibTestWPF.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more.
            }
            DBLangEngine.InitalizeLanguage("LangLibTestWPF.Messages");
            lbFallbackCulture.Content = DBLangEngine.FallBackCulture.ToString();
            lbCurrentSysCulture.Content = System.Globalization.CultureInfo.CurrentCulture.ToString();
            try
            {
                Title = "LangLibTestWPF - " + ((AssemblyCopyrightAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
            }
            catch
            {

            }
            lbLoadSec.Content = DBLangEngine.InitTime.ToString();
        }

        private void mnuAbout_Click(object sender, RoutedEventArgs e)
        {
            new AboutWindow().ShowDialog();
            lbLoadSec.Content = DBLangEngine.InitTime.ToString();
        }

        private void btMessageTest_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(DBLangEngine.GetMessage("msgTest", "A test message. The last or (|) character in the string {0} splits the message into message and comment part.|A test message (this is the message comment part).", Environment.NewLine));
        }
    }
}
