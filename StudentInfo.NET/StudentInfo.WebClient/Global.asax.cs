using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StudentInfo.Enums;
using StudentInfo.UsersManager;
using StudentInfo.CourseManager;

namespace StudentInfo.WebClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CustomSeed();
            //Data.CustomSeed.AddSemesterCourses();
        }

        private void CustomSeed()
        {
            AssignUserRoles();
            //CreateCourses();
        }

        private void AssignUserRoles()
        {
            var userService = new UsersManagerService();

            userService.AssignRole("malek.atwiz@hotmail.com", SystemRoles.Administrator);
            userService.AssignRole("bruce.wayne@batman.com", SystemRoles.Student);
            userService.AssignRole("mr.garrison@southpark.com", SystemRoles.Instructor);
            userService.AssignRole("donald.duck@disney.com", SystemRoles.FacultyMember);
        }

        private void CreateCourses()
        {
            var classroomService = new ClassroomService();
            var facultyService = new FacultyService();
            var departmentService = new DepartmentService();
            var programService = new ProgramService();
            var courseService = new CourseService();

            var class1 = classroomService.CreateClassroom(Constants.SgwCampus, "H2.110", 20);
            var class2 = classroomService.CreateClassroom(Constants.SgwCampus, "H2.112", 20);
            var class3 = classroomService.CreateClassroom(Constants.SgwCampus, "H2.114", 20);
            var class4 = classroomService.CreateClassroom(Constants.SgwCampus, "H2.116", 20);
            var class5 = classroomService.CreateClassroom(Constants.SgwCampus, "H2.118", 20);
            var class6 = classroomService.CreateClassroom(Constants.SgwCampus, "H2.120", 20);

            var faculty1 = facultyService.CreateFaculty("Faculty of Engineering and Computer Science");
            var faculty2 = facultyService.CreateFaculty("Faculty of Arts and Science");
            var faculty3 = facultyService.CreateFaculty("Faculty of Fine Arts");
            var faculty4 = facultyService.CreateFaculty("John Molson School of Business");

            if (faculty1 != null)
            {
                var department1 = departmentService.CreateDepartment("Building, Civil and Environmental Engineering", faculty1);
                var department2 = departmentService.CreateDepartment("Electrical and Computer Engineering", faculty1);
                var department3 = departmentService.CreateDepartment("Mechanical, Industrial and Aerospace Engineering", faculty1);
                var department4 = departmentService.CreateDepartment("Centre for Engineering in Society", faculty1);
                var department5 = departmentService.CreateDepartment("Computer Science & Software Engineering", faculty1);
                var department6 = departmentService.CreateDepartment("Concordia Institute for Information Systems Engineering (CIISE)", faculty1);
                var department7 = departmentService.CreateDepartment("Concordia Institute for Aerospace Design and Innovation", faculty1);

                if (department2 != null)
                {
                    var program1 = programService.CreateProgram(department2, "Computer Engineering (BEng)", ProgramLevel.Undergraduate, DegreeType.Major, ProgramStudyType.CourseBased);
                    var program2 = programService.CreateProgram(department2, "Electrical Engineering (BEng)", ProgramLevel.Undergraduate, DegreeType.Major, ProgramStudyType.CourseBased);
                    var program3 = programService.CreateProgram(department2, "Electrical & Computer Engineering (MASc)", ProgramLevel.Graduate, DegreeType.Master, ProgramStudyType.ThesisBased);
                    var program4 = programService.CreateProgram(department2, "Electrical & Computer Engineering (MEng)", ProgramLevel.Graduate, DegreeType.Master, ProgramStudyType.CourseBased);
                    var program5 = programService.CreateProgram(department2, "Electrical & Computer Engineering (PhD)", ProgramLevel.Graduate, DegreeType.Doctorate, ProgramStudyType.ThesisBased);
                }

                if (department5 != null)
                {
                    var program6 = programService.CreateProgram(department5, "Master in Computer Science (MCompSc)", ProgramLevel.Graduate, DegreeType.Master, ProgramStudyType.ThesisBased);
                    var program7 = programService.CreateProgram(department5, "Master in Applied Science, Software Engineering (MASc)", ProgramLevel.Graduate, DegreeType.Master, ProgramStudyType.ThesisBased);
                    var program8 = programService.CreateProgram(department5, "Master of Engineering, Software Engineering (MEng)", ProgramLevel.Graduate, DegreeType.Master, ProgramStudyType.CourseBased);
                    var program9 = programService.CreateProgram(department5, "Master in Applied Computer Science (MApCompSc)", ProgramLevel.Graduate, DegreeType.Master, ProgramStudyType.CourseBased);
                    var program10 = programService.CreateProgram(department5, "Graduate diploma in Computer Science (GrDip)", ProgramLevel.Graduate, DegreeType.GraduateDiploma, ProgramStudyType.CourseBased);

                    var course1 = courseService.CreateCourse("COMP 691", "TOPICS/COMPUTER SCIENCE", "", 4, ProgramLevel.Graduate, department5);
                    var course2 = courseService.CreateCourse("COMP 5261", "COMPUTER ARCHITECTURE", "", 4, ProgramLevel.Graduate, department5);
                    var course3 = courseService.CreateCourse("COMP 6231", "DISTRIBUTED SYSTEM DESIGN", "", 4, ProgramLevel.Graduate, department5);
                    var course4 = courseService.CreateCourse("COMP 6281", "PARALLEL PROGRAMMING", "", 4, ProgramLevel.Graduate, department5);
                    var course5 = courseService.CreateCourse("SOEN 691", "TOPICS/SOFTWARE ENGINEERING", "", 4, ProgramLevel.Graduate, department5);
                    var course6 = courseService.CreateCourse("SOEN 6431", "SOFT. COMP.& MAINTENANCE", "", 4, ProgramLevel.Graduate, department5);
                    var course7 = courseService.CreateCourse("SOEN 6441", "ADV. PROG. PRACTICES", "", 4, ProgramLevel.Graduate, department5);
                    var course8 = courseService.CreateCourse("SOEN 6841", "SOFTWARE PROJECT MANAGEMENT", "", 4, ProgramLevel.Graduate, department5);
                    var course9 = courseService.CreateCourse("COMP 6961", "GRADUATE SEMINAR-COMP.SC.", "", 1, ProgramLevel.Graduate, department5);

                    CreateTeachers(new List<Dto.Course> { course1, course2 , course3 , course4 , course5 , course6 , course7 , course8 , course9 }, class1);
                }
            }

            if (faculty4 != null)
            {
                var department8 = departmentService.CreateDepartment("Accountancy", faculty4);
                var department9 = departmentService.CreateDepartment("Finance", faculty4);
                var department10 = departmentService.CreateDepartment("Management", faculty4);
                var department11 = departmentService.CreateDepartment("Marketing", faculty4);
                var department12 = departmentService.CreateDepartment("Supply Chain & Business Technology Management", faculty4);
            }
        }

        protected void Application_PostAuthorizeRequest(object sender, EventArgs e)
        {
            var securedRoutes = new List<string>
            {
                "Course",
                "Faculty",
                "ManageAccount",
                "Registration",
                "Student",
                "User"
            };

            if (securedRoutes.Any(x => Request.Path.Contains(x)))
            {
                if (HttpContext.Current.User == null)
                {
                    Response.Redirect("/Home");
                }
                else if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("/Account/Login");
                }
            }
        }

        protected void Application_PostAcquireRequestState(object sender, EventArgs e)
        {
            if (!Request.Path.Contains("Account/Login"))
            {
                if (User != null && User.Identity.IsAuthenticated && Session["emailAddress"] == null)
                {
                    Response.Redirect("/Account/Login");
                }
            }
        }

        private void CreateTeachers(List<Dto.Course> courses, Guid classroomId)
        {
            var db = new Data.StudentInfoContext();

            var userAccount = db.ApplicationUsers.FirstOrDefault(x => x.Email == "mr.garrison@southpark.com");

            if (userAccount != null)
            {
                var userId = Guid.Parse(userAccount.Id);

                if (!db.Teachers.Any(x => x.ApplicationUserId == userId))
                {
                    db.Teachers.Add(new Dto.Teacher()
                    {
                        Id = Guid.NewGuid(),
                        ApplicationUserId = userId
                    });

                    db.SaveChanges();

                    var courseService = new CourseService();
                    foreach (var c in courses)
                    {
                        courseService.AssignSemester(c.Id, classroomId, userId, true, Term.Fall, DateTime.Now);
                    }
                }
            }
        }
    }
}
