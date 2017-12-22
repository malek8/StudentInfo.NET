using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentInfo.Data;
using StudentInfo.Dto;
using StudentInfo.Enums;

namespace StudentInfo.WebClient.App_Start
{
    public static class AdminPublishCommands
    {
        public static void PublishCourses()
        {
            var db = new StudentInfoContext();

            // Computer Science
            #region
            var computerScienceDept = db.Departments.FirstOrDefault(x => x.Name == "Computer Science & Software Engineering");
            if (computerScienceDept != null)
            {
                if (!db.Courses.Any(x => x.Code == "COMP 5261"))
                {
                    db.Courses.Add(new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "COMPUTER ARCHITECTURE",
                        Code = "COMP 5261",
                        Description = "Description",
                        NumberOfCredits = 4,
                        Level = ProgramLevel.Graduate,
                        Department = computerScienceDept
                    });
                }

                if (!db.Courses.Any(x => x.Code == "SOEN 6841"))
                {
                    db.Courses.Add(new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "SOFTWARE PROJECT MANAGEMENT",
                        Code = "SOEN 6841",
                        Description = "Description",
                        NumberOfCredits = 4,
                        Level = ProgramLevel.Graduate,
                        Department = computerScienceDept
                    });
                }

                if (!db.Courses.Any(x => x.Code == "COMP 6231"))
                {
                    db.Courses.Add(new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "DISTRIBUTED SYSTEM DESIGN",
                        Code = "COMP 6231",
                        Description = "Description",
                        NumberOfCredits = 4,
                        Level = ProgramLevel.Graduate,
                        Department = computerScienceDept
                    });
                }

