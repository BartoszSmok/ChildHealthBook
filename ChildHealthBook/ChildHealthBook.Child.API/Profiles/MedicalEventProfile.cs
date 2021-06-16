﻿using AutoMapper;
using ChildHealthBook.Child.API.Models;
using ChildHealthBook.Common.WebDtos.EventDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Child.API.Profiles
{
    public class MedicalEventProfile : Profile
    {
        public MedicalEventProfile()
        {
            CreateMap<MedicalEventModel, MedicalEventReadDto>();
        }
    }
}
