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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.saveDataButton = new System.Windows.Forms.Button();
            this.loadDataButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.clearSolutionButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainGrid
            // 
            this.mainGrid.Location = new System.Drawing.Point(2, 12);
            this.mainGrid.Name = "mainGrid";
            this.mainGrid.Size = new System.Drawing.Size(800, 484);
            this.mainGrid.TabIndex = 0;
            this.mainGrid.TabStop = false;
            this.mainGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainGrid_MouseClick);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(14, 109);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(97, 40);
            this.clearButton.TabIndex = 1;
            this.clearButton.Text = "Delete Map";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(8, 19);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(42, 40);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // endButton
            // 
            this.endButton.Location = new System.Drawing.Point(69, 19);
            this.endButton.Name = "endButton";
            this.endButton.Size = new System.Drawing.Size(42, 40);
            this.endButton.TabIndex = 2;
            this.endButton.Text = "End";
            this.endButton.UseVisualStyleBackColor = true;
            this.endButton.Click += new System.EventHandler(this.endButton_Click);
            // 
            // wallButton
            // 
            this.wallButton.Location = new System.Drawing.Point(8, 65);
            this.wallButton.Name = "wallButton";
            this.wallButton.Size = new System.Drawing.Size(42, 40);
            this.wallButton.TabIndex = 2;
            this.wallButton.Text = "Wall";
            this.wallButton.UseVisualStyleBackColor = true;
            this.wallButton.Click += new System.EventHandler(this.wallButton_Click);
            // 
            // eraseButton
            // 
            this.eraseButton.Location = new System.Drawing.Point(67, 65);
            this.eraseButton.Name = "eraseButton";
            this.eraseButton.Size = new System.Drawing.Size(42, 40);
            this.eraseButton.TabIndex = 2;
            this.eraseButton.Text = "None";
            this.eraseButton.UseVisualStyleBackColor = true;
            this.eraseButton.Click += new System.EventHandler(this.eraseButton_Click);
            // 
            // runAlgoButton
            // 
            this.runAlgoButton.Location = new System.Drawing.Point(15, 18);
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
            this.languageComboBox.Location = new System.Drawing.Point(6, 19);
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.Size = new System.Drawing.Size(96, 21);
            this.languageComboBox.TabIndex = 4;
            // 
            // asmLabel
            // 
            this.asmLabel.AutoSize = true;
            this.asmLabel.Location = new System.Drawing.Point(12, 25);
            this.asmLabel.Name = "asmLabel";
            this.asmLabel.Size = new System.Drawing.Size(36, 13);
            this.asmLabel.TabIndex = 5;
            this.asmLabel.Text = "Asm : ";
            // 
            // netLabel
            // 
            this.netLabel.AutoSize = true;
            this.netLabel.Location = new System.Drawing.Point(7, 45);
            this.netLabel.Name = "netLabel";
            this.netLabel.Size = new System.Drawing.Size(41, 13);
            this.netLabel.TabIndex = 6;
            this.netLabel.Text = ".NET : ";
            // 
            // asmTimeLabel
            // 
            this.asmTimeLabel.AutoSize = true;
            this.asmTimeLabel.Location = new System.Drawing.Point(54, 25);
            this.asmTimeLabel.Name = "asmTimeLabel";
            this.asmTimeLabel.Size = new System.Drawing.Size(13, 13);
            this.asmTimeLabel.TabIndex = 5;
            this.asmTimeLabel.Text = "0";
            // 
            // netTimeLabel
            // 
            this.netTimeLabel.AutoSize = true;
            this.netTimeLabel.Location = new System.Drawing.Point(54, 45);
            this.netTimeLabel.Name = "netTimeLabel";
            this.netTimeLabel.Size = new System.Drawing.Size(13, 13);
            this.netTimeLabel.TabIndex = 6;
            this.netTimeLabel.Text = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.wallButton);
            this.groupBox1.Controls.Add(this.startButton);
            this.groupBox1.Controls.Add(this.endButton);
            this.groupBox1.Controls.Add(this.eraseButton);
            this.groupBox1.Location = new System.Drawing.Point(808, 207);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(118, 116);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Drawing Options";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.asmLabel);
            this.groupBox2.Controls.Add(this.asmTimeLabel);
            this.groupBox2.Controls.Add(this.netLabel);
            this.groupBox2.Controls.Add(this.netTimeLabel);
            this.groupBox2.Location = new System.Drawing.Point(808, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(118, 76);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Execution Times";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.clearSolutionButton);
            this.groupBox3.Controls.Add(this.runAlgoButton);
            this.groupBox3.Controls.Add(this.clearButton);
            this.groupBox3.Location = new System.Drawing.Point(808, 386);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(118, 161);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Run";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.saveDataButton);
            this.groupBox4.Controls.Add(this.loadDataButton);
            this.groupBox4.Location = new System.Drawing.Point(808, 88);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(118, 113);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Files";
            // 
            // saveDataButton
            // 
            this.saveDataButton.Location = new System.Drawing.Point(6, 65);
            this.saveDataButton.Name = "saveDataButton";
            this.saveDataButton.Size = new System.Drawing.Size(96, 39);
            this.saveDataButton.TabIndex = 12;
            this.saveDataButton.Text = "Save Map";
            this.saveDataButton.UseVisualStyleBackColor = true;
            this.saveDataButton.Click += new System.EventHandler(this.saveDataBtn_Click);
            // 
            // loadDataButton
            // 
            this.loadDataButton.Location = new System.Drawing.Point(6, 19);
            this.loadDataButton.Name = "loadDataButton";
            this.loadDataButton.Size = new System.Drawing.Size(97, 40);
            this.loadDataButton.TabIndex = 11;
            this.loadDataButton.Text = "Load Map";
            this.loadDataButton.UseVisualStyleBackColor = true;
            this.loadDataButton.Click += new System.EventHandler(this.loadDataBtn_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.languageComboBox);
            this.groupBox5.Location = new System.Drawing.Point(808, 329);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(118, 51);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Select Version";
            // 
            // clearSolutionButton
            // 
            this.clearSolutionButton.Location = new System.Drawing.Point(14, 63);
            this.clearSolutionButton.Name = "clearSolutionButton";
            this.clearSolutionButton.Size = new System.Drawing.Size(97, 40);
            this.clearSolutionButton.TabIndex = 4;
            this.clearSolutionButton.Text = "Clear Solution";
            this.clearSolutionButton.UseVisualStyleBackColor = true;
            this.clearSolutionButton.Click += new System.EventHandler(this.clearSolutionButton_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(931, 559);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mainGrid);
            this.Name = "GUI";
            this.Text = "Dijkstra Pathfinder";
            ((System.ComponentModel.ISupportInitialize)(this.mainGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button saveDataButton;
        private System.Windows.Forms.Button loadDataButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button clearSolutionButton;
    }
}

