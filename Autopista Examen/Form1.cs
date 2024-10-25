using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autopista_Examen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown); // Enlazar el evento KeyDown
            this.KeyPreview = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Focus();  // Forzar el enfoque al cargar el formulario
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int velocidad = 10; // Velocidad de movimiento del carro

            // Mover a la izquierda
            if (e.KeyCode == Keys.Left)
            {
                if (picCarro.Left > 0) // Limitar para que no salga del borde izquierdo
                {
                    picCarro.Left -= velocidad;
                }
            }

            // Mover a la derecha
            if (e.KeyCode == Keys.Right)
            {
                if (picCarro.Right < panel1.Width) // Limitar para que no salga del borde derecho
                {
                    picCarro.Left += velocidad;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Mover las líneas de la carretera
            pictureBox1.Top += 5;
            pictureBox2.Top += 5;

            // Recolocar las líneas si salen de la pantalla
            if (pictureBox1.Top >= panel1.Height)
                pictureBox1.Top = -pictureBox1.Height;

            if (pictureBox2.Top >= panel1.Height)
                pictureBox2.Top = -pictureBox2.Height;

            // Mover el obstáculo
            moverObstaculo();

            // Detectar colisiones
            detectarColision();
        }

        private void moverObstaculo()
        {
            int velocidadObstaculo = 8; // Velocidad del obstáculo

            // Mover el obstáculo hacia abajo
            pictureBoxObstaculo.Top += velocidadObstaculo;

            // Si el obstáculo sale del panel, recolocarlo en la parte superior
            if (pictureBoxObstaculo.Top >= panel1.Height)
            {
                Random rand = new Random();
                pictureBoxObstaculo.Top = -pictureBoxObstaculo.Height; // Recolocar en la parte superior
                pictureBoxObstaculo.Left = rand.Next(0, panel1.Width - pictureBoxObstaculo.Width); // Posición aleatoria en la carretera
            }
        }

        private void detectarColision()
        {
            // Si el carro colisiona con el obstáculo, detener el juego
            if (picCarro.Bounds.IntersectsWith(pictureBoxObstaculo.Bounds))
            {
                timer1.Stop();
                MessageBox.Show("¡Has chocado! Fin del juego.");
            }
        }
    }
}
