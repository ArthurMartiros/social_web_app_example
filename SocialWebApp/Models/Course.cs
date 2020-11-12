using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialWebApp.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        [Display(Name ="Start Date Of Course")]
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name ="End Date Of Course")]
        public DateTime? FinishDate { get; set; }

        //public int DepartmentID { get; set; }
        public string EducationCenterID { get; set; }

        //public virtual Department Department { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ApplicationUser EducationCenter { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}