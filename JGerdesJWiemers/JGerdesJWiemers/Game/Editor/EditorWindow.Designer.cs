namespace JGerdesJWiemers.Game.Editor
{
    partial class EditorWindow
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
            this.path = new System.Windows.Forms.TextBox();
            this.load = new System.Windows.Forms.Button();
            this.result = new System.Windows.Forms.TextBox();
            this.width = new System.Windows.Forms.NumericUpDown();
            this.height = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.clipboard = new System.Windows.Forms.Button();
            this.reset = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.height)).BeginInit();
            this.SuspendLayout();
            // 
            // path
            // 
            this.path.Location = new System.Drawing.Point(12, 31);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(162, 20);
            this.path.TabIndex = 0;
            this.path.Text = "asteroids/asteroid1.png";
            // 
            // load
            // 
            this.load.Location = new System.Drawing.Point(353, 29);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(55, 23);
            this.load.TabIndex = 1;
            this.load.Text = "laden";
            this.load.UseVisualStyleBackColor = true;
            this.load.Click += new System.EventHandler(this.load_Click);
            // 
            // result
            // 
            this.result.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.result.Location = new System.Drawing.Point(12, 92);
            this.result.Multiline = true;
            this.result.Name = "result";
            this.result.ReadOnly = true;
            this.result.Size = new System.Drawing.Size(396, 302);
            this.result.TabIndex = 2;
            // 
            // width
            // 
            this.width.Location = new System.Drawing.Point(185, 31);
            this.width.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(68, 20);
            this.width.TabIndex = 3;
            // 
            // height
            // 
            this.height.Location = new System.Drawing.Point(266, 31);
            this.height.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(68, 20);
            this.height.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Breite";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(263, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Höhe";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Dateipfad";
            // 
            // clipboard
            // 
            this.clipboard.Location = new System.Drawing.Point(298, 400);
            this.clipboard.Name = "clipboard";
            this.clipboard.Size = new System.Drawing.Size(110, 23);
            this.clipboard.TabIndex = 8;
            this.clipboard.Text = "in Zwischenablage";
            this.clipboard.UseVisualStyleBackColor = true;
            this.clipboard.Click += new System.EventHandler(this.clipboard_Click);
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(12, 400);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(85, 23);
            this.reset.TabIndex = 9;
            this.reset.Text = "zurücksetzen";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 58);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(71, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "animieren";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // EditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 435);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.clipboard);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.height);
            this.Controls.Add(this.width);
            this.Controls.Add(this.result);
            this.Controls.Add(this.load);
            this.Controls.Add(this.path);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditorWindow";
            this.Text = "Editor";
            ((System.ComponentModel.ISupportInitialize)(this.width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.Button load;
        private System.Windows.Forms.TextBox result;
        private System.Windows.Forms.NumericUpDown width;
        private System.Windows.Forms.NumericUpDown height;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button clipboard;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}