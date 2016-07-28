using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanoramioTest.Model
{
    public class Photography
    {
        public double height { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public int owner_id { get; set; }
        public string owner_name { get; set; }
        public string owner_url { get; set; }
        public string photo_file_url { get; set; }
        public int photo_id { get; set; }
        public string photo_title { get; set; }
        public string photo_url { get; set; }
        public string upload_date { get; set; }
        public double width { get; set; }
    }
}
