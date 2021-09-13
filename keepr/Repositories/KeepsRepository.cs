using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    // Pending to do GetKeepsByProfileID / Update Views 
    public List<Keep> GetAll()
    {
      //  Initally fetch without Creator
      //   string sql = "SELECT * FROM keep;";
      //   return _db.Query<Keep>(sql).ToList();

      string sql = @"
      SELECT 
        a.*,
        k.*
      FROM keep k
      JOIN accounts a ON a.id = k.creatorId;";
      return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
      {
        keep.Creator = prof;
        return keep;
      }, splitOn: "id").ToList();
    }

    internal Keep AddViewCount(Keep keep)
    {
      string sql = @"
      UPDATE keep
      SET
        views = @Views
      WHERE id = @Id
      ;";
      _db.Execute(sql, keep);
      return keep;
    }

    public Keep GetById(int id)
    {
      //  Initally fetch without Creator
      //   string sql = "SELECT * FROM keep WHERE id = @id;";
      //   return _db.QueryFirstOrDefault<Keep>(sql, new { id });
      string sql = @"
      SELECT
       a.*,
       k.* 
      FROM keep k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.id = @id;";
      return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
      {
        keep.Creator = prof;
        return keep;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
      string sql = @"
      SELECT
       a.*,
       k.* 
      FROM keep k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.creatorId = @ProfileId;";
      return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
      {
        keep.Creator = prof;
        return keep;
      }, new { profileId }, splitOn: "id").ToList();
    }

    public Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keep
      (name, description, img, views, shares, keeps, creatorId)
      VALUES
      (@Name, @Description,@Img, @Views, @Shares, @Keeps, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return GetById(newKeep.Id);
    }

    public Keep Edit(Keep updatedData)
    {
      string sql = @"
      UPDATE keep
      SET
        name = @Name, description = @Description,
        img = @Img, views=@Views, shares = @Shares, keeps = @Keeps
      WHERE id = @Id
      ;";
      _db.Execute(sql, updatedData);
      return updatedData;
    }

    public Keep EditKeepsCount(Keep keep)
    {
      string sql = @"
      UPDATE keep
      SET
        keeps = @Keeps
      WHERE id = @Id
      ;";
      _db.Execute(sql, keep);
      return keep;
    }

    public void Delete(int keepId)
    {
      string sql = "DELETE FROM keep WHERE id = @keepId LIMIT 1;";
      _db.Execute(sql, new { keepId });

    }

  }
}