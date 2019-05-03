using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;

namespace RHTemplates
{
    class TemplateFiller
    {
        public string FillTemplate(string template, Empleado empleado)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string name = string.Format("{1}_{0}.docx", empleado.RFC, Path.GetFileNameWithoutExtension(template));
            string end_path = Path.Combine(dir, name);

            // clone the template
            using (WordprocessingDocument TemplateDoc =
            WordprocessingDocument.Open(template, false))
            {                
                TemplateDoc.SaveAs(end_path).Close();
                
            }
            
            // do the swappening
            using (WordprocessingDocument OutDoc = WordprocessingDocument.Open(end_path, true))
            {

                    string docText = null;
                    using (StreamReader sr = new StreamReader(OutDoc.MainDocumentPart.GetStream()))
                    {
                        docText = sr.ReadToEnd();
                    }

                    Regex regexText = new Regex(@"\$RFC");
                    docText = regexText.Replace(docText, empleado.RFC);
                    regexText = new Regex(@"\$NOMBRE");
                    docText = regexText.Replace(docText, empleado.Nombre);
                    regexText = new Regex(@"\$SEXO");
                    docText = regexText.Replace(docText, empleado.Sexo);
                    regexText = new Regex(@"\$ESTADO_CIVIL");
                    docText = regexText.Replace(docText, empleado.EstadoCivil);

                    using (StreamWriter sw = new StreamWriter(OutDoc.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(docText);
                    }
                }

                return end_path;
            }


        }


                   
    }


