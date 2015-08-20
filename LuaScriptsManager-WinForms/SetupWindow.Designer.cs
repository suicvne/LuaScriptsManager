namespace LuaScriptsManager_WinForms
{
    partial class SetupWindow
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
            this.stepPanel = new System.Windows.Forms.Panel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.nextButton = new System.Windows.Forms.Button();
            this.welcomeStep1 = new LuaScriptsManager_WinForms.SetupUI.WelcomeStep();
            this.stepPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // stepPanel
            // 
            this.stepPanel.Controls.Add(this.welcomeStep1);
            this.stepPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.stepPanel.Location = new System.Drawing.Point(0, 0);
            this.stepPanel.Name = "stepPanel";
            this.stepPanel.Size = new System.Drawing.Size(611, 328);
            this.stepPanel.TabIndex = 0;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.nextButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(0, 280);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(611, 39);
            this.buttonPanel.TabIndex = 1;
            // 
            // nextButton
            // 
            this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.nextButton.Location = new System.Drawing.Point(524, 9);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 0;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // welcomeStep1
            // 
            this.welcomeStep1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.welcomeStep1.Location = new System.Drawing.Point(0, 0);
            this.welcomeStep1.Name = "welcomeStep1";
            this.welcomeStep1.Size = new System.Drawing.Size(611, 328);
            this.welcomeStep1.TabIndex = 0;
            this.welcomeStep1.Tag = "welcome";
            // 
            // SetupWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 319);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.stepPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LunaLua Module Manager Setup";
            this.stepPanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel stepPanel;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button nextButton;
        private SetupUI.WelcomeStep welcomeStep1;
    }
}