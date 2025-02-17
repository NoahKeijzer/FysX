﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Services
{
    public interface AddTreatmentService
    {
        public bool AddTreatment(Treatment t, string token);
        public bool UpdateTreatment(Treatment t, int id, string token);
    }
}
