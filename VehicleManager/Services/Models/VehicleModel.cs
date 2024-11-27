﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManager.Services.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public VehicleMake VehicleMake { get; set; }
        public int VehicleMakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}