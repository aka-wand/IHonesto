using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHonesto.Core.Entities
{
    public class ScanError
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? PlataformId { get; set; }
        public int? StrategyId { get; set; }
        public string Message { get; set; }
        public string? Metadados { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
