using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agenda.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Modificar : ContentPage
    {
        public Modificar()
        {
            InitializeComponent();
        }

        private async void Btn_Modificar(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(IdEntry.Text, out id))
            {
                var item = await App.Context.GetItemAsync(id);
                if (item != null)
                {

                    await Navigation.PushAsync(new Detalle(item));

                }
                else
                {
                    await DisplayAlert("Error", "El ID especificado no existe en la base de datos.", "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Por favor, ingresa un ID válido.", "Aceptar");
            }
        }

    }
}