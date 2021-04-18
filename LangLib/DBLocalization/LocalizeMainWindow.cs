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
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using VPKSoft.LangLib;

namespace VPKSoft.DBLocalization
{
    public partial class LocalizeMainWindow : Form
    {
        SQLiteConnection conn = null;
        public LocalizeMainWindow(string databaseFile)
        {
            InitializeComponent();
            mnuSelectCurrentCulture.Text = "Select current culture (" + CultureInfo.CurrentCulture.ToString() + ")";

            OpenDatabase(databaseFile);
        }

        Culture culture;
        private string currentFile = string.Empty;

        private void OpenDatabase(string fileName)
        {
            CloseDBConnection();
            conn = new SQLiteConnection("Data Source=" + fileName + ";Pooling=true;FailIfMissing=false");
            Text = "DBLangVersion [" + fileName + "]";
            mnuSave.Enabled = true;
            mnuAddFromCulture.Enabled = true;
            mnuRemoveUnused.Enabled = true;
            mnuExportDatabase.Enabled = true;
            currentFile = fileName;
            cbCulture.Enabled = true;
            conn.Open();
            culture = new Culture(ref conn);
            mnuAddFomEN_US.Enabled = false;
            mnuSelectCurrentCulture.Enabled = true;

            cbCulture.Items.AddRange(culture.Cultures.ToArray());
            for (int i = 0; i < cbCulture.Items.Count; i++)
            {
                if ((cbCulture.Items[i] as Culture).LCID == 1033)
                {
                    cbCulture.SelectedIndex = i;
                }
                cbCulture.AutoCompleteCustomSource.Add((cbCulture.Items[i] as Culture).NativeName);
            }

            mnuSelectSomeCulture.Enabled = true;
            ListCulturesMenu();
        }


        private void mnuLoadDB_Click(object sender, EventArgs e)
        {
            if (odSQLite.ShowDialog() == DialogResult.OK)
            {
                OpenDatabase(odSQLite.FileName);
            }
        }

        private void ListCulturesMenu()
        {
            mnuSelectSomeCulture.DropDownItems.Clear();
            using (SQLiteCommand command = new SQLiteCommand(conn))
            {
                command.CommandText = "SELECT DISTINCT CULTURE FROM FORMITEMS ORDER BY CULTURE ";
                using (SQLiteDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem(dr.GetString(0)) { Tag = CultureInfo.GetCultureInfo(dr.GetString(0)), Checked = (cbCulture.SelectedItem as Culture).CultureText == dr.GetString(0) };
                        item.Click += SelectSomeCultureClick;
                        mnuSelectSomeCulture.DropDownItems.Add(item);
                    }
                }
            }
        }

