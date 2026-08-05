using ApplicationService;
using ApplicationService.Dtos;

namespace View
{
    public partial class PersonForm : Form
    {
        private readonly PersonApplicationService _personApplicationService;
        private bool _isLoadingData = false;

        public PersonForm()
        {
            InitializeComponent();
            _personApplicationService = new PersonApplicationService();
        }

        private void RefreshData()
        {
            _isLoadingData = true;
            dgvPerson.DataSource = _personApplicationService.GetAllPerson();
            dgvPerson.ClearSelection();
            ClearTextboxes();
            _isLoadingData = false;
            UpdateButtonStates();
        }

        private void BtnRefresh_Click(object? sender, EventArgs? e)
        {
            RefreshData();
            MessageBox.Show("Fetching is Done!");
        }

        private bool IsValidName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            foreach (char c in input)
            {
                if (!char.IsLetter(c) && c != ' ' && c != '-' && c != '\'')
                {
                    return false;
                }
            }
            return true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();

            // Check if fields are empty
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Both First Name and Last Name are required!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate First Name
            if (!IsValidName(firstName))
            {
                MessageBox.Show("First Name can only contain letters, spaces, hyphens, and apostrophes. Numbers are not allowed!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Last Name
            if (!IsValidName(lastName))
            {
                MessageBox.Show("Last Name can only contain letters, spaces, hyphens, and apostrophes. Numbers are not allowed!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var postPersonDto = new PostPersonDto()
            {
                FirstName = firstName,
                LastName = lastName,
            };

            _personApplicationService.PostPerson(postPersonDto);
            MessageBox.Show("Saving is Done!");
            RefreshData();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Main.Instance.Show();
            this.Close();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPerson.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a person from the list.",
                    "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvPerson.SelectedRows[0];
            var idCell = selectedRow.Cells["Id"];
            if (idCell?.Value == null)
            {
                MessageBox.Show("Invalid person selection.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedId = Convert.ToInt32(idCell.Value);

            var confirmResult = MessageBox.Show($"Are you sure you want to delete the person with ID: {selectedId}?",
                                                "Confirm Deletion",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                var deletePersonDto = new DeletePersonDto { Id = selectedId };
                _personApplicationService.DeletePerson(deletePersonDto);
                RefreshData();
                MessageBox.Show("Person deleted successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PersonForm_Load(object sender, EventArgs e)
        {
            dgvPerson.MultiSelect = false;
            dgvPerson.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvPerson.SelectionChanged -= DgvPerson_SelectionChanged!;
            dgvPerson.SelectionChanged += DgvPerson_SelectionChanged;

            txtFirstName.TextChanged += TextBox_TextChanged!;
            txtLastName.TextChanged += TextBox_TextChanged!;

            txtFirstName.KeyPress += TxtName_KeyPress!;
            txtLastName.KeyPress += TxtName_KeyPress!;

            RefreshData();
            UpdateButtonStates();
        }

        private void TxtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != '\'')
            {
                e.Handled = true; 
               
            }
        }

        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void DgvPerson_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isLoadingData) return;

            if (dgvPerson.SelectedRows.Count > 0)
            {
                txtFirstName.TextChanged -= TextBox_TextChanged!;
                txtLastName.TextChanged -= TextBox_TextChanged!;

                var selectedRow = dgvPerson.SelectedRows[0];
                txtFirstName.Text = selectedRow.Cells["FirstName"]?.Value?.ToString() ?? "";
                txtLastName.Text = selectedRow.Cells["LastName"]?.Value?.ToString() ?? "";

                txtFirstName.TextChanged += TextBox_TextChanged!;
                txtLastName.TextChanged += TextBox_TextChanged!;
            }

            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            bool isRowSelected = dgvPerson.SelectedRows.Count > 0;

            bool isFirstNameFilled = !string.IsNullOrWhiteSpace(txtFirstName.Text);
            bool isLastNameFilled = !string.IsNullOrWhiteSpace(txtLastName.Text);
            bool areBothTextboxesFull = isFirstNameFilled && isLastNameFilled;
            bool areBothTextboxesEmpty = string.IsNullOrWhiteSpace(txtFirstName.Text) &&
                                        string.IsNullOrWhiteSpace(txtLastName.Text);

            btnSave.Enabled = areBothTextboxesFull && !isRowSelected;

            btnDelete.Enabled = isRowSelected;

            btnEdit.Enabled = isRowSelected && areBothTextboxesFull;

            btnClear.Enabled = !areBothTextboxesEmpty;

            btnRefresh.Enabled = true;

            bool isPartiallyFilled = (isFirstNameFilled || isLastNameFilled) && !areBothTextboxesFull;
            if (isPartiallyFilled)
            {
                btnSave.Enabled = false;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearTextboxes();
            dgvPerson.ClearSelection();
            UpdateButtonStates();
        }

        private void ClearTextboxes()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPerson.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a Person from the list.",
                    "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Both First Name and Last Name must be filled to edit.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidName(firstName))
            {
                MessageBox.Show("First Name can only contain letters, spaces, hyphens, and apostrophes. Numbers are not allowed!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidName(lastName))
            {
                MessageBox.Show("Last Name can only contain letters, spaces, hyphens, and apostrophes. Numbers are not allowed!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvPerson.SelectedRows[0];
            var idCell = selectedRow.Cells["Id"];
            if (idCell?.Value == null)
            {
                MessageBox.Show("Invalid person selection.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedId = Convert.ToInt32(idCell.Value);

            var confirmResult = MessageBox.Show($"Are you sure you want to Edit this person with ID: {selectedId}?",
                                                "Confirm Edit",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                var updatePersonDto = new UpdatePersonDto()
                {
                    Id = selectedId,
                    FirstName = firstName,
                    LastName = lastName,
                };

                _personApplicationService.UpdatePerson(updatePersonDto);
                RefreshData();
                MessageBox.Show("Person updated successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PersonForm_Click(object sender, EventArgs e)
        {
            dgvPerson.ClearSelection();
            ClearTextboxes();
            UpdateButtonStates();
        }
    }
}