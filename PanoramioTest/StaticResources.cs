using PanoramioTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanoramioTest
{
    public static class StaticResources
    {
        public const string MapURL = "http://www.panoramio.com/map/get_panoramas.php";
        public const string PhotoURL = "http://www.panoramio.com/photos/";
        public const string MapServiceToken = "Xq5VHQvWjCEEW7rpUaqv~847cjYEz3ZZzE7uQUuqWoA~AgLQ_sFb-oeRh4mIM_yBR7p87bz-gh_ecAF2zQXAFwq2OESMFJqqOya0P4TD4OQQ";

        /// <summary>ZoomLevel по умолчанию для карты</summary>
        public const int ZoomLevel = 1;

        /// <summary>Размер изображение для просмотра крупного изображения</summary>
        public const Size LargePhotoSize = Size.original;

        /// <summary>Размер изображение для preview</summary>
        public const Size PreviewSize = Size.square;

        /// <summary>Использование фильтрации</summary>
        public const bool MapFilter = true;

        /// <summary>Размер изображение для preview</summary>
        public const string RequestSet = "full";

        /// <summary>Размер страницы при загрузке изображений</summary>
        public const int PageSize = 20;
        
        /// <summary>Сообщение, отображаемое при загрузке фотографии</summary>
        public const string LoadingMessage = "Загрузка изображения...";
    }
}
