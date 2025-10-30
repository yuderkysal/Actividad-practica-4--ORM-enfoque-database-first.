using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace menu
{
    public partial class formclientes : Form
    {
        public formclientes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 menu = new Form1();
            this.Hide();
            menu.Show();
        }

        private void formclientes_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            try
            {
                var cliente = new dblicorstoreEntities();
                var data = cliente.Clientes.ToList();
                dataGridView1.DataSource = data;
                dataGridView1.Refresh();
            }
            catch (Exception p)
            {
                MessageBox.Show($"Error:{p}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cargar();
            limpiar();
        }
        private void limpiar()
        {
            textBox6.Clear();
            foreach (Control ct in groupBox1.Controls)
            {
                if (ct is TextBox)
                {
                    ct.Text = "";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int ts;
            if (int.TryParse(textBox6.Text, out ts))
            {
                int id = Convert.ToInt32(textBox6.Text);
                var db = new dblicorstoreEntities();


                try
                {
                    var search = db.Clientes.Find(id);
                    var result = new List<Cliente>();
                    if (search != null)
                    {
                        result.Add(search);

                    }
                    else
                    {
                        MessageBox.Show("Dato no encontrado");
                    }
                    dataGridView1.DataSource = result;
                    textBox6.Clear();
                    textBox6.Select();
                }
                catch (Exception p)
                {
                    MessageBox.Show($"Error:{p}");
                }
            }
            else
            {
                MessageBox.Show("Digite un valor valido");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
                
                if (string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
                {
                   
                    try {
                        var context = new dblicorstoreEntities();
                    var prod = new Cliente
                    {
                        NombreCompleto = textBox2.Text,
                        CorreoElectronico = textBox3.Text,
                        Telefono=textBox4.Text,
                        Direccion=textBox5.Text
                        };
                        context.Clientes.Add(prod);
                        context.SaveChanges();
                        cargar();
                        limpiar();

                        MessageBox.Show("Datos guardados exitosamente");
                    }
                    catch (Exception p)
                    {
                        MessageBox.Show($"Error:{p}");
                    }

             
                }
                else
                {
                    MessageBox.Show("Favor completar todos los campos, verifique si tiene selecionado un id si es asi utilice el boton limpiar luego registre los datos nuevamente");
                }
            
          
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["ClienteID"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["NombreCompleto"].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["CorreoElectronico"].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells["Direccion"].Value.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                int id = Convert.ToInt32(textBox1.Text);
                try
                {
                    var db = new dblicorstoreEntities();
                    var actualizar = db.Clientes.Find(id);

                    DialogResult rs = MessageBox.Show("Desea actualizar este cliente?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:
                            if (actualizar != null)
                            {
                                actualizar.NombreCompleto = textBox2.Text;
                                actualizar.CorreoElectronico = textBox3.Text;
                                actualizar.Telefono = textBox4.Text;
                                actualizar.Direccion = textBox5.Text;
                                db.SaveChanges();
                                cargar();
                                limpiar();

                                MessageBox.Show("Datos correctamente actualizados");
                            }
                            else
                            {
                                MessageBox.Show("cliente no encontrado");
                            }
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                }
                catch (Exception p)
                {
                    MessageBox.Show($"Error:{p}");
                }


            }
            else
            {
                MessageBox.Show("Debe selecionar un cliente del datagriedview para actualizar y verifique que todos los campos estan completos");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {

                int id = Convert.ToInt32(textBox1.Text);
                try
                {
                    var db = new dblicorstoreEntities();
                    var delete = db.Clientes.Find(id);
                    DialogResult rs = MessageBox.Show("Desea eliminar este cliente?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:
                            if (delete != null)
                            {
                                db.Clientes.Remove(delete);
                                db.SaveChanges();
                                cargar();
                                limpiar();

                                MessageBox.Show("Cliente correctamente eliminado");
                            }
                            else
                            {
                                MessageBox.Show("cliente no encontrado");
                            }
                            break;
                        case DialogResult.Cancel:
                            break;
                    }

                }
                catch (Exception p)
                {
                    MessageBox.Show($"Error:{p}");
                }

            }
            else
            {
                MessageBox.Show("Selecione un cliente del datagriedview para eliminar");
            }
        }
    }
}
