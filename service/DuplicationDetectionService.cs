namespace DupliClean
{
    internal class DuplicationDetectionService
    {
        private IFileRepository fileRepository;

        public DuplicationDetectionService(IFileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }

        internal IEnumerable<IGrouping<string, MyFile>> GetDuplicatedFileGroups(string ext = "*")
        {
            var files = fileRepository.GetFiles(ext);
            return files.GroupBy(x => x.Hash).Where(x => x.Count() > 1);
        }
    }
}