public class UpdateItemDto
{
    public int ItemId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
}
