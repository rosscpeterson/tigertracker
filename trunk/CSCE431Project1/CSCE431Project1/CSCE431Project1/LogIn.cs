using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CSCE431Project1
{
    public partial class LogIn : Form
    {
        // Connection variable.
        MySqlConnection conSQL;
        // Data Set used to populate lists.
        DataTable Projs, Users, Links;
        // Permission level to be gotten from table.
        int linkIndex;
        Int32 linkID;

        public static string databaseHost = "69.93.227.36";
        public static string userAuthURL = "http://69.93.227.36/webservices/bugTrackerAuth.php";
        public static string databaseName;
        public static string databaseUser;
        public static string databasePassword;
        public static string connectionString;

        public LogIn(/*MySqlConnection _conSQL*/)
        {
            //conSQL = _conSQL;
            InitializeComponent();
            user.Focus();
            linkIndex = -1;
            linkID = -1;
            // When the enter is pressed login
            this.passwordText.KeyDown += new KeyEventHandler(this.Form2_Load_Keypress);
            this.user.KeyDown += new KeyEventHandler(this.Form2_Load_Keypress);
        }

        private void Form2_Load_Keypress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonLogIn_Click(sender, e);
            }
        }

        //private void PopulateComponents()
        //{
        //    DataSet ProjsUsersLinks = new DataSet();
        //    MySqlDataAdapter adapter = new MySqlDataAdapter();
        //    MySqlCommand command;

        //    command = new MySqlCommand("SELECT * FROM projects", conSQL);
        //    adapter.SelectCommand = command;
        //    adapter.Fill(ProjsUsersLinks, "First Table");

        //    adapter.SelectCommand.CommandText = "SELECT * FROM users";
        //    adapter.Fill(ProjsUsersLinks, "Second Table");

        //    adapter.SelectCommand.CommandText = "SELECT * FROM userprojectlinks";
        //    adapter.Fill(ProjsUsersLinks, "Third Table");

        //    adapter.Dispose();
        //    command.Dispose();

        //    Projs = ProjsUsersLinks.Tables[0];
        //    Users = ProjsUsersLinks.Tables[1];
        //    Links = ProjsUsersLinks.Tables[2];

        //    this.projectComboBox.Items.Clear();
        //    this.projectComboBox.DataSource = Projs.DefaultView;
        //    this.projectComboBox.DisplayMember = "name";
        //    Int32 projID = (Int32)Projs.Rows[0].ItemArray[0];

        //    this.userComboBox.Items.Clear();
        //    // Clear index.
        //    linkIndex = -1;
        //    for (int i = 0; i < Users.Rows.Count; ++i)
        //    {
        //        Int32 userID = (Int32)Users.Rows[i].ItemArray[0];
        //        for (int j = 0; j < Links.Rows.Count; ++j)
        //        {
        //            Int32 uid = (Int32)Links.Rows[j].ItemArray[2];
        //            Int32 pid = (Int32)Links.Rows[j].ItemArray[1];
        //            if (uid.Equals(userID) && pid.Equals(projID))
        //            {
        //                this.userComboBox.Items.Add(Users.Rows[i].ItemArray[1]);
        //                if (linkIndex < 0)
        //                    linkIndex = j;
        //            }
        //        }
        //    }
        //    this.userComboBox.SelectedIndex = 0;
        //}

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        public int LinkID()
        {
            return linkID;
        }

        private void un_TextChanged(object sender, EventArgs e)
        {

        }

        public static void openTopLevel(string conn)
        {
            //Application.Run(new Form1(conn));
        }


        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            //user = test
            //pass = bugtrack
            XmlTextReader reader = new XmlTextReader(userAuthURL + "?username=" + user.Text + "&password=" + passwordText.Text);
            bool inDBName = false;
            bool inDBUser = false;
            bool inDBPass = false;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        inDBName = false;
                        inDBUser = false;
                        inDBPass = false;

                        if (reader.Name == "DatabaseName")
                        {
                            inDBName = true;
                        }
                        if (reader.Name == "DatabaseUser")
                        {
                            inDBUser = true;
                        }
                        if (reader.Name == "DatabasePassword")
                        {
                            inDBPass = true;
                        }
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        Console.WriteLine(reader.Name);
                        if (inDBName)
                            databaseName = reader.Value;
                        if (inDBPass)
                            databasePassword = reader.Value;
                        if (inDBUser)
                            databaseUser = reader.Value;
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        break;
                }
            }

            if (databaseName != null && databasePassword != null && databaseUser != null)
            {
                Console.WriteLine("Login Successful");
                connectionString = "server=" + databaseHost + ";database=" + databaseName
                    + ";user id=" + databaseUser + ";password=" + databasePassword + ";";
                //create a new toplevel form and open it up
                Form1 topLevelForm = new Form1(connectionString, user.Text);
                topLevelForm.Show();
                //this.Hide(); //FORMS A ZOMBIE PROCESS!!!!!!!
                //openTopLevel(connectionString);
            }
            else
            {
                Console.WriteLine("Login Failed");
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

       
    }
}
