namespace SortVisualizer
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
            TopMenu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            AlgorithmLabel = new Label();
            AlgoPicker = new ComboBox();
            ResetButton = new Button();
            GraphicsPanel = new Panel();
            SortButton = new Button();
            TopMenu.SuspendLayout();
            SuspendLayout();
            // 
            // TopMenu
            // 
            TopMenu.ImageScalingSize = new Size(24, 24);
            TopMenu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            TopMenu.Location = new Point(0, 0);
            TopMenu.Name = "TopMenu";
            TopMenu.Size = new Size(1844, 33);
            TopMenu.TabIndex = 0;
            TopMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(54, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(141, 34);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(65, 29);
            helpToolStripMenuItem.Text = "Help";
            // 
            // AlgorithmLabel
            // 
            AlgorithmLabel.AutoSize = true;
            AlgorithmLabel.Location = new Point(12, 33);
            AlgorithmLabel.Name = "AlgorithmLabel";
            AlgorithmLabel.Size = new Size(92, 25);
            AlgorithmLabel.TabIndex = 1;
            AlgorithmLabel.Text = "Algorithm";
            // 
            // AlgoPicker
            // 
            AlgoPicker.FormattingEnabled = true;
            AlgoPicker.Location = new Point(110, 30);
            AlgoPicker.Name = "AlgoPicker";
            AlgoPicker.Size = new Size(391, 33);
            AlgoPicker.TabIndex = 2;
            // 
            // ResetButton
            // 
            ResetButton.Location = new Point(516, 28);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(112, 34);
            ResetButton.TabIndex = 3;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = true;
            ResetButton.Click += ResetButton_Click;
            // 
            // GraphicsPanel
            // 
            GraphicsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GraphicsPanel.BackColor = SystemColors.AppWorkspace;
            GraphicsPanel.Location = new Point(12, 78);
            GraphicsPanel.Name = "GraphicsPanel";
            GraphicsPanel.Size = new Size(1820, 990);
            GraphicsPanel.TabIndex = 4;
            // 
            // SortButton
            // 
            SortButton.Location = new Point(634, 28);
            SortButton.Name = "SortButton";
            SortButton.Size = new Size(112, 34);
            SortButton.TabIndex = 5;
            SortButton.Text = "Sort Array";
            SortButton.UseVisualStyleBackColor = true;
            SortButton.Click += SortButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1844, 1080);
            Controls.Add(SortButton);
            Controls.Add(GraphicsPanel);
            Controls.Add(ResetButton);
            Controls.Add(AlgoPicker);
            Controls.Add(AlgorithmLabel);
            Controls.Add(TopMenu);
            DoubleBuffered = true;
            MainMenuStrip = TopMenu;
            Name = "Form1";
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
    }
}
