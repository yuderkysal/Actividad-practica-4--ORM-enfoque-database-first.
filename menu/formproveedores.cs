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
    public partial class formproveedores : Form
    {
        public formproveedores()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 menu = new Form1();
            this.Hide();
            menu.Show();
        }

        private void formproveedores_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            try
            {
                var cliente = new dblicorstoreEntities();
                var data = cliente.Proveedores.ToList();
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
                    var search = db.Proveedores.Find(id);
                    var result = new List<Proveedore>();
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
            if (string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) )
            {

                try
                {
                    var context = new dblicorstoreEntities();
                    var prod = new Proveedore
                    {
                        NombreProveedor = textBox2.Text,
                        Telefono=textBox4.Text,
                        CorreoElectronico=textBox3.Text

                    };
                    context.Proveedores.Add(prod);
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
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["ProveedorID"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["NombreProveedor"].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["CorreoElectronico"].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                int id = Convert.ToInt32(textBox1.Text);
                try
                {
                    var db = new dblicorstoreEntities();
                    var actualizar = db.Proveedores.Find(id);

                    DialogResult rs = MessageBox.Show("Desea actualizar este proveedor?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:
                            if (actualizar != null)
                            {
                                actualizar.NombreProveedor = textBox2.Text;
                                actualizar.CorreoElectronico = textBox3.Text;
                                actualizar.Telefono = textBox4.Text;
                                db.SaveChanges();
                                cargar();
                                limpiar();

                                MessageBox.Show("Datos correctamente actualizados");
                            }
                            else
                            {
                                MessageBox.Show("proveedor no encontrado");
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
                MessageBox.Show("Debe selecionar un proveedor del datagriedview para actualizar y verifique que todos los campos estan completos");
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
                    var delete = db.Proveedores.Find(id);
                    DialogResult rs = MessageBox.Show("Desea eliminar este proveedor?", "Aviso", MessageBoxButtons.OKCancel);
                    switch (rs)
                    {
                        case DialogResult.OK:
                            if (delete != null)
                            {
                                db.Proveedores.Remove(delete);
                                db.SaveChanges();
                                cargar();
                                limpiar();

                                MessageBox.Show("Proveedor correctamente eliminado");
                            }
                            else
                            {
                                MessageBox.Show("proveedor no encontrado");
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
                MessageBox.Show("Selecione un proveedor del datagriedview para eliminar");
            }
        }
    }
}
