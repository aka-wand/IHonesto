using IHonesto.Core.Data.Repository;

namespace IHonesto.Core.Data
{
    public sealed class PersistenceContext
    {
        private readonly DbSession _session;
        public PlataformRepository Plataform { get; set; }
        public ProductRepository Products { get; set; }
        public ProductPlataformRepository ProductPlataform { get; set; }
        public StrategyRepository Strategy { get; set; }
        public ScanRepository Scan { get; set; }

        public PersistenceContext(DbSession session,
            PlataformRepository plataform,
            ProductRepository products,
            ProductPlataformRepository productPlataform,
            StrategyRepository strategy,
            ScanRepository scan)
        {
            _session = session;
            Plataform = plataform;
            Products = products;
            Strategy = strategy;
            ProductPlataform = productPlataform;
            Scan = scan;
        }

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}
