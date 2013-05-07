namespace CS_WinForms
{
    partial class FacebookInfoDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FacebookInfoDialog));
            this.lnkName = new System.Windows.Forms.LinkLabel();
            this.lblUserId = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.chkCSharpSdkFan = new System.Windows.Forms.CheckBox();
            this.lnkFacebokSdkFan = new System.Windows.Forms.LinkLabel();
            this.btnProgressAndCancellation = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.picProfile = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalFriends = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // lnkName
            // 
            this.lnkName.AutoSize = true;
            this.lnkName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkName.Location = new System.Drawing.Point(309, 21);
            this.lnkName.Name = "lnkName";
            this.lnkName.Size = new System.Drawing.Size(95, 24);
            this.lnkName.TabIndex = 1;
            this.lnkName.TabStop = true;
            this.lnkName.Text = "[lnkName]";
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(310, 58);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(54, 13);
            this.lblUserId.TabIndex = 2;
            this.lblUserId.Text = "[lblUserId]";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(310, 84);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(70, 13);
            this.lblFirstName.TabIndex = 3;
            this.lblFirstName.Text = "[lblFirstName]";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(310, 108);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(71, 13);
            this.lblLastName.TabIndex = 4;
            this.lblLastName.Text = "[lblLastName]";
            // 
            // chkCSharpSdkFan
            // 
            this.chkCSharpSdkFan.AutoSize = true;
            this.chkCSharpSdkFan.Enabled = false;
            this.chkCSharpSdkFan.Location = new System.Drawing.Point(83, 156);
            this.chkCSharpSdkFan.Name = "chkCSharpSdkFan";
            this.chkCSharpSdkFan.Size = new System.Drawing.Size(280, 17);
            this.chkCSharpSdkFan.TabIndex = 5;
            this.chkCSharpSdkFan.Text = "Is fan of the official Facebok C# SDK facebook page.";
            this.chkCSharpSdkFan.UseVisualStyleBackColor = true;
            this.chkCSharpSdkFan.Visible = false;
            // 
            // lnkFacebokSdkFan
            // 
            this.lnkFacebokSdkFan.AutoSize = true;
            this.lnkFacebokSdkFan.Location = new System.Drawing.Point(115, 175);
            this.lnkFacebokSdkFan.Name = "lnkFacebokSdkFan";
            this.lnkFacebokSdkFan.Size = new System.Drawing.Size(200, 13);
            this.lnkFacebokSdkFan.TabIndex = 13;
            this.lnkFacebokSdkFan.TabStop = true;
            this.lnkFacebokSdkFan.Text = "Like us on Facebook at our official page.";
            this.lnkFacebokSdkFan.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFacebokSdkFan_LinkClicked);
            // 
            // btnProgressAndCancellation
            // 
            this.btnProgressAndCancellation.Location = new System.Drawing.Point(0, 171);
            this.btnProgressAndCancellation.Name = "btnProgressAndCancellation";
            this.btnProgressAndCancellation.Size = new System.Drawing.Size(178, 23);
            this.btnProgressAndCancellation.TabIndex = 14;
            this.btnProgressAndCancellation.Text = "Upload Progress and Cancellation Sample";
            this.btnProgressAndCancellation.UseVisualStyleBackColor = true;
            this.btnProgressAndCancellation.Visible = false;
            this.btnProgressAndCancellation.Click += new System.EventHandler(this.btnProgressAndCancellation_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(321, 166);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "ABOUT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::WOW_SOCIAL.Properties.Resources.right_arrow;
            this.pictureBox2.Location = new System.Drawing.Point(152, 36);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(90, 105);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WOW_SOCIAL.Properties.Resources.world_of_warcraft;
            this.pictureBox1.Location = new System.Drawing.Point(12, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(134, 129);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // picProfile
            // 
            this.picProfile.Location = new System.Drawing.Point(248, 58);
            this.picProfile.Name = "picProfile";
            this.picProfile.Size = new System.Drawing.Size(50, 50);
            this.picProfile.TabIndex = 0;
            this.picProfile.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(402, 166);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "DONATE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Do not close this window. Tha application is : RUNNING";
            // 
            // lblTotalFriends
            // 
            this.lblTotalFriends.AutoSize = true;
            this.lblTotalFriends.Location = new System.Drawing.Point(310, 137);
            this.lblTotalFriends.Name = "lblTotalFriends";
            this.lblTotalFriends.Size = new System.Drawing.Size(81, 13);
            this.lblTotalFriends.TabIndex = 6;
            this.lblTotalFriends.Text = "[lblTotalFriends]";
            // 
            // FacebookInfoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 197);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnProgressAndCancellation);
            this.Controls.Add(this.lnkFacebokSdkFan);
            this.Controls.Add(this.lblTotalFriends);
            this.Controls.Add(this.chkCSharpSdkFan);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.lblUserId);
            this.Controls.Add(this.lnkName);
            this.Controls.Add(this.picProfile);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FacebookInfoDialog";
            this.Text = "World Of Warcraft - Social Activity Updater";
            this.Load += new System.EventHandler(this.FacebookInfoDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProfile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picProfile;
        private System.Windows.Forms.LinkLabel lnkName;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.CheckBox chkCSharpSdkFan;
        private System.Windows.Forms.LinkLabel lnkFacebokSdkFan;
        private System.Windows.Forms.Button btnProgressAndCancellation;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalFriends;
    }
}