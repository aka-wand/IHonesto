using Dapper;
using IHonesto.Core.Entities;

namespace IHonesto.Core.Data.Repository
{
    public class ProductPlataformRepository
    {
        private DbSession _session;

        public ProductPlataformRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<ProductPlataform>> GetByProduct(int productId)
        {
            var query = "SELECT * FROM [dbo].[TB_PRODUCTS_PLATAFORM] WHERE ProductId = @ProductId";
            var result = await _session.Connection.QueryAsync<ProductPlataform>(query, new { ProductId = productId });
            return result;
        }
    }
}
