using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public class ConvertCourseData
    {
        ToTxt ToTxt = new ToTxt();
        FromTxt FromTxt = new FromTxt();
        Encrypt Encrypt;
        Decrypt Decrypt;
        const string FILE_NAME = "\\course.txt";
        const string FILE_PATH = "C:\\TestSite";

        #region Course Methods
        public void SaveCourse(Course course)
        {
            Encrypt = new Encrypt();
            CourseViewModel courseView = GetCourses();
            courseView.Courses.Add(course);
            int id = 0;
            if (courseView.Courses.Count != 0 || courseView.Courses != null)
            {
                foreach (var item in courseView.Courses)
                {
                    item.Id = id;
                    id++;
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(course.Name + "," + course.CourseNumber + "," + course.NumberOfStudents + "," + course.Duration + "," + course.Defined + "," + course.Id);
            ToTxt.AppendStringToTxt(FILE_PATH + FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5) + Environment.NewLine);
        }
        public void SaveAllCourses(CourseViewModel courseViewModel)
        {
            foreach (var item in courseViewModel.Courses)
            {
                SaveCourse(item);
            }
        }
        public CourseViewModel GetCourses()
        {
            try
            {
                var courseModel = new CourseViewModel();

                string[] rawCourse = FromTxt.StringsFromTxt(FILE_PATH + FILE_NAME);

                foreach (string line in rawCourse)
                {
                    Decrypt = new Decrypt();
                    string raw = Decrypt.DecryptString(line, "SkPRingsted", 5);
                    string[] courseData = raw.Split(',');
                    Course course = new Course();
                    course.Name = courseData[0];
                    course.CourseNumber = int.Parse(courseData[1]);
                    course.NumberOfStudents = int.Parse(courseData[2]);
                    course.Duration = int.Parse(courseData[3]);
                    course.Defined = Convert.ToBoolean(courseData[4]);
                    course.Id = int.Parse(courseData[5]);
                    courseModel.Courses.Add(course);

                }
                return courseModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public void EditCourse(Course course)
        {
            //Logic for Edit Item
            var courseModelOld = GetCourses();


            // finds the old item and removes it
            Course OldCourse = courseModelOld.Courses.Where(x => x.Id == course.Id).FirstOrDefault();
            courseModelOld.Courses.Remove(OldCourse);

            //Inserts the new course 
            courseModelOld.Courses.Insert(course.Id, course);
            //Saves the list
            SaveAllCourses(courseModelOld);
            
        }

        public void DeleteCourse(Course course)
        {
            // Code input item that has to be deleted 
            var courseModels = GetCourses();
         
            // finds the old item and removes it
            Course removeCourse = courseModels.Courses.Where(x => x.Id == course.Id).FirstOrDefault();
            courseModels.Courses.Remove(removeCourse);
            SaveAllCourses(courseModels);
        }
        #endregion	        #endregion

    }
}
