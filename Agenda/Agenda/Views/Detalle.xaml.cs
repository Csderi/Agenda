using Agenda.Models;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agenda.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detalle : ContentPage
    {
        private ToDoItem item;
        private byte[] imageBytes;

        public Detalle(ToDoItem selectedItem)
        {
            InitializeComponent();
            item = selectedItem;
            LoadItemDetails();
        }

        private void LoadItemDetails()
        {
            Name.Text = item.Nombre;
            Last.Text = item.Apellido;
            gmail.Text = item.Correo;
            tel.Text = item.Telefono;
            ImagePreview.Source = ImageSource.FromStream(() => new MemoryStream(item.Imagen));
        }

        private async void SelectImage_Clicked(object sender, EventArgs e)
        {
            try
            {
                var filePickerResult = await MediaPicker.PickPhotoAsync();

                if (filePickerResult != null)
                {
                    using (var stream = await filePickerResult.OpenReadAsync())
                    {
                        // Obtener la longitud del archivo de imagen en bytes
                        var fileSize = stream.Length;

                        // Verificar el tamaño del archivo (por ejemplo, 2MB)
                        var maxFileSize = 2 * 1024 * 1024; // Cambiar el tamaño máximo según tus necesidades

                        if (fileSize > maxFileSize)
                        {
                            await DisplayAlert("Advertencia", "La imagen seleccionada es demasiado grande. Por favor, selecciona una imagen más pequeña.", "Aceptar");
                            return;
                        }

                        // Copiar el stream en una nueva instancia
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);

                            // Mostrar la imagen
                            ImagePreview.Source = ImageSource.FromStream(() => memoryStream);

                            // Convertir la imagen en bytes
                            imageBytes = memoryStream.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }


        private async void Btn_Modificar(object sender, EventArgs e)
        {
            try
            {
                item.Nombre = Name.Text;
                item.Apellido = Last.Text;
                item.Correo = gmail.Text;
                item.Telefono = tel.Text;

                if (imageBytes != null)
                {
                    item.Imagen = imageBytes;
                }

                // Realizar la actualización en la base de datos
                var result = await App.Context.UpdateItemAsync(item);

                if (result == 1)
                {
                    await DisplayAlert("Proceso", "Se actualizó la información", "Aceptar");
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un error al actualizar la información", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }
    }
}

