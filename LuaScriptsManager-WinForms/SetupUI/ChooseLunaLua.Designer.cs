namespace LuaScriptsManager_WinForms.SetupUI
{
    partial class ChooseLunaLua
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lunaluaDirTextbox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.getLunaLuaButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lunaluaDirTextbox
            // 
            this.lunaluaDirTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lunaluaDirTextbox.Location = new System.Drawing.Point(8, 45);
            this.lunaluaDirTextbox.Name = "lunaluaDirTextbox";
            this.lunaluaDirTextbox.Size = new System.Drawing.Size(332, 20);
            this.lunaluaDirTextbox.TabIndex = 0;
            this.lunaluaDirTextbox.TextChanged += new System.EventHandler(this.lunaluaDirTextbox_TextChanged);
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.browseButton.Location = new System.Drawing.Point(347, 43);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // getLunaLuaButton
            // 
            this.getLunaLuaButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.getLunaLuaButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.getLunaLuaButton.Location = new System.Drawing.Point(166, 71);
            this.getLunaLuaButton.Name = "getLunaLuaButton";
            this.getLunaLuaButton.Size = new System.Drawing.Size(95, 23);
            this.getLunaLuaButton.TabIndex = 2;
            this.getLunaLuaButton.Text = "Get LunaLua";
            this.getLunaLuaButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Please select your LunaLua directory. This directory is the one \r\nthat has your s" +
    "mbx.exe or LuaScriptsLib folder in it.";
            // 
            // ChooseLunaLua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.getLunaLuaButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.lunaluaDirTextbox);
            this.Name = "ChooseLunaLua";
            this.Size = new System.Drawing.Size(431, 102);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox lunaluaDirTextbox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button getLunaLuaButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
