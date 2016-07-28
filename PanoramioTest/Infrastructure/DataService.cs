using PanoramioTest.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace PanoramioTest.Infrastructure
{
    public class DataService : IDataService
    {
        private const string urlParameters = "?set={0}&from={1}&to={2}&minx={3}&miny={4}&maxx={5}&maxy={6}&size={7}&mapfilter={8}";

        private async Task<T> Communicate<T>(Uri uri)
        {
            HttpRequestMessage mes = new HttpRequestMessage();
            mes.Method = HttpMethod.Get;
            mes.RequestUri = uri;

            HttpResponseMessage response;
            
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.SendAsync(mes);
            }

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

            if (response.IsSuccessStatusCode)
            {
                T data = (T)serializer.ReadObject(await response.Content.ReadAsStreamAsync());

                if (data is T)
                    return data;

                throw new InvalidCastException();
            }

            throw new Exception();
        }

        public async Task<Photography[]> GetPhotos(double minx, double miny, double maxx, double maxy)
        {
            Response response = await Communicate<Response>(new Uri(StaticResources.MapURL + string.Format(urlParameters, StaticResources.RequestSet, 0, StaticResources.PageSize, minx, miny, maxx, maxy, StaticResources.PreviewSize, StaticResources.MapFilter)));
            return response.photos;
        }

        public string GetPhotoUrl(Photography photo, Size size)
        {
            if (photo != null)
            {
                if (photo.photo_file_url != null)
                {
                    string photo_url = photo.photo_file_url;

                    int index = photo_url.LastIndexOf('.') + 1;
                    string format = photo_url.Substring(index, photo_url.Length - index);

                    return StaticResources.PhotoURL + string.Format("{0}/{1}.{2}", size.ToString(), photo.photo_id, format);
                }
            }
            return null;
        }
    }
}
