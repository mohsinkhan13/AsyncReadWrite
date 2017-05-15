using System.IO;

namespace SaveTheChildren.Exercise2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FileProcessor fp = new FileProcessor();

            foreach (var filePath in Directory.EnumerateFiles("test"))
            {
                FileInfo info = new FileInfo(filePath);
                var file = FileFactory.GetFile(info);
                if (file == null)
                {
                    System.Console.WriteLine("Invalid file - " + info.FullName );
                    continue;
                }
                file.Process(fp);
            }
        }
    }
}
