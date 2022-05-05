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
            var lastScan = await _session.Connection.QueryFirstOrDefaultAsync<Scan>("SELECT TOP(1) * FROM TB_SCANS WHERE ProductId = @ProductId AND PlataformId = @PlataformId AND StrategyId = @StrategyId  ORDER BY CreatedAt DESC;", entity);


            var query = String.Empty;

            if (lastScan != null)
            {
                if (lastScan.Price == entity.Price)
                {
                    query = "UPDATE TB_SCANS SET Incidence = Incidence + 1 WHERE Id = @Id";
                    await _session.Connection.ExecuteAsync(query, new { Id = lastScan.Id });
                    return;
                }
            }

            query = "INSERT INTO [dbo].[TB_SCANS] ([ProductId] ,[PlataformId] ,[StrategyId] ,[Price] ,[Metadados] ,[CreatedAt]) VALUES (@ProductId ,@PlataformId ,@StrategyId ,@Price , @Metadados, @CreatedAt)";

            await _session.Connection.ExecuteAsync(query, entity);

        }

        public async Task InsertError(ScanError entity)
        {
            var query = "INSERT INTO [dbo].[TB_SCANS_ERRORS] ([ProductId] ,[PlataformId] ,[StrategyId] ,[Message] ,[Metadados] ,[CreatedAt]) VALUES (@ProductId ,@PlataformId ,@StrategyId ,@Message , @Metadados, @CreatedAt)";

            await _session.Connection.ExecuteAsync(query, entity);
        }
    }
}
