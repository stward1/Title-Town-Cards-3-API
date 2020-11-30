namespace API.Models.Interfaces
{
    public interface ILogInCustomer
    {
        //this interface is for getting the rewards points of a customer; -1 is returned if no customer exists
        int FindCustomer(Customer value);
    }
}