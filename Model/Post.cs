﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Post
    {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
