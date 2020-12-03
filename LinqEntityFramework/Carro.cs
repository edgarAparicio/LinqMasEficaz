using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqEntityFramework
{
    public class Carro
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public string Fabricante { get; set; }
        public string Nombre { get; set; }
        public double Desplazamiento { get; set; }
        public int Cilindros { get; set; }
        public int Ciudad { get; set; }
        public int Autopista { get; set; }
        public int Rendimiento { get; set; }
    }
}
