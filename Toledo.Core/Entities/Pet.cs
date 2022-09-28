using Toledo.Core.Enumerations;

namespace Toledo.Core.Entities
{
    public class Pet : BaseEntity
    {
        public string Name { get; set; } = "";
        public EnumGender Gender { get; set; } = EnumGender.MALE;
        public string Breed { get; set; } = "";
        public EnumPetSize Size { get; set; } = EnumPetSize.MEDIUM;

        [NotMapped]
        public int Years { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Sterilized { get; set; } = false;
        public string? LocationOfSterilization { get; set; }
        public string Address { get; set; } = "";
        public string Ubication { get; set; } = "";
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Zone { get; set; } = "";
        public string Color { get; set; } = "";
        public string Notes { get; set; } = "";
        public string PetType { get; set; } = "";

        [NotMapped]
        public bool Silvestre { get; set; }
        public Guid? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
        public virtual ICollection<PetDisease> Diseases { get; set; } = new List<PetDisease>();
        public virtual ICollection<PetImage> PetImages { get; set; } = new List<PetImage>();
    }
}
