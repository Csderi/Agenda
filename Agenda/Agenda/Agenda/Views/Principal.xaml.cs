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
    public partial class Principal : ContentPage
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void Btn_ClickAdd(object sender, EventArgs e)
        {

            Navigation.PushAsync(new Registro());
            
        }

        private void Btn_ClickMod(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Modificar());
        }

        private void Btn_ClickConsulta(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Consulta());
        }

        private void Btn_ClickContratar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Contratar1());
        }


    }
}