using Application.Uploads;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Uploads
{
    public class Base64FileUploader : IBase64FileUploader
    {
        private List<string> _allowedExtensions = new List<string>
        {
            "jpg", "png", "jpeg"
        };

        public string Upload(string base64File)
        {
            var extension = base64File.GetFileExtension();
            if (!_allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Unspported file extension.");
            }
            var path = GetPath(extension);
            System.IO.File.WriteAllBytes(path, Convert.FromBase64String(base64File));
            return path.Split("\\").Last();
        }

        private string GetPath(string ext)
        {
            var path = new List<string> { "wwwroot", "images"};

            var fileName = "";

            foreach (var pathItem in path)
            {
                fileName = Path.Combine(fileName, pathItem);
            }

            return Path.Combine(fileName, Guid.NewGuid().ToString() + "." + ext);
        }

        public IEnumerable<string> Upload(IEnumerable<string> base64Files)
        {
            List<string> uploadedFiles = new List<string>();

            foreach (var file in base64Files)
            {

                uploadedFiles.Add(Upload(file));
            }

            return uploadedFiles;
        }

        public bool IsExtensionValid(string base64File)
        {
            return _allowedExtensions.Contains(GetExtension(base64File));
        }

        public string GetExtension(string base64File)
        {
            return base64File.GetFileExtension();
        }
    }
}
