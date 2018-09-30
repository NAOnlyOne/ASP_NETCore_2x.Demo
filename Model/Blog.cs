using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Blog
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public string Name { get; set; }

        public virtual User Owner { get; set; }

        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
