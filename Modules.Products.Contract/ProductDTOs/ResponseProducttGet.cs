namespace Modules.Products.Contract.ProductDTOs
{
    public class ResponseProductGet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public string CategroyName { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}