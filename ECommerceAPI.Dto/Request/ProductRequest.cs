namespace ECommerceAPI.Dto.Request
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Aqui tiene que ir el binario de la imagen
        /// </summary>
        public string Base64Image { get; set; }
        public string FileName { get; set; }
        public bool Active { get; set; }
    }
}
