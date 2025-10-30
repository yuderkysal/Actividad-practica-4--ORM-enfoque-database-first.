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
    public partial class formproductos : Form
    {
        public formproductos()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 menu = new Form1();
            this.Hide();
            menu.Show();
        }

        private void formproductos_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            try
            {
                var producto = new dblicorstoreEntities();
                var data = producto.Productos.ToList();
                dataGridView1.DataSource = data;
                dataGridView1.Refresh();

                //esta parte es para rellenar el combobox con los datos de la tabla categoria
                var combo = producto.Categorias.ToList();
                comboBox1.DataSource = combo;
                comboBox1.DisplayMember = "NombreCategoria";
                comboBox1.ValueMember = "CategoriaID";
            }
            catch (Exception p)
            {
                MessageBox.Show($"Error:{p}");
            }
        }

        private void button5_Click(object sender, EventArgs e)
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
                }else if (ct is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)ct; 
                    comboBox.SelectedIndex = 0; 
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
                    var search = db.Productos.Find(id);
                    var result = new List<Producto>();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox5.Text)&& comboBox1.SelectedValue != null)
            {

                try
                {
                    var context = new dblicorstoreEntities();
                    var prod = new Producto
                    {
                        NombreProducto=textBox2.Text,
                        Precio=Convert.ToDecimal(textBox3.Text),
                        Stock=Convert.ToInt32(textBox4.Text),
                        CategoriaID=Convert.ToInt32(comboBox1.SelectedValue),
                        Descripcion=textBox5.Text
                        
                    };
                    context.Productos.Add(prod);
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
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                var cat = dataGridView1.Rows[e.RowIndex].Cells["CategoriaID"].Value;
                comboBox1.SelectedValue = cat;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox5.Text)&&comboBox1.SelectedValue!=null)
            {
                int id = Convert.ToInt32(textBox1.Text);
                try
                {
                    var db = new dblicorstoreEntities();
                    var actualizar = db.Productos.Find(id);

                    DialogResult rs = MessageBox.Show("Desea actualizar este producto?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:
                            if (actualizar != null)
                            {
                                actualizar.NombreProducto = textBox2.Text;
                                actualizar.Precio=Convert.ToDecimal( textBox3.Text);
                                actualizar.Stock =Convert.ToInt32( textBox4.Text);
                                actualizar.CategoriaID = Convert.ToInt32(comboBox1.SelectedValue);
                                actualizar.Descripcion = textBox5.Text;
                                db.SaveChanges();
                                cargar();
                                limpiar();

                                MessageBox.Show("Datos correctamente actualizados");
                            }
                            else
                            {
                                MessageBox.Show("producto no encontrado");
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
                MessageBox.Show("Debe selecionar un producto del datagriedview para actualizar y verifique que todos los campos estan completos");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }


            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }


            if (e.KeyChar == '.' && textBox1.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }


            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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
                    var delete = db.Productos.Find(id);
                    DialogResult rs = MessageBox.Show("Desea eliminar este producto?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:
                            if (delete != null)
                            {
                                db.Productos.Remove(delete);
                                db.SaveChanges();
                                cargar();
                                limpiar();

                                MessageBox.Show("Producto correctamente eliminado");
                            }
                            else
                            {
                                MessageBox.Show("producto no encontrado");
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
                MessageBox.Show("Selecione un productodel datagriedview para eliminar");
            }
        }
    }
}
