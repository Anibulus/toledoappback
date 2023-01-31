namespace Toledo.Core.Entities
{
    public class Location : BaseEntity
    {
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string Zone { get; set; } = "";
        public string SubZone { get; set; } = "";
        public string Address { get; set; } = "";
    }
}
