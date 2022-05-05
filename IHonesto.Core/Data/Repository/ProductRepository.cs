using Dapper;
using IHonesto.Core.Entities;

namespace IHonesto.Core.Data.Repository
{
    public class ProductRepository
    {
        private DbSession _session;

        public ProductRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<Product> GetById(int id)
        {
            var query = "SELECT TOP(1) * FROM TB_PRODUCTS WHERE Id = @Id";
            var result = await _session.Connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
            return result;
        }

        public async Task<IEnumerable<Product>> GetBash()
        {
            var query = @"SELECT TOP(100) * FROM [dbo].[TB_PRODUCTS] p WHERE NOT EXISTS(SELECT * FROM TB_SCANS WHERE ProductId = p.id) UNION SELECT * FROM [dbo].[TB_PRODUCTS] p WHERE (SELECT TOP(1) DATEADD(HOUR, 2, CreatedAt) FROM TB_SCANS WHERE ProductId = p.Id ORDER BY CreatedAt) <= GETDATE();";
            var result = await _session.Connection.QueryAsync<Product>(query);
            return result;
        }
    }
}
