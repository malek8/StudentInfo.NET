using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StudentInfo.Data;
using PagedList;
using StudentInfo.Enums;
using StudentInfo.WebClient.Helpers;
using StudentInfo.WebClient.Models;
using StudentInfo.Dto;
using StudentInfo.Helpers;
using StudentInfo.CourseManager;
using Newtonsoft;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using StudentInfo.Students;

namespace StudentInfo.WebClient.Controllers
{
    [RequireHttps]
    [Authorize]
    public class CourseController : Controller
    {
        private CourseService _courseService;
        private ClassroomService _classroomService;
        private StudentService _studentService;

        public CourseController()
        {
            _courseService = new CourseService();
            _classroomService = new ClassroomService();
            _studentService = new StudentService();
        }

        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.Student)]
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var db = new StudentInfoContext();

            var courses = db.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(x => x.Code.Contains(searchString) ||
                x.Name.Contains(searchString));
            }

            int pageNumber = (page ?? 1);
            return View(courses.OrderBy(x => x.Code).ToPagedList(pageNumber, SearchConstants.PageSize));
        }

        [HttpGet]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember)]
        public ActionResult Details(Guid id, bool allowEdit = false)
        {
            var db = new StudentInfoContext();

            var courseDetails = db.Courses.FirstOrDefault(x => x.Id == id);

            if (courseDetails != null)
            {
                return PartialView("_EditDetails", courseDetails);
            }
            return HttpNotFound();
        }

        [HttpGet]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember)]
        public ActionResult CourseDetails(Guid id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var course = _courseService.FindById(id);
                if (course != null)
                {
                    var model = new EditCourseModel()
                    {
                        Id = course.Id,
                        Code = course.Code,
                        Title = course.Name,
                        Description = course.Description,
                        NumberOfCredits = course.NumberOfCredits,
                        Level = course.Level
                    };

                    return View("_EditCourse", model);
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember)]
        public JsonResult EditCourse(EditCourseModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = _courseService.UpdateCourse(model.Id, model.Title, model.Description, model.NumberOfCredits, model.Level);
                if (result)
                {
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public ActionResult SemesterCourseDetails(Guid id)
        {
            var db = new StudentInfoContext();

            var courseDetails = db.SemesterCourses.FirstOrDefault(x => x.Course.Id == id);

            if (courseDetails != null)
            {
                return PartialView("_Details", courseDetails);
            }
            return HttpNotFound();
        }

        [HttpGet]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember)]
        public ActionResult AssignSemesterCourse(Guid courseId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new AssignSemesterCourseModel();

                var db = new StudentInfoContext();

                var course = db.Courses.FirstOrDefault(x => x.Id == courseId);
                if (course != null)
                {
                    var semesterCourses = db.SemesterCourses.Where(x => x.Course.Id == courseId);

                    if (semesterCourses.Any())
                    {
                        model.SemesterCourse = semesterCourses.OrderByDescending(x => x.CourseDate).ToList();
                    }
                    else
                    {
                        model.SemesterCourse = new List<SemesterCourse>();
                    }
                    model.Course = course;

                    return View("_AssignSemesterCourse", model);
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember)]
        public JsonResult AssignSemesterCourse(Guid courseId, Guid scheduleId, Guid teacherId, bool isOpen, Term term)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = _courseService.CreateCourseSemester(courseId, scheduleId, teacherId, isOpen, term);
                if (result)
                {
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles(SystemRoles.Administrator)]
        public JsonResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                var db = new StudentInfoContext();

                var courseToUpdate = db.Courses.FirstOrDefault(x => x.Id == course.Id);

                if (courseToUpdate != null)
                {

                    courseToUpdate.Name = course.Name;
                    courseToUpdate.Description = course.Description;
                    courseToUpdate.NumberOfCredits = course.NumberOfCredits;
                    courseToUpdate.Level = course.Level;
                    db.SaveChanges();
                }
                return Json(new { success = true });
            }

            var errors = new List<string>();
            foreach (var e in ModelState.Values)
            {
                errors.AddRange(e.Errors.Select(x => x.ErrorMessage));
            }
            return Json(errors);
        }

        [HttpGet]
        public ActionResult Enroll()
        {
            return View(new CourseSearchModel());
        }

        [HttpPost]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.Student)]
        public ActionResult Enroll(Guid semesterCourseId)
        {
            if (User.IsInRole(SystemRoles.Student))
            {
                var studentId = HttpContext.Session["studentId"];
                if (studentId != null)
                {
                    var parsedStudentId = Guid.Parse(studentId.ToString());

                    var message = string.Empty;
                    var result = _studentService.Enroll(parsedStudentId, semesterCourseId, out message);

                    if (result)
                    {
                        return Helper.CreateResponse(true, message);
                    }
                    else
                    {
                        return Helper.CreateResponse(false, message);
                    }
                }
            }
            return Helper.CreateResponse(false, "Sorry, you cannot add this course");
        }

        [HttpPost]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.Student)]
        public ActionResult Drop(Guid studentCourseId)
        {
            var message = "Sorry, something went wrong";
            var result = false;
            if (User.IsInRole(SystemRoles.Student))
            {
                var studentId = HttpContext.Session["studentId"];
                if (studentId != null)
                {
                    var parsedStudentId = Guid.Parse(studentId.ToString());

                    result = _studentService.Drop(parsedStudentId, studentCourseId, out message);
                }
            }
            return Json(new
            {
                success = result,
                message = message
            });
        }

        [HttpGet]
        public JsonResult GetDepartments(Guid facultyId)
        {
            var db = new StudentInfoContext();

            var departments = db.Departments.AsQueryable();

            if (facultyId != null)
            {
                departments = departments.Where(x => x.Faculty.Id == facultyId);
            }

            return Json(departments.Select(x => new { text = x.Name, value = x.Id }),
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPrograms(Guid departmentId)
        {
            var db = new StudentInfoContext();

            var programs = db.Programs.Where(x => x.Department.Id == departmentId);

            return Json(programs.Select(x => new { text = x.Name, value = x.Id }),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(CourseSearchModel model, int? page)
        {
            if (model == null) model = new CourseSearchModel();

            var db = new StudentInfoContext();

            var courses = db.SemesterCourses.Where(x => x.Schedule != null).AsQueryable();
            courses = courses.Where(x => x.Schedule.ScheduleItems.All(z => z.Date >= DateTime.Now));

            if (User.IsInRole(SystemRoles.Instructor))
            {
                var instructorCourses = InstructorCourses().ToList();
                courses = courses.Where(x => instructorCourses.Contains(x.Id));
            }
            else if (User.IsInRole(SystemRoles.Advisor))
            {
                var userId = User.Identity.GetUserId();
                var advisor = db.FacultyAdvisors.FirstOrDefault(x => x.User.Id == userId);
                if (advisor != null)
                {
                    courses = courses.Where(x => x.Course.Department.Faculty.Id == advisor.Faculty.Id);
                }
            }
            if (!string.IsNullOrEmpty(model.Keyword))
            {
                courses = courses.Where(x => x.Course.Name.ToLower().Contains(model.Keyword.ToLower()) ||
                x.Course.Code.ToLower().Contains(model.Keyword.ToLower()) ||
                x.Course.Description.ToLower().Contains(model.Keyword.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.Code))
            {
                courses = courses.Where(x => x.Course.Code.ToLower().Contains(model.Code.ToLower()));
            }
            if (model.DepartmentId.HasValue)
            {
                courses = courses.Where(x => x.Course.Department.Id == model.DepartmentId);
            }
            if (model.FacultyId.HasValue)
            {
                courses = courses.Where(x => x.Course.Department.Faculty.Id == model.FacultyId);
            }

            if (model.Semester.HasValue)
            {
                courses = courses.Where(x => x.Term == model.Semester);
            }
            else
            {
                var currentTerm = CourseHelper.CurrentTerm();
                courses = courses.Where(x => x.Term == currentTerm);
            }

            int pageNumber = (page ?? 1);
            model.Results = courses.OrderBy(x => x.Course.Code).ToPagedList(pageNumber, SearchConstants.PageSize);
            return View("Search", model);
        }

        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember)]
        public ActionResult SearchAll(CourseSearchModel model, int? page)
        {
            if (model == null) model = new CourseSearchModel();
            IEnumerable<Course> courses = new List<Course>();

            if (User.Identity.IsAuthenticated)
            {
                var db = new StudentInfoContext();

                courses = db.Courses.ToList();

                if (!string.IsNullOrEmpty(model.Keyword))
                {
                    courses = courses.Where(x => x.Name.ToLower().Contains(model.Keyword.ToLower()) ||
                    x.Code.ToLower().Contains(model.Keyword.ToLower()) ||
                    x.Description.ToLower().Contains(model.Keyword.ToLower()));
                }
                if (!string.IsNullOrEmpty(model.Code))
                {
                    courses = courses.Where(x => x.Code.ToLower().Contains(model.Code.ToLower()));
                }
                if (model.DepartmentId.HasValue)
                {
                    courses = courses.Where(x => x.Department.Id == model.DepartmentId);
                }
                if (model.FacultyId.HasValue)
                {
                    courses = courses.Where(x => x.Department.Faculty.Id == model.FacultyId);
                }
            }

            int pageNumber = (page ?? 1);
            model.CoursesResults = courses.OrderBy(x => x.Code).ToPagedList(pageNumber, SearchConstants.PageSize);
            return View("CoursesSeach", model);
        }

        public ActionResult StudentCourses(CourseSearchModel model, int? page, bool includeAll = false)
        {
            if (model == null) model = new CourseSearchModel();

            var db = new StudentInfoContext();

            var userId = Guid.Parse(User.Identity.GetUserId());
            var student = db.Students.FirstOrDefault(x => x.ApplicationUserId == userId);
            var studentCourses = new List<StudentCourse>().AsQueryable();

            if (student != null)
            {
                studentCourses = db.StudentCourses.AsQueryable().Where(x => x.StudentId == student.Id);

                if (!includeAll)
                {
                    studentCourses = studentCourses.Where(x => x.CourseState == CourseRegistrationState.Enrolled);
                }

                if (model.Semester.HasValue)
                {
                    studentCourses = studentCourses.Where(x => x.SemesterCourse.Term == model.Semester);
                }
                studentCourses = studentCourses.OrderBy(x => x.SemesterCourse.Term);
            }

            int pageNumber = (page ?? 1);
            model.StudentCourses = studentCourses.ToPagedList(pageNumber, SearchConstants.PageSize);
            return View("StudentCourses", model);
        }

        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember)]
        public ActionResult AssignInstructor(Guid userId, Guid semesterCourseId)
        {
            var context = new StudentInfoContext();

            var applicationUser = context.ApplicationUsers.FirstOrDefault(x => x.Id == userId.ToString());
            if (applicationUser != null && applicationUser.EmailConfirmed)
            {
                var teacherFile = context.Teachers.FirstOrDefault(x => x.ApplicationUserId == userId);
                if (teacherFile == null)
                {
                    teacherFile = new Teacher()
                    {
                        Id = Guid.NewGuid(),
                        ApplicationUserId = userId
                    };

                    context.Teachers.Add(teacherFile);
                    context.SaveChanges();
                }
                if (!context.TeacherCourses.Any(x => x.TeacherId == teacherFile.Id && x.SemesterCourse.Id == semesterCourseId))
                {
                    var semesterCourse = context.SemesterCourses.FirstOrDefault(x => x.Id == semesterCourseId);
                    if (semesterCourse != null)
                    {
                        context.TeacherCourses.Add(new TeacherCourse
                        {
                            Id = Guid.NewGuid(),
                            TeacherId = teacherFile.Id,
                            State = CourseState.Open,
                            SemesterCourse = semesterCourse,
                            CreateDate = DateTime.Now,
                            LastUpdate = DateTime.Now,
                        });

                        context.SaveChanges();

                        return Helper.CreateResponse(true, $"{applicationUser.FirstName} {applicationUser.LastName} was assigned to {semesterCourse.Course.Name} successfully!");
                    }
                }
            }

            return Helper.CreateResponse(false, "Failed to assign instructor to course");
        }

        [HttpGet]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember)]
        public ActionResult GetInstructor(Guid semesterCourseId)
        {
            var db = new StudentInfoContext();

            SemesterCourse semesterCourse = null;
            var teacherCourse = db.TeacherCourses.FirstOrDefault(x => x.SemesterCourse.Id == semesterCourseId);
            if (teacherCourse == null)
            {
                semesterCourse = db.SemesterCourses.FirstOrDefault(x => x.Id == semesterCourseId);
                if (semesterCourse != null)
                {
                    if (semesterCourse.Teacher != null)
                    {

                        semesterCourse.Teacher.User = new ApplicationUser()
                        {
                            FirstName = Helper.GetUserFirstName(semesterCourse.Teacher.ApplicationUserId),
                            LastName = Helper.GetUserLastName(semesterCourse.Teacher.ApplicationUserId)
                        };
                    }
                }
            }
            else
            {
                semesterCourse = teacherCourse.SemesterCourse;
            }

            return View("_AssignTeacher", semesterCourse);
        }

        [HttpGet]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember, SystemRoles.Instructor)]
        public ActionResult GetCourseGrades(Guid semesterCourseId)
        {
            var db = new StudentInfoContext();

            var semesterCourse = db.SemesterCourses.FirstOrDefault(x => x.Id == semesterCourseId);

            var courseStudents = db.StudentCourses.Where(x => x.SemesterCourse.Id == semesterCourseId);

            var model = new CourseGradesModel
            {
                SemesterCourse = semesterCourse,
                Students = courseStudents.ToList()
            };

            return View("_Grades", model);
        }

        [HttpGet]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.Student)]
        public ActionResult StudentCourse(Guid studentCourseId)
        {
            if (User.IsInRole(SystemRoles.Student))
            {
                var db = new StudentInfoContext();

                var studentCourse = db.StudentCourses.Find(studentCourseId);

                if (studentCourse != null)
                {
                    return View("_StudentCourseDetails", studentCourse);
                }
            }

            return HttpNotFound();
        }

        [HttpGet]
        [AuthorizeRoles(SystemRoles.Administrator)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.Instructor)]
        public JsonResult AddStudentGrade(Guid studentId, string grade)
        {
            var result = _courseService.AssignStudentGrade(studentId, grade);
            if (result)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        [AuthorizeRoles(SystemRoles.Administrator)]
        public JsonResult Create(CreateCourseModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (model != null)
                {
                    var newCourse = _courseService.CreateCourse(model.Code, model.Title, model.Description, model.NumberOfCredits, model.Level, model.DepartmentId);

                    if (newCourse != null)
                    {
                        return Json(new { success = true });
                    }
                }
            }
            return Json(new { success = false });
        }

        [HttpGet]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember)]
        public JsonResult CheckClassroom(Guid classroomId, DateTime startTime, DateTime endTime, string dates)
        {
            var message = string.Empty;
            var result = false;
            var validDates = ParseDates(dates);

            if (classroomId == Guid.Empty)
            {
                message = "Please select a classroom";
            }
            else if (validDates == null || validDates.Count == 0)
            {
                message = "Please select class dates";
            }
            else if (_classroomService.IsClassroomAvailable(classroomId, startTime, endTime, validDates))
            {
                message = "Selected classroom is available";
                result = true;
            }
            else
            {
                message = "Selected classroom is not available";
            }
            return Json(new { success = result, message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember)]
        public ActionResult CreateSchedule(Guid courseId)
        {
            if (courseId == Guid.Empty) return HttpNotFound();

            ViewBag.CourseId = courseId;
            return View();
        }

        [HttpPost]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember)]
        public JsonResult CreateSchedule(Guid classroomId, string title, DateTime startTime, DateTime endTime, string dates)
        {
            var message = string.Empty;
            var result = false;
            var scheduleId = Guid.Empty;
            var validDates = ParseDates(dates);

            if (classroomId != Guid.Empty && validDates != null && validDates.Count > 0)
            {
                if (_classroomService.IsClassroomAvailable(classroomId, startTime, endTime, validDates))
                {
                    result = _classroomService.ReserveClassroom(classroomId, title, startTime, endTime, validDates, out scheduleId);
                    message = "Schedule has been created successfully";
                }
            }
            else
            {
                message = "Invalid inputs";
            }

            return Json(new { success = result, message = message, scheduleId = scheduleId }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CourseSemester(Guid courseId, string scheduleId = "")
        {
            if (courseId != Guid.Empty)
            {
                ViewBag.Courseid = courseId;
                var schedules = new List<ClassroomSchedule>();
                var id = Guid.Empty;
                if (Guid.TryParse(scheduleId, out id))
                {
                    schedules.Add(_classroomService.GetSchedule(id));
                }
                else
                {
                    schedules = _classroomService.GetUnassignedSchedules();
                }
                return View(schedules);
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult GetCourseSchedule(Guid courseId)
        {
            return View("_CourseSchedule");
        }

        private List<DateTime> ParseDates(string jsonParam)
        {
            var dates = new List<DateTime>();
            if (!string.IsNullOrEmpty(jsonParam))
            {
                var jArray = JArray.Parse(jsonParam);

                foreach(JObject item in jArray)
                {
                    var day = item.GetValue("day").Value<int>();
                    var month = item.GetValue("month").Value<int>();
                    var year = item.GetValue("year").Value<int>();

                    if (day > 0 && month > 0 && year > 0)
                    {
                        dates.Add(new DateTime(year, month, day));
                    }
                }
            }
            return dates;
        }

        private IEnumerable<TeacherCourse> GetTeacherCourses()
        {
            return new List<TeacherCourse>();
        }

        private IEnumerable<Guid> InstructorCourses()
        {
            var db = new StudentInfoContext();
            var userId = Guid.Parse(User.Identity.GetUserId());

            var courses = db.SemesterCourses.Where(x => x.Teacher.ApplicationUserId == userId).Select(x => x.Id);

            return courses;
        }
    }
}