using System;

namespace F1Manager.Entities
{
    public class RaceEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Track { get; set; }

        public DateTime Date { get; set; }

        public int Length { get; set; }
    }
}