namespace View
{
    partial class Main
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
            btnPerson = new Button();
            btnProduct = new Button();
            SuspendLayout();
            // 
            // btnPerson
            // 
            btnPerson.BackColor = Color.Khaki;
            btnPerson.Location = new Point(75, 117);
            btnPerson.Name = "btnPerson";
            btnPerson.Size = new Size(298, 179);
            btnPerson.TabIndex = 0;
            btnPerson.Text = "Person";
            btnPerson.UseVisualStyleBackColor = false;
            btnPerson.Click += btnPerson_Click;
            // 
            // btnProduct
            // 
            btnProduct.BackColor = Color.Khaki;
            btnProduct.Location = new Point(426, 117);
            btnProduct.Name = "btnProduct";
            btnProduct.Size = new Size(283, 179);
            btnProduct.TabIndex = 1;
            btnProduct.Text = "Product";
            btnProduct.UseVisualStyleBackColor = false;
            btnProduct.Click += btnProduct_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(btnProduct);
            Controls.Add(btnPerson);
            Name = "Main";
            Text = "Main";
            Load += Main_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnPerson;
        private Button btnProduct;
    }
}