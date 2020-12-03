using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXML
{
    class Program
    {
        static void Main(string[] args)
        {

            //Estas 3 variables Se descomennta para todas las formas diferentes menos para la forma 6
            //var registros = ProcesarArchivoFuelCSV("fuel.csv");

            //var documentoXML = new XDocument();
            //var carros = new XElement("Carros");




            //Diferentes formas de crear el archivo XML
            //Forma Numero 1
            //foreach (var registro in registros)
            //{
            //    var carro = new XElement("Carro");
            //    var nombre = new XElement("Nombre", registro.Nombre);
            //    var rendimiento = new XElement("Rendimiento", registro.Rendimiento);
            //    carro.Add(nombre);
            //    carro.Add(rendimiento);
            //    carros.Add(carro);
            //}

            ////Forma Numero 2
            //foreach (var registro in registros)
            //{
            //    var nombre = new XAttribute("Nombre", registro.Nombre);
            //    var rendimiento = new XAttribute("Rendimiento", registro.Rendimiento);
            //    var carro = new XElement("Carro", nombre, rendimiento);
            //    carros.Add(carro);
            //}

            //Forma Numero 3
            //foreach(var registro in registros)
            //{
            //    var carro = new XElement("Carro",
            //                    new XAttribute("Nombre", registro.Nombre),
            //                    new XAttribute("Rendimiento", registro.Rendimiento),
            //                    new XAttribute("Fabricante", registro.Fabricante)
            //        );
            //    carros.Add(carro);
            //}

            //Forma Numero 4
            //var elementos =
            //        from registro in registros
            //        select new XElement("Carro",
            //                  new XAttribute("Nombre", registro.Nombre),
            //                  new XAttribute("Rendimiento", registro.Rendimiento),
            //                  new XAttribute("Cilindros", registro.Cilindros)
            //        );
            //carros.Add(elementos);


            //Forma Numero 5
            //De la variable carros que esta al inicio de la clase la comentamos y la creamos aqui
            //var carros = new XElement("Carros",
            //        from registro in registros
            //        select new XElement("Carro",
            //              new XAttribute("Nombre", registro.Nombre),
            //              new XAttribute("Rendimiento", registro.Rendimiento),
            //              new XAttribute("Fabricante", registro.Fabricante))
            //              );



            //Forma Numero 6 Crear una refactorizacion
            //De las variable registros, documentoXML y carros que esta al inicio de la clase las comentamos y las creamos aqui
            //Seleccionamos todo este codigo vamos a editar>Refactorizar>ExtraerMetodo
            //Y aqui en el main solo mandamos a llamar al metodo
            //CrearXML();




            //Se descomennta para todas las formas diferentes menos para la forma 6
            //documentoXML.Add(carros);
            //documentoXML.Save("fuel.xml");





            //Leer un documento XML("fuel.xml")
            //Aqui en el Main Mandamos a llamar nuestro metodo para leer XML
            //LeerArchivoXML();
            CrearArchivoXMLConEspacioNombre();

            Console.Read();


        }

        private static void CrearArchivoXMLConEspacioNombre()
        {
            var registros = ProcesarArchivoFuelCSV("fuel.csv");
            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";
            var documentoXML = new XDocument();
            var carros = new XElement(ns + "Carros",    //hace un espacio de nombres en general
                    from registro in registros
                    select new XElement( ex + "Carro", //hace un espacio de nombre por cada elemento sin la linea de abajo: carros.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));
                          new XAttribute("Nombre", registro.Nombre),
                          new XAttribute("Rendimiento", registro.Rendimiento),
                          new XAttribute("Fabricante", registro.Fabricante))
                          );

            carros.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));   
            documentoXML.Add(carros);
            documentoXML.Save("fuel.xml");
        }

        private static void LeerArchivoXML()
        {
            var documentoXML = XDocument.Load("fuel.xml");
            var query = 
                        //from elemento in documentoXML.Element("Carros").Elements("Carro")  //o esta 
                        from elemento in documentoXML.Descendants("Carro")
                        where elemento.Attribute("Fabricante")?.Value == "BMW"          //El signo de ? indica que si no hay un valor con bmw la aplicacion no manda error y manda un nulo e igualemnte si el nombre del atrubuto no esta bien escrito este permite que la aplicacion continue sin error
                        select elemento.Attribute("Nombre").Value;

            foreach(var nombre in query)
            {
                Console.WriteLine(nombre);
            }


        }

        private static void CrearXML()
        {
            var registros = ProcesarArchivoFuelCSV("fuel.csv");

            var documentoXML = new XDocument();
            var carros = new XElement("Carros",
                    from registro in registros
                    select new XElement("Carro",
                          new XAttribute("Nombre", registro.Nombre),
                          new XAttribute("Rendimiento", registro.Rendimiento),
                          new XAttribute("Fabricante", registro.Fabricante))
                          );

            documentoXML.Add(carros);
            documentoXML.Save("fuel.xml");
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
