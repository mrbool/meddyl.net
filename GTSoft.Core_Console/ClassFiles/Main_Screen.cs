using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
using GTSoft.CoreDotNet;


namespace GTSoft.Core_Console
{
    public partial class Main_Screen : Form
    {
        public Main_Screen()
        {
            InitializeComponent();
        }



        #region Form Methods

        private void Main_Screen_Load(object sender, EventArgs e)
        {
            try
            {
                Load_Form_Data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string input = "", encrypt = "";

                byte[] desKey = new ASCIIEncoding().GetBytes("k&hH%g4B");
                byte[] desIV = new ASCIIEncoding().GetBytes("k&hH%g4B");

                input = this.txtInput.Text;

                GTSoft.CoreDotNet.Security cryptor = new GTSoft.CoreDotNet.Security();
                encrypt = cryptor.DESEncryptString(input, desKey, desIV);

                this.txtEncrypt.Text = encrypt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string encrypt = "", decrypt = "";

                byte[] desKey = new ASCIIEncoding().GetBytes("k&hH%g4B");
                byte[] desIV = new ASCIIEncoding().GetBytes("k&hH%g4B");

                encrypt = this.txtEncrypt.Text;

                GTSoft.CoreDotNet.Security cryptor = new GTSoft.CoreDotNet.Security();
                decrypt = cryptor.DESDecryptString(encrypt, desKey, desIV);

                this.txtDecrypt.Text = decrypt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lv1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Load_Data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Load_DAL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gvConnections_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string connection_name, status;

                connection_name = this.gvConnections.Rows[this.gvConnections.CurrentRow.Index].Cells[1].Value.ToString();

                GTSoft.CoreDotNet.Database.SQLServer_Connector sql_connect = new GTSoft.CoreDotNet.Database.SQLServer_Connector(connection_name);
                sql_connect.DBConnection.Open();

                status = "Connection is " + sql_connect.DBConnection.State.ToString();

                MessageBox.Show(status);
            }
            catch
            {
                MessageBox.Show("Connection is no good!");
            }
        }

