using System.Text;

namespace Application.UnitTests.TestUtils.TestConstants;

public partial class Constants
{
    public static class File
    {
        public const string OrdinalFileName = "OrdinalFileName";
        public const string FileContent = "FileContent";
        public const int EmptyFileLength = 0;
        public const string FileNameWithoutExtension = "FileName";
        public const string FileNameWithExtension = "FileName.txt";
        public const string FileUrl = "FileUrl";
        public const string ContentType = "ContentType";
        
        public static byte[] FileContentAsArrayOfBytes 
            => Encoding.UTF8.GetBytes(FileContent);
    }
}