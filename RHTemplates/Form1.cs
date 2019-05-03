using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace RHTemplates
{
    public partial class Form1 : Form
    {
        string TemplatesPath;

        List<string> NombresPlantillas;
        IEnumerable<Empleado> Empleados;

        public Form1()
        {
            InitializeComponent();

            // Get plantillas Path
            TemplatesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PlantillasRH");
            Directory.CreateDirectory(TemplatesPath);

            // Load Plantillas
            NombresPlantillas = Directory.EnumerateFiles(TemplatesPath, @"*.docx").Where(s => s.Last() != '#' ).Select<string,string>((path)=> Path.GetFileName(path) ).ToList();
            listBoxPlantillas.DataSource = NombresPlantillas;

            // Load Empleados
            Empleados = EmpleadoDatabase.GetDatabase().GetEmpleados();
            listBoxEmpleados.DataSource = Empleados;

            // Search plantilla
            textBoxBusquedaPlantilla.TextChanged += (o, e) => {
                string text = textBoxBusquedaPlantilla.Text;
                if (string.IsNullOrEmpty(text))
                    listBoxPlantillas.DataSource = NombresPlantillas;
                else
                    listBoxPlantillas.DataSource = NombresPlantillas.Where(s => s.Contains(text)).ToList();
            };

            // Search Empleado
            textBoxBusquedaEmpleado.TextChanged += (o, e) => {
                string text = textBoxBusquedaEmpleado.Text;
                if (string.IsNullOrEmpty(text))
                    listBoxEmpleados.DataSource = Empleados;
                else
                    listBoxEmpleados.DataSource = Empleados.Where(s => s.Nombre.Contains(text) || s.RFC.Contains(text)).ToList();
            };


            // Mostrar Empleado Seleccionado
            listBoxEmpleados.SelectedValueChanged += (o, e) => {
                try
                {
                    var empleado = listBoxEmpleados.SelectedValue as Empleado;
                    labelNombre.Text = empleado.Nombre;
                    labelRFC.Text = empleado.RFC;
                    labelEstadoCivil.Text = empleado.EstadoCivil;
                    labelSexo.Text = empleado.Sexo;
                }
                catch { }
            };
        }

        private void ButtonAplicar_Click(object sender, EventArgs e)
        {
            try
            {                
               // Get Selected stuff
                string selected_template = listBoxPlantillas.SelectedValue as string;
                Empleado selected_empleado = listBoxEmpleados.SelectedValue as Empleado;

                //Fill Template
                TemplateFiller filler = new TemplateFiller();
                var end_path = filler.FillTemplate(Path.Combine(TemplatesPath, selected_template) , selected_empleado);

                // open file
                Process p = new Process();
                p.StartInfo.FileName = end_path;
                p.Start();

            }catch (Exception ex)
            {
                //MessageBox.Show(ex.Message,ex.ToString());
                throw ex;
            }
        }
    }
}
