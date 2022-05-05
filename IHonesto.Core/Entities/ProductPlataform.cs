namespace IHonesto.Core.Entities
{
    public class ProductPlataform
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PlataformId { get; set; }
        public string Path { get; set; }
        public string Metadados { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
