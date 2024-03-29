﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static EuroHelp.Data.DataConstants.InsuranceCompany;

namespace EuroHelp.Data.Models
{
    public class InsuranceCompany
    {
        [Key]
        public string? Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(NameMaxLength)]
        public string? Name { get; set; }

        [Required]
        [Range(BulstatMinValue, BulstatMaxValue)]
        public int Bulstat { get; set; }

        [MaxLength(AddressMaxLength)]
        public string? Address { get; set; }

        [Required]
        [MaxLength(MaxPhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(MaxMobilePhoneNumber)]
        public string? MobilePhoneNumber { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string? Email { get; set; }

        [Required]
        public string Status { get; set; } = "Non active";

        [Range(FaxMinValue, FaxMaxValue)]
        public int FAX { get; set; }

        [MaxLength(NotesMaxLength)]
        public string? Notes { get; set; }

        [ForeignKey("Employee")]
        public string? EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();

        public List<Damage> Damages { get; set; } = new List<Damage>();
    }
}
