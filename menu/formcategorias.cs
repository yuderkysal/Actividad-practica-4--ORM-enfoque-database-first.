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
    public partial class formcategorias : Form
    {
        public formcategorias()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 menu = new Form1();
            this.Hide();
            menu.Show();
        }

        private void formcategorias_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            try
            {
                var categoria = new dblicorstoreEntities();
                var data = categoria.Categorias.ToList();
                dataGridView1.DataSource = data;
                dataGridView1.Refresh();
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
                    var search = db.Categorias.Find(id);
                    var result = new List<Categoria>();
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
            if (string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {

                try
                {
                    var context = new dblicorstoreEntities();
                    var prod = new Categoria
                    {
                        NombreCategoria = textBox2.Text
                    };
                    context.Categorias.Add(prod);
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
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["CategoriaID"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["NombreCategoria"].Value.ToString();
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                int id = Convert.ToInt32(textBox1.Text);
                try
                {
                    var db = new dblicorstoreEntities();
                    var actualizar = db.Categorias.Find(id);

                    DialogResult rs = MessageBox.Show("Desea actualizar esta categoria?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:
                            if (actualizar != null)
                            {
                                actualizar.NombreCategoria = textBox2.Text;
                                db.SaveChanges();
                                cargar();
                                limpiar();

                                MessageBox.Show("Datos correctamente actualizados");
                            }
                            else
                            {
                                MessageBox.Show("categoria no encontrada");
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
                MessageBox.Show("Debe selecionar una categoria del datagriedview para actualizar y verifique que todos los campos estan completos");
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
                    var delete = db.Categorias.Find(id);
                    DialogResult rs = MessageBox.Show("Desea eliminar esta categoria?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:
                            if (delete != null)
                            {
                                db.Categorias.Remove(delete);
                                db.SaveChanges();
                                cargar();
                                limpiar();

                                MessageBox.Show("Categoria correctamente eliminada");
                            }
                            else
                            {
                                MessageBox.Show("categoria no encontrada");
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
                MessageBox.Show("Selecione una categoria del datagriedview para eliminar");
            }
        }
    }
}