        private void btnCreateXML_Click(object sender, EventArgs e)
        {
            try
            {
                //XML_Create_File();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                //XML_Insert();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //XML_Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //XML_Delete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnCreateDAL_Click(object sender, EventArgs e)
        {
            try
            {
                Create_DAL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCreateRESTFiles_Click(object sender, EventArgs e)
        {
            try
            {
                Create_REST_Files();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        #endregion



        #region Private Methods

        private void Load_Form_Data()
        {
            try
            {
                this.txtRoot.Text = "CE_Migration";
                this.txtRootSub.Text = "Migrations";

                Load_Connections();
                Load_CreateDAL();
                Load_REST_Screen();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_CreateDAL()
        {
            try
            {
                string folder;

                //this.cbServerType.SelectedItem = "SQL Server";
                //this.txtServer.Text = @"(local)";
                //this.txtDatabase.Text = "House_Tosser";
                //this.txtUserID.Text = "sa";
                //this.txtPassword.Text = "nyjets";
                //this.txtNamespace.Text = "HouseTosser.DAL.LNMS";
                //this.txtXMLDataSource.Text = "LNMS";
                //this.txtStoredProcPrefix.Text = "sp_";

                this.cbServerType.SelectedItem = "SQL Server";
                this.txtServer.Text = @"DESKTOP-5G3R4HI";
                this.txtDatabase.Text = "meddyl";
                this.txtUserID.Text = "sa";
                this.txtPassword.Text = "Blacksb3ach";
                this.txtNamespace.Text = "GTSoft.Meddyl";
                this.txtXMLDataSource.Text = "Meddyl";
                this.txtStoredProcPrefix.Text = "sp_";

                System.Reflection.Module[] modules = System.Reflection.Assembly.GetExecutingAssembly().GetModules();
                folder = System.IO.Path.GetDirectoryName(modules[0].FullyQualifiedName) + "\\Files";
                this.txtDirectory.Text = folder;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void Load_REST_Screen()
        {
            try
            {
                string rest_folder, output_folder;

                System.Reflection.Module[] modules = System.Reflection.Assembly.GetExecutingAssembly().GetModules();
                rest_folder = System.IO.Path.GetDirectoryName(modules[0].FullyQualifiedName) + "\\Files";
                rest_folder = rest_folder.Substring(0,rest_folder.IndexOf("GTSoft")) + "GTSoft.Meddyl.API";

                output_folder = System.IO.Path.GetDirectoryName(modules[0].FullyQualifiedName) + "\\REST_Files";

                this.txtRESTApp.Text = rest_folder;
                this.txtRESTOutput.Text = output_folder;
                this.txtRESTPackageName.Text = "com.gtsoft.meddyl.customer";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Connections()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string system_folder, path, connection_name;

                GTSoft.CoreDotNet.Utilities utilities = new GTSoft.CoreDotNet.Utilities();
                system_folder = utilities.Get_Application_Directory("System");
                path = system_folder + @"\\connections.xml";

                dt.Columns.Add("connection_name");

                FileStream reader = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                System.Xml.XmlDocument xml_connection = new System.Xml.XmlDocument();
                xml_connection.Load(reader);

                XmlNodeList node_list = xml_connection.GetElementsByTagName("connection");
                for (int i = 0; i < node_list.Count; i++)
                {
                    DataRow dr = dt.NewRow();

                    connection_name = node_list[i].ChildNodes[0].InnerText;

                    dr["connection_name"] = connection_name;
                    dt.Rows.Add(dr);
                }

                this.gvConnections.DataSource = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Create_DAL()
        {
            try
            {
                //this.button_create.Enabled = false;

                string database;

                database = this.txtDatabase.Text;

                CreateDAL dal_creator = new CreateDAL();
                dal_creator.server_type = this.cbServerType.SelectedItem.ToString();
                dal_creator.server = this.txtServer.Text;
                dal_creator.database = database;
                dal_creator.user_name = this.txtUserID.Text;
                dal_creator.password = this.txtPassword.Text;
                dal_creator.namespace_used = this.txtNamespace.Text;
                dal_creator.xml_data_source = this.txtXMLDataSource.Text;
                dal_creator.folder = this.txtDirectory.Text;
                dal_creator.stored_proc_prefix = this.txtStoredProcPrefix.Text;
                dal_creator.Process();

                MessageBox.Show("DAL has been built for database " + database, "DAL Builder", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.btnCreateDAL.Enabled = true;

                System.Diagnostics.Process.Start(this.txtDirectory.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region XML

        /*
        private void XML_Create_File()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataRow dr = dt.NewRow();
                string column_name, value;

                ds.DataSetName = this.txtRoot.Text;
                dt.TableName = this.txtRootSub.Text;

                // Create columns
                for (int i = 0; i < this.dgvColumns.Rows.Count - 1; i++)
                {
                    column_name = this.dgvColumns.Rows[i].Cells[0].Value.ToString();
                    value = this.dgvColumns.Rows[i].Cells[1].Value.ToString();

                    dt.Columns.Add(column_name);
                    dr[column_name] = value;
                }
                dt.Rows.Add(dr);

                GTSoft.CoreDotNet.XML xml = new GTSoft.CoreDotNet.XML();
                xml.xml_file = @"c:\development\xml_test.xml";
                xml.Create_File(dt, this.txtRoot.Text);

                MessageBox.Show("XML file created");

                XML_Load();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void XML_Update()
        {
            try
            {
                CoreDotNet.XML xml = new CoreDotNet.XML();
                xml.xml_file = @"c:\development\xml_test.xml";
                xml.Update_Entire_Column(this.txtXMLColumn.Text, this.txtXMLValue.Text);

                XML_Load();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void XML_Insert()
        {
            try
            {
                DataTable dt = new DataTable();

                dt = (DataTable)this.dgvXMLModify.DataSource;

                CoreDotNet.XML xml = new CoreDotNet.XML();
                xml.xml_file = @"c:\development\xml_test.xml";
                xml.Insert(dt);

                XML_Load();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void XML_Delete()
        {
            try
            {
                DataTable dt = new DataTable();

                dt = (DataTable)this.dgvXMLModify.DataSource;

                GTSoft.CoreDotNet.XML xml = new CoreDotNet.XML();
                xml.xml_file = @"c:\development\xml_test.xml";
                xml.Delete(dt);

                XML_Load();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void XML_Load()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt;
                int count;
                ds.ReadXml(@"c:\development\xml_test.xml", XmlReadMode.Auto);

                this.dgvXMLSchema.DataSource = ds.Tables[0];

                dt = ds.Tables[0].Copy();
                count = dt.Rows.Count-1;
                for (int i = 0; i <= count; count--)
                {
                    dt.Rows[count].Delete();
                }
                dt.AcceptChanges();

                this.dgvXMLModify.DataSource = dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        */

        #endregion

        #region DAL

        private void Load_DAL()
        {
            try
            {
                string dal, class_name = "";
                Assembly asy = null;
                int i = 0;

                lv1.Clear();
                dal = comboBox1.SelectedItem.ToString();

                ColumnHeader ch1 = new ColumnHeader();
                ch1.Text = "Table Name";
                ch1.TextAlign = HorizontalAlignment.Left;
                ch1.Width = 210;
              
                lv1.Columns.Add(ch1);

                asy = Assembly.Load("LexisNexis.DAL." + dal);

                Type[] types = asy.GetTypes();
                lv1.BeginUpdate();
                foreach (Type t in types)
                {
                    if (t.ToString().IndexOf("Base_") > 0)
                    {
                        i++;
                        class_name = t.ToString().Substring(t.ToString().IndexOf("Base_") + 5);

                        ListViewItem li = new ListViewItem(class_name);
                        //li.Group = lv1.Groups[class_name];
                        li.SubItems.Add(class_name);

                        li.Tag = i;
                        lv1.Items.Add(li);
                    }
                }
                lv1.EndUpdate();

                lv1.Sorting = System.Windows.Forms.SortOrder.Ascending;


                this.lblObjectCount.Text = i.ToString() + "  Objects";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Load_Data()
        {
            try
            {
                int i;
                string class_object;
                DataTable dt = null;

                i = lv1.SelectedIndices[0];

                class_object = lv1.Items[i].Text;

                #region LNMS

                #endregion



                dataGridView1.DataSource = dt;
                this.lblRowCount.Text = dt.Rows.Count + "  Rows";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private void tabCreateDAL_Click(object sender, EventArgs e)
        {

        }


        private void Create_REST_Files()
        {
            try
            {

                Create_REST_Files rest_creator = new Core_Console.Create_REST_Files();
                rest_creator.output_folder = this.txtRESTOutput.Text;
                rest_creator.rest_folder = this.txtRESTApp.Text;
                rest_creator.package_name = this.txtRESTPackageName.Text;
                rest_creator.Process();

                MessageBox.Show("REST layer has been built", "REST Builder", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.btnCreateDAL.Enabled = true;

                System.Diagnostics.Process.Start(this.txtRESTOutput.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }




    }
}