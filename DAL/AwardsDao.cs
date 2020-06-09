using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;
using Npgsql;

namespace DAL
{
    public class AwardsDao : IAwardsDao
    {
        private readonly string _connectionString;

        public AwardsDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Save(Award award)
        {
            using (var connection = new NpgsqlConnection())
            {
                connection.Open();
                var sqlCommand = new NpgsqlCommand("sp_add_award", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var title = new NpgsqlParameter()
                {
                    ParameterName = "@title",
                    Value = award.Title
                };

                sqlCommand.Parameters.Add(title);

                var updatedRows = sqlCommand.ExecuteNonQuery();
            }
        }

        public void Delete(Award award)
        {
            Delete(award.Id);
        }
        
        public void Delete(long id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sqlCommand = new NpgsqlCommand("sp_delete_award", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var uId = new NpgsqlParameter()
                {
                    ParameterName = "@id",
                    Value = id
                };

                sqlCommand.Parameters.Add(uId);

                var updatedRows = sqlCommand.ExecuteNonQuery();
            }
        }

        public List<Award> FindAll()
        {
            var awards = new List<Award>();
            
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sqlCommand = new NpgsqlCommand("sp_get_all_awards", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt64(0);
                        var title = reader.GetString(1);

                        awards.Add(new Award(id, title));
                    }
                }
            }

            return awards;
        }

        public Award FindById(long id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sqlCommand = new NpgsqlCommand("sp_get_award_by_id", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                
                var uId = new NpgsqlParameter()
                {
                    ParameterName = "@id",
                    Value = id
                };

                sqlCommand.Parameters.Add(uId);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var awardId = reader.GetInt64(0);
                        var title = reader.GetString(1);

                        return new Award(awardId, title);
                    }
                }
            }

            return null;
        }
    }
}