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
        MySqlConnection m_conSQL;
        // Keep a user ID, name, and level.
        // These are queried by MainForm1 after a successful login.
        String m_userName;
        Int32 m_userID, m_userLvl;
        // Database authentification parameters.
        const string databaseHost = "69.93.227.36";
        const string userAuthURL = "http://69.93.227.36/webservices/bugTrackerAuth.php";

        public LogIn(MySqlConnection _conSQL)
        {
            m_conSQL = _conSQL;
            m_userID = -1;
            m_userLvl = -1;
            m_userName = "";
            InitializeComponent();
            // When the enter is pressed login
            this.passwordText.KeyDown += new KeyEventHandler(this.Form2_Load_Keypress);
            this.user.KeyDown         += new KeyEventHandler(this.Form2_Load_Keypress);
            this.user.Focus();
        }

        private void Form2_Load_Keypress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonLogIn_Click(sender, e);
        }

        private void Form2_Load(object sender, EventArgs e)
        {}

        public Int32  UserID   ()  {   return m_userID;    }
        public String UserName ()  {   return m_userName;  }
        public Int32  UserLevel()  {   return m_userLvl;   }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            string databaseName = null;
            string databaseUser = null;
            string databasePassword = null;

            XmlTextReader reader = new XmlTextReader(userAuthURL + "?username=" + this.user.Text + "&password=" + this.passwordText.Text);
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
                            inDBName = true;
                        if (reader.Name == "DatabaseUser")
                            inDBUser = true;
                        if (reader.Name == "DatabasePassword")
                            inDBPass = true;

                        break;

                    case XmlNodeType.Text: //Display the text in each element.

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
                m_conSQL.ConnectionString = "server=" + databaseHost + ";database=" + databaseName
                                          + ";user id=" + databaseUser + ";password=" + databasePassword + ";";
                try
                {
                    // Try opening.
                    m_conSQL.Open();
                    //Query for user ID.
                    string IDQuery = "SELECT uid, permissionLevel FROM users WHERE username = '" + this.user.Text + "' AND password = '" + this.passwordText.Text + "';";
                    MySqlCommand cmdSQL = new MySqlCommand(IDQuery, m_conSQL);
                    MySqlDataReader rdrSQL = cmdSQL.ExecuteReader();
                    // Only one row to read with one entry.
                    if (rdrSQL.Read())
                    {
                        m_userID = (Int32)rdrSQL[0];
                        m_userLvl = Convert.ToInt32((String)rdrSQL[1]);
                        m_userName = this.user.Text;
                    }
                    rdrSQL.Close();
                    // Go away.
                    this.Hide();
                    return;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString(), "Connection Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Incorrect Information", "Log In Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
