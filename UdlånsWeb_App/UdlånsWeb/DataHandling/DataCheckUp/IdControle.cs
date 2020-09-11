using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling.DataCheckUp
{
    public static class IdControl
    {
//--------------------------------------------------------------------------------Host
        /// <summary>
        /// Gives the Host an id
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static Host GiveIdToHost(Host host)
        {
            host.Id = GetUnUsedIdForHost();
            return host;
        }

        /// <summary>
        /// Checks if the host id already exist
        /// </summary>
        /// <returns></returns>
        private static int GetUnUsedIdForHost()
        {
            var hostmodel = Data.GetHosts().Hosts;
            if (hostmodel.Count != 0)
            {
                hostmodel.Sort();
                int highestIdNumber = hostmodel.Last().Id;
                return highestIdNumber + 1;
            }
            return 0;
        }

//--------------------------------------------------------------------------------Course
        public static Course GiveIdToCourse(Course course)
        {
            course.Id = GetUnUsedIdForCourse();
            return course;
        }

        private static int GetUnUsedIdForCourse()
        {
            var courseModel = Data.GetCourses().Courses;
            if (courseModel.Count != 0)
            {
                courseModel.Sort();
                int highestIdNumber = courseModel.Last().Id;
                return highestIdNumber + 1;
            }
            return 0;
        }

//--------------------------------------------------------------------------------User
        public static User GiveIdToUser(User user)
        {
            user.Id = GetUnUsedIdForUser();
            return user;
        }

        private static int GetUnUsedIdForUser()
        {
            var userModel = Data.GetUsers().Users;
            if (userModel.Count != 0)
            {
                userModel.Sort();
                int highestIdNumber = userModel.Last().Id;
                return highestIdNumber + 1;
            }
            return 0;
        }
    }
}