                if (!db.Courses.Any(x => x.Code == "COMP 691"))
                {
                    db.Courses.Add(new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "TOPICS/COMPUTER SCIENCE",
                        Code = "COMP 691",
                        Description = "Description",
                        NumberOfCredits = 4,
                        Level = ProgramLevel.Graduate,
                        Department = computerScienceDept
                    });
                }

                if (!db.Courses.Any(x => x.Code == "SOEN 691"))
                {
                    db.Courses.Add(new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "TOPICS/SOFTWARE ENGINEERING",
                        Code = "SOEN 691",
                        Description = "Description",
                        NumberOfCredits = 4,
                        Level = ProgramLevel.Graduate,
                        Department = computerScienceDept
                    });
                }

                if (!db.Courses.Any(x => x.Code == "SOEN 6441"))
                {
                    db.Courses.Add(new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "ADV. PROG. PRACTICES",
                        Code = "SOEN 6441",
                        Description = "Description",
                        NumberOfCredits = 4,
                        Level = ProgramLevel.Graduate,
                        Department = computerScienceDept
                    });
                }

                if (!db.Courses.Any(x => x.Code == "COMP 6281"))
                {
                    db.Courses.Add(new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "PARALLEL PROGRAMMING",
                        Code = "COMP 6281",
                        Description = "Description",
                        NumberOfCredits = 4,
                        Level = ProgramLevel.Graduate,
                        Department = computerScienceDept
                    });
                }

                if (!db.Courses.Any(x => x.Code == "SOEN 6431"))
                {
                    db.Courses.Add(new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "SOFT. COMP.& MAINTENANCE",
                        Code = "SOEN 6431",
                        Description = "Description",
                        NumberOfCredits = 4,
                        Level = ProgramLevel.Graduate,
                        Department = computerScienceDept
                    });
                }

                if (!db.Courses.Any(x => x.Code == "COMP 6961"))
                {
                    db.Courses.Add(new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "SOFT. COMP.& MAINTENANCE",
                        Code = "COMP 6961",
                        Description = "Description",
                        NumberOfCredits = 4,
                        Level = ProgramLevel.Graduate,
                        Department = computerScienceDept
                    });
                }
            }

            db.SaveChanges();
            #endregion

            // Art
            #region
            var artDept = db.Departments.FirstOrDefault(x => x.Name == "ART EDUCATION");
            if (artDept != null)
            {
                if (!db.Courses.Any(x => x.Code == "ARTE 201"))
                {
                    db.Courses.Add(new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "Art in Early Childhood I",
                        Code = "ARTE 201",
                        Description = "Description",
                        NumberOfCredits = 4,
                        Level = ProgramLevel.Undergraduate,
                        Department = artDept
                    });
                }

                if (!db.Courses.Any(x => x.Code == "ARTE 230"))
                {
                    db.Courses.Add(new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "Practicum: Observation and Analysis of Children’s Learning",
                        Code = "ARTE 230",
                        Description = "Description",
                        NumberOfCredits = 4,
                        Level = ProgramLevel.Undergraduate,
                        Department = artDept
                    });
                }

                if (!db.Courses.Any(x => x.Code == "ARTE 330"))
                {
                    db.Courses.Add(new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "Introduction to Community Art Education",
                        Code = "ARTE 330",
                        Description = "Description",
                        NumberOfCredits = 4,
                        Level = ProgramLevel.Undergraduate,
                        Department = artDept
                    });
                }

                if (!db.Courses.Any(x => x.Code == "ARTE 352"))
                {
                    db.Courses.Add(new Course
                    {
                        Id = Guid.NewGuid(),
                        Name = "Light Based Media",
                        Code = "ARTE 352",
                        Description = "Description",
                        NumberOfCredits = 4,
                        Level = ProgramLevel.Undergraduate,
                        Department = artDept
                    });
                }

                db.SaveChanges();
            }
            #endregion
        }

        public static void PublishClassrooms()
        {
            var db = new StudentInfoContext();

            if (!db.Classrooms.Any(x => x.Number == "H2.120"))
            {
                db.Classrooms.Add(new Classroom
                {
                    Id = Guid.NewGuid(),
                    Number = "H2.120",
                    Campus = "SGW",
                    Capacity = 30
                });
            }

            if (!db.Classrooms.Any(x => x.Number == "H2.130"))
            {
                db.Classrooms.Add(new Classroom
                {
                    Id = Guid.NewGuid(),
                    Number = "H2.130",
                    Campus = "SGW",
                    Capacity = 30
                });
            }

            if (!db.Classrooms.Any(x => x.Number == "H2.140"))
            {
                db.Classrooms.Add(new Classroom
                {
                    Id = Guid.NewGuid(),
                    Number = "H2.140",
                    Campus = "SGW",
                    Capacity = 30
                });
            }

            if (!db.Classrooms.Any(x => x.Number == "H2.140"))
            {
                db.Classrooms.Add(new Classroom
                {
                    Id = Guid.NewGuid(),
                    Number = "H2.140",
                    Campus = "SGW",
                    Capacity = 30
                });
            }

            if (!db.Classrooms.Any(x => x.Number == "H2.150"))
            {
                db.Classrooms.Add(new Classroom
                {
                    Id = Guid.NewGuid(),
                    Number = "H2.150",
                    Campus = "SGW",
                    Capacity = 30
                });
            }

            if (!db.Classrooms.Any(x => x.Number == "H2.160"))
            {
                db.Classrooms.Add(new Classroom
                {
                    Id = Guid.NewGuid(),
                    Number = "H2.160",
                    Campus = "SGW",
                    Capacity = 30
                });
            }

            if (!db.Classrooms.Any(x => x.Number == "H2.170"))
            {
                db.Classrooms.Add(new Classroom
                {
                    Id = Guid.NewGuid(),
                    Number = "H2.170",
                    Campus = "SGW",
                    Capacity = 30
                });
            }

            if (!db.Classrooms.Any(x => x.Number == "H2.180"))
            {
                db.Classrooms.Add(new Classroom
                {
                    Id = Guid.NewGuid(),
                    Number = "H2.180",
                    Campus = "SGW",
                    Capacity = 30
                });
            }

            if (!db.Classrooms.Any(x => x.Number == "H2.190"))
            {
                db.Classrooms.Add(new Classroom
                {
                    Id = Guid.NewGuid(),
                    Number = "H2.190",
                    Campus = "SGW",
                    Capacity = 30
                });
            }

            db.SaveChanges();
        }
    }
}