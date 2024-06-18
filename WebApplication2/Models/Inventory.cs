namespace WebApplication2.Models
{
    public class Inventory
    {
        public int ItemId { get; set; }
        public string ItemNo { get; set; } = string.Empty;
        public string ItemDescription { get; set; } = string.Empty;
        public string ItemUom { get; set; } = string.Empty;
        public string ItemLotNumber { get; set; } = string.Empty;
        public string ItemBatchLotNumber { get; set; }
        public string ItemExpiry { get; set; } = string.Empty;
        public int ItemQuantity { get; set; }
        public string ItemDateLog { get; set; } = string.Empty;
        public string ItemCountCode { get; set; } = string.Empty;
        public string ItemEmployeeCode { get; set; } = string.Empty;
    }
}
