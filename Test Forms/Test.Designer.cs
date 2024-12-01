namespace Test_Forms
{
    partial class Test
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
            button1 = new Button();
            btnProcessFile = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(30, 30);
            button1.Name = "button1";
            button1.Size = new Size(76, 33);
            button1.TabIndex = 0;
            button1.Text = "User Name";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnProcessFile
            // 
            btnProcessFile.Location = new Point(30, 69);
            btnProcessFile.Name = "btnProcessFile";
            btnProcessFile.Size = new Size(76, 33);
            btnProcessFile.TabIndex = 1;
            btnProcessFile.Text = "Process File";
            btnProcessFile.UseVisualStyleBackColor = true;
            btnProcessFile.Click += btnProcessFile_Click;
            // 
            // Test
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnProcessFile);
            Controls.Add(button1);
            Name = "Test";
            Text = "Test";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button btnProcessFile;
    }
}