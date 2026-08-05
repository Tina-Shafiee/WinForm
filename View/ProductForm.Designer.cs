namespace View
{
    partial class ProductForm
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
            btnClear = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            btnRefresh = new Button();
            lblTitle = new Label();
            lblUnitPrice = new Label();
            lblQuantity = new Label();
            txtTitle = new TextBox();
            txtUnitPrice = new TextBox();
            txtQuantity = new TextBox();
            dgvProduct = new DataGridView();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProduct).BeginInit();
            SuspendLayout();
            // 
            // BtnClear
            // 
            btnClear.Location = new Point(31, 39);
            btnClear.Name = "BtnClear";
            btnClear.Size = new Size(94, 29);
            btnClear.TabIndex = 0;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += BtnClear_Click;
            // 
            // BtnEdit
            // 
            btnEdit.Location = new Point(191, 39);
            btnEdit.Name = "BtnEdit";
            btnEdit.Size = new Size(94, 29);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += BtnEdit_Click;
            // 
            // BtnDelete
            // 
            btnDelete.Location = new Point(344, 39);
            btnDelete.Name = "BtnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(504, 39);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(669, 39);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(94, 29);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(31, 87);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(38, 20);
            lblTitle.TabIndex = 5;
            lblTitle.Text = "Title";
            // 
            // lblUnitPrice
            // 
            lblUnitPrice.AutoSize = true;
            lblUnitPrice.Location = new Point(31, 129);
            lblUnitPrice.Name = "lblUnitPrice";
            lblUnitPrice.Size = new Size(68, 20);
            lblUnitPrice.TabIndex = 6;
            lblUnitPrice.Text = "UnitPrice";
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(31, 178);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(65, 20);
            lblQuantity.TabIndex = 7;
            lblQuantity.Text = "Quantity";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(179, 84);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(125, 27);
            txtTitle.TabIndex = 8;
            // 
            // txtUnitPrice
            // 
            txtUnitPrice.Location = new Point(179, 126);
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.Size = new Size(125, 27);
            txtUnitPrice.TabIndex = 9;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(179, 171);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(125, 27);
            txtQuantity.TabIndex = 10;
            // 
            // dgvProduct
            // 
            dgvProduct.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProduct.Location = new Point(31, 213);
            dgvProduct.Name = "dgvProduct";
            dgvProduct.RowHeadersWidth = 51;
            dgvProduct.Size = new Size(732, 188);
            dgvProduct.TabIndex = 11;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(669, 409);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(94, 29);
            btnBack.TabIndex = 12;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += BtnBack_Click;
            // 
            // ProductForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkKhaki;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBack);
            Controls.Add(dgvProduct);
            Controls.Add(txtQuantity);
            Controls.Add(txtUnitPrice);
            Controls.Add(txtTitle);
            Controls.Add(lblQuantity);
            Controls.Add(lblUnitPrice);
            Controls.Add(lblTitle);
            Controls.Add(btnRefresh);
            Controls.Add(btnSave);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnClear);
            Name = "ProductForm";
            Text = "ProductForm";
            Load += ProductForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProduct).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnClear;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSave;
        private Button btnRefresh;
        private Label lblTitle;
        private Label lblUnitPrice;
        private Label lblQuantity;
        private TextBox txtTitle;
        private TextBox txtUnitPrice;
        private TextBox txtQuantity;
        private DataGridView dgvProduct;
        private Button btnBack;
    }
}