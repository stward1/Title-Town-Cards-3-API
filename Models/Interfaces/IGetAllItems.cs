using System.Collections.Generic;

namespace API.Models
{
    public interface IGetAllItems
    {
        List<Item> GetAllItems();
    }
}