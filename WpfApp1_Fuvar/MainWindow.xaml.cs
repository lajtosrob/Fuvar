using System;
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
using System.IO;

namespace WpfApp1_Fuvar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Fuvar> fuvarok = new List<Fuvar>();
        public MainWindow()
        {
            InitializeComponent();

            //2. 

            FajlBeolvasas("DataSource\\fuvar.csv");

            //3. 

            lblFeladat3.Content = $"3. feladat: {fuvarok.Count} fuvar";

            //4. 

            double bevetel = fuvarok.Where(x => x.Id == 6185).Sum(x => x.Viteldij + x.Borravalo);
            int fuvarokSzama = fuvarok.Where(x => x.Id == 6185).Count();

            lblFeladat4.Content = $"4. feladat:  {fuvarokSzama}  alatt:  {bevetel}$";

            //5.

            var fizetesiModok = fuvarok.Select(x => x.FizetesiMod).Distinct();
            List<string>[] fizModSzam;

            foreach (var fizetesiMod in fizetesiModok)
            {
                int szamlalo = fuvarok.Count(x => x.FizetesiMod == fizetesiMod);
                lbFeladat5.Items.Add($"\t{fizetesiMod}: {szamlalo} fuvar");
            }

            //6. 

            lblFeladat6.Content = $"6. feladat:  {Math.Round(fuvarok.Select(x => x.MegtettUt).Sum() * 1.6, 2)}km";

            //7.

            var leghosszabbFuvar = fuvarok.OrderByDescending(x => x.Idotartam).FirstOrDefault();

            lblIdotartam.Content = $"\tFuvar hossza: {leghosszabbFuvar.Idotartam} másodperc";
            lblTaxiId.Content = $"\tTaxi azonosító: {leghosszabbFuvar.Id}";
            lblMegtettUt.Content = $"\tMegtett távolság: {leghosszabbFuvar.MegtettUt} km";
            lblViteldij.Content = $"\tViteldíj: {leghosszabbFuvar.Viteldij}$";

            //8.

            FajlbaIras("hibak.txt");
            
        }

        public void FajlBeolvasas(string fajlnev)
        {
            
            /*
            string[] lines = File.ReadAllLines(fajlnev);

            for(var index = 1; index < lines.Length; index++)
            {
                string[] line = lines[index].Split(';');

                int id = int.Parse(line[0]);
                DateTime indulas = DateTime.Parse(line[1]);
                int idotartam = int.Parse(line[2]);
                double megtettUt = double.Parse(line[3]);
                double viteldij = double.Parse(line[4]);
                double borravalo = double.Parse(line[5]);
                string fizetesiMod = line[6];

                Fuvar fuvarSor = new(id, indulas, idotartam, megtettUt, viteldij, borravalo, fizetesiMod);

                fuvarok.Add(fuvarSor);

            } */

            StreamReader sr = new StreamReader(fajlnev);

            string headerLine = sr.ReadLine();

            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split(';');

                Fuvar fuvarDatas = new Fuvar(
                    int.Parse(line[0]),
                    DateTime.Parse(line[1]),
                    int.Parse(line[2]),
                    double.Parse(line[3]),
                    double.Parse(line[4]),
                    double.Parse(line[5]),
                    line[6]
                    );

                fuvarok.Add( fuvarDatas );
            }

            MessageBox.Show("A fájlbeolvasás sikeresen megtörtént!");

        }

        public void FajlbaIras(string fajlnev)
        {
            List<Fuvar> hibas = fuvarok.OrderBy(x => x.Indulas).Where(x => x.Idotartam > 0 && x.Viteldij > 0 && x.MegtettUt == 0).ToList();

            /* File.WriteAllLines("Datasource\\hibak.txt", fuvarok.OrderBy(x => x.Indulas).Where(x => x.Idotartam > 0 && x.Viteldij > 0 && x.MegtettUt == 0));
            File.WriteAllLines("Datasource\\hibak.txt", hibas); */

            StreamWriter sr = new StreamWriter(fajlnev);

            foreach (var item in hibas)
            {
                string line = $"{item.Id};{item.Indulas};{item.Idotartam};{item.MegtettUt};{item.Viteldij};{item.Borravalo};{item.FizetesiMod}";

                sr.WriteLine(line);
            }

            sr.Close();
            MessageBox.Show("Az adatok fájlbaírása sikeresen megtörtént!");
        }
    }
}
