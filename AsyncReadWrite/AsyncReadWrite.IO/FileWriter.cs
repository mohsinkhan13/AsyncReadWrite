using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncReadWrite.IO
{
    public class FileWriter : IWriter
    {
        private string _filePath = string.Empty;
        public FileWriter()
        {
            CancellationToken = new CancellationToken(false);
        }
        public CancellationToken CancellationToken { get; set; }

        public async Task<bool> Refresh(int frequencyInSeconds)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(string.Empty);

            await Write(_filePath, frequencyInSeconds, FileMode.Truncate);

            return true;
        }

        public async Task Write(string filePath, int frequencyInSeconds)
        {
            _filePath = filePath;
            

            await this.Write(filePath, frequencyInSeconds, FileMode.Append);
        }

        private async Task Write(string filePath, int frequencyInSeconds, FileMode mode)
        {
            while (!CancellationToken.IsCancellationRequested)
            {
                byte[] encodedText = Encoding.Unicode.GetBytes(DateTime.Now.ToShortTimeString() + "\r\n");
                using (FileStream sourceStream = new FileStream(filePath,
                    mode, FileAccess.Write, FileShare.Write,
                    bufferSize: 4096, useAsync: true))
                {
                    if(mode != FileMode.Truncate)
                        await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);

                };
                await Task.Delay(frequencyInSeconds*1000, CancellationToken);
            }
        }

        
    }
}
