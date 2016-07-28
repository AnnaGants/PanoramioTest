using PanoramioTest.Infrastructure;
using PanoramioTest.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace PanoramioTest.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        private List<Photography> _photos;
        public List<Photography> Photos
        {
            get
            {
                return _photos;
            }
            set
            {
                if (_photos != value)
                {
                    this._photos = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Visibility _photosVisibility;
        public Visibility PhotosVisibility
        {
            get
            {
                return _photosVisibility;
            }
            set
            {
                if (_photosVisibility != value)
                {
                    this._photosVisibility = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Photography _selectedPhoto;
        public Photography SelectedPhoto
        {
            get
            {
                return _selectedPhoto;
            }
            set
            {
                if (_selectedPhoto != value)
                {
                    this._selectedPhoto = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                if (_imageSource != value)
                {
                    this._imageSource = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _mapServiceToken;
        public string MapServiceToken
        {
            get
            {
                return _mapServiceToken;
            }
            set
            {
                if (_mapServiceToken != value)
                {
                    _mapServiceToken = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _zoomLevel;
        public int ZoomLevel
        {
            get
            {
                return _zoomLevel;
            }
            set
            {
                if (_zoomLevel != value)
                {
                    _zoomLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Visibility _markVisibility;
        public Visibility MarkVisibility
        {
            get
            {
                return _markVisibility;
            }
            set
            {
                if (_markVisibility != value)
                {
                    _markVisibility = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _loadPhotoProgress;
        public int LoadPhotoProgress
        {
            get
            {
                return _loadPhotoProgress;
            }
            set
            {
                if (_loadPhotoProgress != value)
                {
                    _loadPhotoProgress = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _loadingMessage;
        public string LoadingMessage
        {
            get
            {
                return _loadingMessage;
            }
            set
            {
                if (_loadingMessage != value)
                {
                    _loadingMessage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Visibility _errorMessageVisibility;
        public Visibility ErrorMessageVisibility
        {
            get
            {
                return _errorMessageVisibility;
            }
            set
            {
                if (_errorMessageVisibility != value)
                {
                    _errorMessageVisibility = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Visibility _loadingPhotosProgressVisibility;
        public Visibility LoadingPhotosProgressVisibility
        {
            get
            {
                return _loadingPhotosProgressVisibility;
            }
            set
            {
                if (_loadingPhotosProgressVisibility != value)
                {
                    _loadingPhotosProgressVisibility = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Geopoint _location;
        public Geopoint Location
        {
            get
            {
                return _location;
            }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private BitmapImage _bitmap;

        public RelayCommand<MapInputEventArgs> MapTappedCommand { get; private set; }
        public RelayCommand<Photography> PhotoTappedCommand { get; private set; }
        public RelayCommand ViewNextCommand { get; private set; }

        public MainViewModel(IDataService dataService)
            :base(dataService)
        {
            MapTappedCommand = new RelayCommand<MapInputEventArgs>(_mapTappedAction);
            PhotoTappedCommand = new RelayCommand<Photography>(_photoTappedAction);
            ViewNextCommand = new RelayCommand(_viewNextAction);

            ZoomLevel = StaticResources.ZoomLevel;

            ErrorMessageVisibility = Visibility.Collapsed;
            LoadingPhotosProgressVisibility = Visibility.Collapsed;
            PhotosVisibility = Visibility.Collapsed;
            MarkVisibility = Visibility.Collapsed;

            MapServiceToken = StaticResources.MapServiceToken;           
        }
        
        private async void _mapTappedAction(MapInputEventArgs args)
        {
            if (args != null)
            {
                MarkVisibility = Visibility.Visible;
                Location = args.Location;

                double p = 1 / Math.Pow(1.4, _zoomLevel);
                LoadingPhotosProgressVisibility = Visibility.Visible;
                ErrorMessageVisibility = Visibility.Collapsed;
                try
                {
                    var array = await _dataService.GetPhotos(args.Location.Position.Longitude - p, args.Location.Position.Latitude - p, args.Location.Position.Longitude + p, args.Location.Position.Latitude + p);
                    Photos = array.ToList();
                    PhotosVisibility = _photos.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
                }
                catch (AggregateException exception)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(exception.Message);

                    foreach (Exception e in exception.InnerExceptions)
                        sb.AppendFormat("\n{0}", e.Message);

                    ErrorMessageVisibility = Visibility.Visible;
                    ErrorMessage = sb.ToString();
                    PhotosVisibility = Visibility.Collapsed;
                }
                catch (Exception exception)
                {
                    ErrorMessageVisibility = Visibility.Visible;
                    ErrorMessage = exception.Message;
                    PhotosVisibility = Visibility.Collapsed;
                }

                LoadingPhotosProgressVisibility = Visibility.Collapsed;
            }
        }

        private void _viewNextAction()
        {
            if(_photos!= null)
            {
                int index = _photos.IndexOf(_selectedPhoto) + 1;
                if (index > 0 && index < _photos.Count)
                    _loadPhoto(_photos[index]);
            }
        }

        private void _photoTappedAction(Photography photo)
        {
            _loadPhoto(photo);
        }

        private void _loadPhoto(Photography photo)
        {
            SelectedPhoto = photo;

            if (photo != null)
            {
                string selectedPhotoUrl = _dataService.GetPhotoUrl(photo, StaticResources.LargePhotoSize);
                if (selectedPhotoUrl != null)
                {
                    if (_bitmap != null)
                    {
                        _bitmap.DownloadProgress -= _downloadProgress;
                        _bitmap.ImageFailed -= _imageFailed;
                        _bitmap.ImageOpened -= _imageOpened;
                    }
                    _bitmap = new BitmapImage(new Uri(selectedPhotoUrl));

                    _bitmap.DownloadProgress += _downloadProgress;
                    _bitmap.ImageFailed += _imageFailed;
                    _bitmap.ImageOpened += _imageOpened;

                    this.ImageSource = _bitmap;
                    LoadPhotoProgress = 0;
                    LoadingMessage = StaticResources.LoadingMessage;
                }
            }
        }

        private void _imageOpened(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoadPhotoProgress = 100;
        }

        private void _imageFailed(object sender, Windows.UI.Xaml.ExceptionRoutedEventArgs e)
        {
            LoadingMessage = e.ErrorMessage;
        }

        private void _downloadProgress(object sender, DownloadProgressEventArgs e)
        {
            LoadPhotoProgress = e.Progress;
        }
    }
}
