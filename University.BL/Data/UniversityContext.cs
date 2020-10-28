using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using University.BL.Models;


namespace University.BL.Data
{
    public class UniversityContext : DbContext
    {
        private static UniversityContext universityContext = null;
        public UniversityContext() : base("UniversityContext")
        {

        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments  { get; set; }
        public DbSet<CourseInstructor> CourseInstructores  { get; set; }
        public DbSet<Instructor> Instructores  { get; set; }
        public DbSet<Department> Departments  { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments  { get; set; }

        public static UniversityContext Create()
        {
            if (universityContext == null)
                universityContext = new UniversityContext();

            return  universityContext;
        }

    }
}


//Atajos
//control + K + D acomoda el texto
//control + K + C comentarial
//control + K + U descomentarial
//control + K + S rodeamos el codigo
//prop doble tap = public int ID { get; set; }
//control + D al final de la linea duplicar
//ctor doble tap crea el metodo constructor