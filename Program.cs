using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            //Printer.Beep(10000, 500, 10);
            imprimirCursosEscuela(engine.Escuela);  

            var listaObjetos = engine.GetObjetoEscuela(); 

            //engine.Escuela.LimpiarLugar();           
            /*var listaIlugar = from obj in listaObjetos
                              where obj is ILugar
                              select (ILugar)obj;*/
            
            Dictionary<int, string> diccionario= new Dictionary<int, string>();
            diccionario.Add(10, "Jhon Alzate");
            diccionario.Add(23, "Yesica Herrera");

            foreach (var keyValPair in diccionario)
            {
                WriteLine($"Key: {keyValPair.Key} Valor: {keyValPair.Value}");
            }

            Printer.WriteTitle("Acceso a Dicionario");
            diccionario[0] = "Pekerman";
            WriteLine(diccionario[0]);

            Printer.WriteTitle("Otro diccionario");
            var dic = new Dictionary<string, string>();
            dic["Luna"] = "Cuerpo celeste que gira alrededor de la tierra";
            WriteLine(dic["Luna"]);
            dic["Luna"] = "La protagonista de soy Luna";
            WriteLine(dic["Luna"]);
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
