namespace BANK_MANAGEMENT.Managers
{
    public interface IAccountType
    {
        int Id { get; }

        Task<double> getInterestRate();
    }
}