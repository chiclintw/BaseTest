using System;

using Xamarin.Forms;
using Media.Plugin;
using System.Threading.Tasks;
using System.IO;
using Acr.UserDialogs;

namespace BaseTest
{
	public class TakePhoto : ContentPage
	{
		Media.Plugin.Abstractions.MediaFile file;

		public TakePhoto ()
		{
			Title = "照片";

			//Media.Plugin.Abstractions.MediaFile file;
			var photoImage = new Image ();

			var takePhoto = new Button { Text = "取得照片" };
			takePhoto.Clicked += async (sender, e) =>
			{
				var action = await DisplayActionSheet ("取得方式", "放棄", null, "直接拍照", "從相簿取");

				if ( action == "直接拍照" ) {
					if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
					{
						await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
						return;
					};
					file = await CrossMedia.Current.TakePhotoAsync(new Media.Plugin.Abstractions.StoreCameraMediaOptions
					{

						Directory = "Sample",
						Name = "Photo.jpg"
					});
				} else if ( action == "從相簿取" ) {
					file = await CrossMedia.Current.PickPhotoAsync();
				} else {
					return;
				}

				if (file == null)
					return;

				await DisplayAlert("File Location", file.Path, "OK");

				photoImage.Source = ImageSource.FromStream(() =>
				{
					var stream = file.GetStream();
					//file.Dispose();
					return stream;
				}); 
			};

			var UploadPhoto = new Button { Text = "上傳照片" };
			UploadPhoto.Clicked += async (sender, e) => {
				var fname = await DependencyService.Get<FuncInterface> ().UploadFile (file.Path);
				await DisplayAlert("Uploaded file name", fname, "OK");
			};

			var skt1 = new StackLayout {
				Spacing = 10, Padding = 10,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Horizontal, 
				Children = 
				{ 
					takePhoto,
					UploadPhoto
				}
			};
			
			Content = new StackLayout { 
				Children = {
					skt1,
					photoImage
				}
			};
		}
	}
}


