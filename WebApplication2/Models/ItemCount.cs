namespace WebApplication2.Models
{
    public class ItemCount
    {
        public int ItemCounter { get; set; }
        public string ItemCountCode { get; set; } = string.Empty;
        public string ItemCode { get; set; } = string.Empty;
        public string ItemDescription { get; set; } = string.Empty;
        public string ItemUom { get; set; } = string.Empty;
        public string ItemBatchLotNumber { get; set; } = string.Empty ;
        public string ItemExpiry { get; set; } = string.Empty;
        public int ItemQuantity { get; set; }
        public DateTime ItemDateLog { get; set; } 
    }
}
