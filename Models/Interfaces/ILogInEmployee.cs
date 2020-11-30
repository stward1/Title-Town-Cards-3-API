namespace API.Models.Interfaces
{
    public interface ILogInEmployee
    {
        //this interface is for getting the employee ID of an employee; -1 is returned if no employee exists
        int FindEmployee(Employee value);
    }
}