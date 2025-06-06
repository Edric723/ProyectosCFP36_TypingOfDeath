namespace Typing_Death
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Instanciar una lista de etiquetas.
        List<Label> etiquetas = new List<Label>();

        private void Form1_Load(object sender, EventArgs e)
        {
            // Detectas teclas
            this.KeyPreview = true; 

            timer1.Interval = 50;
            timer1.Start();

            // Agrego las etiquetas a la lista.
            for (int i = 1; i <= 10; i++)
            {
                Label? etiqueta = this.Controls["label" + i.ToString()] as Label;
                if (etiqueta != null)
                    etiquetas.Add(etiqueta);
            }

            // Asignación de texto.
            label1.Text = "Celeste";
            label2.Text = "Naranja";
            label3.Text = "Violeta";
            label4.Text = "Amarillo";
            label5.Text = "Colorado";
            label6.Text = "Rosado";
            label7.Text = "Marrón";
            label8.Text = "Azulado";
            label9.Text = "Verdoso";
            label10.Text = "Negro";


            //// Fuente
            //label1.Font = new Font("Arial", 32, FontStyle.Bold);
            // Etiqueta size
            //label1.Size = new Size(400, 100);


            // BackColor
            label1.BackColor = Color.Blue;
            label2.BackColor = Color.Yellow;
            label3.BackColor = Color.Red;
            label4.BackColor = Color.Green;
            label5.BackColor = Color.Green;
            label6.BackColor = Color.Red;
            label7.BackColor = Color.Green;
            label8.BackColor = Color.Green;
            label9.BackColor = Color.Green; 
            label10.BackColor = Color.Green;

            // TextColor
            label1.ForeColor = Color.Yellow;
            label2.ForeColor = Color.Red;
            label3.ForeColor = Color.Green;
            label4.ForeColor = Color.Violet;
            //label5.ForeColor = Color.Green;
            //label6.ForeColor = Color.Green;
            //label7.ForeColor = Color.Green;
            //label8.ForeColor = Color.Green;
            //label9.ForeColor = Color.Green;
            //label10.ForeColor = Color.Green;    

            
            // Random para generar distintas alturas de spawn
            Random rng = new Random();

            // Bucle que define la altura y el lateral por el que aparece la etiqueta.
            foreach (Label etiqueta in etiquetas)
            {
                etiqueta.Font = new Font("Arial", 32, FontStyle.Bold);
                etiqueta.Size = new Size(400, 100);
                etiqueta.Visible = false;
                etiqueta.Top = rng.Next(0, this.ClientSize.Height - etiqueta.Height);
                etiqueta.Left = this.ClientSize.Width;
            }

            // Solamente visible la primera etiqueta.
            if (etiquetas.Count > 0)
                etiquetas[0].Visible = true;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Label etiquetaActual = etiquetas[0];
            if (etiquetaActual != null)
            {
                etiquetaActual.Left -= 25; // Velocidad hacia la Izq

                if (etiquetaActual.Right <= 0) // Si llega al borde Izq, ya no se ve
                {
                    timer1.Stop();
                    MessageBox.Show("¡Estuviste muy lenteja, probá de nuevo!");
                    this.Close();
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (etiquetas.Count == 0) return;

            // La actual es la primera de la lista siempre.
            Label etiquetaActual = etiquetas[0];

            if (etiquetaActual != null && etiquetaActual.Text.Length > 0)
            {
                char tecla = char.ToLower(e.KeyChar);
                char letraEsperada = char.ToLower(etiquetaActual.Text[0]);


                // Si es la letra correcta...
                if (tecla == letraEsperada)
                {
                    // Borrar la letra
                    etiquetaActual.Text = etiquetaActual.Text.Substring(1);

                    // Si no quedan letras...
                    if (etiquetaActual.Text.Length == 0)
                    {
                        // Eliminación visual y de la lista
                        this.Controls.Remove(etiquetaActual);
                        etiquetas.Remove(etiquetaActual);
                        etiquetaActual.Dispose();

                        // Si quedan más etiquetas...
                        if (etiquetas.Count > 0)
                        {
                            Label siguiente = etiquetas[0];
                            siguiente.Visible = true;
                            siguiente.Left = this.ClientSize.Width;
                            siguiente.Top = new Random().Next(0, this.ClientSize.Height - siguiente.Height);
                        }
                        else
                        {
                            // VICTORIA
                            timer1.Stop();
                            MessageBox.Show("¡Ganaste! Todas las palabras fueron tipeadas.");
                            this.Close();
                        }
                    }
                }
            }
        }

    }
}
