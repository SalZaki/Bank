namespace Payment.Bank.Common.Abstractions.Domain;

public interface IEntity
{
    public DateTime? CreatedOnUtc { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public string? ModifiedBy { get; set; }
}
