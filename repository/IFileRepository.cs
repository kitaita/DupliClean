namespace DupliClean
{
    public interface IFileRepository
    {
        public IEnumerable<MyFile> GetFiles(string ext = "*");
    }
}