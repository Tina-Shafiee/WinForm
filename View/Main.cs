namespace View
{
    public partial class Main : Form
    {
        public static Main? Instance { get; private set; }
        public Main()
        {
            InitializeComponent();
            Instance = this;  
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            
            ProductForm productForm = new ProductForm();

            productForm.Show();

           
            this.Hide();

        }

        private void btnPerson_Click(object sender, EventArgs e)
        {
            PersonForm personForm = new PersonForm();

            personForm.Show();

            this.Hide();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
