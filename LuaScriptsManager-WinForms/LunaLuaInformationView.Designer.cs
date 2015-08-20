namespace LuaScriptsManager_WinForms
{
    partial class LunaLuaInformationView
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
            this.versionLabel = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.launchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // versionLabel
            // 
            this.versionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.versionLabel.Location = new System.Drawing.Point(0, 0);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(455, 13);
            this.versionLabel.TabIndex = 0;
            this.versionLabel.Text = "Your current LunaLua version: 0.0.0.0";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateButton
            // 
            this.updateButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.updateButton.Location = new System.Drawing.Point(3, 37);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 1;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            // 
            // launchButton
            // 
            this.launchButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.launchButton.Location = new System.Drawing.Point(84, 37);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(94, 23);
            this.launchButton.TabIndex = 2;
            this.launchButton.Text = "Launch SMBX";
            this.launchButton.UseVisualStyleBackColor = true;
            // 
            // LunaLuaInformationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.launchButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.versionLabel);
            this.Name = "LunaLuaInformationView";
            this.Size = new System.Drawing.Size(455, 197);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button launchButton;
    }
}
