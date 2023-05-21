using Agenda.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Agenda.Views;

namespace Agenda
{
    public partial class App : Application
    {

        public static DataBaseContext Context { get; set; }
        public App()
        {
            InitializeComponent();
            InitializeDatabase();

            MainPage = new NavigationPage (new Principal());
        }

        private void InitializeDatabase()
        {

            var folderApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = System.IO.Path.Combine(folderApp, "ToDo.db3");
            Context = new DataBaseContext(dbPath);
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
