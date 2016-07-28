using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanoramioTest.Model
{
    public class Response
    {
        public int count { get; set; }
        public bool has_more { get; set; }
        public MapLocation map_location { get; set; }
        public Photography[] photos { get; set; }
    }
}