        private void SelectSomeCultureClick(object sender, EventArgs e)
        {
            if (((sender as ToolStripMenuItem).Tag as CultureInfo).Name != (cbCulture.SelectedItem as Culture).CultureText)
            {
                for (int i = 0; i < cbCulture.Items.Count; i++)
                {
                    if ((cbCulture.Items[i] as Culture).LCID == ((sender as ToolStripMenuItem).Tag as CultureInfo).LCID)
                    {
                        cbCulture.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private bool CurrentCultureLoaded()
        {
            return (cbCulture.SelectedItem as Culture).LCID == CultureInfo.CurrentCulture.LCID;
        }

        private bool EnUSCultureLoaded()
        {
            return (cbCulture.SelectedItem as Culture).LCID == 1033;
        }

        private void LoadDB(string culture = "")
        {
            gvFormItems.Rows.Clear();
            gvMessages.Rows.Clear();
            if (culture == string.Empty)
            {
                culture = (cbCulture.Items[cbCulture.SelectedIndex] as Culture).CultureText;
            }

            string culture2 = (cbCulture.Items[cbCulture.SelectedIndex] as Culture).CultureText;

            using (SQLiteCommand command = new SQLiteCommand(conn))
            {
                command.CommandText = "SELECT APP_FORM, ITEM, CULTURE, PROPERTYNAME, VALUETYPE, VALUE, IFNULL(INUSE, 0) AS INUSE " +
                                      "FROM FORMITEMS " +
                                      "WHERE CULTURE = " + DbUtils.MkStr(culture) + " " +
                                      "ORDER BY APP_FORM, ITEM";
                using (SQLiteDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string split1, split2;
                        try
                        {
                            string[] split = dr.GetString(0).Split('.');
                            split1 = split[0];
                            split2 = split[1];
                        }
                        catch
                        {
                            split1 = dr.GetString(0);
                            split2 = string.Empty;
                        }
                        gvFormItems.Rows.Add(split1,
                                             split2,
                                             dr.GetString(1),
                                             culture2,
                                             dr.GetString(3),
                                             dr.GetString(5),
                                             dr.GetString(4),
                                             dr.GetInt32(6) == 1);
                    }
                }
            }

            using (SQLiteCommand command = new SQLiteCommand(conn))
            {
                command.CommandText = "SELECT MESSAGENAME, VALUE, COMMENT_EN_US, CULTURE, IFNULL(INUSE, 0) AS INUSE " +
                                      "FROM MESSAGES " +
                                      "WHERE CULTURE = " + DbUtils.MkStr(culture) + " " +
                                      "ORDER BY MESSAGENAME, VALUE ";

                using (SQLiteDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        gvMessages.Rows.Add(dr.GetString(0),
                                            dr.GetString(1),
                                            dr.GetString(2),
                                            culture2,
                                            dr.GetInt32(4) == 1);
                    }
                }
            }
            mnuAddFomEN_US.Enabled = !EnUSCultureLoaded();
            mnuSelectCurrentCulture.Enabled = !CurrentCultureLoaded();
        }

        private void cbCulture_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDB();
            ListCulturesMenu();
        }

        private void SaveIfNotExist()
        {
            string sql = string.Empty;
            for (int i = 0; i < gvFormItems.Rows.Count; i++)
            {
                sql += string.Format("INSERT INTO FORMITEMS (APP_FORM, ITEM, CULTURE, PROPERTYNAME, VALUETYPE, VALUE, INUSE) " +
                                     "SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6} " +
                                     "WHERE NOT EXISTS (SELECT 1 FROM FORMITEMS WHERE APP_FORM = {0} AND ITEM = {1} AND " +
                                     "CULTURE = {2} AND PROPERTYNAME = {3}); " +
                                     "UPDATE FORMITEMS SET INUSE = {6} WHERE APP_FORM = {0} AND ITEM = {1} AND " +
                                     "CULTURE = {2} AND PROPERTYNAME = {3}; ",
                                     DbUtils.MkStr(gvFormItems.Rows[i].Cells[colApp.Index].Value.ToString() + "." + gvFormItems.Rows[i].Cells[colForm.Index].Value.ToString()),
                                     DbUtils.MkStr(gvFormItems.Rows[i].Cells[colItem.Index].Value.ToString()),
                                     DbUtils.MkStr(gvFormItems.Rows[i].Cells[colCulture.Index].Value.ToString()),
                                     DbUtils.MkStr(gvFormItems.Rows[i].Cells[colPropertyName.Index].Value.ToString()),
                                     DbUtils.MkStr(gvFormItems.Rows[i].Cells[colValueType.Index].Value.ToString()),
                                     DbUtils.MkStr(gvFormItems.Rows[i].Cells[colValue.Index].Value.ToString()),
                                     (gvFormItems.Rows[i].Cells[colInUse.Index].Value.ToString() == false.ToString() ? "0" : "1"));
            }
            using (SQLiteTransaction trans = conn.BeginTransaction())
            {
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
                trans.Commit();
            }

            sql = string.Empty;

            for (int i = 0; i < gvMessages.Rows.Count; i++)
            {
                sql += string.Format("INSERT INTO MESSAGES(CULTURE, MESSAGENAME, VALUE, COMMENT_EN_US, INUSE) " +
                                     "SELECT {0}, {1}, {2}, {3}, {4} " +
                                     "WHERE NOT EXISTS(SELECT 1 FROM MESSAGES WHERE CULTURE = {0} AND MESSAGENAME = {1}); " +
                                     "UPDATE MESSAGES SET INUSE = {4} WHERE CULTURE = {0} AND MESSAGENAME = {1}; ",
                                     DbUtils.MkStr(gvMessages.Rows[i].Cells[colMessageCulture.Index].Value.ToString()),
                                     DbUtils.MkStr(gvMessages.Rows[i].Cells[colMessageName.Index].Value.ToString()),
                                     DbUtils.MkStr(gvMessages.Rows[i].Cells[colMessageValue.Index].Value.ToString()),
                                     DbUtils.MkStr(gvMessages.Rows[i].Cells[colCommentEnUs.Index].Value.ToString()),
                                     (gvMessages.Rows[i].Cells[colMsgInUse.Index].Value.ToString() == false.ToString() ? "0" : "1"));                
            }

            using (SQLiteTransaction trans = conn.BeginTransaction())
            {
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
                trans.Commit();
            }
        }

        private void Save()
        {
            string sql = string.Empty;
            for (int i = 0; i < gvMessages.Rows.Count; i++)
            {
                sql += string.Format("UPDATE MESSAGES SET VALUE = {0} " +
                                     "WHERE CULTURE = {1} AND MESSAGENAME = {2}; ",
                                     DbUtils.MkStr(gvMessages.Rows[i].Cells[colMessageValue.Index].Value.ToString()),
                                     DbUtils.MkStr(gvMessages.Rows[i].Cells[colMessageCulture.Index].Value.ToString()),
                                     DbUtils.MkStr(gvMessages.Rows[i].Cells[colMessageName.Index].Value.ToString()));

            }

            using (SQLiteTransaction trans = conn.BeginTransaction())
            {
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
                trans.Commit();
            }

            sql = string.Empty;
            for (int i = 0; i < gvFormItems.Rows.Count; i++)
            {
                sql += string.Format("UPDATE FORMITEMS SET VALUE = {0} " +
                                     "WHERE CULTURE = {1} AND APP_FORM = {2} AND PROPERTYNAME = {3} AND ITEM = {4}; ",
                                     DbUtils.MkStr(gvFormItems.Rows[i].Cells[colValue.Index].Value.ToString()),
                                     DbUtils.MkStr(gvFormItems.Rows[i].Cells[colCulture.Index].Value.ToString()),
                                     DbUtils.MkStr(gvFormItems.Rows[i].Cells[colApp.Index].Value.ToString() + "." + gvFormItems.Rows[i].Cells[colForm.Index].Value.ToString()),
                                     DbUtils.MkStr(gvFormItems.Rows[i].Cells[colPropertyName.Index].Value.ToString()),
                                     DbUtils.MkStr(gvFormItems.Rows[i].Cells[colItem.Index].Value.ToString()));
            }
            using (SQLiteTransaction trans = conn.BeginTransaction())
            {
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
                trans.Commit();
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void CloseDBConnection()
        {
            if (conn != null)
            {
                conn.Dispose();
                conn = null;
                Text = "DBLangVersion";
                mnuSave.Enabled = false;
                cbCulture.Enabled = false;
                mnuAddFomEN_US.Enabled = false;
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseDBConnection();
        }

        private void mnuAddFomEN_US_Click(object sender, EventArgs e)
        {
            LoadDB("en-US");
            SaveIfNotExist();
            LoadDB();
        }

        private void mnuRemoveUnused_Click(object sender, EventArgs e)
        {
            string culture = (cbCulture.Items[cbCulture.SelectedIndex] as Culture).CultureText;
            string sql = string.Format("DELETE FROM MESSAGES  WHERE IFNULL(INUSE, 0) = 0 AND CULTURE = {0}; ", DbUtils.MkStr(culture));
            sql += string.Format("DELETE FROM FORMITEMS WHERE IFNULL(INUSE, 0) = 0 AND CULTURE = {0}; ", DbUtils.MkStr(culture));
            using (SQLiteTransaction trans = conn.BeginTransaction())
            {
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
                trans.Commit();
            }
            LoadDB();
        }

        private void gvFormItems_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void gvFormItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string eValue = gvFormItems.Rows[e.RowIndex].Cells[colValue.Index].Value.ToString();
            if (FormEditCell.Execute(ref eValue))
            {
                gvFormItems.Rows[e.RowIndex].Cells[colValue.Index].Value = eValue;
            }
        }

        private void mnuSelectCurrentCulture_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbCulture.Items.Count; i++)
            {
                if ((cbCulture.Items[i] as Culture).LCID == CultureInfo.CurrentCulture.LCID)
                {
                    cbCulture.SelectedIndex = i;
                }
            }
        }

        private void mnuAddFromCulture_Click(object sender, EventArgs e)
        {
            Culture selected;
            if (AddFromCulture.Execute(culture.Cultures, (Culture)cbCulture.SelectedItem, out selected))
            {
                LoadDB(selected.CultureText);
                SaveIfNotExist();
                LoadDB();
            }
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
        }

        private void mnuExportDatabase_Click(object sender, EventArgs e)
        {
            sdSQLite.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            if (sdSQLite.ShowDialog() == DialogResult.OK)
            {
                Save();
                CloseDBConnection();
                File.Copy(currentFile, sdSQLite.FileName, true);
                OpenDatabase(currentFile);
            }
        }
    }
}
