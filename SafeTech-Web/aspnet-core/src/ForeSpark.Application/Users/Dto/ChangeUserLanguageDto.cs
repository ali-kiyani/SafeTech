using System.ComponentModel.DataAnnotations;

namespace ForeSpark.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}