using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
