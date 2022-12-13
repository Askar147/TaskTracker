﻿using TaskTrackerData.Statuses;

namespace TaskTrackerData.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ProjectTaskStatus TaskStatus { get; set; }
        public int Priority { get; set; }
    }
}