using System;
using System.Linq;
using System.Collections.Generic;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObsEsc)
        {
            if (dicObsEsc == null)
            {
                throw new ArgumentNullException(nameof(dicObsEsc));
            }
            _diccionario = dicObsEsc;
        }

        public IEnumerable<Evaluacion> GetListaEvaluaciones()
        {
            
            if (_diccionario.TryGetValue(LlaveDiccionario.Evaluacion, out IEnumerable<ObjetoEscuelaBase> lista))
            {
                return lista.Cast<Evaluacion>();
            }
            {
                return new List<Evaluacion>();
            }            
            
        }

        public IEnumerable<String> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
            
        }

        public IEnumerable<String> GetListaAsignaturas(out IEnumerable<Evaluacion> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaEvaluaciones();

            return (from Evaluacion ev in listaEvaluaciones
                    select ev.Asignatura.Nombre).Distinct();
            
        }

        public Dictionary<string, IEnumerable<Evaluacion>> GetDicEvaluaXAsig()
        {
            var dicRta = new Dictionary<string, IEnumerable<Evaluacion>>();

            var listaAsig = GetListaAsignaturas(out var listaEval);

            foreach (var asig in listaAsig)
            {
                var evalsAsig = from eval in listaEval
                                where eval.Asignatura.Nombre == asig
                                select eval;

                dicRta.Add(asig, evalsAsig);
            }

            return dicRta;
        }
        
        public Dictionary<string, IEnumerable<Object>> GetPromeAlumnPorAsignatura(bool mejoresPromedio = false, int maximo = 5)
        {
            var rta = new Dictionary<string, IEnumerable<Object>>();
            var dicEvaluaXAsig = GetDicEvaluaXAsig();

            if (mejoresPromedio)
            {
                foreach (var asigConEval in dicEvaluaXAsig)
                {
                    var promsAlumn =    from eval in asigConEval.Value                                
                                        group eval by new {
                                            eval.Alumno.UniqueId,
                                            eval.Alumno.Nombre
                                        }
                                        into grupoEvalsAlumno
                                        orderby grupoEvalsAlumno.Average(evaluacion => evaluacion.Nota) descending                                 
                                        select new AlumnoPromedio{
                                            AlumnoId = grupoEvalsAlumno.Key.UniqueId,
                                            AlumnoNombre = grupoEvalsAlumno.Key.Nombre,
                                            Promedio = grupoEvalsAlumno.Average(evaluacion => evaluacion.Nota)
                                        };                                 

                    rta.Add(asigConEval.Key, promsAlumn.Take(maximo));
                }
            }else{
                foreach (var asigConEval in dicEvaluaXAsig)
                {
                    var promsAlumn = from eval in asigConEval.Value
                                group eval by new {
                                    eval.Alumno.UniqueId,
                                    eval.Alumno.Nombre
                                }
                                into grupoEvalsAlumno
                                select new AlumnoPromedio{
                                    AlumnoId = grupoEvalsAlumno.Key.UniqueId,
                                    AlumnoNombre = grupoEvalsAlumno.Key.Nombre,
                                    Promedio = grupoEvalsAlumno.Average(evaluacion => evaluacion.Nota)
                                }; 

                    rta.Add(asigConEval.Key, promsAlumn);
                }
            }
            
            return rta;
        }

        /*public Dictionary<string, IEnumerable<Object>> GetMejoresXPromeAlumnPorAsignatura(int maximo = 5)
        {
            var rta = new Dictionary<string, IEnumerable<Object>>();
            var promAlumnXAsig = GetPromeAlumnPorAsignatura();
           
            var promsMejoresAlumn = from eval in asigConEval.Value
                        group eval by new {
                            eval.Alumno.UniqueId,
                            eval.Alumno.Nombre
                        }
                        into grupoEvalsAlumno
                        select new AlumnoPromedio{
                            AlumnoId = grupoEvalsAlumno.Key.UniqueId,
                            AlumnoNombre = grupoEvalsAlumno.Key.Nombre,
                            Promedio = grupoEvalsAlumno.Average(evaluacion => evaluacion.Nota)
                        }; 

            rta.Add(asigConEval.Key, promsAlumn);
            
            return rta;
        }*/

    }
}