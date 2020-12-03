using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqEntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DbContextLinqEntityFramework>());
            InsertDatos();
            //QueriesLinq();
            QueryAvanzado();

            Console.Read();
        }

        private static void QueryAvanzado()
        {
            var db = new DbContextLinqEntityFramework();

            //Esta linea muestra la consulta en sql server de tu consulta linq
            db.Database.Log = Console.WriteLine;

            var querySinLamda = from carro in db.Carros
                                group carro by carro.Fabricante into fabricante
                                select new
                                {
                                    Nombre = fabricante.Key,
                                    Carros = (from carro in fabricante
                                              orderby carro.Rendimiento descending
                                              select carro).Take(2)
                                };

            var queryConLamda = db.Carros.GroupBy(c => c.Fabricante)
                .Select(g => new
                {
                    Nombre = g.Key,
                    Carros = g.OrderByDescending(c => c.Rendimiento).Take(2)
                });

            foreach(var grupo in querySinLamda)
            {
                Console.WriteLine(grupo.Nombre);
                foreach(var carro in grupo.Carros)
                {
                    Console.WriteLine($"\t{carro.Nombre} : {carro.Rendimiento}");
                }
            }
        }

        private static void QueriesLinq()
        {
            var db = new DbContextLinqEntityFramework();

            //Esta linea muestra la consulta en sql server de tu consulta linq
            db.Database.Log = Console.WriteLine;

            //var querySinLamda = from carro in db.Carros
            //            orderby carro.Rendimiento descending, carro.Nombre ascending
            //            select carro;

            //foreach (var carro in querySinLamda.Take(10))
            //{
            //    Console.WriteLine($"{carro.Nombre} : {carro.Rendimiento}");
            //}


            //var queryConLamda = db.Carros
            //    .Where(c => c.Fabricante == "BMW ")
            //    .OrderByDescending(o => o.Rendimiento)
            //    .ThenBy(c => c.Nombre)
            //    .Take(10);
                

            //foreach (var carro in queryConLamda)
            //{
            //    Console.WriteLine($"{carro.Nombre} : {carro.Rendimiento}");
            //}

            var queryConLamda2 = db.Carros
                .Where(c => c.Fabricante == "BMW ")
                .OrderByDescending(o => o.Rendimiento)
                .ThenBy(c => c.Nombre)
                .Take(10)
                .Select(c => new { Nombre = c.Nombre.ToUpper() })
                .ToList();

            foreach (var item in queryConLamda2)
            {
                Console.WriteLine(item.Nombre);
            }

            
        }

        private static void InsertDatos()
        {
            var carros = ProcesarArchivoFuelCSV("fuel.csv");
            var db = new DbContextLinqEntityFramework();

            if (!db.Carros.Any())
            {
                foreach(var carro in carros)
                {
                    db.Carros.Add(carro);
                }

                db.SaveChanges();
            }
        }

        private static List<Carro> ProcesarArchivoFuelCSV(string ruta)
        {
            //con lamda
            //return
            //     File.ReadAllLines(ruta)
            //    .Skip(1) //Salta la primera linea del archivo que es el encabezado
            //    .Where(linea => linea.Length > 1)  //Esta vacia una ultima columna del archivo asi que el where llegara hasta esa linea
            //    .Select(Carro.AnalizarArchivoCSV)
            //    .ToList();

            //Metodo sin lamda
            //var query = from linea in File.ReadAllLines(ruta)
            //            .Skip(1)
            //            where linea.Length > 1
            //            select Carro.AnalizarArchivoCSV(linea);

            //return query.ToList();



            //Con Metodo de Extension Propio
            //Para hacerlo de esta manera vamos en la entidad carro y comentamos el metodo que inicializa los valosres de las propiedades
            var query =
                 File.ReadAllLines(ruta)
                .Skip(1) //Salta la primera linea del archivo que es el encabezado
                .Where(linea => linea.Length > 1)  //Esta vacia una ultima columna del archivo asi que el where llegara hasta esa linea
                .ConvertirArchivoACarroMedianteMetodoExtension();

            return query.ToList();
        }
    }

    public static class ExtensionCarro
    {
        public static IEnumerable<Carro> ConvertirArchivoACarroMedianteMetodoExtension(this IEnumerable<string> recurso)
        {
            foreach (var linea in recurso)
            {
                var columnas = linea.Split(',');
                yield return new Carro
                {
                    Anio = int.Parse(columnas[0]),
                    Fabricante = columnas[1],
                    Nombre = columnas[2],
                    Desplazamiento = double.Parse(columnas[3]),
                    Cilindros = int.Parse(columnas[4]),
                    Ciudad = int.Parse(columnas[5]),
                    Autopista = int.Parse(columnas[6]),
                    Rendimiento = int.Parse(columnas[7])
                };
            }
        }
    }
}

