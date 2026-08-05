using ApplicationService;
using ApplicationService.Dtos;

namespace View
{
    public partial class ProductForm : Form
    {
        private readonly ProductApplicationService _productApplicationService;
        private bool _isLoadingData = false;

        public ProductForm()
        {
            InitializeComponent();
            _productApplicationService = new ProductApplicationService();
        }

        private void RefreshData()
        {
            _isLoadingData = true;
            dgvProduct.DataSource = _productApplicationService.GetAllProduct();
            dgvProduct.ClearSelection();
            ClearTextboxes();
            _isLoadingData = false;
            UpdateButtonStates();
        }

        private void BtnRefresh_Click(object? sender, EventArgs? e)
        {
            RefreshData();
            MessageBox.Show("Fetching is Done!");
        }

        private bool IsValidTitle(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            foreach (char c in input)
            {
                if (!char.IsLetter(c) && c != ' ')
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsValidUnitPrice(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            if (int.TryParse(input, out int price))
            {
                return price > 0;
            }
            return false;
        }

        private bool IsValidQuantity(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            if (int.TryParse(input, out int quantity))
            {
                return quantity >= 0;
            }
            return false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string unitPrice = txtUnitPrice.Text.Trim();
            string quantity = txtQuantity.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(unitPrice) ||
                string.IsNullOrWhiteSpace(quantity))
            {
                MessageBox.Show("All fields (Title, Unit Price, and Quantity) are required!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidTitle(title))
            {
                MessageBox.Show("Title can only contain letters and spaces. Numbers and special characters are not allowed!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidUnitPrice(unitPrice))
            {
                MessageBox.Show("Unit Price must be a valid positive number (e.g., 10, 10.50, 100.99).",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Quantity
            if (!IsValidQuantity(quantity))
            {
                MessageBox.Show("Quantity must be a valid positive whole number (e.g., 1, 5, 100).",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var postProductDto = new PostProductDto()
                {
                    Title = title,
                    UnitPrice = Convert.ToInt32(unitPrice),
                    Quantity = Convert.ToInt32(quantity),
                };

                _productApplicationService.PostProduct(postProductDto);
                MessageBox.Show("Saving is Done!");
                RefreshData();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid number format. Please check your input.",
                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Number is too large or too small.",
                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Main.Instance.Show();
            this.Close();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProduct.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product from the list.",
                    "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvProduct.SelectedRows[0];
            var idCell = selectedRow.Cells["Id"];
            if (idCell?.Value == null)
            {
                MessageBox.Show("Invalid product selection.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedId = Convert.ToInt32(idCell.Value);
            var confirmResult = MessageBox.Show($"Are you sure you want to delete the product with ID: {selectedId}?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var deleteProductDto = new DeleteProductDto { Id = selectedId };
                    _productApplicationService.DeleteProduct(deleteProductDto);
                    RefreshData();
                    MessageBox.Show("Product deleted successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            dgvProduct.MultiSelect = false;
            dgvProduct.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvProduct.SelectionChanged -= DgvProduct_SelectionChanged!;
            dgvProduct.SelectionChanged += DgvProduct_SelectionChanged;

            txtTitle.TextChanged += TextBox_TextChanged!;
            txtUnitPrice.TextChanged += TextBox_TextChanged!;
            txtQuantity.TextChanged += TextBox_TextChanged!;

            txtTitle.KeyPress += TxtTitle_KeyPress!;
            txtUnitPrice.KeyPress += TxtUnitPrice_KeyPress!;
            txtQuantity.KeyPress += TxtQuantity_KeyPress!;

            //RefreshData();
            UpdateButtonStates();
        }

        private void TxtTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void TxtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == '.' && (sender as TextBox)!.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void TxtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void DgvProduct_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isLoadingData) return;

            if (dgvProduct.SelectedRows.Count > 0)
            {
                txtTitle.TextChanged -= TextBox_TextChanged!;
                txtUnitPrice.TextChanged -= TextBox_TextChanged!;
                txtQuantity.TextChanged -= TextBox_TextChanged!;

                var selectedRow = dgvProduct.SelectedRows[0];
                txtTitle.Text = selectedRow.Cells["Title"]?.Value?.ToString() ?? "";
                txtUnitPrice.Text = selectedRow.Cells["UnitPrice"]?.Value?.ToString() ?? "";
                txtQuantity.Text = selectedRow.Cells["Quantity"]?.Value?.ToString() ?? "";

                txtTitle.TextChanged += TextBox_TextChanged!;
                txtUnitPrice.TextChanged += TextBox_TextChanged!;
                txtQuantity.TextChanged += TextBox_TextChanged!;
            }

            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            bool isRowSelected = dgvProduct.SelectedRows.Count > 0;

            string title = txtTitle.Text.Trim();
            string unitPrice = txtUnitPrice.Text.Trim();
            string quantity = txtQuantity.Text.Trim();

            bool isTitleFilled = !string.IsNullOrWhiteSpace(title) && IsValidTitle(title);
            bool isUnitPriceFilled = !string.IsNullOrWhiteSpace(unitPrice) && IsValidUnitPrice(unitPrice);
            bool isQuantityFilled = !string.IsNullOrWhiteSpace(quantity) && IsValidQuantity(quantity);

            bool areAllTextboxesFull = isTitleFilled && isUnitPriceFilled && isQuantityFilled;
            bool areAllTextboxesEmpty = string.IsNullOrWhiteSpace(txtTitle.Text) &&
                                        string.IsNullOrWhiteSpace(txtUnitPrice.Text) &&
                                        string.IsNullOrWhiteSpace(txtQuantity.Text);

            btnSave.Enabled = areAllTextboxesFull && !isRowSelected;

            btnDelete.Enabled = isRowSelected;

            btnEdit.Enabled = isRowSelected && areAllTextboxesFull;

            btnClear.Enabled = !areAllTextboxesEmpty;

            btnRefresh.Enabled = true;

            bool isPartiallyFilled = (isTitleFilled || isUnitPriceFilled || isQuantityFilled) && !areAllTextboxesFull;
            if (isPartiallyFilled)
            {
                btnSave.Enabled = false;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearTextboxes();
            dgvProduct.ClearSelection();
            UpdateButtonStates();
        }

        private void ClearTextboxes()
        {
            txtTitle.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProduct.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a Product from the list.",
                    "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string title = txtTitle.Text.Trim();
            string unitPrice = txtUnitPrice.Text.Trim();
            string quantity = txtQuantity.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(unitPrice) ||
                string.IsNullOrWhiteSpace(quantity))
            {
                MessageBox.Show("All fields (Title, Unit Price, and Quantity) must be filled to edit.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidTitle(title))
            {
                MessageBox.Show("Title can only contain letters and spaces. Numbers and special characters are not allowed!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidUnitPrice(unitPrice))
            {
                MessageBox.Show("Unit Price must be a valid positive number (e.g., 10, 10.50, 100.99).",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidQuantity(quantity))
            {
                MessageBox.Show("Quantity must be a valid positive whole number (e.g., 1, 5, 100).",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvProduct.SelectedRows[0];
            var idCell = selectedRow.Cells["Id"];
            if (idCell?.Value == null)
            {
                MessageBox.Show("Invalid product selection.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedId = Convert.ToInt32(idCell.Value);

            var confirmResult = MessageBox.Show($"Are you sure you want to Edit this product with ID: {selectedId}?",
                                                "Confirm Edit",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var updateProductDto = new UpdateProductDto()
                    {
                        Id = selectedId,
                        Title = title,
                        UnitPrice = Convert.ToInt32(unitPrice),
                        Quantity = Convert.ToInt32(quantity),
                    };

                    _productApplicationService.UpdateProduct(updateProductDto);
                    RefreshData();
                    MessageBox.Show("Product updated successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid number format. Please check your input.",
                        "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Number is too large or too small.",
                        "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ProductForm_Click(object sender, EventArgs e)
        {
            dgvProduct.ClearSelection();
            ClearTextboxes();
            UpdateButtonStates();
        }
    }
}

