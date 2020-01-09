using System.ComponentModel.DataAnnotations;

namespace HallOfFameAPI.Models
{
    public class Skill
    {
        public int ID { get; set; }
        public long PersonID { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить название навыка")]
        [Display(Name = "Навык")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необходимо указать уровень навыка")]
        [Display(Name = "Уровень навыка")]
        [Range(1, 6, ErrorMessage = "Уровень навыка не может быть больше 6 и меньше 1")]
        public byte Level { get; set; }
    }
}
