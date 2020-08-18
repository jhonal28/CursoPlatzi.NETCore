using System;
using System.Collections.Generic;
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
            Printer.Beep(10000, 500, 10);
            imprimirCursosEscuela(engine.Escuela);    
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
