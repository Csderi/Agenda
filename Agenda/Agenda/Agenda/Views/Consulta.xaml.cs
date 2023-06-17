using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Agenda.Converters;
using Agenda.Models;

namespace Agenda.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Consulta : ContentPage
    {
        public Consulta()
        {
            InitializeComponent();
       
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadItems();
        }

        public async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
        }

        private async void LoadItems()
        {
            var items = await App.Context.GetItemsAsync();
            lista_tareas.ItemsSource = items;
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {

            if (await DisplayAlert("Confirmacion", "Estas seguro de eliminar el elemento", "Si", "No"))
            {
                var item = (ToDoItem)(sender as MenuItem).CommandParameter;
                var result = await App.Context.DeleteItemAsync(item);
                if(result == 1)
                {
                    LoadItems();
                }
            }


        }


    }
}
