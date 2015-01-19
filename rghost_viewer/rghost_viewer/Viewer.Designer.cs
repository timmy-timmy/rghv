namespace RGHV {
    partial class Viewer {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Viewer));
            this.contentPanel = new System.Windows.Forms.Panel();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.btnShowFinder = new System.Windows.Forms.Button();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.cbFilter = new System.Windows.Forms.CheckBox();
            this.panelSplit = new System.Windows.Forms.Panel();
            this.tbProxy = new System.Windows.Forms.TextBox();
            this.cbProxy = new System.Windows.Forms.CheckBox();
            this.contentPanel.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.contentPanel.AutoScroll = true;
            this.contentPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.contentPanel.Controls.Add(this.flowPanel);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 87);
            this.contentPanel.Name = "panel1";
            this.contentPanel.Size = new System.Drawing.Size(516, 312);
            this.contentPanel.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowPanel.AutoSize = true;
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.Location = new System.Drawing.Point(0, 0);
            this.flowPanel.Name = "flowLayoutPanel1";
            this.flowPanel.Size = new System.Drawing.Size(512, 308);
            this.flowPanel.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 399);
            this.statusStrip.Name = "statusStrip1";
            this.statusStrip.Size = new System.Drawing.Size(516, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.statusLabel.Name = "toolStripStatusLabel1";
            this.statusLabel.Size = new System.Drawing.Size(79, 17);
            this.statusLabel.Text = "Disconnected";
            // 
            // toolStrip1
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip1";
            this.toolStrip.Size = new System.Drawing.Size(516, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(66, 22);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // panel2
            // 
            this.controlPanel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.controlPanel.Controls.Add(this.btnShowFinder);
            this.controlPanel.Controls.Add(this.tbFilter);
            this.controlPanel.Controls.Add(this.cbFilter);
            this.controlPanel.Controls.Add(this.panelSplit);
            this.controlPanel.Controls.Add(this.tbProxy);
            this.controlPanel.Controls.Add(this.cbProxy);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlPanel.Location = new System.Drawing.Point(0, 25);
            this.controlPanel.Name = "panel2";
            this.controlPanel.Size = new System.Drawing.Size(516, 62);
            this.controlPanel.TabIndex = 4;
            // 
            // btnShowFinder
            // 
            this.btnShowFinder.Location = new System.Drawing.Point(327, 5);
            this.btnShowFinder.Name = "btnShowFinder";
            this.btnShowFinder.Size = new System.Drawing.Size(86, 23);
            this.btnShowFinder.TabIndex = 5;
            this.btnShowFinder.Text = "Find proxy...";
            this.btnShowFinder.UseVisualStyleBackColor = true;
            this.btnShowFinder.Click += new System.EventHandler(this.btnShowFinder_Click);
            // 
            // tbFilter
            // 
            this.tbFilter.Location = new System.Drawing.Point(200, 35);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(213, 20);
            this.tbFilter.TabIndex = 4;
            this.tbFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
            // 
            // cbFilter
            // 
            this.cbFilter.AutoSize = true;
            this.cbFilter.Location = new System.Drawing.Point(7, 37);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(196, 17);
            this.cbFilter.TabIndex = 3;
            this.cbFilter.Text = "Use filtering (e.g.: \"sa-mp; gta_sa;\"):";
            this.cbFilter.UseVisualStyleBackColor = true;
            // 
            // panelSplit
            // 
            this.panelSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSplit.Location = new System.Drawing.Point(3, 30);
            this.panelSplit.Name = "panelSplit";
            this.panelSplit.Size = new System.Drawing.Size(350, 1);
            this.panelSplit.TabIndex = 2;
            // 
            // tbProxy
            // 
            this.tbProxy.Location = new System.Drawing.Point(149, 6);
            this.tbProxy.Name = "tbProxy";
            this.tbProxy.Size = new System.Drawing.Size(173, 20);
            this.tbProxy.TabIndex = 1;
            this.tbProxy.Text = "59.151.103.15:80";
            this.tbProxy.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // cbProxy
            // 
            this.cbProxy.AutoSize = true;
            this.cbProxy.Location = new System.Drawing.Point(7, 8);
            this.cbProxy.Name = "cbProxy";
            this.cbProxy.Size = new System.Drawing.Size(136, 17);
            this.cbProxy.TabIndex = 0;
            this.cbProxy.Text = "Use proxy (HTTP only):";
            this.cbProxy.UseVisualStyleBackColor = true;
            this.cbProxy.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 421);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Name = "Viewer";
            this.Text = "RGhost viewer v.1.1";
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.TextBox tbProxy;
        private System.Windows.Forms.CheckBox cbProxy;
        private System.Windows.Forms.Panel panelSplit;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.CheckBox cbFilter;
        private System.Windows.Forms.Button btnShowFinder;
    }
}

