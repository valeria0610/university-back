using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class CourseDTO
    {
        [Required(ErrorMessage ="El campo Course Id es requerido")]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "El campo Titulo es requerido")]
        [StringLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "El campo Credito es requerido")]
        public int Credits { get; set; }
    }
}
