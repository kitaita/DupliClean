using System.IO;

namespace DupliClean
{
    public class MyFileRepository : IFileRepository
    {
        private string directoryPath;

        public MyFileRepository(string directoryPath)
        {
            this.directoryPath = directoryPath;
        }

        public IEnumerable<MyFile> GetFiles(string ext = "*")
        {
            var files = Directory.EnumerateFiles(directoryPath, $"*.{ext}", SearchOption.AllDirectories);
            var myFiles = new List<MyFile>();
            foreach (var file in files)
            {
                myFiles.Add(new MyFile());
            }
            return myFiles;
        }
    }
}