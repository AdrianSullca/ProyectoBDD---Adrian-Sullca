namespace ProjecteBBDD
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
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 41);
            label1.Name = "label1";
            label1.Size = new Size(96, 20);
            label1.TabIndex = 0;
            label1.Text = "Cargar Datos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 80);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 1;
            label2.Text = "Fichero Txt";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(31, 114);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(731, 27);
            textBox1.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(31, 160);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 3;
            button1.Text = "Cargar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += accionCargarBoton;
            // 
            // button2
            // 
            button2.Location = new Point(668, 160);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 4;
            button2.Text = "Crear Xml";
            button2.UseVisualStyleBackColor = true;
            button2.Click += accionCrearXmlBoton;
            // 
            // button3
            // 
            button3.Location = new Point(556, 160);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 5;
            button3.Text = "Reset BD";
            button3.UseVisualStyleBackColor = true;
            button3.Click += accionResetBD;
            // 
            // button4
            // 
            button4.Location = new Point(624, 32);
            button4.Name = "button4";
            button4.Size = new Size(138, 29);
            button4.TabIndex = 6;
            button4.Text = "Stored Procedure";
            button4.UseVisualStyleBackColor = true;
            button4.Click += accionStoredProcedure;
            // 
            // button5
            // 
            button5.Location = new Point(491, 32);
            button5.Name = "button5";
            button5.Size = new Size(94, 29);
            button5.TabIndex = 7;
            button5.Text = "SP Backup";
            button5.UseVisualStyleBackColor = true;
            button5.Click += accionSPBackupBoton;
            // 
            // button6
            // 
            button6.Location = new Point(373, 32);
            button6.Name = "button6";
            button6.Size = new Size(94, 29);
            button6.TabIndex = 8;
            button6.Text = "Trigger";
            button6.UseVisualStyleBackColor = true;
            button6.Click += accionTriggerBoton;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 201);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
    }
}
