using System.ComponentModel.DataAnnotations;

namespace AssignmentMohsin.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? CourseName { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
