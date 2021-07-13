using BigShool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigShool.ViewModels
{
    public class CoursesViewModel
    {
        public IEnumerable<Course> UpcommingCoures { get; set; }
        public bool ShowAction { get; set; }
        public IEnumerable<Attendance> FollowCourses { get; set; }
        public IEnumerable<Following> FollowLecturers { get; set; }
    }
}