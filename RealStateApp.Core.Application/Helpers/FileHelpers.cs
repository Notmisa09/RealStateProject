using Microsoft.AspNetCore.Http;

namespace RealStateApp.Core.Application.Helpers
{
    public class FileHelpers
    {
        public static string UploadFile(IFormFile file, dynamic Id, string pathRoute , bool Editmode = false, string Photofile = "")
        {
            if (Editmode && file == null)
            {
                return Photofile;
            }

            string basepath = $"/Image/{pathRoute}/{Id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basepath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileinfo = new(file.FileName);
            string filename = guid + fileinfo.Extension;
            //
            string absolutefilepath = Path.Combine(path, filename);
            //
            using(var stam = new FileStream(absolutefilepath, FileMode.Create))
            {
                file.CopyTo(stam);
            }

            if(Editmode == true)
            {
                string[] oldimage = Photofile.Split('/');
                string OldimageName = oldimage[^1];
                string completeImageOldPath = Path.Combine(path, OldimageName);


                if(File.Exists(completeImageOldPath))
                {
                    File.Delete(completeImageOldPath);
                }

            }
            return $"{basepath}/{filename}";
        }

        public static void ElimProfileImage(string ImagePath)
        {
            if (File.Exists(ImagePath))
            {
                File.Delete(ImagePath);
            }
        }
    }
}
