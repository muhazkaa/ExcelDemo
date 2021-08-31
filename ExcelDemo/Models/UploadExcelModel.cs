using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ExcelDemo.Web.Models
{
    public class UploadExcelModel
    {
        [Display(Name = "UploadFile")]
        [Required]
        public IFormFile ExcelFile { get; set; }
        public string FileName { get; set; }
    }

    public class FileModel
    {
        public string FileName { get; set; }
        public string Path { get; set; }
    }
}
