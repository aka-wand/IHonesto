using Dapper;
using IHonesto.Core.Entities;

namespace IHonesto.Core.Data.Repository
{
    public class PlataformRepository
    {
        private DbSession _session;

        public PlataformRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<Plataform> GetById(int id)
        {
            var query = "SELECT * FROM TB_PLATAFORMS WHERE Id = @Id";
            var result = await _session.Connection.QueryFirstOrDefaultAsync<Plataform>(query, new { Id = id });
            return result;
        }
    }
}
