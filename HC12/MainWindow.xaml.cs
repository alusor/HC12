using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace HC12
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Instruction> linea;
        string[] temp;
        public MainWindow()
        {
            InitializeComponent();
            linea = new  List<Instruction>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string nameOfFile = dlg.FileName;
                filename.Content = nameOfFile;
                inputFile.Text = File.ReadAllText(nameOfFile).ToString();
                temp = inputFile.Text.Split('\n');
            }
            else {
                filename.Content = "Vuelve a intentarlo";
            }

        }

        private void analizar_Click(object sender, RoutedEventArgs e)
        {
            Instruction aux;
            for (int i = 0; i < temp.Length; i++) {
                if (temp[i].Length > 0)
                    if (temp[i].Contains(';'))
                    {
                        if (temp[i][0] == ';' && temp[i].Length <= 80)
                            salida.Text += "COMENTARIO\n\n";
                        else {
                            salida.Text = "error";
                            break;
                        }
                    }
                    else if (Char.IsLetterOrDigit(temp[i][0]))
                    {
                        temp[i] = Regex.Replace(temp[i], @"\s+", " ");
                        temp[i] = temp[i].Trim();
                        string[] a = temp[i].Split();
                        aux = new Instruction();
                        switch (a.Length)
                        {
                            case 3:
                                aux.etiqueta = analizarEtiqueta(a[0]) ? a[0] : "Error etiqueta sin formato correcto.";
                                aux.codop = analizarCodop(a[1])? a[1] : "Error Codop sin formato correcto.";
                                aux.operando = a[2];
                                break;
                            case 2:
                                aux.etiqueta = analizarEtiqueta(a[0]) ? a[0] : "Error etiqueta sin formato correcto.";
                                aux.codop = analizarCodop(a[1]) ? a[1] : "Error Codop sin formato correcto.";
                                break;
                        } 
                    // analizarCodop(aux.codop);
                    // if (analizarEtiqueta(aux.etiqueta))
                       salida.Text += "ETIQUETA: " + aux.etiqueta + "\nCODOP: " + aux.codop + "\nOPERANDO: " + aux.operando + "\n\n";
                    //else { salida.Text += "Error: " + aux.etiqueta + " no es una etiqueta valida.\n\n";
                        //break;
                    //}
                        linea.Add(aux);
                    }
                    else {
                        temp[i] = Regex.Replace(temp[i], @"\s+", " ");
                        temp[i] = temp[i].Trim();
                        string[] a = temp[i].Split();
                        aux = new Instruction();
                        switch (a.Length)
                        {
                            case 2:
                                aux.codop = analizarCodop(a[0]) ? a[0] : "Error Codop sin formato correcto.";
                                aux.operando = a[1];
                                break;
                            case 1:
                                aux.codop = analizarCodop(a[0]) ? a[0] : "Error Codop sin formato correcto.";
                                break;
                        }
                        if (temp.Length == i + 1 && !aux.codop.ToUpper().Equals("END"))
                        {
                            salida.Text = "Error.";
                            break;
                        }
                        salida.Text += "ETIQUETA: " + aux.etiqueta + "\nCODOP: " + aux.codop + "\nOPERANDO: " + aux.operando + "\n\n";
                        linea.Add(aux);
                    }
            }
        }

        private bool analizarEtiqueta(string etiqueta) {
           
            string patron = @"([A-Z]|[a-z])([A-Z]|[a-z]|[0-9]|_)*";
            Regex rx = new Regex(patron);
            if (etiqueta.Length <= 8 && rx.Match(etiqueta).ToString() == etiqueta) {
                return true;
            }

            return false;
        }
        private bool analizarCodop(string codop) {
            string patron = @"([a-z]|[A-Z])+\.?([a-z]|[A-Z])*";
            Regex rx = new Regex(patron);
            if (codop.Length <= 5 && rx.Match(codop).ToString() == codop)
                return true;
            return false;
        }
    }
}
