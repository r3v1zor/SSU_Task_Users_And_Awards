using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;
using Npgsql;

namespace DAL
{
    public class UsersDao : IUsersDao
    {
        private readonly string _connectionString;

        public UsersDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Save(User user)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("sp_add_user", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var userName = new NpgsqlParameter()
                {
                    ParameterName = "@name",
                    Value = user.Name
                };
                
                var dateOfBirth = new NpgsqlParameter()
                {
                    ParameterName = "@dateOfBirth",
                    Value = user.DateOfBirth
                };
                
                var age = new NpgsqlParameter()
                {
                    ParameterName = "@age",
                    Value = user.Age
                };

                command.Parameters.Add(userName);
                command.Parameters.Add(dateOfBirth);
                command.Parameters.Add(age);

                var updatedRows = command.ExecuteNonQuery();
            }
        }

        public void Delete(User user)
        {
            Delete(user.Id);
        }

        public void Delete(long id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("sp_delete_user", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var userId = new NpgsqlParameter()
                {
                    ParameterName = "@id",
                    Value = id
                };
                
                command.Parameters.Add(userId);

                var updatedRows = command.ExecuteNonQuery();
            }
        }

        public List<User> FindAll()
        {
            var users = new List<User>();
            
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("sp_get_all_users", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt64(0);
                        var name = reader.GetString(1);
                        var dateOfBirth = reader.GetDateTime(2);
                        var age = reader.GetInt32(3);
                        
                        users.Add(new User(id, name, dateOfBirth, age));
                    }
                }
            }

            return users;
        }

        public User FindById(long id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand("sp_find_user_by_id", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                
                var userId = new NpgsqlParameter()
                {
                    ParameterName = "@id",
                    Value = id
                };

                command.Parameters.Add(userId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var uId = reader.GetInt64(0);
                        var name = reader.GetString(1);
                        var dateOfBirth = reader.GetDateTime(2);
                        var age = reader.GetInt32(3);
                        
                        return new User(uId, name, dateOfBirth, age);
                    }
                }
            }

            return null;
        }

        public List<Award> GetAllAwards(long id)
        {
            var awards = new List<Award>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                
                var command = new NpgsqlCommand("sp_get_all_awards_by_user_id", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var userId = new NpgsqlParameter()
                {
                    ParameterName = "@id",
                    Value = id
                };

                command.Parameters.Add(userId);

                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var awardId = reader.GetInt32(0);
                        var title = reader.GetString(1);
                    
                        awards.Add(new Award(awardId, title));
                    }
                }
            }

            return awards;
        }

        public void AddAward(long userId, Award award)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                
                var command = new NpgsqlCommand("sp_add_award_to_user", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var uId = new NpgsqlParameter()
                {
                    ParameterName = "@userId",
                    Value = userId
                };

                var aId = new NpgsqlParameter()
                {
                    ParameterName = "@awardId",
                    Value = award.Id
                };
                
                command.Parameters.Add(uId);
                command.Parameters.Add(aId);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteAwardById(long userId, long awardId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                
                var command = new NpgsqlCommand("sp_delete_award_from_user", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var uId = new NpgsqlParameter()
                {
                    ParameterName = "@userId",
                    Value = userId
                };

                var aId = new NpgsqlParameter()
                {
                    ParameterName = "@awardId",
                    Value = awardId
                };
                
                command.Parameters.Add(uId);
                command.Parameters.Add(aId);

                command.ExecuteNonQuery();
            }
        }
        
    }
}