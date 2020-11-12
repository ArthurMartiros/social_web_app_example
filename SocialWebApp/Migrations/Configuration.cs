namespace SocialWebApp.Migrations
{
    using SocialWebApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var students = new List<Student>
            {
                new Student { FirstMidName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    EducationCenterID="06ba0f13-fcba-4d42-b70d-6cb080f7fd2e"
                },
                
                new Student { FirstMidName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01"),
                    EducationCenterID="65fac3a1-64a9-407a-86c4-66446dba4536"
                },
                new Student { FirstMidName = "Arturo",   LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01"),
                    EducationCenterID="8e4e4fdb-bc53-4e31-9df6-ed5043cb3bbc"
                },
                new Student { FirstMidName = "Gytis",    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01"),
                    EducationCenterID="a5176359-44f7-4235-95c5-8bb5ebcd55d4"
                },
                new Student { FirstMidName = "Yan",      LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01"),
                    EducationCenterID="06ba0f13-fcba-4d42-b70d-6cb080f7fd2e"
                },
                new Student { FirstMidName = "Peggy",    LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01"),
                    EducationCenterID="a5176359-44f7-4235-95c5-8bb5ebcd55d4"
                },
                new Student { FirstMidName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01"),
                    EducationCenterID="8e4e4fdb-bc53-4e31-9df6-ed5043cb3bbc"
                },
                new Student { FirstMidName = "Nino",     LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-09-01"),
                    EducationCenterID="8e4e4fdb-bc53-4e31-9df6-ed5043cb3bbc"
                }
            };


            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var instructors = new List<Instructor>
            {
                new Instructor { FirstMidName = "Kim",     LastName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11"),
                    EducationCenterID="06ba0f13-fcba-4d42-b70d-6cb080f7fd2e" },
                new Instructor { FirstMidName = "Fadi",    LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06"),
                    EducationCenterID="65fac3a1-64a9-407a-86c4-66446dba4536"
                },
                new Instructor { FirstMidName = "Roger",   LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01"),
                    EducationCenterID="8e4e4fdb-bc53-4e31-9df6-ed5043cb3bbc"
                },
                new Instructor { FirstMidName = "Candace", LastName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15"),
                    EducationCenterID="a5176359-44f7-4235-95c5-8bb5ebcd55d4"
                },
                new Instructor { FirstMidName = "Roger",   LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12"),
                    EducationCenterID="a5176359-44f7-4235-95c5-8bb5ebcd55d4"
                }
            };
            instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            //var departments = new List<Department>
            //{
            //    new Department { Name = "English",     Budget = 350000,
            //        StartDate = DateTime.Parse("2007-09-01"),
            //        InstructorID  = instructors.Single( i => i.LastName == "Abercrombie").ID },
            //    new Department { Name = "Mathematics", Budget = 100000,
            //        StartDate = DateTime.Parse("2007-09-01"),
            //        InstructorID  = instructors.Single( i => i.LastName == "Fakhouri").ID },
            //    new Department { Name = "Engineering", Budget = 350000,
            //        StartDate = DateTime.Parse("2007-09-01"),
            //        InstructorID  = instructors.Single( i => i.LastName == "Harui").ID },
            //    new Department { Name = "Economics",   Budget = 100000,
            //        StartDate = DateTime.Parse("2007-09-01"),
            //        InstructorID  = instructors.Single( i => i.LastName == "Kapoor").ID }
            //};
            //departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            //context.SaveChanges();

            var courses = new List<Course>
            {
                new Course {CourseID = 1050, Title = "Chemistry",      Credits = 3,
                  Instructors = new List<Instructor>(),
                    EducationCenterID="a5176359-44f7-4235-95c5-8bb5ebcd55d4"

                },
                new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3,
                  Instructors = new List<Instructor>(),
                    EducationCenterID="a5176359-44f7-4235-95c5-8bb5ebcd55d4"

                },
                new Course {CourseID = 4041, Title = "Macroeconomics", Credits = 3,
                  Instructors = new List<Instructor>(),
                    EducationCenterID="06ba0f13-fcba-4d42-b70d-6cb080f7fd2e"

                },
                new Course {CourseID = 1045, Title = "Calculus",       Credits = 4,
                  Instructors = new List<Instructor>(),
                    EducationCenterID="8e4e4fdb-bc53-4e31-9df6-ed5043cb3bbc"

                },
                new Course {CourseID = 3141, Title = "Trigonometry",   Credits = 4,
                  Instructors = new List<Instructor>(),
                    EducationCenterID="8e4e4fdb-bc53-4e31-9df6-ed5043cb3bbc"


                },
                new Course {CourseID = 2021, Title = "Composition",    Credits = 3,
                  Instructors = new List<Instructor>(),
                    EducationCenterID="06ba0f13-fcba-4d42-b70d-6cb080f7fd2e"

                },
                new Course {CourseID = 2042, Title = "Literature",     Credits = 4,
                  Instructors = new List<Instructor>(),
                    EducationCenterID="a5176359-44f7-4235-95c5-8bb5ebcd55d4"

                },
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Fakhouri").ID,
                    Location = "Smith 17" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Harui").ID,
                    Location = "Gowan 27" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Kapoor").ID,
                    Location = "Thompson 304" },
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.InstructorID, s));
            context.SaveChanges();

            AddOrUpdateInstructor(context, "Chemistry", "Kapoor");
            AddOrUpdateInstructor(context, "Chemistry", "Harui");
            AddOrUpdateInstructor(context, "Microeconomics", "Zheng");
            AddOrUpdateInstructor(context, "Macroeconomics", "Zheng");

            AddOrUpdateInstructor(context, "Calculus", "Fakhouri");
            AddOrUpdateInstructor(context, "Trigonometry", "Harui");
            AddOrUpdateInstructor(context, "Composition", "Abercrombie");
            AddOrUpdateInstructor(context, "Literature", "Abercrombie");

            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    Grade = Grade.A
                },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                    Grade = Grade.C
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                    Grade = Grade.B
                 },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Li").ID,
                    CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Justice").ID,
                    CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                    Grade = Grade.B
                 }
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                         s.Student.ID == e.StudentID &&
                         s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }

        void AddOrUpdateInstructor(ApplicationDbContext context, string courseTitle, string instructorName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs.Instructors.SingleOrDefault(i => i.LastName == instructorName);
            if (inst == null)
                crs.Instructors.Add(context.Instructors.Single(i => i.LastName == instructorName));
        }
    }
}
