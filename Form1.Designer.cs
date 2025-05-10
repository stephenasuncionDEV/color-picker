namespace color_picker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            colorDisplay = new PictureBox();
            pickBtn = new Button();
            hexTxt = new TextBox();
            label1 = new Label();
            label2 = new Label();
            rgbTxt = new TextBox();
            label3 = new Label();
            savedList = new ListBox();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            toggleMenuItem = new ToolStripMenuItem();
            closeMenuItem = new ToolStripMenuItem();
            worker = new System.Windows.Forms.Timer(components);
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            statusLabel = new ToolStripStatusLabel();
            cancelOnPick = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)colorDisplay).BeginInit();
            contextMenuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // colorDisplay
            // 
            colorDisplay.BorderStyle = BorderStyle.FixedSingle;
            colorDisplay.Location = new Point(12, 41);
            colorDisplay.Name = "colorDisplay";
            colorDisplay.Size = new Size(150, 150);
            colorDisplay.TabIndex = 0;
            colorDisplay.TabStop = false;
            colorDisplay.Paint += colorDisplay_Paint;
            // 
            // pickBtn
            // 
            pickBtn.Location = new Point(12, 12);
            pickBtn.Name = "pickBtn";
            pickBtn.Size = new Size(86, 23);
            pickBtn.TabIndex = 1;
            pickBtn.Text = "Pick Color";
            pickBtn.UseVisualStyleBackColor = true;
            pickBtn.Click += pickBtn_Click;
            // 
            // hexTxt
            // 
            hexTxt.Location = new Point(47, 197);
            hexTxt.Name = "hexTxt";
            hexTxt.ReadOnly = true;
            hexTxt.Size = new Size(118, 23);
            hexTxt.TabIndex = 2;
            hexTxt.DoubleClick += hexTxt_DoubleClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 200);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 3;
            label1.Text = "HEX";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 229);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 4;
            label2.Text = "RGB";
            // 
            // rgbTxt
            // 
            rgbTxt.Location = new Point(47, 226);
            rgbTxt.Name = "rgbTxt";
            rgbTxt.ReadOnly = true;
            rgbTxt.Size = new Size(118, 23);
            rgbTxt.TabIndex = 5;
            rgbTxt.DoubleClick += rgbTxt_DoubleClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(177, 16);
            label3.Name = "label3";
            label3.Size = new Size(56, 15);
            label3.TabIndex = 7;
            label3.Text = "Saved list";
            // 
            // savedList
            // 
            savedList.FormattingEnabled = true;
            savedList.ItemHeight = 15;
            savedList.Location = new Point(177, 41);
            savedList.Name = "savedList";
            savedList.Size = new Size(142, 244);
            savedList.TabIndex = 8;
            savedList.DoubleClick += savedList_DoubleClick;
            // 
            // notifyIcon1
            // 
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipText = "Click to re-open the app";
            notifyIcon1.BalloonTipTitle = "Color Picker is still running";
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Color Picker";
            notifyIcon1.BalloonTipClicked += notifyIcon1_BalloonTipClicked;
            notifyIcon1.DoubleClick += notifyIcon1_DoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { toggleMenuItem, closeMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(129, 48);
            contextMenuStrip1.ItemClicked += contextMenuStrip1_ItemClicked;
            // 
            // toggleMenuItem
            // 
            toggleMenuItem.Name = "toggleMenuItem";
            toggleMenuItem.Size = new Size(128, 22);
            toggleMenuItem.Text = "Pick Color";
            // 
            // closeMenuItem
            // 
            closeMenuItem.Name = "closeMenuItem";
            closeMenuItem.Size = new Size(128, 22);
            closeMenuItem.Text = "Close";
            // 
            // worker
            // 
            worker.Tick += worker_Tick;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, statusLabel });
            statusStrip1.Location = new Point(0, 298);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(331, 22);
            statusStrip1.TabIndex = 9;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(42, 17);
            toolStripStatusLabel1.Text = "Status:";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(26, 17);
            statusLabel.Text = "Idle";
            // 
            // cancelOnPick
            // 
            cancelOnPick.AutoSize = true;
            cancelOnPick.Checked = true;
            cancelOnPick.CheckState = CheckState.Checked;
            cancelOnPick.Location = new Point(58, 258);
            cancelOnPick.Name = "cancelOnPick";
            cancelOnPick.Size = new Size(104, 19);
            cancelOnPick.TabIndex = 10;
            cancelOnPick.Text = "Cancel on pick";
            cancelOnPick.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(331, 320);
            Controls.Add(cancelOnPick);
            Controls.Add(statusStrip1);
            Controls.Add(savedList);
            Controls.Add(label3);
            Controls.Add(rgbTxt);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(hexTxt);
            Controls.Add(pickBtn);
            Controls.Add(colorDisplay);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Color Picker by Typedef";
            TopMost = true;
            Resize += Form1_Resize;
            ((System.ComponentModel.ISupportInitialize)colorDisplay).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox colorDisplay;
        private Button pickBtn;
        private TextBox hexTxt;
        private Label label1;
        private Label label2;
        private TextBox rgbTxt;
        private Label label3;
        private ListBox savedList;
        private NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer worker;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel statusLabel;
        private CheckBox cancelOnPick;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toggleMenuItem;
        private ToolStripMenuItem closeMenuItem;
    }
}
