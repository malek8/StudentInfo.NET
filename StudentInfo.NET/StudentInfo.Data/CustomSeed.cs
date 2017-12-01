using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using StudentInfo.Enums;
using StudentInfo.Users.Dto;

namespace StudentInfo.Data
{
    public static class CustomSeed
    {
        public static void AssignUserRoles()
        {
            var context = UserDbContext.UserDbContext.Create();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var adminUser = userManager.FindByEmail("malek.atwiz@hotmail.com");
            if (adminUser != null) userManager.AddToRole(adminUser.Id, SystemRoles.Administrator);

            var studentUser = userManager.FindByEmail("bruce.wayne@batman.com");
            if (studentUser != null) userManager.AddToRole(studentUser.Id, SystemRoles.Student);

            var teacherUser = userManager.FindByEmail("mr.garrison@southpark.com");
            if (teacherUser != null) userManager.AddToRole(teacherUser.Id, SystemRoles.Instructor);

            var facultyUser = userManager.FindByEmail("donald.duck@disney.com");
            if (facultyUser != null) userManager.AddToRole(facultyUser.Id, SystemRoles.FacultyMember);
        }

        public static void AddSemesterCourses()
        {
            var context = new StudentInfoContext();

            if (context.SemesterCourses.Count() == 0)
            {
                // Winter
                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.112"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "COMP 6281"),
                    CourseDate = DateTime.Now,
                    Term = Term.Winter
                });

                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.114"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "SOEN 6441"),
                    CourseDate = DateTime.Now,
                    Term = Term.Winter
                });

                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.116"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "SOEN 6431"),
                    CourseDate = DateTime.Now,
                    Term = Term.Winter
                });

                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.118"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "COMP 6231"),
                    CourseDate = DateTime.Now,
                    Term = Term.Winter
                });

                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.120"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "COMP 691"),
                    CourseDate = DateTime.Now,
                    Term = Term.Winter
                });

                // Fall
                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.112"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "COMP 6281"),
                    CourseDate = DateTime.Now,
                    Term = Term.Fall
                });

                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.114"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "SOEN 6441"),
                    CourseDate = DateTime.Now,
                    Term = Term.Fall
                });

                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.116"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "SOEN 6431"),
                    CourseDate = DateTime.Now,
                    Term = Term.Fall
                });

                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.118"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "COMP 6231"),
                    CourseDate = DateTime.Now,
                    Term = Term.Fall
                });

                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.120"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "COMP 691"),
                    CourseDate = DateTime.Now,
                    Term = Term.Fall
                });

                // Summer 1
                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.112"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "COMP 6281"),
                    CourseDate = DateTime.Now,
                    Term = Term.Summer1
                });

                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.114"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "SOEN 6441"),
                    CourseDate = DateTime.Now,
                    Term = Term.Summer1
                });

                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.116"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "SOEN 6431"),
                    CourseDate = DateTime.Now,
                    Term = Term.Summer1
                });

                // Summer 2
                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.118"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "COMP 6231"),
                    CourseDate = DateTime.Now,
                    Term = Term.Summer2
                });

                context.SemesterCourses.Add(new SemesterCourse
                {
                    Id = Guid.NewGuid(),
                    Classroom = context.Classrooms.FirstOrDefault(x => x.Number == "H2.120"),
                    Course = context.Courses.FirstOrDefault(x => x.Code == "COMP 691"),
                    CourseDate = DateTime.Now,
                    Term = Term.Summer2
                });

                context.SaveChanges();
            }
        }
    }
}
