namespace Modules.Products.Contract.ProductDTOs // Və ya istifadə etdiyin qovluq adı
{
    public class RequestProductCreate
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int CategoryId { get; set; } 
        public string Description { get; set; } = string.Empty;
    }
}