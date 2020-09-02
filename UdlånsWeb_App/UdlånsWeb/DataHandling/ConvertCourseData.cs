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

        public void SaveCourseAdd(Course course)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Encrypt = new Encrypt();
            //Main props to save 

            stringBuilder.Append(Data.ConvertObjectToJson(course));
            ToTxt.AppendStringToTxt(FILE_PATH + FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5) + Environment.NewLine);
        }

        public void RewriteCourseFile(CourseViewModel courseViewModel)
        {
            StringBuilder stringBuilder = new StringBuilder();

            //Main props to save 
            foreach (var course in courseViewModel.Courses)
            {
                Encrypt = new Encrypt();
                stringBuilder.Append(Data.ConvertObjectToJson(course));
                stringBuilder.Append("|");
            }
            ToTxt.StringToTxt(FILE_PATH + FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted"));
        }

        public CourseViewModel GetCourses()
        {
            CourseViewModel courseViewModel = new CourseViewModel();
            try
            {
                string[] rawCourse = FromTxt.StringsFromTxt(FILE_PATH + FILE_NAME);
                foreach (string line in rawCourse)
                {
                    Decrypt = new Decrypt();
                    string raw = Decrypt.DecryptString(line, "SkPRingsted");

                    foreach (var item in raw.Split("|"))
                    {
                        if (item.Length != 0)
                        {
                            Course course = (Course)Data.ConvertJsonToObejct(item, "Course");
                            courseViewModel.Courses.Add(course);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new CourseViewModel();
            }
            return courseViewModel;
        }
    }
}
