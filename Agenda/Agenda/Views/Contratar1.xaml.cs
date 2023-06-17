using Agenda.Models;
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
    public partial class Contratar1 : ContentPage
    {
        public Contratar1()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadItems();
        }

        private async void LoadItems()
        {
            var items = await App.Context.GetItemsAsync();
            lista_tareas2.ItemsSource = items;
        }

        private async void ContratarButton_Clicked(object sender, EventArgs e)
        {
            var selectedItem = (ToDoItem)lista_tareas2.SelectedItem;
            if (selectedItem != null)
            {
                await Navigation.PushAsync(new Pago(selectedItem));
            }
            else
            {
                // Manejar la situación en la que no se selecciona ningún elemento
            }
        }





    }
}