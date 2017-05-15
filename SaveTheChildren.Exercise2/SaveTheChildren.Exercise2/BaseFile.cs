using System.IO;

namespace SaveTheChildren.Exercise2
{
    public abstract class BaseFile
    {
        FileInfo _file;
        public BaseFile(FileInfo file)
        {
            _file = file;
        }

        public virtual void Process(FileProcessor p)
        {
            p.Process("Processing Text File " + _file.FullName);
        }


    }
}
