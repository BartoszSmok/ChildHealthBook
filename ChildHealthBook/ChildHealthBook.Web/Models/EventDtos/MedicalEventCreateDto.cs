﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Models.EventDtos
{
    public class MedicalEventCreateDto
    {
        public DateTime DateOfEvent { get; set; }

        public string EventType { get; set; }

        public string EventTitle { get; set; }

        public string Comment { get; set; }
    }
}
