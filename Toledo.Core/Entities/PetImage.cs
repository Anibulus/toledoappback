namespace Toledo.Core.Entities
{
    public class PetImage : BaseEntity
    {
        public string Url { get; set; } = "";
        public Guid PetId { get; set; }
        [ForeignKey(nameof(PetId))]
        public virtual Pet? Pet { get; set; }
    }
}
