using Moq;
using System.IO;
using DupliClean;
using System.Runtime.CompilerServices;

namespace DupliCleanTest;

public class repositoryTests
{
    [SetUp]
    public void Setup()
    {
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