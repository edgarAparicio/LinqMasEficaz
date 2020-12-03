using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carros
{
    public class Carro
    {
        public int Anio { get; set; }
        public string Fabricante { get; set; }
        public string Nombre { get; set; }
        public double Desplazamiento { get; set; }
        public int Cilindros { get; set; }
        public int Ciudad { get; set; }
        public int Autopista { get; set; }
        public int Rendimiento { get; set; }

        //internal static Carro AnalizarArchivoCSV(string linea)
        //{
        //    var columnas = linea.Split(',');
        //    return new Carro
        //    {
        //        Anio = int.Parse(columnas[0]),
        //        Fabricante = columnas[1],
        //        Nombre = columnas[2],
        //        Desplazamiento = double.Parse(columnas[3]),
        //        Cilindros = int.Parse(columnas[4]),
        //        Ciudad = int.Parse(columnas[5]),
        //        Autopista = int.Parse(columnas[6]),
        //        Rendimiento = int.Parse(columnas[7])
        //    };
        //}
    }
}
