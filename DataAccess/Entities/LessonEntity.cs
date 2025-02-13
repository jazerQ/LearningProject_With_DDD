﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class LessonEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LessonText { get; set; } = string.Empty;

        public int CourseId { get; set; }
        public CourseEntity? Course { get; set; } 
    }
}
