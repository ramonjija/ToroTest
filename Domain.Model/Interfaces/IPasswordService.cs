namespace Domain.Model.Interfaces
{
    public interface IPasswordService
    {
        string HashPassword(string password);

        bool IsPasswordValid(string password, string hashedPassword);
    }
}
