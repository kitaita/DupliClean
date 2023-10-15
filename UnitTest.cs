using Moq;
using System.IO;
using DupliClean;
using System.Runtime.CompilerServices;

namespace DupliCleanTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ServiceCreationTest()
    {
        var dummyRepository = new DummyFileRepository();
        var service = new DuplicationDetectionService(dummyRepository);
    }

    [Test]
    public void NoDuplicationTest()
    {
        var repository = new Mock<IFileRepository>();
        repository.Setup(r => r.GetFiles("txt")).Returns(
            new List<MyFile>
        {
            new MyFile { FilePath = "file1.txt", Hash = "hash1" },
            new MyFile { FilePath = "file2.txt", Hash = "hash2" },
            new MyFile { FilePath = "file3.txt", Hash = "hash3" },
            new MyFile { FilePath = "file4.txt", Hash = "hash4" },
            new MyFile { FilePath = "file5.txt", Hash = "hash5" },
        }
        );
        var service = new DuplicationDetectionService(repository.Object);

        var duplicatedFileSets = service.GetDuplicatedFileGroups();

        Assert.That(duplicatedFileSets.Count, Is.EqualTo(0));
    }

    [Test]
    public void TwoFileDuplicateTest()
    {
        var repository = new Mock<IFileRepository>();
        repository.Setup(r => r.GetFiles("txt")).Returns(
            new List<MyFile>
        {
            new MyFile { FilePath = "file1.txt", Hash = "hash1" },
            new MyFile { FilePath = "file2.txt", Hash = "hash2" },
            new MyFile { FilePath = "file3.txt", Hash = "hash3" },
            new MyFile { FilePath = "file4.txt", Hash = "hash4" },
            new MyFile { FilePath = "file5.txt", Hash = "hash1" },
        }
        );
        var service = new DuplicationDetectionService(repository.Object);
        var duplicatedFileSets = service.GetDuplicatedFileGroups("txt");

        Assert.That(duplicatedFileSets.Count, Is.EqualTo(1));
        Assert.That(duplicatedFileSets.First().Count, Is.EqualTo(2));
    }

    [Test]
    public void ThreeFileDuplicateTest()
    {
        var repository = new Mock<IFileRepository>();
        repository.Setup(r => r.GetFiles("txt")).Returns(
            new List<MyFile>
        {
            new MyFile { FilePath = "file1.txt", Hash = "hash1" },
            new MyFile { FilePath = "file2.txt", Hash = "hash2" },
            new MyFile { FilePath = "file3.txt", Hash = "hash1" },
            new MyFile { FilePath = "file4.txt", Hash = "hash4" },
            new MyFile { FilePath = "file5.txt", Hash = "hash1" },
        }
        );
        var service = new DuplicationDetectionService(repository.Object);

        var duplicatedFileSets = service.GetDuplicatedFileGroups("txt");

        Assert.That(duplicatedFileSets.Count, Is.EqualTo(1));
        Assert.That(duplicatedFileSets.First().Count, Is.EqualTo(3));
    }

    [Test]
    public void TwoDuplicateGroupsTest()
    {
        var repository = new Mock<IFileRepository>();
        repository.Setup(r => r.GetFiles("txt")).Returns(
            new List<MyFile>
        {
            new MyFile { FilePath = "file1.txt", Hash = "hash1" },
            new MyFile { FilePath = "file2.txt", Hash = "hash2" },
            new MyFile { FilePath = "file3.txt", Hash = "hash1" },
            new MyFile { FilePath = "file4.txt", Hash = "hash4" },
            new MyFile { FilePath = "file5.txt", Hash = "hash2" },
        }
        );
        var service = new DuplicationDetectionService(repository.Object);

        var duplicatedFileSets = service.GetDuplicatedFileGroups("txt");

        Assert.That(duplicatedFileSets.Count, Is.EqualTo(2));
    }

    [Test]
    public void EmptyDirectoryGetFilesTest()
    {
        var repository = new MyFileRepository("../../../test-directory/empty");
        Assert.That(repository.GetFiles().Count, Is.EqualTo(0));
    }

    [Test]
    public void SingleLevelDirectoryGetFilesTest()
    {
        var repository = new MyFileRepository("../../../test-directory/one-file");
        Assert.That(repository.GetFiles().Count, Is.EqualTo(1));

        repository = new MyFileRepository("../../../test-directory/two-file");
        Assert.That(repository.GetFiles().Count, Is.EqualTo(2));
    }

    [Test]
    public void MultiLevelDirectoryGetFilesTest()
    {
        var repository = new MyFileRepository("../../../test-directory/multi-level");
        Assert.That(repository.GetFiles().Count, Is.EqualTo(5));
    }

    [Test]
    public void MultiExtGetFilesTest()
    {
        var repository = new MyFileRepository("../../../test-directory/multi-ext");
        Assert.That(repository.GetFiles("txt").Count, Is.EqualTo(2));
    }

    [Test]
    public void FileHashTest()
    {
        var file1 = new MyFile("../../../test-directory/hash-test/1.txt");
        var file2 = new MyFile("../../../test-directory/hash-test/2.txt");
        var file3 = new MyFile("../../../test-directory/hash-test/3.txt");

        Assert.That(file1.Hash, Is.EqualTo(file2.Hash));
        Assert.That(file1.Hash, !Is.EqualTo(file3.Hash));
    }

}