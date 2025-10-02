﻿namespace LabWork8.Models
{
    public class Visitor
    {
        public int VisitorId { get; set; }
        public string Phone { get; set; } = null!;
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
    }
}
