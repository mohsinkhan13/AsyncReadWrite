using System.IO;

namespace SaveTheChildren.Exercise2
{
    public static class FileFactory
    {
        public static BaseFile GetFile(FileInfo fileInfo)
        {
            var ext = Path.GetExtension(fileInfo.FullName);
            if (ext == ".txt")
            {
                return new TextFile(fileInfo);
            }

            if (ext.Equals(".zip"))
            {
                return new ZipFile(fileInfo);
            }

            if (ext.Equals(".html"))
            {
                return new HtmlFile(fileInfo);
            }

            return null;
        }
    }
}
