using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HallOfFameAPI.Models
{
    public class Person
    {
        public long ID { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить имя")]
        [Display(Name = "ФИО")]
        public string Name { get; set; }

        [Display(Name = "Навыки")]
        public List<Skill> Skills { get; set; }
    }
}
