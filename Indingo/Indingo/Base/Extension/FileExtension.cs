using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Indingo.Base.Extension
{
    public static class FileExtension
    {
        public  static bool CheckTypeFile(this IFormFile formFile ,string type)
        {
            if (formFile.ContentType.Contains(type)) { return true; } return false;

        }
        public  static bool CheckSizeFile(this IFormFile formFile, int size)
        {
            if(formFile.Length<=size*1024) { return true; } return false;
        }
        public async static Task<string> CreateFileAsync(this IFormFile formFile, string root,string folder)
        {
            string filname = Guid.NewGuid().ToString()+formFile.FileName;
            string path = Path.Combine(root,folder, filname);
            using(FileStream sr = new FileStream(path,FileMode.Create))
            {
               await formFile.CopyToAsync(sr);
            }
            return filname;
        }
        public static void Delete(this string formFile, string root, string folder)
        {
            if(File.Exists(formFile))
            {
                File.Delete(formFile);
            }


        }

    }
}
