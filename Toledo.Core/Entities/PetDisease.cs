namespace Toledo.Core.Entities
{
    public class PetDisease : BaseEntity
    {
        public string Description { get; set; } = "";
        public Guid PetId { get; set; }
        [ForeignKey(nameof(PetId))]
        public virtual Pet? Pet { get; set; }
    }
}
