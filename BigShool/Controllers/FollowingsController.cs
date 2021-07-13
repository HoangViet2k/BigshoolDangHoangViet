using BigShool.DTOs;
using BigShool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigShool.Controllers
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)

        {
            var userId = User.Identity.GetUserId();
            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId
            };
            if (_dbContext.followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId))
            {
                _dbContext.Entry(following).State = System.Data.Entity.EntityState.Deleted;
                _dbContext.SaveChanges();
                return Json(new { isFollow = false, followeeId = followingDto.FolloweeId });
            }

            _dbContext.followings.Add(following);
            _dbContext.SaveChanges();
            return Json(new { isFollow = true, followeeId = followingDto.FolloweeId });
        }

        //[HttpDelete]
        //public IHttpActionResult DeleteFollowing(int id)
        //{
        //    var userId = User.Identity.GetUserId();
        //    var following = _dbContext.followings
        //        .SingleOrDefault(a => a.FollowerId == userId && a.FolloweeId == id);
        //    if (attendance == null)
        //        return NotFound();
        //    _dbContext.attendances.Remove(attendance);
        //    _dbContext.SaveChanges();
        //    return Ok(id);
        //}
    }
}
