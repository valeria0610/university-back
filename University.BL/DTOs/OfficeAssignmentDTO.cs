using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class OfficeAssignmentDTO
    {
        public OfficeAssignmentDTO()
        {
            Instructor = new InstructorDTO();
        }

        public int InstructorID { get; set; }
        [StringLength(50)]
        public string Location { get; set; }

        //Foranea a primaria
        public InstructorDTO Instructor { get; set; }
    }
}
