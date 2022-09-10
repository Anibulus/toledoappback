using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using Toledo.Core.Enumerations;

namespace Toledo.Core.Entities
{
    [Index(nameof(DNI), IsUnique = true)]
    public class User : BaseEntity
    {
        //<summary>
        //Cc, Identification Number
        //</summary>
        public string DNI { get; set; } = "";
        public string Name { get; set; } = "";
        public bool Active { get; set; } = true;
        public int LoginCount { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Email { get; set; } = "";
        [GraphQLIgnore]
        public string Password { get; set; } = "";
        [GraphQLIgnore]
        public string PasswordSalt { get; set; } = "";
        public EnumRole Role { get; set; } = EnumRole.USER;
        public EnumGender Gender { get; set; } = EnumGender.OTHER;
        public string? Phone { get; set; }
        public string? Observation { get; set; }
        public virtual Location? Location { get; set; }
        public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}
