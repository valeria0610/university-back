﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.BL.Models
{
    [Table("CourseInstructor", Schema = "dbo")]
    public class CourseInstructor
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Course")]
        public int CourseID{ get; set; }
        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }

        //Foranea a primaria
        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
    }
}
