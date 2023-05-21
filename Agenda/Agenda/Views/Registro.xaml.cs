using Agenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Agenda.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registro : ContentPage
	{

		private byte[] imageBytes;
        private ImageSource imageSource;


        public Registro ()
		{
			InitializeComponent ();
		}


        private async void SelectImage_Clicked(object sender, EventArgs e)
        {
            try
            {
                var filePickerResult = await MediaPicker.PickPhotoAsync();

                if (filePickerResult != null)
                {
                    var stream = await filePickerResult.OpenReadAsync();

                    // Obtener la longitud del archivo de imagen en bytes
                    var fileSize = stream.Length;

                    // Verificar el tamaño del archivo (por ejemplo, 2MB)
                    var maxFileSize = 2 * 1024 * 1024; // Cambiar el tamaño máximo según tus necesidades

                    if (fileSize > maxFileSize)
                    {
                        await DisplayAlert("Advertencia", "La imagen seleccionada es demasiado grande. Por favor, selecciona una imagen más pequeña.", "Aceptar");
                        return;
                    }

                    // Convertir la imagen a un arreglo de bytes
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        imageBytes = memoryStream.ToArray();
                    }

                    // Asignar el arreglo de bytes como origen de la imagen
                    imageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));

                    // Mostrar la imagen
                    ImagePreview.Source = imageSource;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        private async void Btn_Add(object sender, EventArgs e)
        {
            if (ImagePreview.Source == null)
            {
                await DisplayAlert("Advertencia", "Por favor, selecciona una imagen.", "Aceptar");
                return;
            }

            if (!IsValidEmail(gmail.Text))
            {
                await DisplayAlert("Advertencia", "Por favor, ingresa un correo electrónico válido.", "Aceptar");
                return;
            }

            try
            {
                var item = new ToDoItem
                {
                    Nombre = Name.Text,
                    Apellido = Last.Text,
                    Correo = gmail.Text,
                    Telefono = tel.Text,
                    Imagen = imageBytes

                };
                var result = await App.Context.InsertItemAsyn(item);

                if (result == 1)
                {
                    await DisplayAlert("Proceso", "Se guardó la información", "Aceptar");

                    await Navigation.PushAsync(new Consulta());

                }
                else
                {
                    await DisplayAlert("Error", "Hubo un error", "Aceptar");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");

            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


    }
}