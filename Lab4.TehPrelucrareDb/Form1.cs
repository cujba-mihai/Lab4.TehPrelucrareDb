namespace Lab4.TehPrelucrareDb
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows.Forms;

        public partial class Form1 : Form
        {
            private AdventureWorksDataAccess _dataAccess;
            private BindingSource _bindingSource = new BindingSource();

            public Form1()
            {
                InitializeComponent();
            }

            private void Form1_Load(object sender, EventArgs e)
            {
                comboBoxTables.Items.AddRange(AdventureWorksDataAccess.GetTableNames());
                if (comboBoxTables.Items.Count > 0)
                    comboBoxTables.SelectedIndex = 0;
            }

            private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (comboBoxTables.SelectedIndex != -1)
                {
                    string selectedTable = comboBoxTables.SelectedItem.ToString();
                    _dataAccess = new AdventureWorksDataAccess(selectedTable);
                    _bindingSource.DataSource = _dataAccess.DataTable;
                    dataGridView1.DataSource = _bindingSource;
                }
            }


            private void updateButton_Click(object sender, EventArgs e)
            {
                _dataAccess.CommitChanges();
            }

            private void deleteButton_Click(object sender, EventArgs e)
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var result = MessageBox.Show("Deleting this record will also delete all related subcategory records. Continue?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            _bindingSource.RemoveCurrent();
                            _dataAccess.CommitChanges();
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 547)
                            {
                                MessageBox.Show("This record cannot be deleted because it is referenced by other data.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }
            }

            private void saveButton_Click(object sender, EventArgs e)
            {
                _dataAccess.CommitChanges();

            }

        }
}