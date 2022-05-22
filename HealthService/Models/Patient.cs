using HealthService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthService.Models
{
    public enum Status {
        Submitted,
        Approved,
        Rejected
    };


    public class Patient
    {
        public int Id { get; set; }
        public DateTime entrydate { get; set; }
        public DateTime registrationdate { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public string Name { get; set; }
        public string Mother { get; set; }
        public string Father_or_Husband { get; set; }
        public string RelationwithGuardian { get; set; }
        public string NID { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public int? UpazillaId { get; set; }
        public virtual Upazilla Upazilla { get; set; }
        public string BankAccount { get; set; }
        public int? DiseaseId { get; set; }
        public virtual Disease Disease { get; set; }
        public Status Status { get; set; }
    }

}