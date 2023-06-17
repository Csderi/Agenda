using System.Threading.Tasks;

namespace Agenda.Services
{
    public interface ISaveFile
    {
        Task SaveTextAsync(string filename, string text);
    }
}