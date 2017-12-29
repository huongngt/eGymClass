using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eGymClass.Models
{
    public class MemberListViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime TimeOfRegistration { get; set; }
        public string Email { get; set; }
        public ICollection<GymClass> AttendedClasses { get; set; }
        public ICollection<string> MemberRoles { get; set; }

        public MemberListViewModel()
        {

        }

        public MemberListViewModel(ApplicationUser u)
        {
            Id = u.Id;
            FirstName = u.FirstName;
            LastName = u.LastName;
            TimeOfRegistration = u.TimeOfRegistration;
            AttendedClasses = u.AttendedClasses;
            Email = u.Email;
           
        }
    }
}