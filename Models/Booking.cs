using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSignToSem.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        public string Message { get; set; }

        // public int NumberOfSits { get; set; }

        public int SeminarId { get; set; }
        public Seminar Seminar { get; set; }
        // public ICollection<Seminar> Seminar { get; set; }
    }
}
