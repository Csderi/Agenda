using Agenda.Models;
using Agenda.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agenda.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pago : ContentPage
    {
        private ToDoItem selectedToDoItem;

        public Pago(ToDoItem selectedItem)
        {
            InitializeComponent();
            selectedToDoItem = selectedItem;
        }

        private string GenerateBoleto()
        {
            // Generar el boleto de compra basado en los detalles de la banda seleccionada
            string boleto = $"Banda: {selectedToDoItem.Nombre} {selectedToDoItem.Apellido}\n" +
                            $"Correo: {selectedToDoItem.Correo}\n" +
                            $"Teléfono: {selectedToDoItem.Telefono}\n" +
                            $"Repertorio: {selectedToDoItem.repertorio}\n" +
                            $"Costo: {selectedToDoItem.costo}\n" +
                            $"Fecha: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";

            return boleto;
        }

        private bool ValidateCreditCard(string creditCardNumber)
        {
            // Eliminar los espacios en blanco y guiones del número de tarjeta
            creditCardNumber = creditCardNumber.Replace(" ", "").Replace("-", "");

            // Comprobar que el número de tarjeta tenga 16 dígitos
            if (creditCardNumber.Length != 16)
            {
                return false;
            }

            // Comprobar que el número de tarjeta consista únicamente en dígitos
            if (!creditCardNumber.All(char.IsDigit))
            {
                return false;
            }

            // Comprobar la validez utilizando el algoritmo de Luhn
            int sum = 0;
            bool alternate = false;
            for (int i = creditCardNumber.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(creditCardNumber[i].ToString());

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                alternate = !alternate;
            }

            return sum % 10 == 0;
        }



        private async void ContinuarButton_Clicked(object sender, EventArgs e)
        {
            // Validar la información de la tarjeta de crédito
            string creditCardNumber = numeroTarjetaEntry.Text.Trim();

            if (string.IsNullOrWhiteSpace(creditCardNumber))
            {
                await DisplayAlert("Error", "Ingrese un número de tarjeta válido", "OK");
                return;
            }

            if (!ValidateCreditCard(creditCardNumber))
            {
                await DisplayAlert("Error", "Número de tarjeta no válido", "OK");
                return;
            }

            // Generar el boleto de compra
            string boleto = GenerateBoleto();

            // Guardar el boleto en el dispositivo
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "boleto.txt");
            File.WriteAllText(fileName, boleto);

            // Mostrar un mensaje de éxito y ofrecer guardar el boleto
            bool result = await DisplayAlert("Compra exitosa", "¡La compra se ha realizado con éxito!\n¿Desea guardar el boleto?", "Sí", "No");

            if (result)
            {
                // Abrir el archivo del boleto
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(fileName)
                });
            }

            // Volver a la página principal
            await Navigation.PopToRootAsync();
        }


        private async Task SaveBoletoToDevice(string boleto)
        {
            string filename = "boleto.txt";

            // Utilizar DependencyService para acceder a la implementación de ISaveFile en Android
            if (DependencyService.Get<ISaveFile>() is ISaveFile saveFile)
            {
                try
                {
                    await saveFile.SaveTextAsync(filename, boleto);
                    await DisplayAlert("Éxito", "El boleto ha sido guardado.", "OK");
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que pueda ocurrir al guardar el archivo
                    await DisplayAlert("Error", $"No se pudo guardar el boleto: {ex.Message}", "OK");
                }
            }
        }
    }
}
