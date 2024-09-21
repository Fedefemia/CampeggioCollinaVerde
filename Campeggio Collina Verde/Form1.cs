using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Campeggio_Collina_Verde
{
    public partial class Form1 : Form
    {
        private int postiliberi = 0;
        private List<Posti> listaPosti = new List<Posti>();

        public Form1()
        {
            InitializeComponent();
            dataGrid();
        }

        public class Posti
        {
            public string NomePosto { get; set; }
            public string TargaPosto { get; set; }
            public DateTime Data { get; set; }

            public Posti(string nome, string tposto, DateTime date)
            {
                NomePosto = nome;
                TargaPosto = tposto;
                Data = date;
            }
        }
        private void dataGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 3;

            dataGridView1.Columns[0].Name = "NomePosto";
            dataGridView1.Columns[0].HeaderText = "Nome Posto";
            dataGridView1.Columns[0].DataPropertyName = "NomePosto";

            dataGridView1.Columns[1].Name = "TargaPosto";
            dataGridView1.Columns[1].HeaderText = "Targa Posto";
            dataGridView1.Columns[1].DataPropertyName = "TargaPosto";

            dataGridView1.Columns[2].Name = "Data";
            dataGridView1.Columns[2].HeaderText = "Data";
            dataGridView1.Columns[2].DataPropertyName = "Data";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (postiliberi < 20)
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    Posti nuovoPosto = new Posti(textBox1.Text, textBox2.Text, dateTimePicker1.Value);
                    listaPosti.Add(nuovoPosto);
                    postiliberi++;
                    textBox1.Clear();
                    textBox2.Clear();
                    dateTimePicker1.Value = DateTime.Now;
                    aggiorna();
                }
            }
            else
            {
                MessageBox.Show("I posti sono finiti");
            }
        }

        private void aggiorna()
        {
            comboBox1.Items.Clear();
            foreach (Posti posto in listaPosti)
            {
                comboBox1.Items.Add(posto.TargaPosto + " - " + posto.NomePosto);
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listaPosti;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Posti foundPosto = null;
            foreach (Posti posto in listaPosti)
            {
                string completa = posto.TargaPosto + " - " + posto.NomePosto;
                if (completa == comboBox1.Text)
                {
                    int datetot = dateTimePicker2.Value.Day - posto.Data.Day;
                    comboBox1.Items.Remove(completa);
                    datetot *= 25;
                    MessageBox.Show("Piazzola liberata con successo! costo: " + datetot);
                    foundPosto = posto;
                }
            }

            if (foundPosto != null)
            {
                listaPosti.Remove(foundPosto);
                postiliberi--;
                aggiorna();
            }
            comboBox1.Text = "";
        }
    }
}
