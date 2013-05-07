using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CS_WinForms
{

    public partial class Form1 : Form
    {
        Microsoft.Win32.RegistryKey key;
        string value1, value2, value3, value4, value5, value6, value7;
        //private const string AppId = Variables.AppID;
        //AGUSTIN.RUDAS
        /// <summary>
        /// Extended permissions is a comma separated list of permissions to ask the user.
        /// </summary>
        /// <remarks>
        /// For extensive list of available extended permissions refer to 
        /// https://developers.facebook.com/docs/reference/api/permissions/
        /// </remarks>
        private const string ExtendedPermissions = "manage_pages,user_about_me,publish_stream,offline_access";

        public Form1()
        {
            InitializeComponent();
        }

        public string getMD5Hash(string Serial)
        {
                Serial = Serial + "DTSEVIS";
                System.Security.Cryptography.MD5CryptoServiceProvider md5Obj = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] bytesToHash = System.Text.Encoding.ASCII.GetBytes(Serial);

                bytesToHash = md5Obj.ComputeHash(bytesToHash);

                string strResult = "";

                foreach (byte b in bytesToHash)
                {
                    strResult += b.ToString("x2");
                }

                return strResult;


                // Example Usage
        }

        private void btnFacebookLogin_Click(object sender, EventArgs e)
        {
            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("WowSocial", true);
            //key.SetValue("Licencia", textBox2.Text);
            //key.SetValue("Character", textBox1.Text);
            key.SetValue("Realm", comboBox1.Text);
            key.SetValue("Guild", textBox4.Text);
            key.SetValue("AppID", textBox2.Text);
            key.SetValue("PageID", textBox5.Text);
            //key.SetValue("FBID", textBox3.Text);
            key.Close();
            //if (textBox2.Text == getMD5Hash(textBox3.Text))
            //{

                //var Variab = new Variables();
                //Variables.Character = textBox1.Text;
                Variables.Realm = comboBox1.Text;
                Variables.Guild = textBox4.Text;
                Variables.AppID = textBox2.Text;
                Variables.PageID = textBox5.Text;
                // open the Facebook Login Dialog and ask for user permissions.
                var fbLoginDlg = new FacebookLoginDialog(Variables.AppID, ExtendedPermissions);
                fbLoginDlg.ShowDialog();

                // The user has taken action, either allowed/denied or cancelled the authorization,
                // which can be known by looking at the dialogs FacebookOAuthResult property.
                // Depending on the result take appropriate actions.
                TakeLoggedInAction(fbLoginDlg.FacebookOAuthResult);
            //}
            //else
            //{
            //    MessageBox.Show("Facebook ID or Licence is Wrong. Try Again","Information");
            //}
        }

        private void TakeLoggedInAction(Facebook.FacebookOAuthResult facebookOAuthResult)
        {
            if (facebookOAuthResult == null)
            {
                // the user closed the FacebookLoginDialog, so do nothing.
                MessageBox.Show("Cancelled!");
                return;
            }

            // Even though facebookOAuthResult is not null, it could had been an 
            // OAuth 2.0 error, so make sure to check IsSuccess property always.
            if (facebookOAuthResult.IsSuccess)
            {
                // since our respone_type in FacebookLoginDialog was token,
                // we got the access_token
                // The user now has successfully granted permission to our app.
                var dlg = new FacebookInfoDialog(facebookOAuthResult.AccessToken);
                dlg.ShowDialog();
            }
            else
            {
                // for some reason we failed to get the access token.
                // most likely the user clicked don't allow.
                MessageBox.Show(facebookOAuthResult.ErrorDescription);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("C:\\Updates.txt", true);
            sw.Close();
            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("WowSocial",true);
            if (key != null)
            {
                //value1 = (String)key.GetValue("Licencia");
                //value2 = (String)key.GetValue("Character");
                value3 = (String)key.GetValue("Realm");
                value5 = (String)key.GetValue("Guild");
                value6 = (String)key.GetValue("AppID");
                value7 = (String)key.GetValue("PageID");
                //value4 = (String)key.GetValue("FBID");
                //textBox1.Text = value2;
                //textBox2.Text = value1;
                comboBox1.Text = value3;
                //textBox3.Text = value4;
                textBox4.Text = value5;
                textBox2.Text = value6;
                textBox5.Text = value7;
            }
            else
            {
                
                if ((value1 == null))
                {
                    key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("WowSocial");
                    //key.SetValue("Licencia", "");
                    //key.SetValue("Character", "");
                    key.SetValue("Realm", "");
                    //key.SetValue("FBID", "");
                    key.SetValue("Guild", "");
                    key.SetValue("AppID", "");
                }

            }
            key.Close();

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
