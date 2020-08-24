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
        public void AddCourse(Course course)
        {
            Encrypt = new Encrypt();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(course.Name + "," + course.NumberOfStudents + "," + course.Duration + "," + course.Defined);
            ToTxt.AppendStringToTxt(FILE_PATH + FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5) + Environment.NewLine);
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
                    course.NumberOfStudents = int.Parse(courseData[1]);
                    course.Duration = int.Parse(courseData[2]);
                    course.Defined = Convert.ToBoolean(courseData[2]);

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
            var courseModelOld = new CourseViewModel();

            // gets all items from file
            string[] rawCourse = FromTxt.StringsFromTxt(FILE_PATH + FILE_NAME);

            foreach (string itemLine in rawCourse)
            {
                Decrypt = new Decrypt();
                string raw = Decrypt.DecryptString(itemLine, "SkPRingsted", 5);
                string[] courseData = raw.Split(',');
                Course oCourse = new Course();
                oCourse.Name = courseData[0];
                oCourse.NumberOfStudents = int.Parse(courseData[1]);
                oCourse.Duration = int.Parse(courseData[2]);
                oCourse.Defined = Convert.ToBoolean(courseData[3]);
                
                courseModelOld.Courses.Add(oCourse);
            }

            // finds the old item and removes it
            Course OldCourse = courseModelOld.Courses.Where(x => x.Name == course.Name).FirstOrDefault();
            courseModelOld.Courses.Remove(OldCourse);

            // creates new list from old, and inserts edited item at index Id
            CourseViewModel CourseModelNew = new CourseViewModel();
            CourseModelNew = courseModelOld;
            CourseModelNew.Courses.Insert(course.Id, course);


            // creates correct item string
            List<string> coursesTosave = new List<string>();

            // makes each item into a new string
            foreach (Course xcourse in CourseModelNew.Courses)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(course.Name + "," + course.NumberOfStudents + "," + course.Duration + "," + course.Defined);

                Encrypt = new Encrypt();
                coursesTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
            }
            // overrides file with new strings
            ToTxt.StringsToTxt(FILE_PATH + FILE_NAME, coursesTosave.ToArray());
        }

        public void DeleteCourse(Course course)
        {
            // Code input item that has to be deleted 
            var courseModel = new CourseViewModel();
            try
            {
                // gets all users from file
                string[] rawCourse = FromTxt.StringsFromTxt(FILE_PATH + FILE_NAME);

                foreach (string Line in rawCourse)
                {
                    Decrypt = new Decrypt();
                    string raw = Decrypt.DecryptString(Line, "SkPRingsted", 5);
                    string[] courseData = raw.Split(',');
                    Models.Course oCourse = new Course();
                    oCourse.Name = courseData[0];           
                    oCourse.NumberOfStudents = int.Parse(courseData[1]);
                    oCourse.Duration = int.Parse(courseData[2]);
                    oCourse.Defined = Convert.ToBoolean(courseData[3]);
                    courseModel.Courses.Add(oCourse);
                }

            }
            catch (Exception)
            {

            }

            // finds the old item and removes it
            Course removeCourse = courseModel.Courses.Where(x => x.Name == course.Name && x.Duration == course.Duration).First();
            courseModel.Courses.Remove(removeCourse);

            // creates correct user string
            List<string> coursesTosave = new List<string>();

            foreach (Course coursex in courseModel.Courses)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(course.Name + "," + course.NumberOfStudents + "," + course.Duration + "," + course.Defined);

                Encrypt = new Encrypt();
                coursesTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
            }

            // overrides file with new strings
            ToTxt.StringsToTxt(FILE_PATH + FILE_NAME, coursesTosave.ToArray());
        }
        #endregion	        #endregion

    }
}
