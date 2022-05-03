using Dapper;
using IHonesto.Core.Entities;

namespace IHonesto.Core.Data.Repository
{
    public class ScanRepository
    {
        private DbSession _session;

        public ScanRepository(DbSession session)
        {
            _session = session;
        }

        public async Task Insert(Scan entity)
        {
            var query = "INSERT INTO [dbo].[TB_SCANS] ([ProductId] ,[PlataformId] ,[StrategyId] ,[Status] ,[Price] ,[Metadados] ,[CreatedAt]) VALUES (@ProductId ,@PlataformId ,@StrategyId ,@Status ,@Price , @Metadados, @CreatedAt)";
            await _session.Connection.ExecuteAsync(query, entity);
        }
    }
}
