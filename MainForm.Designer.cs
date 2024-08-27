﻿namespace SortVisualizer
{
    partial class MainForm
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
            TopMenu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            AlgorithmLabel = new Label();
            AlgoPicker = new ComboBox();
            ResetButton = new Button();
            GraphicsPanel = new Panel();
            SortButton = new Button();
            bgWorker = new System.ComponentModel.BackgroundWorker();
            TopMenu.SuspendLayout();
            SuspendLayout();
            // 
            // TopMenu
            // 
            TopMenu.ImageScalingSize = new Size(24, 24);
            TopMenu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            TopMenu.Location = new Point(0, 0);
            TopMenu.Name = "TopMenu";
            TopMenu.Padding = new Padding(4, 1, 0, 1);
            TopMenu.Size = new Size(1291, 24);
            TopMenu.TabIndex = 0;
            TopMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 22);
            fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(93, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 22);
            helpToolStripMenuItem.Text = "Help";
            // 
            // AlgorithmLabel
            // 
            AlgorithmLabel.AutoSize = true;
            AlgorithmLabel.Location = new Point(8, 20);
            AlgorithmLabel.Margin = new Padding(2, 0, 2, 0);
            AlgorithmLabel.Name = "AlgorithmLabel";
            AlgorithmLabel.Size = new Size(61, 15);
            AlgorithmLabel.TabIndex = 1;
            AlgorithmLabel.Text = "Algorithm";
            // 
            // AlgoPicker
            // 
            AlgoPicker.FormattingEnabled = true;
            AlgoPicker.Location = new Point(77, 18);
            AlgoPicker.Margin = new Padding(2);
            AlgoPicker.Name = "AlgoPicker";
            AlgoPicker.Size = new Size(275, 23);
            AlgoPicker.TabIndex = 2;
            // 
            // ResetButton
            // 
            ResetButton.Location = new Point(361, 17);
            ResetButton.Margin = new Padding(2);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(78, 20);
            ResetButton.TabIndex = 3;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = true;
            ResetButton.Click += ResetButton_Click;
            // 
            // GraphicsPanel
            // 
            GraphicsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GraphicsPanel.BackColor = SystemColors.AppWorkspace;
            GraphicsPanel.Location = new Point(8, 47);
            GraphicsPanel.Margin = new Padding(2);
            GraphicsPanel.Name = "GraphicsPanel";
            GraphicsPanel.Size = new Size(1274, 594);
            GraphicsPanel.TabIndex = 4;
            // 
            // SortButton
            // 
            SortButton.Location = new Point(444, 17);
            SortButton.Margin = new Padding(2);
            SortButton.Name = "SortButton";
            SortButton.Size = new Size(78, 20);
            SortButton.TabIndex = 5;
            SortButton.Text = "Sort Array";
            SortButton.UseVisualStyleBackColor = true;
            SortButton.Click += SortButton_Click;
            // 
            // bgWorker
            // 
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.ProgressChanged += bgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1291, 648);
            Controls.Add(SortButton);
            Controls.Add(GraphicsPanel);
            Controls.Add(ResetButton);
            Controls.Add(AlgoPicker);
            Controls.Add(AlgorithmLabel);
            Controls.Add(TopMenu);
            DoubleBuffered = true;
            MainMenuStrip = TopMenu;
            Margin = new Padding(2);
            Name = "MainForm";
            Text = "Form1";
            TopMenu.ResumeLayout(false);
            TopMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip TopMenu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private Label AlgorithmLabel;
        private ComboBox AlgoPicker;
        private Button ResetButton;
        private Panel GraphicsPanel;
        private Button SortButton;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}
