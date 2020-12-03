using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroduccionLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            string ruta = @"C:\windows";
            Console.WriteLine("Sin LINQ");
            MostrarArchivosGrandesSinLinq(ruta);
            Console.WriteLine("Con LINQ ");
            MostrarArchivosGrandesConLinq(ruta);
            Console.WriteLine("Con LINQ lamda ");
            MostrarArchivosGrandesConLinqLamda(ruta);

            Console.Read();

        }

        private static void MostrarArchivosGrandesConLinqLamda(string ruta)
        {
            var query = new DirectoryInfo(ruta).GetFiles()
                .OrderByDescending(archivo => archivo.Length)
                .Take(5);

            foreach (var archivo in query)
            {
                Console.WriteLine($"{archivo.Name} : {archivo.Length}");
            }
        }

        private static void MostrarArchivosGrandesConLinq(string ruta)
        {
            var query = from archivo in new DirectoryInfo(ruta).GetFiles()
                        orderby archivo.Length descending
                        select archivo;

            foreach (var archivo in query.Take(5))
            {
                Console.WriteLine($"{archivo.Name} : {archivo.Length}");
            }
        }

        private static void MostrarArchivosGrandesSinLinq(string ruta)
        {
            DirectoryInfo directorio = new DirectoryInfo(ruta);
            FileInfo[] archivos= directorio.GetFiles();
            Array.Sort(archivos, new CompararArchivos());

            //Con Foreach
            //foreach (FileInfo archivo in archivos)
            //{
            //    Console.WriteLine($"{archivo.Name} : {archivo.Length}");

            //}

            //Con For Normal
            for (int i = 0; i < 5; i++)
            {

                FileInfo archivo = archivos[i];
                Console.WriteLine($"{archivo.Name} : {archivo.Length}");

            }

            
        }

        
    }

    public class CompararArchivos : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
