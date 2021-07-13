using BigShool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BigShool.DTOs;

namespace BigShool.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            var attendance = new Attendance
            {
                CourseId = attendanceDto.CourseId,
                AttendeeId = userId
            };

            if (_dbContext.attendances.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId))
            {

                //_dbContext.Attendances.Remove(attendance);
                _dbContext.Entry(attendance).State = System.Data.Entity.EntityState.Deleted;
                _dbContext.SaveChanges();
                return Json(new { isFollow = false });
            }
            _dbContext.attendances.Add(attendance);
            _dbContext.SaveChanges();
            return Json(new { isFollow = true });

        }
        //[HttpDelete]
        //public IHttpActionResult DeleteAttendance(int id)
        //{
        //    var userId = User.Identity.GetUserId();
        //    var attendance = _dbContext.attendances
        //        .SingleOrDefault(a => a.AttendeeId == userId && a.CourseId == id);
        //    if (attendance == null)
        //        return NotFound();
        //    _dbContext.attendances.Remove(attendance);
        //    _dbContext.SaveChanges();
        //    return Ok(id);
        //}
    }
}
