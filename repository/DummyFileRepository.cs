namespace DupliClean
{
    public class DummyFileRepository : IFileRepository
    {
        IEnumerable<MyFile> IFileRepository.GetFiles(string ext)
        {
            return new List<MyFile>();
        }
    }
}