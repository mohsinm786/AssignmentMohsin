using System.ComponentModel.DataAnnotations;

namespace AssignmentMohsin.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [StringLength(50)]
        public string? Email { get; set; }

        [Required]
        [StringLength(15)]
        public string? Phone { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
