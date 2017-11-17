﻿using System;
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
using StudentInfo.Users.Dto;
using StudentInfo.Data.UserDbContext;

namespace StudentInfo.WebClient.Controllers
{
  [RequireHttps]
  [Authorize]
  public class CourseController : Controller
  {
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
    public ActionResult Details(Guid id, bool allowEdit = false)
    {
      var db = new StudentInfoContext();

      var courseDetails = db.Courses.FirstOrDefault(x => x.Id == id);

      if (courseDetails != null)
      {
        if (allowEdit && User.IsInRole(SystemRoles.Administrator))
        {
          return PartialView("_EditDetails", courseDetails);
        }
        return PartialView("_Details", courseDetails);
      }
      return HttpNotFound();
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

    //[ValidateAntiForgeryToken]
    [HttpPost]
    [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.Student)]
    public ActionResult Enroll(Guid semesterCourseId)
    {
      if (User.IsInRole(SystemRoles.Student))
      {
        var context = new StudentInfoContext();

        var userId = Guid.Parse(User.Identity.GetUserId());
        var student = context.Students.FirstOrDefault(x => x.ApplicationUserId == userId);
        if (student == null)
        {
          student = new Student
          {
            Id = Guid.NewGuid(),
            ApplicationUserId = userId
          };

          context.Students.Add(student);
          context.SaveChanges();
        }
        if (!context.StudentCourses.Any(x => x.StudentId == student.Id && x.CourseSemester.Id == semesterCourseId))
        {
          var semesterCourse = context.SemesterCourses.FirstOrDefault(x => x.Id == semesterCourseId);

          context.StudentCourses.Add(new StudentCourse
          {
            Id = Guid.NewGuid(),
            StudentId = student.Id,
            CourseSemester = semesterCourse,
            CourseState = CourseRegistrationState.Added,
            CreateDate = DateTime.Now,
            LastUpdate = DateTime.Now
          });
          context.SaveChanges();

          return Json(new
          {
            success = true,
            message = $"{semesterCourse.Course.Name} was added successfully!"
          });
        }
      }
      return Json(new
      {
        success = false,
        message = "Failed to add selected course"
      });
    }

    [HttpPost]
    [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.Student)]
    public ActionResult Drop(Guid studentCourseId)
    {
      if (User.IsInRole(SystemRoles.Student))
      {
        var context = new StudentInfoContext();

        var studentCourse = context.StudentCourses.FirstOrDefault(x => x.Id == studentCourseId);

        if (studentCourse != null)
        {
          context.StudentCourses.Remove(studentCourse);
          context.SaveChanges();

          return Json(new
          {
            success = true,
            message = "Course was dropped successfully!"
          });
        }
        
      }
      return Json(new
      {
        success = false,
        message = "Failed to drop selected course"
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

    public ActionResult Search(CourseSearchModel model, int? page)
    {
      if (model == null) model = new CourseSearchModel();

      var db = new StudentInfoContext();

      var courses = db.SemesterCourses.AsQueryable();

      if (!string.IsNullOrEmpty(model.Keyword))
      {
        courses = courses.Where(x => x.Course.Name.Contains(model.Keyword) ||
        x.Course.Description.Contains(model.Keyword));
      }
      if (!string.IsNullOrEmpty(model.Code))
      {
        courses = courses.Where(x => x.Course.Code.Contains(model.Code));
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
        var currentTerm = Helper.CurrentTerm();
        courses = courses.Where(x => x.Term == currentTerm);
      }

      int pageNumber = (page ?? 1);
      model.Results = courses.OrderBy(x => x.Course.Code).ToPagedList(pageNumber, SearchConstants.PageSize);
      return View("Search", model);
    }

    public ActionResult StudentCourses(CourseSearchModel model, int? page)
    {
      if (model == null) model = new CourseSearchModel();

      var db = new StudentInfoContext();

      var userId = Guid.Parse(User.Identity.GetUserId());
      var student = db.Students.FirstOrDefault(x => x.ApplicationUserId == userId);
      var studentCourses = new List<StudentCourse>().AsQueryable();

      if (student != null)
      {
        studentCourses = db.StudentCourses.AsQueryable().Where(x => x.StudentId == student.Id);

        if (model.Semester.HasValue)
        {
          studentCourses = studentCourses.Where(x => x.CourseSemester.Term == model.Semester);
        }
        studentCourses = studentCourses.OrderBy(x => x.CourseSemester.Term);
      }

      int pageNumber = (page ?? 1);
      model.StudentCourses = studentCourses.ToPagedList(pageNumber, SearchConstants.PageSize);
      return View("StudentCourses", model);
    }
  }
}