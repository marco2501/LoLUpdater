namespace Backend
{
    partial class Menu
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.Options = new System.Windows.Forms.GroupBox();
            this.Defrag = new System.Windows.Forms.CheckBox();
            this.Cleantemp = new System.Windows.Forms.CheckBox();
            this.UninstallPMB = new System.Windows.Forms.CheckBox();
            this.WindowsUpdate = new System.Windows.Forms.CheckBox();
            this.Patcher = new System.Windows.Forms.RadioButton();
            this.MainFunction = new System.Windows.Forms.GroupBox();
            this.Exit = new System.Windows.Forms.RadioButton();
            this.onlycheckboxes = new System.Windows.Forms.RadioButton();
            this.Restorebackups = new System.Windows.Forms.RadioButton();
            this.OKButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.WindowsServices = new System.Windows.Forms.CheckBox();
            this.ElevateButton = new System.Windows.Forms.Button();
            this.EnableMousefix = new System.Windows.Forms.RadioButton();
            this.DisableMousefix = new System.Windows.Forms.RadioButton();
            this.Combininggroupbox = new System.Windows.Forms.GroupBox();
            this.AdminOptions = new System.Windows.Forms.GroupBox();
            this.Options.SuspendLayout();
            this.MainFunction.SuspendLayout();
            this.Combininggroupbox.SuspendLayout();
            this.AdminOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // Options
            // 
            this.Options.Controls.Add(this.Defrag);
            this.Options.Controls.Add(this.Cleantemp);
            this.Options.Controls.Add(this.UninstallPMB);
            this.Options.Location = new System.Drawing.Point(1, 0);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(104, 108);
            this.Options.TabIndex = 1;
            this.Options.TabStop = false;
            this.Options.Text = "Options";
            // 
            // Defrag
            // 
            this.Defrag.AutoSize = true;
            this.Defrag.Location = new System.Drawing.Point(5, 39);
            this.Defrag.Name = "Defrag";
            this.Defrag.Size = new System.Drawing.Size(85, 17);
            this.Defrag.TabIndex = 5;
            this.Defrag.Text = "Defrag HDD";
            this.toolTip1.SetToolTip(this.Defrag, "Defrags HDD");
            this.Defrag.UseVisualStyleBackColor = true;
            // 
            // Cleantemp
            // 
            this.Cleantemp.AutoSize = true;
            this.Cleantemp.Location = new System.Drawing.Point(5, 16);
            this.Cleantemp.Name = "Cleantemp";
            this.Cleantemp.Size = new System.Drawing.Size(79, 17);
            this.Cleantemp.TabIndex = 0;
            this.Cleantemp.Text = "Clean temp";
            this.toolTip1.SetToolTip(this.Cleantemp, "Cleans hardrive with cleanmanager");
            this.Cleantemp.UseVisualStyleBackColor = true;
            // 
            // UninstallPMB
            // 
            this.UninstallPMB.AutoSize = true;
            this.UninstallPMB.Location = new System.Drawing.Point(5, 61);
            this.UninstallPMB.Name = "UninstallPMB";
            this.UninstallPMB.Size = new System.Drawing.Size(92, 17);
            this.UninstallPMB.TabIndex = 2;
            this.UninstallPMB.Text = "Uninstall PMB";
            this.toolTip1.SetToolTip(this.UninstallPMB, "Uninstalls Pando Media Booster");
            this.UninstallPMB.UseVisualStyleBackColor = true;
            // 
            // WindowsUpdate
            // 
            this.WindowsUpdate.AutoSize = true;
            this.WindowsUpdate.Location = new System.Drawing.Point(6, 40);
            this.WindowsUpdate.Name = "WindowsUpdate";
            this.WindowsUpdate.Size = new System.Drawing.Size(106, 17);
            this.WindowsUpdate.TabIndex = 6;
            this.WindowsUpdate.Text = "Windows update";
            this.toolTip1.SetToolTip(this.WindowsUpdate, "Does a WIndows update");
            this.WindowsUpdate.UseVisualStyleBackColor = true;
            // 
            // Patcher
            // 
            this.Patcher.AutoSize = true;
            this.Patcher.Checked = true;
            this.Patcher.Location = new System.Drawing.Point(6, 19);
            this.Patcher.Name = "Patcher";
            this.Patcher.Size = new System.Drawing.Size(74, 17);
            this.Patcher.TabIndex = 2;
            this.Patcher.TabStop = true;
            this.Patcher.Text = "Patch LoL";
            this.toolTip1.SetToolTip(this.Patcher, "Patches League of Legends (Garena works too)");
            this.Patcher.UseVisualStyleBackColor = true;
            // 
            // MainFunction
            // 
            this.MainFunction.Controls.Add(this.Exit);
            this.MainFunction.Controls.Add(this.onlycheckboxes);
            this.MainFunction.Controls.Add(this.Patcher);
            this.MainFunction.Controls.Add(this.Restorebackups);
            this.MainFunction.Location = new System.Drawing.Point(7, 6);
            this.MainFunction.Name = "MainFunction";
            this.MainFunction.Size = new System.Drawing.Size(118, 108);
            this.MainFunction.TabIndex = 3;
            this.MainFunction.TabStop = false;
            this.MainFunction.Text = "Main function";
            // 
            // Exit
            // 
            this.Exit.AutoSize = true;
            this.Exit.Location = new System.Drawing.Point(6, 85);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(42, 17);
            this.Exit.TabIndex = 5;
            this.Exit.Text = "Exit";
            this.toolTip1.SetToolTip(this.Exit, "Select this to exit");
            this.Exit.UseVisualStyleBackColor = true;
            // 
            // onlycheckboxes
            // 
            this.onlycheckboxes.AutoSize = true;
            this.onlycheckboxes.Location = new System.Drawing.Point(6, 62);
            this.onlycheckboxes.Name = "onlycheckboxes";
            this.onlycheckboxes.Size = new System.Drawing.Size(107, 17);
            this.onlycheckboxes.TabIndex = 4;
            this.onlycheckboxes.Text = "Only checkboxes";
            this.toolTip1.SetToolTip(this.onlycheckboxes, "Select this if you only want to do the checkboxes");
            this.onlycheckboxes.UseVisualStyleBackColor = true;
            // 
            // Restorebackups
            // 
            this.Restorebackups.AutoSize = true;
            this.Restorebackups.Location = new System.Drawing.Point(6, 39);
            this.Restorebackups.Name = "Restorebackups";
            this.Restorebackups.Size = new System.Drawing.Size(106, 17);
            this.Restorebackups.TabIndex = 3;
            this.Restorebackups.Text = "Restore backups";
            this.toolTip1.SetToolTip(this.Restorebackups, "Restore backups");
            this.Restorebackups.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(7, 120);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(223, 20);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "OK";
            this.toolTip1.SetToolTip(this.OKButton, "Confirm");
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // WindowsServices
            // 
            this.WindowsServices.AutoSize = true;
            this.WindowsServices.Location = new System.Drawing.Point(6, 20);
            this.WindowsServices.Name = "WindowsServices";
            this.WindowsServices.Size = new System.Drawing.Size(112, 17);
            this.WindowsServices.TabIndex = 7;
            this.WindowsServices.Text = "Windows services";
            this.toolTip1.SetToolTip(this.WindowsServices, "Sets some services to startuptype manual");
            this.WindowsServices.UseVisualStyleBackColor = true;
            // 
            // ElevateButton
            // 
            this.ElevateButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ElevateButton.Location = new System.Drawing.Point(230, 120);
            this.ElevateButton.Name = "ElevateButton";
            this.ElevateButton.Size = new System.Drawing.Size(124, 20);
            this.ElevateButton.TabIndex = 7;
            this.ElevateButton.Text = "Self-Elevate";
            this.toolTip1.SetToolTip(this.ElevateButton, "Select this if you want to run the program as admin");
            this.ElevateButton.UseVisualStyleBackColor = true;
            this.ElevateButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // EnableMousefix
            // 
            this.EnableMousefix.AutoSize = true;
            this.EnableMousefix.Location = new System.Drawing.Point(6, 61);
            this.EnableMousefix.Name = "EnableMousefix";
            this.EnableMousefix.Size = new System.Drawing.Size(103, 17);
            this.EnableMousefix.TabIndex = 6;
            this.EnableMousefix.Text = "Enable Mousefix";
            this.toolTip1.SetToolTip(this.EnableMousefix, "Sets mouse hz to 500hz");
            this.EnableMousefix.UseVisualStyleBackColor = true;
            // 
            // DisableMousefix
            // 
            this.DisableMousefix.AutoSize = true;
            this.DisableMousefix.Location = new System.Drawing.Point(6, 84);
            this.DisableMousefix.Name = "DisableMousefix";
            this.DisableMousefix.Size = new System.Drawing.Size(105, 17);
            this.DisableMousefix.TabIndex = 8;
            this.DisableMousefix.Text = "Disable Mousefix";
            this.toolTip1.SetToolTip(this.DisableMousefix, "Removes Mousefix");
            this.DisableMousefix.UseVisualStyleBackColor = true;
            // 
            // Combininggroupbox
            // 
            this.Combininggroupbox.Controls.Add(this.Options);
            this.Combininggroupbox.Controls.Add(this.AdminOptions);
            this.Combininggroupbox.Location = new System.Drawing.Point(125, 6);
            this.Combininggroupbox.Name = "Combininggroupbox";
            this.Combininggroupbox.Size = new System.Drawing.Size(229, 108);
            this.Combininggroupbox.TabIndex = 6;
            this.Combininggroupbox.TabStop = false;
            // 
            // AdminOptions
            // 
            this.AdminOptions.Controls.Add(this.DisableMousefix);
            this.AdminOptions.Controls.Add(this.WindowsUpdate);
            this.AdminOptions.Controls.Add(this.WindowsServices);
            this.AdminOptions.Controls.Add(this.EnableMousefix);
            this.AdminOptions.Location = new System.Drawing.Point(105, 0);
            this.AdminOptions.Name = "AdminOptions";
            this.AdminOptions.Size = new System.Drawing.Size(124, 108);
            this.AdminOptions.TabIndex = 5;
            this.AdminOptions.TabStop = false;
            this.AdminOptions.Text = "Admin Options";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 141);
            this.Controls.Add(this.ElevateButton);
            this.Controls.Add(this.Combininggroupbox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.MainFunction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.Text = "LoLUpdater";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Options.ResumeLayout(false);
            this.Options.PerformLayout();
            this.MainFunction.ResumeLayout(false);
            this.MainFunction.PerformLayout();
            this.Combininggroupbox.ResumeLayout(false);
            this.AdminOptions.ResumeLayout(false);
            this.AdminOptions.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.GroupBox Options;
        private System.Windows.Forms.CheckBox UninstallPMB;
        private System.Windows.Forms.CheckBox Cleantemp;
        private System.Windows.Forms.RadioButton Patcher;
        private System.Windows.Forms.GroupBox MainFunction;
        private System.Windows.Forms.RadioButton onlycheckboxes;
        private System.Windows.Forms.RadioButton Restorebackups;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox Combininggroupbox;
        private System.Windows.Forms.Button ElevateButton;
        private System.Windows.Forms.CheckBox WindowsUpdate;
        private System.Windows.Forms.CheckBox WindowsServices;
        private System.Windows.Forms.GroupBox AdminOptions;
        private System.Windows.Forms.CheckBox Defrag;
        private System.Windows.Forms.RadioButton DisableMousefix;
        private System.Windows.Forms.RadioButton EnableMousefix;
        private System.Windows.Forms.RadioButton Exit;
    }
}
