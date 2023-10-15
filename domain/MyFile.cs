using System.Security.Cryptography;

namespace DupliClean;
public class MyFile
{
    public string Hash { get; internal set; }
    public string FilePath { get; internal set; }

    public MyFile()
    {
    }

    public MyFile(string filePath)
    {
        this.FilePath = filePath;
        this.Hash = calculateSHA256(filePath);
    }

    private static string calculateSHA256(string filePath)
    {
        using (var sha256 = SHA256.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hash = sha256.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}