using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace menu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Aqua;
            button1.Cursor = Cursors.Hand;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Black;
            button1.Cursor = Cursors.Default;
        }
        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.Aqua;
            button2.Cursor = Cursors.Hand;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Black;
            button2.Cursor = Cursors.Default;
        }
        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.Aqua;
            button3.Cursor = Cursors.Hand;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.Black;
            button3.Cursor = Cursors.Default;
        }
        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.BackColor = Color.Aqua;
            button4.Cursor = Cursors.Hand;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackColor = Color.Black;
            button4.Cursor = Cursors.Default;
        }
        ToolTip tp = new ToolTip();
        private void button5_MouseEnter(object sender, EventArgs e)
        {
       
            tp.SetToolTip(button5,"Cerrar Ventana");
            tp.AutoPopDelay = 2000;
            tp.InitialDelay = 1000;
            tp.ReshowDelay = 500;

            button5.BackColor = Color.Red;
            button5.ForeColor = Color.White;
            button5.Cursor = Cursors.Hand;
            
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackColor = Color.White;
            button5.ForeColor = Color.Black;
            button5.Cursor = Cursors.Default;
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formcategorias categoria = new formcategorias();
            this.Hide();
            categoria.Show();

        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            tp.SetToolTip(button6, "Minimizar Ventana");
            tp.AutoPopDelay = 2000;
            tp.InitialDelay = 1000;
            tp.ReshowDelay = 500;
            button6.Cursor = Cursors.Hand;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.Cursor = Cursors.Default;
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            formclientes cliente = new formclientes();
            this.Hide();
            cliente.Show();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            tp.SetToolTip(pictureBox1, "Haga clik para visitar Git Hub.");
            tp.AutoPopDelay = 5000;
            tp.InitialDelay = 1000;
            tp.ReshowDelay = 500;
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Default;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formproveedores proveedores = new formproveedores();
            this.Hide();
            proveedores.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            formproductos producto = new formproductos();
            this.Hide();
            producto.Show();
        }
    }
}
