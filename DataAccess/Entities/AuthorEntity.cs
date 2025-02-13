﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class AuthorEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public int CourseId { get; set; }
        public CourseEntity? Course { get; set; }
    }
}
