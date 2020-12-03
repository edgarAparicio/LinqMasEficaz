using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carros
{
    class Program
    {
        static void Main(string[] args)
        {
            var carros = ProcesarArchivo("fuel.csv");
            var fabricantes = ProcesarArchivoFabricantes("manufacturers.csv");





            //foreach (var carro in carros)
            //{
            //    Console.WriteLine(carro.Nombre);
            //}








            //Con lamda ordenacion secundaria los 10 mejores
            //var query = carros
            //            .Where(c => c.Fabricante == "BMW" && c.Anio == 2016)
            //            .OrderByDescending(c => c.Rendimiento)
            //            .ThenBy(c => c.Nombre)  //Ordenacion Secundaria, primero ordena por conjunto y despues por nombre
            //            .Take(10);

            //foreach (var carro in query)
            //{
            //    Console.WriteLine($"Nombre: {carro.Nombre}, Rendimiento: {carro.Rendimiento}, Fabricante: {carro.Fabricante}, Año: {carro.Anio}");
            //}


            //Sin lamda ordenacion secundaria
            //var query = from carro in carros
            //            where carro.Fabricante == "BMW" && carro.Anio == 2016
            //            orderby carro.Rendimiento descending, carro.Nombre ascending
            //            select carro;

            //foreach (var carro in query.Take(10))
            //{
            //    Console.WriteLine($"Nombre: {carro.Nombre}, Rendimeinto: {carro.Rendimiento}, Fabricante: {carro.Fabricante}, Año: {carro.Anio}");
            //}

            //Con Lamda ordenacion secundarfia el mejor
            //var top1 = carros
            //            .Where(c => c.Fabricante == "BMW" && c.Anio == 2016)
            //            .OrderByDescending(c => c.Rendimiento)
            //            .ThenBy(c => c.Nombre)  //Ordenacion Secundaria, primero ordena por conjunto y despues por nombre
            //            .First();

            //Console.WriteLine($"{top1.Nombre} : {top1.Anio}");






            //Regresa un bit con true o false si hay algun o algunos carros que tengan el fabricante ford 
            //var resultado = carros.Any(c => c.Fabricante == "Ford");

            ////Regresa un bot con true o false si todos los carros son fabricante ford
            //var resultado1 = carros.All(c => c.Fabricante == "Ford");

            //Manda error en consola
            //var resultado2 = carros.Select(c => new
            //{
            //    c.Fabricante,
            //    c.Nombre
            //});

            //Console.WriteLine(resultado1);








            //Doble Foreach para que de una secuencia de los nombres de los carros ahora has otra secuencia de los nombre pero ahora caracter por caracter
            //var resultado5 = carros.Select(c => c.Nombre);
            //foreach(var nombre in resultado5)
            //{
            //    foreach(var caracter in nombre)
            //    {
            //        Console.WriteLine(caracter);
            //    }
            //}

            //El mismo resultado pero ahora usando SelectMany
            //var resultado5 = carros.SelectMany(c => c.Nombre)
            //                    .OrderBy(c => c);

            //    foreach (var caracter in resultado5)
            //    {
            //        Console.WriteLine(caracter);
            //    }







            //join normal 2 tablas

            //var querySinLamda =
            //    from carro in carros
            //    join fabricante in fabricantes
            //    on carro.Fabricante equals fabricante.Nombre
            //    orderby carro.Rendimiento descending,
            //    carro.Nombre ascending
            //    select new 
            //    {
            //        fabricante.Sede,
            //        carro.Nombre,
            //        carro.Rendimiento
            //    };

            //var queryConLamda =
            //    carros.Join(fabricantes,
            //        carro => carro.Fabricante,
            //        fabricante => fabricante.Nombre,
            //        (carro, fabricante) => new
            //        {
            //            fabricante.Sede,
            //            carro.Nombre,
            //            carro.Rendimiento
            //        }
            //    )
            //    .OrderByDescending(carro => carro.Rendimiento)
            //    .ThenBy(carro => carro.Nombre);

            //foreach (var carro in queryConLamda.Take(10))
            //{
            //    Console.WriteLine($"{carro.Sede} - {carro.Nombre} : {carro.Nombre}");
            //}






            //Group by por fabricante sencillo

            //var query = from carro in carros
            //            group carro by carro.Fabricante;

            //foreach(var resultado in query)
            //{
            //    Console.WriteLine($"{resultado.Key} tiene {resultado.Count()} carros");
            //}






            //Group by los dos autos con mejor rendimiento agrupados por fabricante

            //var querySinLamda = from carro in carros
            //                    group carro by carro.Fabricante;

            //var queryConLamda = carros.GroupBy(c => c.Fabricante);

            //var querySinLamdaConKey = from carro in carros
            //                          group carro by carro.Fabricante.ToUpper() into fabricante
            //                          orderby fabricante.Key
            //                          select fabricante;
            //var queryConLamdaConKey = carros.GroupBy(c => c.Fabricante.ToUpper()).OrderBy(o => o.Key);


            //foreach (var gruposFabricante in querySinLamdaConKey)
            //{
            //    Console.WriteLine(gruposFabricante.Key);
            //    foreach(var carro in gruposFabricante.OrderByDescending(c => c.Rendimiento).Take(2))
            //    {
            //        Console.WriteLine($"\t{carro.Nombre} : {carro.Rendimiento} kilometros por litro de gasolina.");
            //    }
            //}







            //Group by los dos autos con mejor rendimiento agrupados por fabricante y el pais del fabricante

            //var querySimLamda = from fabricante in fabricantes
            //                    join carro in carros
            //                    on fabricante.Nombre equals carro.Fabricante
            //                    into grupoCarros
            //                    orderby fabricante.Nombre
            //                    select new
            //                    {
            //                        Fabricante = fabricante,
            //                        Carros = grupoCarros
            //                    };

            //var queryConLamda = fabricantes.GroupJoin(carros,
            //                                        fabricante => fabricante.Nombre, carro => carro.Fabricante,
            //                                (f, grupoCarros) =>
            //                                        new
            //                                        {
            //                                            Fabricante = f,
            //                                            Carros = grupoCarros
            //                                        })
            //                                .OrderBy(o => o.Fabricante.Nombre);

            //foreach(var grupo in queryConLamda)
            //{
            //    Console.WriteLine($"{grupo.Fabricante.Nombre} : {grupo.Fabricante.Sede}");
            //    foreach(var carro in grupo.Carros.OrderByDescending(c => c.Rendimiento).Take(2))
            //    {
            //        Console.WriteLine($"\t{carro.Nombre} : {carro.Rendimiento} kilometros por litro");
            //    }

            //}





            //Group by agrupar por pais los 3 autos con mejor rendimiento 

            //var querySinLamda = from fabricante in fabricantes
            //                    join carro in carros
            //                    on fabricante.Nombre equals carro.Fabricante
            //                        into grupoCarros
            //                    select new
            //                    {
            //                        Fabricante = fabricante,
            //                        GrupoCarros = grupoCarros
            //                    } into resultado
            //                    group resultado by resultado.Fabricante.Sede;

            //var queryConLamda = fabricantes.GroupJoin(carros,
            //                                        fabricante => fabricante.Nombre, carro => carro.Fabricante,
            //                                (f, grupoCarros) =>
            //                                        new
            //                                        {
            //                                            Fabricante = f,
            //                                            GrupoCarros = grupoCarros
            //                                        })
            //                                .GroupBy(o => o.Fabricante.Sede);

            //foreach (var grupoPais in queryConLamda)
            //{
            //    Console.WriteLine($"{grupoPais.Key}");
            //    foreach(var carro in grupoPais.SelectMany(g => g.GrupoCarros).OrderByDescending(o => o.Rendimiento).Take(3))
            //    {
            //        Console.WriteLine($"\t{carro.Nombre} : {carro.Rendimiento} km por litro");
            //    }
            //}



            //Seleccioname el carro que tenga  el maximo, el minimo y el promedio en rendimiento agrupados por fabricante

            var query = from carro in carros
                        group carro by carro.Fabricante
                       into grupoCarros
                        select new
                        {
                            Nombre = grupoCarros.Key,
                            Maximo = grupoCarros.Max(c => c.Rendimiento),
                            Minimo = grupoCarros.Min(c => c.Rendimiento),
                            Promedio = grupoCarros.Average(c => c.Rendimiento)
                        };

            //Ordenados por el maximo

            var query2 = from carro in carros
                         group carro by carro.Fabricante
                       into grupoCarros
                         select new
                         {
                             Nombre = grupoCarros.Key,
                             Maximo = grupoCarros.Max(c => c.Rendimiento),
                             Minimo = grupoCarros.Min(c => c.Rendimiento),
                             Promedio = grupoCarros.Average(c => c.Rendimiento)
                         }
                        into grupoResultado
                         orderby grupoResultado.Maximo descending
                         select grupoResultado;

            foreach (var resultado in query2)
            {
                Console.WriteLine($"{resultado.Nombre}");
                Console.WriteLine($"\t Maximo: {resultado.Maximo}");
                Console.WriteLine($"\t Minimo: {resultado.Minimo}");
                Console.WriteLine($"\t Promedio: {resultado.Promedio}");
            }






            Console.Read();
        }

        
        private static List<Carro> ProcesarArchivo(string ruta)
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


        private static List<Fabricante> ProcesarArchivoFabricantes(string ruta)
        {
            var query = File.ReadAllLines(ruta)
                .Where(l => l.Length > 1)
                .Select(l =>
                {
                    var columnas = l.Split(',');
                    return new Fabricante
                    {
                        Nombre = columnas[0],
                        Sede = columnas[1],
                        Anio = int.Parse(columnas[2])
                    };
                });
            return query.ToList();
        }
    }





    public static class ExtensionCarro
    {
        public static IEnumerable<Carro> ConvertirArchivoACarroMedianteMetodoExtension(this IEnumerable<string> recurso)
        {
            foreach(var linea in recurso)
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
