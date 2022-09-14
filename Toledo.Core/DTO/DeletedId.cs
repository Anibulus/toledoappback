namespace Toledo.Core.DTO
{
    public class DeletedId
    {
        public DeletedId(Guid _id)
        {
            this.Id = _id;
        }
        public Guid Id { get; set; }
    }
}
