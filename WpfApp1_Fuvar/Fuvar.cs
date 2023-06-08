using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1_Fuvar
{
    internal class Fuvar
    {
        int id;
        DateTime indulas;
        int idotartam;
        double megtettUt;
        double viteldij;
        double borravalo;
        string fizetesiMod;

        public Fuvar(int id, DateTime indulas, int idotartam, double megtettUt, double viteldij, double borravalo, string fizetesiMod)
        {
            this.id = id;
            this.indulas = indulas;
            this.idotartam = idotartam;
            this.megtettUt = megtettUt;
            this.viteldij = viteldij;
            this.borravalo = borravalo;
            this.fizetesiMod = fizetesiMod;
        }

        public int Id { get => id; }
        public DateTime Indulas { get => indulas; }
        public int Idotartam { get => idotartam; }
        public double MegtettUt { get => megtettUt; }
        public double Viteldij { get => viteldij; }
        public double Borravalo { get => borravalo;  }
        public string FizetesiMod { get => fizetesiMod;  }
    }
}
