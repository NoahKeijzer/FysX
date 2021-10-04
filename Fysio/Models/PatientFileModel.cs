﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Fysio.Models
{
    public class PatientFileModel
    {
        [Required]
        public string Complaints { get; set; }
        [Required]
        public string DiagnosisDescription { get; set; }
        [Required]
        public string DiagnosisCode { get; set; }
        [Required]
        public string TreatorEmail { get; set; }
        [Required]
        public int TreatmentsPerWeek { get; set; }
        [Required]
        public int MinutesPerSession { get; set; }
    }
}
