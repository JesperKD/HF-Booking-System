using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class BookingViewModel : IComparable<BookingViewModel>
    {
        public int Id { get; set; }
        public User CurrentUser { get; set; }
        [DisplayName("Lånes fra")]
        public DateTime RentDate { get; set; }
        public DateTime TurnInDate { get; set; }
        public List<Host> HostsRentedForCourse { get; set; } = new List<Host>();
        public Course CourseModel { get; set; } = new Course();
        [DisplayName("Alle fag")]
        public List<Course> CoursesForSelection { get; set; } = new List<Course>();

        public int CompareTo([AllowNull] BookingViewModel other)
        {
            if (other == null)
                return 1;
            else
                return this.Id.CompareTo(other.Id);
        }
    }
}
