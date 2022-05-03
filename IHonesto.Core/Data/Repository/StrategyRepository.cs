using Dapper;
using IHonesto.Core.Entities;

namespace IHonesto.Core.Data.Repository
{
    public class StrategyRepository
    {
        private DbSession _session;

        public StrategyRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<Strategy>> GetByPlataform(int plataformId)
        {
            var query = "SELECT * FROM [dbo].[TB_STRATEGIES] WHERE PlataformId = @PlataformId";
            var result = await _session.Connection.QueryAsync<Strategy>(query, new { PlataformId = plataformId });
            return result;
        }
    }
}
