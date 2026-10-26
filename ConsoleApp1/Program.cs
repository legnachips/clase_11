using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static int max_estudiantes = 40;
        static int max_cursos = 4;
        static string[] nombre_de_cursos = { "matematicas", "programacion", "base de datos", "fisica" };
        static string[] estudiantes = new string[max_estudiantes];
        static double[,] matrizNotas = new double[max_estudiantes, max_cursos];
        static int total_estudiantes = 0;
        static string rutaArchivo = "notas_cursos";
        public static void RegistrosEstudiantes()
        {
            Console.WriteLine("Registos de estudiante");
            if (total_estudiantes >= max_estudiantes)
            {
                Console.WriteLine("Capacidad maxima de estudiantes alcanzada ");
                return;
            }
            string nombre = validador.LeerTexto("Ingresar nombres y apellidos").ToUpper();
            estudiantes[total_estudiantes] = nombre;
            Console.WriteLine("Ingresar notas para cada curso(0 a 20)");
            for(int i=0;i<max_cursos; i++)
            {
                matrizNotas[total_estudiantes, i] = validador.LeerDecimal($"-{nombre_de_cursos[i]}:");
            }
            total_estudiantes++;
            Console.WriteLine("Estudiante Registro..!");
        }
        public static void GuardarArchivos()
        {
            Console.WriteLine("GUARDAR ARCHIVO");
            if (total_estudiantes == 0)
            {
                Console.WriteLine("no hay registros por guardar");
                return;
            }
            using(StreamWriter escribir=new StreamWriter(rutaArchivo))
            {
                for (int i = 0; 1 < total_estudiantes; i++)
                {
                    string linea = estudiantes[i];
                    for(int j=0;j<max_cursos; j++)
                    {
                        linea += $",{matrizNotas[i, j]}";
                        //linea += "," + matrizNotas[i,j];
                    }
                    escribir.WriteLine(linea);
                }
            }
            Console.WriteLine("registros guardados");
        }
        public static void CargarRegistros()
        {
            Console.WriteLine("cargar registros desde archivo");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("no encontro archivo..");
                return;
            }
            total_estudiantes = 0; 
            using(StreamReader lector=new StreamReader(rutaArchivo))
            {
                string linea;
                while((linea=lector.ReadLine())!=null && total_estudiantes < max_estudiantes){
                    string[] partes = linea.Split(',');
                    if (partes.Length == max_cursos + 1)
                    {
                        estudiantes[total_estudiantes] = partes[0];
                        for(int i=0; i < max_cursos; i++)
                        {
                            matrizNotas[total_estudiantes, i] = Convert.ToDouble(partes[i + 1]);
                        }
                        total_estudiantes++;                                       
                    }
                }
            }
            Console.WriteLine("se cargaron " + total_estudiantes + " con sus notas");
        }
        public static void mostrarRegsitros()
        {
            Console.WriteLine(new string('=', 100));
            Console.WriteLine("\t\tREPORTE DE NOATS PRO CURSOS");
            Console.WriteLine(new string('=',100));
            if (total_estudiantes == 0)
            {
                Console.WriteLine("no hay registros");
                return;
            }
            Console.Write($"{"ESTUDIANTE",-25}|");
            for(int i = 0; i < max_cursos; i++)
            {
                Console.Write($"{nombre_de_cursos[i],-12}|");
            }
            Console.WriteLine("PROMEDIO");
            Console.WriteLine(new string('-', 100));
            for(int i = 0; 1 < total_estudiantes; i++)
            {
                Console.Write($"{estudiantes[i],25}|");
                double suma = 0;
                for(int j = 0; j < max_cursos; j++)
                {
                    Console.Write($"{matrizNotas[i, j],-12:F1}");
                    suma += matrizNotas[i, j];
                }
                double promedio = suma / max_cursos;
                string estado = promedio >= 12 ? "APROBADO" : "DESAPROBADO";
                Console.WriteLine($"{promedio:F2}({estado})");
            }
        }
        static void Main(string[] args)
        {
            int opc = 0;
            while (opc != 5)
            {
                Console.WriteLine("[1]Registrar estudiante");
                Console.WriteLine("[2]Guardar en archivo");
                Console.WriteLine("[3]Cargar el archivo");
                Console.WriteLine("[4]Mostrar los registros");
                Console.WriteLine("[5]salir");
                opc = validador.LeerEnteros("Selecciona una opcion[1-5]");
                switch (opc)
                {
                    case 1:
                        RegistrosEstudiantes(); break;
                    case 2:
                        GuardarArchivos(); break;
                    case 3:
                        CargarRegistros(); break;
                    case 4:
                        mostrarRegsitros(); break;
                    case 5:
                        Console.WriteLine("muchos exitos para tu examne final"); break;

                }
                Console.ReadKey();
            }
        }
    }
}