using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.OleDb;


namespace CSCE431Project1
{
    public partial class Notes : Form
    {
        protected MySqlConnection conSQL;
        protected String noteType;
        protected int itemid;
        protected int currentUser = 0;
  

        public Notes(MySqlConnection con,String tp, int id,int userid)
        {
            InitializeComponent();
            noteType = tp;
            itemid = id;
            currentUser = userid;
            conSQL = con;
            if(tp=="Bug")
                label1.Text = "Notes for Bug #" + id;
            if(tp=="Req")
                label1.Text = "Notes for Req #" + id;
            richTextBox2.Text = getNotes(con, tp, id);
            Console.WriteLine(con.ConnectionString);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public static void addNote(MySqlConnection con, String tp, int id, int user,String note)
        {
            String query = "";
            if (tp == "Req")
                query = "INSERT into requirementnotes VALUES ('','"+user+"','"+id+"','"+note+"')";
            if (tp == "Bug")
                query = "INSERT into bugnotes VALUES ('','" + user + "','" + id + "','" + note + "')";

            
               
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.ToString());
                }
           

        }

        public static String getNotes(MySqlConnection con,String tp, int id)
        {
            String notedata = "";
            String commandtext = "";
            if (tp == "Req")
                commandtext = "SELECT requirementnotes.*,users.* FROM requirementnotes,users WHERE users.uid=requirementnotes.userid AND requirementnotes.requirementid="+id+" ORDER BY requirementnotes.rnlid DESC";
            if (tp == "Bug")
                commandtext = "SELECT bugnotes.*, users.* FROM bugnotes,users WHERE users.uid=bugnotes.userid AND bugnotes.bugid="+id+" ORDER BY bugnotes.bnid DESC";


            MySqlDataReader sqlreader = null;

            try
            {

                MySqlCommand command = new MySqlCommand(commandtext, con);
                sqlreader = command.ExecuteReader();

                while (sqlreader.Read())
                {
                    String tu = "";
                    String tn = "";
                    for (int i = 0; i < sqlreader.FieldCount; i++)
                    {
                        if (sqlreader.GetName(i) == "username")
                            tu += sqlreader[i].ToString() + "\n";
                        if (sqlreader.GetName(i) == "note")
                            tn += sqlreader[i].ToString() + "\n\n";
                    }
                    notedata += tu + tn;
                }
            }
            catch (Exception e1)
            {
                notedata = "MySQL Connection Problem.\n" + e1.Data;
            }
            finally
            {
                sqlreader.Close();
            }

            return notedata;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button1.Text = "Submitting...";
            addNote(conSQL, noteType, itemid, currentUser, richTextBox1.Text.ToString());
            richTextBox1.Text = "";
            richTextBox2.Text = getNotes(conSQL,noteType, itemid);
            button1.Enabled = true;
            button1.Text = "Add Note";

        }
    }
}
