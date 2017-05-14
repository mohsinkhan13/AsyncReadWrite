using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncReadWrite.IO
{
    public class FileReader : IReader
    {
        public FileReader()
        {
            CancellationToken = new CancellationToken(false);
        }
        public CancellationToken CancellationToken { get; set; }

        public async Task Read(string filePath, int frequencyInSeconds)
        {
            while (!CancellationToken.IsCancellationRequested)
            {

                using (FileStream sourceStream = new FileStream(filePath,
                    FileMode.Open, FileAccess.Read, FileShare.Read,
                    bufferSize: 4096, useAsync: false))
                {
                    StreamReader reader = new StreamReader(sourceStream);
                    string textToDisplay = string.Empty;
                    while (reader.Peek() > -1)
                    {
                        string newline =  reader.ReadLine();
                        if (newline != "\0")
                            textToDisplay = newline;
                    }

                    Console.WriteLine(textToDisplay.Trim());

                };
                await Task.Delay(frequencyInSeconds*1000, CancellationToken);
            }
        }
    }
}
