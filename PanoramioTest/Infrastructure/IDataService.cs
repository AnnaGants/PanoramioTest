using PanoramioTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanoramioTest.Infrastructure
{
    public interface IDataService
    {
        Task<Photography[]> GetPhotos(double minx, double miny, double maxx, double maxy);
        string GetPhotoUrl(Photography photo, Size size);
    }
}
