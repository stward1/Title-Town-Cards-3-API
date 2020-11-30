using System.Data.SqlTypes;
using System;
namespace API.Models
{
    public class Item
    {
        public int ItemID {get; set;}
        public string ItemName {get; set;}
        public double ItemPrice {get; set;}
        public int ItemYear {get; set;}
        public double ItemCost {get; set;}
        public Boolean ItemIsPurchased {get; set;}
        public string ItemMemorabiliaDescription {get; set;}
        public string ItemCardCondition {get; set;}
        public string ItemCardSport {get; set;}
        public string ItemCardTeam {get; set;}
        
        public  override string ToString()
        {
            return ItemName + " " + ItemPrice + " " + ItemYear;
        }
    }
}