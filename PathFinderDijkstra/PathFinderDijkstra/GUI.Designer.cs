namespace PathFinderDijkstra
{
    partial class GUI
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
            this.mainGrid = new System.Windows.Forms.PictureBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.endButton = new System.Windows.Forms.Button();
            this.wallButton = new System.Windows.Forms.Button();
            this.eraseButton = new System.Windows.Forms.Button();
            this.runAlgoButton = new System.Windows.Forms.Button();
            this.languageComboBox = new System.Windows.Forms.ComboBox();
            this.asmLabel = new System.Windows.Forms.Label();
            this.netLabel = new System.Windows.Forms.Label();
            this.asmTimeLabel = new System.Windows.Forms.Label();
            this.netTimeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mainGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // mainGrid
            // 
            this.mainGrid.Location = new System.Drawing.Point(2, 6);
            this.mainGrid.Name = "mainGrid";
            this.mainGrid.Size = new System.Drawing.Size(800, 402);
            this.mainGrid.TabIndex = 0;
            this.mainGrid.TabStop = false;
            this.mainGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainGrid_MouseClick);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(498, 413);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(97, 40);
            this.clearButton.TabIndex = 1;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(4, 414);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(42, 40);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // endButton
            // 
            this.endButton.Location = new System.Drawing.Point(49, 414);
            this.endButton.Name = "endButton";
            this.endButton.Size = new System.Drawing.Size(42, 40);
            this.endButton.TabIndex = 2;
            this.endButton.Text = "End";
            this.endButton.UseVisualStyleBackColor = true;
            this.endButton.Click += new System.EventHandler(this.endButton_Click);
            // 
            // wallButton
            // 
            this.wallButton.Location = new System.Drawing.Point(93, 414);
            this.wallButton.Name = "wallButton";
            this.wallButton.Size = new System.Drawing.Size(42, 40);
            this.wallButton.TabIndex = 2;
            this.wallButton.Text = "Wall";
            this.wallButton.UseVisualStyleBackColor = true;
            this.wallButton.Click += new System.EventHandler(this.wallButton_Click);
            // 
            // eraseButton
            // 
            this.eraseButton.Location = new System.Drawing.Point(136, 414);
            this.eraseButton.Name = "eraseButton";
            this.eraseButton.Size = new System.Drawing.Size(42, 40);
            this.eraseButton.TabIndex = 2;
            this.eraseButton.Text = "None";
            this.eraseButton.UseVisualStyleBackColor = true;
            this.eraseButton.Click += new System.EventHandler(this.eraseButton_Click);
            // 
            // runAlgoButton
            // 
            this.runAlgoButton.Location = new System.Drawing.Point(601, 414);
            this.runAlgoButton.Name = "runAlgoButton";
            this.runAlgoButton.Size = new System.Drawing.Size(96, 39);
            this.runAlgoButton.TabIndex = 3;
            this.runAlgoButton.Text = "Run Algorithm";
            this.runAlgoButton.UseVisualStyleBackColor = true;
            this.runAlgoButton.Click += new System.EventHandler(this.runAlgoButton_Click);
            // 
            // languageComboBox
            // 
            this.languageComboBox.FormattingEnabled = true;
            this.languageComboBox.Items.AddRange(new object[] {
            ".NET",
            "Assembler"});
            this.languageComboBox.Location = new System.Drawing.Point(703, 414);
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.Size = new System.Drawing.Size(99, 21);
            this.languageComboBox.TabIndex = 4;
            // 
            // asmLabel
            // 
            this.asmLabel.AutoSize = true;
            this.asmLabel.Location = new System.Drawing.Point(255, 420);
            this.asmLabel.Name = "asmLabel";
            this.asmLabel.Size = new System.Drawing.Size(55, 13);
            this.asmLabel.TabIndex = 5;
            this.asmLabel.Text = "Asm time: ";
            // 
            // netLabel
            // 
            this.netLabel.AutoSize = true;
            this.netLabel.Location = new System.Drawing.Point(250, 440);
            this.netLabel.Name = "netLabel";
            this.netLabel.Size = new System.Drawing.Size(60, 13);
            this.netLabel.TabIndex = 6;
            this.netLabel.Text = ".NET time: ";
            // 
            // asmTimeLabel
            // 
            this.asmTimeLabel.AutoSize = true;
            this.asmTimeLabel.Location = new System.Drawing.Point(316, 420);
            this.asmTimeLabel.Name = "asmTimeLabel";
            this.asmTimeLabel.Size = new System.Drawing.Size(13, 13);
            this.asmTimeLabel.TabIndex = 5;
            this.asmTimeLabel.Text = "0";
            // 
            // netTimeLabel
            // 
            this.netTimeLabel.AutoSize = true;
            this.netTimeLabel.Location = new System.Drawing.Point(316, 440);
            this.netTimeLabel.Name = "netTimeLabel";
            this.netTimeLabel.Size = new System.Drawing.Size(13, 13);
            this.netTimeLabel.TabIndex = 6;
            this.netTimeLabel.Text = "0";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 500);
            this.Controls.Add(this.netTimeLabel);
            this.Controls.Add(this.netLabel);
            this.Controls.Add(this.asmTimeLabel);
            this.Controls.Add(this.asmLabel);
            this.Controls.Add(this.languageComboBox);
            this.Controls.Add(this.runAlgoButton);
            this.Controls.Add(this.eraseButton);
            this.Controls.Add(this.wallButton);
            this.Controls.Add(this.endButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.mainGrid);
            this.Name = "GUI";
            this.Text = "Dijkstra Pathfinder";
            ((System.ComponentModel.ISupportInitialize)(this.mainGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mainGrid;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button endButton;
        private System.Windows.Forms.Button wallButton;
        private System.Windows.Forms.Button eraseButton;
        private System.Windows.Forms.Button runAlgoButton;
        private System.Windows.Forms.ComboBox languageComboBox;
        private System.Windows.Forms.Label asmLabel;
        private System.Windows.Forms.Label netLabel;
        private System.Windows.Forms.Label asmTimeLabel;
        private System.Windows.Forms.Label netTimeLabel;
    }
}

