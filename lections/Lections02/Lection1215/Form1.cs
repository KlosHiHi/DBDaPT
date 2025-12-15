using System;
using System.Windows.Forms;

namespace Lection1215
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void gameBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.gameBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.ispp3101DataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "ispp3101DataSet.Category". При необходимости она может быть перемещена или удалена.
            this.categoryTableAdapter.Fill(this.ispp3101DataSet.Category);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "ispp3101DataSet.Game". При необходимости она может быть перемещена или удалена.
            this.gameTableAdapter.Fill(this.ispp3101DataSet.Game);

        }

        private void gameDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gameBindingSource.Filter = "Name LIKE '%" + textBox1.Text + "%'";
        }
    }
}
