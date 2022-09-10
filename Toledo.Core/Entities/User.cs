using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Toledo.Core.Enumerations;

namespace Toledo.Core.Entities
{
    public class User : BaseEntity
    {
        //<summary>
        //Cc, Identification Number
        //</summary>
        //TODO Set unique
        public string DNI { get; set; } = "";
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public EnumRole Role { get; set; } = EnumRole.USER;
        public EnumGender Gender { get; set; } = EnumGender.OTHER;
        public string? Phone { get; set; }
        public string? Observation { get; set; }
        public virtual Location? Location { get; set; }
        public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}
