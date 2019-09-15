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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using VPKSoft.LangLib;

namespace LangLibTestWinforms
{
    public partial class FormMain : DBLangEngineWinforms
    {
        public FormMain()
        {
            InitializeComponent();
            if (DesignMode)
            {
                return;
            }
            DBLangEngine.DBName = "LangLibTestWinforms.sqlite";
            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitializeLanguage("LangLibTestWinforms.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more.
            }


            DBLangEngine.InitializeLanguage("LangLibTestWinforms.Messages");

            lbFallbackCulture.Text = DBLangEngine.FallBackCulture.ToString();
            lbCurrentSysCulture.Text = System.Globalization.CultureInfo.CurrentCulture.ToString();
            try
            {
                Text = "LangLibTestWinforms - " + ((AssemblyCopyrightAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
            }
            catch
            {

            }
            lbLoadSec.Text = DBLangEngine.InitTime.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
            lbLoadSec.Text = DBLangEngine.InitTime.ToString();
        }

        private void btMessageTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DBLangEngine.GetMessage("msgTest", "A test message. The last or (|) character in the string {0} splits the message into message and comment part.|A test message (this is the message comment part).", Environment.NewLine));
        }
    }
}
