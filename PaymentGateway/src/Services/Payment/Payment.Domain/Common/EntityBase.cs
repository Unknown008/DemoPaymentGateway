namespace PaymentNs.Domain.Common
{
    public abstract class EntityBase
    {
        public int Id { get; protected set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
