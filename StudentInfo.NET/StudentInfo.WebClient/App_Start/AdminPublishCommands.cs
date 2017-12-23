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

        public static void PublishFaculties()
        {
            var db = new StudentInfoContext();

            #region Art Faculty
            var artFaculty = db.Faculties.FirstOrDefault(x => x.Name == "Faculty of Fine Arts");
            if (artFaculty == null)
            {
                artFaculty = new Faculty()
                {
                    Id = Guid.NewGuid(),
                    Name = "Faculty of Fine Arts"
                };

                db.Faculties.Add(artFaculty);
                db.SaveChanges();
            }
            if (artFaculty != null)
            {
                var d1 = db.Departments.FirstOrDefault(x => x.Name == "ART HISTORY");
                if (d1 == null)
                {
                    d1 = new Department()
                    {
                        Id = Guid.NewGuid(),
                        Name = "ART HISTORY",
                        Faculty = artFaculty
                    };

                    db.SaveChanges();
                }
                if (d1 != null)
                {
                    #region Programs
                    if (!db.Programs.Any(x => x.Name == "Art History (BFA)"))
                    {
                        db.Programs.Add(new Program
                        {
                            Id = Guid.NewGuid(),
                            Name = "Art History (BFA)",
                            DegreeType = DegreeType.Major,
                            Level = ProgramLevel.Undergraduate,
                            StudyType = ProgramStudyType.CourseBased,
                            TermCost = 350,
                            TotalCredits = 120,
                            Department = d1
                        });

                        db.SaveChanges();
                    }

                    if (!db.Programs.Any(x => x.Name == "Art History and Film Studies"))
                    {
                        db.Programs.Add(new Program
                        {
                            Id = Guid.NewGuid(),
                            Name = "Art History and Film Studies",
                            DegreeType = DegreeType.Major,
                            Level = ProgramLevel.Undergraduate,
                            StudyType = ProgramStudyType.CourseBased,
                            TermCost = 350,
                            TotalCredits = 120,
                            Department = d1
                        });

                        db.SaveChanges();
                    }
                    #endregion

                    #region Courses
                    if (!db.Courses.Any(x => x.Code == "ARTH 614"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "ARTH 614",
                            Name = "EXAMINING THE CRAFT AND ARTISAN TRADITIONS IN NORTH AMERICA",
                            Description = "Description",
                            Level = ProgramLevel.Graduate,
                            NumberOfCredits = 4,
                            Department = d1
                        });

                        db.SaveChanges();
                    }

                    if (!db.Courses.Any(x => x.Code == "ARTH 647"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "ARTH 647",
                            Name = "INDEPENDENT STUDIES IN NORTH AMERICAN ART HISTORY",
                            Description = "Description",
                            Level = ProgramLevel.Graduate,
                            NumberOfCredits = 4,
                            Department = d1
                        });

                        db.SaveChanges();
                    }

                    if (!db.Courses.Any(x => x.Code == "ARTH 654"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "ARTH 654",
                            Name = "ANNOTATED REVIEW OF SOURCES AND DOCUMENTS",
                            Description = "Description",
                            Level = ProgramLevel.Graduate,
                            NumberOfCredits = 4,
                            Department = d1
                        });

                        db.SaveChanges();
                    }

                    if (!db.Courses.Any(x => x.Code == "ARTH 264"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "ARTH 264",
                            Name = "ASPECTS/HISTORY CERAMICS",
                            Description = "Description",
                            Level = ProgramLevel.Undergraduate,
                            NumberOfCredits = 4,
                            Department = d1
                        });

                        db.SaveChanges();
                    }
                    #endregion
                }

                var d2 = db.Departments.FirstOrDefault(x => x.Name == "ART EDUCATION");
                if (d2 == null)
                {
                    d2 = new Department()
                    {
                        Id = Guid.NewGuid(),
                        Name = "ART EDUCATION",
                        Faculty = artFaculty
                    };

                    db.SaveChanges();
                }
                if (d2 != null)
                {
                    #region Programs
                    if (!db.Programs.Any(x => x.Name == "Education - Bachelor of Fine Arts (BFA)"))
                    {
                        db.Programs.Add(new Program
                        {
                            Id = Guid.NewGuid(),
                            Name = "Education - Bachelor of Fine Arts (BFA)",
                            DegreeType = DegreeType.Major,
                            Level = ProgramLevel.Undergraduate,
                            StudyType = ProgramStudyType.CourseBased,
                            TermCost = 450,
                            TotalCredits = 120,
                            Department = d2
                        });

                        db.SaveChanges();
                    }
                    if (db.Programs.Any(x => x.Name == "MA in Art Education"))
                    {
                        db.Programs.Add(new Program
                        {
                            Id = Guid.NewGuid(),
                            Name = "MA in Art Education",
                            DegreeType = DegreeType.Master,
                            Level = ProgramLevel.Graduate,
                            StudyType = ProgramStudyType.CourseBased,
                            TermCost = 550,
                            TotalCredits = 45,
                            Department = d2
                        });

                        db.SaveChanges();
                    }
                    #endregion

                    #region Courses
                    if (!db.Courses.Any(x => x.Code == "ARTE 201"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "ARTE 201",
                            Name = "Art in Early Childhood I",
                            Description = "Description",
                            NumberOfCredits = 4,
                            Level = ProgramLevel.Undergraduate,
                            Department = d2
                        });

                        db.SaveChanges();
                    }

                    if (!db.Courses.Any(x => x.Code == "ARTE 230"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "ARTE 230",
                            Name = "Practicum: Observation and Analysis of Children’s Learning",
                            Description = "Description",
                            NumberOfCredits = 4,
                            Level = ProgramLevel.Undergraduate,
                            Department = d2
                        });

                        db.SaveChanges();
                    }

                    if (!db.Courses.Any(x => x.Code == "ARTE 330"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "ARTE 330",
                            Name = " Introduction to Community Art Education",
                            Description = "Description",
                            NumberOfCredits = 4,
                            Level = ProgramLevel.Undergraduate,
                            Department = d2
                        });

                        db.SaveChanges();
                    }

                    if (!db.Courses.Any(x => x.Code == "ARTE 660"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "ARTE 660",
                            Name = "SELECTED TOPICS IN ARTE",
                            Description = "Description",
                            NumberOfCredits = 4,
                            Level = ProgramLevel.Graduate,
                            Department = d2
                        });

                        db.SaveChanges();
                    }

                    if (!db.Courses.Any(x => x.Code == "ARTE 398"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "ARTE 398",
                            Name = " SPECIAL TOPICS IN ARTE",
                            Description = "Description",
                            NumberOfCredits = 4,
                            Level = ProgramLevel.Undergraduate,
                            Department = d2
                        });

                        db.SaveChanges();
                    }
                    #endregion
                }

                var d3 = db.Departments.FirstOrDefault(x => x.Name == "MUSIC");
                if (d3 == null)
                {
                    d3 = new Department()
                    {
                        Id = Guid.NewGuid(),
                        Name = "MUSIC",
                        Faculty = artFaculty
                    };

                    db.SaveChanges();
                }
                if (d3 != null)
                {
                    #region Programs
                    if (!db.Programs.Any(x => x.Name == "Music - Bachelor of Fine Arts (BFA)"))
                    {
                        db.Programs.Add(new Program
                        {
                            Id = Guid.NewGuid(),
                            Name = "Music - Bachelor of Fine Arts (BFA)",
                            DegreeType = DegreeType.Major,
                            Level = ProgramLevel.Undergraduate,
                            StudyType = ProgramStudyType.CourseBased,
                            TermCost = 250,
                            TotalCredits = 54,
                            Department = d3
                        });

                        db.SaveChanges();
                    }
                    #endregion

                    #region Courses
                    if (!db.Courses.Any(x => x.Code == "EAST 231"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "EAST 231",
                            Name = " Sound For Artists",
                            Description = "Description",
                            Level = ProgramLevel.Undergraduate,
                            NumberOfCredits = 4,
                            Department = d3
                        });

                        db.SaveChanges();
                    }

                    if (!db.Courses.Any(x => x.Code == "EAST 252"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "EAST 252",
                            Name = "Introduction to Recording II",
                            Description = "Description",
                            Level = ProgramLevel.Undergraduate,
                            NumberOfCredits = 4,
                            Department = d3
                        });

                        db.SaveChanges();
                    }

                    if (!db.Courses.Any(x => x.Code == "EAST 452"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "EAST 452",
                            Name = "Advanced Recording II",
                            Description = "Description",
                            Level = ProgramLevel.Undergraduate,
                            NumberOfCredits = 4,
                            Department = d3
                        });

                        db.SaveChanges();
                    }

                    if (!db.Courses.Any(x => x.Code == "EAST 452"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "EAST 452",
                            Name = "Advanced Recording II",
                            Description = "Description",
                            Level = ProgramLevel.Undergraduate,
                            NumberOfCredits = 4,
                            Department = d3
                        });

                        db.SaveChanges();
                    }
                    #endregion
                }
            }
            #endregion

            #region Engineering Faculty
            var engineeringFaculty = db.Faculties.FirstOrDefault(x => x.Name == "Faculty of Engineering and Computer Science");

            if (engineeringFaculty == null)
            {
                engineeringFaculty = new Faculty
                {
                    Id = Guid.NewGuid(),
                    Name = "Faculty of Engineering and Computer Science"
                };

                db.Faculties.Add(engineeringFaculty);

                db.SaveChanges();
            }
            if (engineeringFaculty != null)
            {
                var d1 = db.Departments.FirstOrDefault(x => x.Name == "Computer Science & Software Engineering");
                if (d1 == null)
                {
                    d1 = new Department
                    {
                        Id = Guid.NewGuid(),
                        Name = "Computer Science & Software Engineering",
                        Faculty = engineeringFaculty
                    };

                    db.Departments.Add(d1);

                    db.SaveChanges();
                }
                if (d1 != null)
                {
                    #region Programs
                    if (!db.Programs.Any(x => x.Name == "Master in Applied Computer Science (MApCompSc)"))
                    {
                        db.Programs.Add(new Program
                        {
                            Id = Guid.NewGuid(),
                            Name = "Master in Applied Computer Science (MApCompSc)",
                            Level = ProgramLevel.Graduate,
                            DegreeType = DegreeType.Master,
                            StudyType = ProgramStudyType.CourseBased,
                            TermCost = 350,
                            TotalCredits = 4,
                            Department = d1
                        });

                        db.SaveChanges();
                    }

                    if (!db.Programs.Any(x => x.Name == "Master of Engineering, Software Engineering (MEng)"))
                    {
                        db.Programs.Add(new Program
                        {
                            Id = Guid.NewGuid(),
                            Name = "Master of Engineering, Software Engineering (MEng)",
                            Level = ProgramLevel.Graduate,
                            DegreeType = DegreeType.Master,
                            StudyType = ProgramStudyType.CourseBased,
                            TermCost = 350,
                            TotalCredits = 4,
                            Department = d1
                        });

                        db.SaveChanges();
                    }

                    if (!db.Programs.Any(x => x.Name == "Graduate diploma in Computer Science (GrDip)"))
                    {
                        db.Programs.Add(new Program
                        {
                            Id = Guid.NewGuid(),
                            Name = "Graduate diploma in Computer Science (GrDip)",
                            Level = ProgramLevel.Graduate,
                            DegreeType = DegreeType.GraduateDiploma,
                            StudyType = ProgramStudyType.CourseBased,
                            TermCost = 250,
                            TotalCredits = 4,
                            Department = d1
                        });

                        db.SaveChanges();
                    }

                    if (!db.Programs.Any(x => x.Name == "Master in Computer Science (MCompSc)"))
                    {
                        db.Programs.Add(new Program
                        {
                            Id = Guid.NewGuid(),
                            Name = "Master in Computer Science (MCompSc)",
                            Level = ProgramLevel.Graduate,
                            DegreeType = DegreeType.Master,
                            StudyType = ProgramStudyType.ThesisBased,
                            TermCost = 450,
                            TotalCredits = 4,
                            Department = d1
                        });

                        db.SaveChanges();
                    }

                    if (!db.Programs.Any(x => x.Name == "Master in Applied Science, Software Engineering (MASc)"))
                    {
                        db.Programs.Add(new Program
                        {
                            Id = Guid.NewGuid(),
                            Name = "Master in Applied Science, Software Engineering (MASc)",
                            Level = ProgramLevel.Graduate,
                            DegreeType = DegreeType.Master,
                            StudyType = ProgramStudyType.ThesisBased,
                            TermCost = 450,
                            TotalCredits = 4,
                            Department = d1
                        });

                        db.SaveChanges();
                    }
                    #endregion

                    #region Courses
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
                            Department = d1
                        });

                        db.SaveChanges();
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
                            Department = d1
                        });

                        db.SaveChanges();
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
                            Department = d1
                        });

                        db.SaveChanges();
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
                            Department = d1
                        });

                        db.SaveChanges();
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
                            Department = d1
                        });

                        db.SaveChanges();
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
                            Department = d1
                        });

                        db.SaveChanges();
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
                            Department = d1
                        });

                        db.SaveChanges();
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
                            Department = d1
                        });

                        db.SaveChanges();
                    }

                    if (!db.Courses.Any(x => x.Code == "SOEN 6431"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Name = "GRADUATE SEMINAR-COMP.SC.",
                            Code = "COMP 6961",
                            Description = "Description",
                            NumberOfCredits = 1,
                            Level = ProgramLevel.Graduate,
                            Department = d1
                        });

                        db.SaveChanges();
                    }
                    #endregion
                }

                var d2 = db.Departments.FirstOrDefault(x => x.Name == "Electrical and Computer Engineering");
                if (d2 == null)
                {
                    d2 = new Department
                    {
                        Id = Guid.NewGuid(),
                        Name = "Electrical and Computer Engineering",
                        Faculty = engineeringFaculty
                    };

                    db.Departments.Add(d2);

                    db.SaveChanges();
                }

                if (d2 != null)
                {

                    #region Programs
                    if (!db.Programs.Any(x => x.Name == "Electrical & Computer Engineering (MEng)"))
                    {
                        db.Programs.Add(new Program
                        {
                            Id = Guid.NewGuid(),
                            Name = "Electrical & Computer Engineering (MEng)",
                            DegreeType = DegreeType.Master,
                            Level = ProgramLevel.Graduate,
                            StudyType = ProgramStudyType.CourseBased,
                            TermCost = 450,
                            TotalCredits = 45,
                            Department = d2
                        });

                        db.SaveChanges();
                    }
                    #endregion

                    #region Courses
                    if (!db.Courses.Any(x => x.Code == "COEN 6321"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "COEN 6321",
                            Name = "Applied Genetic and Evolutionary Systems",
                            Description = "Description",
                            NumberOfCredits = 4,
                            Level = ProgramLevel.Graduate,
                            Department = d2
                        });

                        db.SaveChanges();
                    }

                    if (!db.Courses.Any(x => x.Code == "COEN 6741"))
                    {
                        db.Courses.Add(new Course
                        {
                            Id = Guid.NewGuid(),
                            Code = "COEN 6741",
                            Name = "COMPUTER ARCHITECTURE+DESIGN",
                            Description = "Description",
                            NumberOfCredits = 4,
                            Level = ProgramLevel.Graduate,
                            Department = d2
                        });

                        db.SaveChanges();
                    }
                    #endregion
                }
            }
            #endregion
        }
    }
}