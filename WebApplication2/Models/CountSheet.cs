﻿namespace WebApplication2.Models
{
    public class CountSheet
    {
        public string CountCode { get; set; } = string.Empty; 
        public string CountSheetEmployee { get; set; } = string.Empty; 
        public string CountDescription { get; set; } = string.Empty; 
        public DateTime CountDate { get; set; } 
        public int CountStatus { get; set; } 
    }
}
