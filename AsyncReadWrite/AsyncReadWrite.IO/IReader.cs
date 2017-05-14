using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncReadWrite.IO
{
    public interface IReader
    {
        Task Read(string filePath, int frequencyInSeconds);
    }
}
