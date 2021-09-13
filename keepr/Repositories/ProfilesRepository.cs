using System.Data;
using keepr.Models;
using Dapper;


namespace keepr.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetByEmail(string userEmail)
    {
      string sql = "SELECT * FROM profile WHERE email = @userEmail";
      return _db.QueryFirstOrDefault<Profile>(sql, new { userEmail });
    }

    internal Profile GetById(string id)
    {
      string sql = "SELECT * FROM profile WHERE id = @id";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }

    internal Profile Create(Profile newProfile)
    {
      string sql = @"
            INSERT INTO profile
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
      _db.Execute(sql, newProfile);
      return newProfile;
    }

    internal Profile Edit(Profile update)
    {
      string sql = @"
            UPDATE Profile
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
      _db.Execute(sql, update);
      return update;
    }
  }
}
