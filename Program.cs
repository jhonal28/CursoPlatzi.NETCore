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
            //Printer.Beep(10000, 500, 10);
            imprimirCursosEscuela(engine.Escuela);   

            Printer.DrawLine(20);
            Printer.DrawLine(20);
            Printer.DrawLine(20);
            Printer.WriteTitle("Pruebas de polimorfismo"); 

            var alumnoTest = new Alumno{Nombre = "Claire Underwood"};
            
            Printer.WriteTitle("Alumno");
            WriteLine($"Alumno: {alumnoTest.Nombre}");
            WriteLine($"Alumno: {alumnoTest.UniqueId}");
            WriteLine($"Alumno: {alumnoTest.GetType()}");

            ObjetoEscuelaBase ob = alumnoTest;
            Printer.WriteTitle("ObjetoEscuela");
            WriteLine($"Alumno: {ob.Nombre}");
            WriteLine($"Alumno: {ob.UniqueId}");
            WriteLine($"Alumno: {ob.GetType()}");

            var objDummy = new ObjetoEscuelaBase(){Nombre = "Frank Underwood"};
            Printer.WriteTitle("ObjetoEscuela");
            WriteLine($"Alumno: {objDummy.Nombre}");
            WriteLine($"Alumno: {objDummy.UniqueId}");
            WriteLine($"Alumno: {objDummy.GetType()}");

            var evaluacion = new Evaluacion(){Nombre = "Evaluación de math", Nota = 4.5f};
            Printer.WriteTitle("Evaluación");
            WriteLine($"Evaluación: {evaluacion.Nombre}");
            WriteLine($"Evaluación: {evaluacion.Nota}");
            WriteLine($"Evaluación: {evaluacion.UniqueId}");
            WriteLine($"Evaluación: {evaluacion.GetType()}");

            ob = evaluacion;
            Printer.WriteTitle("ObjetoEscuela");
            WriteLine($"Alumno: {ob.Nombre}");
            WriteLine($"Alumno: {ob.UniqueId}");
            WriteLine($"Alumno: {ob.GetType()}");

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
