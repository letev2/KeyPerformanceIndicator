﻿using System;

namespace KpiNew.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } 
        public string Modified { get; set; }
       
    }
}
