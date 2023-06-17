using Agenda.Droid.Services;
using Agenda.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveFileAndroid))]

namespace Agenda.Droid.Services
{
    public class SaveFileAndroid : ISaveFile
    {
        public async Task SaveTextAsync(string filename, string text)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);

            using (var writer = File.CreateText(filePath))
            {
                await writer.WriteAsync(text);
            }
        }
    }
}