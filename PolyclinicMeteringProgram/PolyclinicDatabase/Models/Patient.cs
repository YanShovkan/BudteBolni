﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDatabase.Models
{
    // Пациент
    public class Patient
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }        
        public int? PhoneNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [ForeignKey("Id")]
        public virtual List<Doctor> DoctorId { get; set; }
    }
}
