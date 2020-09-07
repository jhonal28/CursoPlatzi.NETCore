using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            //Printer.Beep(10000, 500, 10);
            //imprimirCursosEscuela(engine.Escuela);  

            //var listaObjetos = engine.GetObjetoEscuela(); 

            //engine.Escuela.LimpiarLugar();           
            /*var listaIlugar = from obj in listaObjetos
                              where obj is ILugar
                              select (ILugar)obj;*/
            
            /*Dictionary<int, string> diccionario= new Dictionary<int, string>();
            diccionario.Add(10, "Jhon Alzate");
            diccionario.Add(23, "Yesica Herrera");

            foreach (var keyValPair in diccionario)
            {
                WriteLine($"Key: {keyValPair.Key} Valor: {keyValPair.Value}");
            }*/

            /*Printer.WriteTitle("Acceso a Dicionario");
            diccionario[0] = "Pekerman";
            WriteLine(diccionario[0]);

            Printer.WriteTitle("Otro diccionario");
            var dic = new Dictionary<string, string>();
            dic["Luna"] = "Cuerpo celeste que gira alrededor de la tierra";
            WriteLine(dic["Luna"]);
            dic["Luna"] = "La protagonista de soy Luna";
            WriteLine(dic["Luna"]);*/

            /*var dictmp = engine.GetDiccionarioObjetos();
            engine.ImprimirDiccionario(dictmp, true);*/

            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
            var evalList = reporteador.GetListaEvaluaciones();
            var listaAsig = reporteador.GetListaAsignaturas();
            var listaEvalXAsig = reporteador.GetDicEvaluaXAsig();
            var listaPromXAsig = reporteador.GetPromeAlumnPorAsignatura(true, 10);

            Printer.WriteTitle("Captura de una evaluación por consola");
            var newEval = new Evaluacion();
            string nombre, notaString;

            WriteLine("Ingrese el nombre de la evaluación");
            Printer.PresioneENTER();
            nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Printer.WriteTitle("El valor del nombre no puede ser vacío");
                WriteLine("Saliendo del programa");
            }else
            {
                newEval.Nombre = nombre.ToLower();
                WriteLine("El nombre de la evaluación ha sido ingresado correctamente");
            }

            WriteLine("Ingrese la nota de la evaluación");
            Printer.PresioneENTER();
            notaString = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Printer.WriteTitle("El valor de la nota no puede ser vacío");
                WriteLine("Saliendo del programa");
            }else
            {
                try
                {
                    newEval.Nota = float.Parse(notaString);
                    if (newEval.Nota < 0 || newEval.Nota > 5)
                    {
                        throw new ArgumentOutOfRangeException("La nota debe estar entre 0 y 5");
                    }
                    WriteLine("La nota de la evaluación ha sido ingresado correctamente");

                }
                catch(ArgumentOutOfRangeException arge)
                {
                    Printer.WriteTitle(arge.Message);
                    WriteLine("Saliendo del programa");
                }
                catch(Exception) 
                {                    
                    Printer.WriteTitle("El valor de la nota no es un número válido");
                    WriteLine("Saliendo del programa");
                }
                finally
                {
                    Printer.WriteTitle("FINALLY");
                    Printer.Beep(2500, 500, 3);
                }
                
            }
        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("SALIENDO");
            Printer.Beep(3000, 1000, 3);
            Printer.WriteTitle("SALIÓ");
        }

        private static void imprimirCursosEscuela(Escuela escuela)
        {            
            Printer.WriteTitle("Cursos de la Escuela");

            //El signo de interrogación al lado de la palabra escuela
            //valida que no sea nula la escuela, es lo mismo que decir if escuela != null
            if(escuela?.Cursos != null)          
            {
                foreach (var curso in escuela.Cursos)
                {
                    Console.WriteLine($"Nombre {curso.Nombre}, Id {curso.UniqueId}");
                }
            }            

        }       

    }
}
