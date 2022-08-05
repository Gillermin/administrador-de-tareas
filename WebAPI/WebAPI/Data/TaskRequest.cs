using Dapper;
using Npgsql;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class TaskRequest : ITaskRequest
    {
        private PostgreSQLConfiguration _connectionString;

        public TaskRequest(PostgreSQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected NpgsqlConnection dbConnection() 
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }   

        public async Task<Tasks> GetTask(int id)
        {
            var db = dbConnection();
            var sql = @"
                        SELECT id_task AS IdTask, id_collaborator AS IdCollaborator, description
                               , status, priority, from_date AS FromDate, to_date AS ToDate, notes
                          FROM public.tbl_task
                         WHERE id_task = @Id
                        ";
            return await db.QueryFirstOrDefaultAsync<Tasks>(sql, new { Id = id});
        }

        public async Task<IEnumerable<Tasks>> GetAllTasks()
        {
            var db = dbConnection();
            var sql = @"
                          SELECT id_task AS IdTask, tbl_task.id_collaborator AS IdCollaborator, collaborator_name AS CollaboratorName, description
                                 , status, priority, from_date AS FromDate, to_date AS ToDate, notes
                            FROM public.tbl_task
                       LEFT JOIN public.tbl_collaborator 
                              ON tbl_task.id_collaborator = tbl_collaborator.id_collaborator
                        ORDER BY from_date ASC 
                        ";
            return await db.QueryAsync<Tasks>(sql, new { });
        }


        public async Task<IEnumerable<Tasks>> GetTasksFiltred(Filter filter)
        {
            var db = dbConnection();
            var sql = $@" 
                           SELECT id_task AS IdTask, tbl_task.id_collaborator AS IdCollaborator, collaborator_name AS CollaboratorName, description
                                  , status, priority, from_date AS FromDate, to_date AS ToDate, notes
                             FROM public.tbl_task
                        LEFT JOIN public.tbl_collaborator 
                               ON tbl_task.id_collaborator = tbl_collaborator.id_collaborator
                            WHERE (from_date >= {"@FromDate"} AND to_date <= {"@ToDate"}) 
                                  {(filter.IdCollaborator == 0 ? "" : "AND tbl_task.id_collaborator = @IdCollaborator")} 
                                  {(filter.Status == "TODOS" ? "" : "AND status = @Status ")}
                                  {(filter.Priority == "TODOS" ? "" : "AND priority = @Priority ")}
                         ORDER BY from_date ASC ";
            return await db.QueryAsync<Tasks>(sql, new { filter.FromDate, filter.ToDate , filter.IdCollaborator, filter.Status, filter.Priority });
        }
  
        public async Task<bool> InsertTask(Tasks tasks)
        {
            var db = dbConnection();
            var sql = $@"
                        INSERT INTO public.tbl_task (
                                    id_collaborator, 
                                    description, 
                                    {(tasks.Status == null ? "" : "status, ")} 
                                    priority, 
                                    from_date, 
                                    to_date, 
                                    notes)
	                         VALUES (@IdCollaborator, 
                                    @Description, 
                                    {(tasks.Status == null ? "" : "@Status, ")}  
                                    @Priority, 
                                    @FromDate, 
                                    @ToDate, 
                                    @Notes);
                        ";
            var result = await db.ExecuteAsync(sql, new { tasks.IdCollaborator, tasks.Description, tasks.Status, tasks.Priority, tasks.FromDate, tasks.ToDate, tasks.Notes });
            
            return result > 0;
        }

        public async Task<bool> UpdateTask(Tasks tasks)
        {
            var db = dbConnection();
            var sql = @"
                        UPDATE public.tbl_task
	                       SET id_collaborator=@IdCollaborator,
                               description=@Description,
                               status=@Status, 
                               priority=@Priority, 
                               from_date=@FromDate, 
                               to_date=@ToDate, 
                               notes=@Notes
	                     WHERE id_task = @IdTask;
                        ";
            var result = await db.ExecuteAsync(sql, new { tasks.IdTask, tasks.IdCollaborator, tasks.Description, tasks.Status, tasks.Priority, tasks.FromDate, tasks.ToDate, tasks.Notes });

            return result > 0;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var db = dbConnection();
            var sql = @"
                        DELETE 
                          FROM public.tbl_task
	                     WHERE id_task = @Id;
                        ";
            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<Collaborator>> GetCollaborators()
        {
            var db = dbConnection();
            var sql = @"
                        SELECT id_collaborator AS IdCollaborator, collaborator_name AS CollaboratorName
                          FROM public.tbl_collaborator
                        ";
            return await db.QueryAsync<Collaborator>(sql, new { });
        }
    }
}
