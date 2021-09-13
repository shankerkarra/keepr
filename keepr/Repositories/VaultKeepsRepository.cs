using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vaultkeep GetById(int id)
    {
      string sql = @"
      SELECT
       a.*,
       vk.* 
      FROM vaultkeep vk
      JOIN accounts a ON a.id = vk.creatorId
      WHERE vk.id = @id;";
      return _db.Query<Profile, Vaultkeep, Vaultkeep>(sql, (prof, vaultkeep) =>
      {
        vaultkeep.Creator = prof;
        return vaultkeep;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }


    internal Vaultkeep Create(Vaultkeep newVaultkeep)
    {
      string sql = @"
      INSERT INTO vaultkeep
      (vaultId, keepId, creatorId)
      VALUES
      (@VaultId, @KeepId, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      newVaultkeep.Id = _db.ExecuteScalar<int>(sql, newVaultkeep);
      // Need to Increment the Keep Count
      return GetById(newVaultkeep.Id);
    }

    internal int Delete(int vaultkeepId)
    {
      string sql = "DELETE FROM vaultkeep WHERE id = @vaultkeepId LIMIT 1;";
      return _db.Execute(sql, new { vaultkeepId });

    }


    public List<Keep> GetKeepsByVaultId(int VaultId)
    {

      string sql = @"
      SELECT
       vk.*,
       k.*
      FROM keep k
      JOIN vaultkeep vk ON vk.vaultId = @VaultId
      ;";
      return _db.Query<Vaultkeep, Keep, Keep>(sql, (vaultkeep, keep) =>
      {
        return keep;
      }, splitOn: "id").ToList();

      // string sql = @"
      // SELECT
      //  vk.*,
      //  k.*
      // FROM keep k
      // JOIN vaultkeep vk on vk.keepId = k.id 
      // WHERE vk.vaultid = @VaultId;";
      // return _db.Query<Keep, Vaultkeep, Vaultkeep>(sql, (vaultkeep, keep) =>
      // {
      //   vaultkeep.Keep = keep;
      //   return keep;
      // }, new { VaultId }, splitOn: "id").ToList();
    }
  }
}