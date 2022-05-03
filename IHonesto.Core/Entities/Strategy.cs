namespace IHonesto.Core.Entities
{
    public class Strategy
    {
        public enum StrategyKind
        {
            Xpath = 0,
        }

        public int Id { get; set; }
        public int PlataformId { get; set; }
        public StrategyKind Kind { get; set; }
        public string Parameter { get; set; }
        public int Weight { get; set; }

        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
