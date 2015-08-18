namespace LuaScriptsManager_WinForms
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.availModulesListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.luaScriptInformationView1 = new LuaScriptsManager_WinForms.LuaScriptInformationView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3,
            this.menuItem4,
            this.menuItem5,
            this.menuItem6});
            this.menuItem1.Text = "LunaLua Module Manager";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "About";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "Settings";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "Submit Bug Report";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 3;
            this.menuItem5.Text = "-";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 4;
            this.menuItem6.Text = "Exit";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(787, 387);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(779, 361);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "LunaLua Scripts";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.availModulesListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.luaScriptInformationView1);
            this.splitContainer1.Size = new System.Drawing.Size(773, 355);
            this.splitContainer1.SplitterDistance = 246;
            this.splitContainer1.TabIndex = 0;
            // 
            // availModulesListView
            // 
            this.availModulesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.availModulesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.availModulesListView.FullRowSelect = true;
            this.availModulesListView.Location = new System.Drawing.Point(0, 0);
            this.availModulesListView.MultiSelect = false;
            this.availModulesListView.Name = "availModulesListView";
            this.availModulesListView.Size = new System.Drawing.Size(246, 355);
            this.availModulesListView.TabIndex = 0;
            this.availModulesListView.UseCompatibleStateImageBehavior = false;
            this.availModulesListView.View = System.Windows.Forms.View.Details;
            this.availModulesListView.SelectedIndexChanged += new System.EventHandler(this.availModulesListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Script";
            this.columnHeader1.Width = 204;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(779, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "LunaLua";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // luaScriptInformationView1
            // 
            this.luaScriptInformationView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.luaScriptInformationView1.Location = new System.Drawing.Point(0, 0);
            this.luaScriptInformationView1.Name = "luaScriptInformationView1";
            this.luaScriptInformationView1.Size = new System.Drawing.Size(523, 355);
            this.luaScriptInformationView1.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 387);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.MinimumSize = new System.Drawing.Size(803, 446);
            this.Name = "MainWindow";
            this.Text = "LunaLua Module Manager";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView availModulesListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private LuaScriptInformationView luaScriptInformationView1;
    }
}

