namespace LoLUpdater
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Deleteoldlogs = new System.Windows.Forms.CheckBox();
            this.Cleantemp = new System.Windows.Forms.CheckBox();
            this.UninstallPMB = new System.Windows.Forms.CheckBox();
            this.Defrag = new System.Windows.Forms.CheckBox();
            this.Patcher = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.onlycheckboxes = new System.Windows.Forms.RadioButton();
            this.Restorebackups = new System.Windows.Forms.RadioButton();
            this.OKButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Mousepollingrate = new System.Windows.Forms.CheckBox();
            this.WindowsServices = new System.Windows.Forms.CheckBox();
            this.Cleanupdatecache = new System.Windows.Forms.CheckBox();
            this.WindowsUpdate = new System.Windows.Forms.CheckBox();
            this.ElevateButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Deleteoldlogs);
            this.groupBox1.Controls.Add(this.Cleantemp);
            this.groupBox1.Controls.Add(this.UninstallPMB);
            this.groupBox1.Controls.Add(this.Defrag);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(116, 108);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // Deleteoldlogs
            // 
            this.Deleteoldlogs.AutoSize = true;
            this.Deleteoldlogs.Location = new System.Drawing.Point(6, 85);
            this.Deleteoldlogs.Name = "Deleteoldlogs";
            this.Deleteoldlogs.Size = new System.Drawing.Size(96, 17);
            this.Deleteoldlogs.TabIndex = 4;
            this.Deleteoldlogs.Text = "Delete old logs";
            this.toolTip1.SetToolTip(this.Deleteoldlogs, "Deletes RIOT logs older than 7 days");
            this.Deleteoldlogs.UseVisualStyleBackColor = true;
            // 
            // Cleantemp
            // 
            this.Cleantemp.AutoSize = true;
            this.Cleantemp.Location = new System.Drawing.Point(6, 20);
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
            this.UninstallPMB.Location = new System.Drawing.Point(6, 42);
            this.UninstallPMB.Name = "UninstallPMB";
            this.UninstallPMB.Size = new System.Drawing.Size(92, 17);
            this.UninstallPMB.TabIndex = 2;
            this.UninstallPMB.Text = "Uninstall PMB";
            this.toolTip1.SetToolTip(this.UninstallPMB, "Uninstalls Pando Media Booster");
            this.UninstallPMB.UseVisualStyleBackColor = true;
            // 
            // Defrag
            // 
            this.Defrag.AutoSize = true;
            this.Defrag.Location = new System.Drawing.Point(6, 64);
            this.Defrag.Name = "Defrag";
            this.Defrag.Size = new System.Drawing.Size(85, 17);
            this.Defrag.TabIndex = 3;
            this.Defrag.Text = "Defrag HDD";
            this.toolTip1.SetToolTip(this.Defrag, "Defragments your HDD");
            this.Defrag.UseVisualStyleBackColor = true;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.onlycheckboxes);
            this.groupBox2.Controls.Add(this.Patcher);
            this.groupBox2.Controls.Add(this.Restorebackups);
            this.groupBox2.Location = new System.Drawing.Point(6, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(118, 108);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Main function";
            // 
            // onlycheckboxes
            // 
            this.onlycheckboxes.AutoSize = true;
            this.onlycheckboxes.Location = new System.Drawing.Point(6, 65);
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
            this.Restorebackups.Location = new System.Drawing.Point(6, 42);
            this.Restorebackups.Name = "Restorebackups";
            this.Restorebackups.Size = new System.Drawing.Size(106, 17);
            this.Restorebackups.TabIndex = 3;
            this.Restorebackups.Text = "Restore backups";
            this.toolTip1.SetToolTip(this.Restorebackups, "Restore backups");
            this.Restorebackups.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(6, 114);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(174, 20);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "OK";
            this.toolTip1.SetToolTip(this.OKButton, "Confirm");
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // Mousepollingrate
            // 
            this.Mousepollingrate.AutoSize = true;
            this.Mousepollingrate.Location = new System.Drawing.Point(6, 85);
            this.Mousepollingrate.Name = "Mousepollingrate";
            this.Mousepollingrate.Size = new System.Drawing.Size(112, 17);
            this.Mousepollingrate.TabIndex = 8;
            this.Mousepollingrate.Text = "Mouse polling rate";
            this.toolTip1.SetToolTip(this.Mousepollingrate, "Sets mouseHz to 500hz only for win8+");
            this.Mousepollingrate.UseVisualStyleBackColor = true;
            // 
            // WindowsServices
            // 
            this.WindowsServices.AutoSize = true;
            this.WindowsServices.Location = new System.Drawing.Point(6, 64);
            this.WindowsServices.Name = "WindowsServices";
            this.WindowsServices.Size = new System.Drawing.Size(112, 17);
            this.WindowsServices.TabIndex = 7;
            this.WindowsServices.Text = "Windows services";
            this.toolTip1.SetToolTip(this.WindowsServices, "Sets some services to startuptype manual");
            this.WindowsServices.UseVisualStyleBackColor = true;
            // 
            // Cleanupdatecache
            // 
            this.Cleanupdatecache.AutoSize = true;
            this.Cleanupdatecache.Location = new System.Drawing.Point(6, 18);
            this.Cleanupdatecache.Name = "Cleanupdatecache";
            this.Cleanupdatecache.Size = new System.Drawing.Size(122, 17);
            this.Cleanupdatecache.TabIndex = 1;
            this.Cleanupdatecache.Text = "Clean update cache";
            this.toolTip1.SetToolTip(this.Cleanupdatecache, "Cleans Windows Update cache");
            this.Cleanupdatecache.UseVisualStyleBackColor = true;
            // 
            // WindowsUpdate
            // 
            this.WindowsUpdate.AutoSize = true;
            this.WindowsUpdate.Location = new System.Drawing.Point(6, 41);
            this.WindowsUpdate.Name = "WindowsUpdate";
            this.WindowsUpdate.Size = new System.Drawing.Size(106, 17);
            this.WindowsUpdate.TabIndex = 6;
            this.WindowsUpdate.Text = "Windows update";
            this.toolTip1.SetToolTip(this.WindowsUpdate, "Does a WIndows update");
            this.WindowsUpdate.UseVisualStyleBackColor = true;
            // 
            // ElevateButton
            // 
            this.ElevateButton.Location = new System.Drawing.Point(186, 114);
            this.ElevateButton.Name = "ElevateButton";
            this.ElevateButton.Size = new System.Drawing.Size(191, 20);
            this.ElevateButton.TabIndex = 7;
            this.ElevateButton.Text = "Self-Elevate";
            this.toolTip1.SetToolTip(this.ElevateButton, "Select this if you want to run the program as admin");
            this.ElevateButton.UseVisualStyleBackColor = true;
            this.ElevateButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Location = new System.Drawing.Point(125, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(252, 108);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ccccc";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Mousepollingrate);
            this.groupBox3.Controls.Add(this.WindowsServices);
            this.groupBox3.Controls.Add(this.Cleanupdatecache);
            this.groupBox3.Controls.Add(this.WindowsUpdate);
            this.groupBox3.Location = new System.Drawing.Point(122, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(130, 108);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Admin Options";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 138);
            this.Controls.Add(this.ElevateButton);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.Text = "LoLUpdater";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

}
#endregion
private System.Windows.Forms.GroupBox groupBox1;
private System.Windows.Forms.CheckBox Defrag;
private System.Windows.Forms.CheckBox UninstallPMB;
private System.Windows.Forms.CheckBox Cleantemp;
private System.Windows.Forms.RadioButton Patcher;
private System.Windows.Forms.GroupBox groupBox2;
private System.Windows.Forms.RadioButton onlycheckboxes;
private System.Windows.Forms.RadioButton Restorebackups;
private System.Windows.Forms.Button OKButton;
private System.Windows.Forms.ToolTip toolTip1;
private System.Windows.Forms.GroupBox groupBox4;
private System.Windows.Forms.Button ElevateButton;
private System.Windows.Forms.CheckBox Deleteoldlogs;
private System.Windows.Forms.CheckBox WindowsUpdate;
private System.Windows.Forms.CheckBox Cleanupdatecache;
private System.Windows.Forms.CheckBox WindowsServices;
private System.Windows.Forms.CheckBox Mousepollingrate;
private System.Windows.Forms.GroupBox groupBox3;
}
}
