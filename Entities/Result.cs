using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public decimal? Mark { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
