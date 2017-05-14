using System.Threading.Tasks;

namespace AsyncReadWrite.IO
{
    public interface IWriter
    {
        Task Write(string filePath, int frequencyInSeconds);
        Task<bool> Refresh(int frequencyInSeconds);
    }
}
