namespace IHonesto.Core.Entities
{
    public class Scan
    {

        public enum StatusCode
        {
            Success = 0,
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PlataformId { get; set; }
        public int StrategyId { get; set; }
        public StatusCode Status { get; set; }
        public string? Price { get; set; }
        public string? Metadados { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}