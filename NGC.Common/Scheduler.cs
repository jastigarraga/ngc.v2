using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.RuntimeInformation;
using System.Diagnostics;
using System.IO;

namespace NGC.Common
{
    public class Scheduler
    {
       public class ScheduledTask
        {
            public string Name { get; set; }
            public string Commmand { get; set; }
            public DateTime DateStart { get; set; }

            public TaskType TaskType { get; set; }
            
        }
        public enum TaskType
        {
            OneTime = 0,
            Daily = 1,
            Yearly = 2
        }
        public Scheduler()
        {
            if (IsOSPlatform(OSPlatform.Windows))
            {
                _getTasks = () => {
                    IEnumerable<ScheduledTask> tasks = null;
                    using (Process process = new Process())
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.CreateNoWindow = true;
                        startInfo.FileName = "cmd.exe";
                        startInfo.Arguments = "/C schtasks /query /fo CSV /v";
                        process.StartInfo = startInfo;
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.RedirectStandardError = true;
                        process.Start();
                        var results = process.StandardOutput.ReadToEnd().Split('\r').Where(l => !l.StartsWith("\"Nombre de")).Select(l => l.Split(',')); ;
                        //tasks = result.Split("\"Nombre de host\",\"Nombre de tarea\",\"Hora próxima ejecución\",\"Estado\",\"Modo de inicio de sesión\",\"Último tiempo de ejecución\",\"Último resultado\",\"Autor\",\"Tarea que se ejecutará\",\"Iniciar en\",\"Comentario\",\"Estado de tarea programada\",\"Tiempo de inactividad\",\"Administración de energía\",\"Ejecutar como usuario\",\"Eliminar tarea si no se vuelve a programar\",\"Eliminar tarea si ejecuta durante X horas y X minutos\",\"Programación\",\"Tipo de programación\",\"Hora de inicio\",\"Fecha de inicio\",\"Fecha final\",\"Días\",\"Meses\",\"Repetir: cada\",\"Repetir: hasta: hora\",\"Repetir: hasta: duración\",\"Repetir: detener si aún se ejecuta\"")
                    }

                    return tasks;
                };
            }
            else
            {
               
            }

        }
        private Func<IEnumerable<ScheduledTask>> _getTasks;
        private Func<ScheduledTask, Boolean> _schedule,_unschedule;

        public IEnumerable<ScheduledTask> GetTasks()
        {
            return _getTasks.Invoke();
        }
        public bool Schedule(ScheduledTask task)
        {
            return _schedule.Invoke(task);
        }
        public bool UnSchedule(ScheduledTask task)
        {
            return _unschedule.Invoke(task);
        }
    }
}
