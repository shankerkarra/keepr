using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault GetById(int id)
    {
      string sql = @"
      SELECT
       a.*,
       v.* 
      FROM vault v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.id = @id;";
      return _db.Query<Profile, Vault, Vault>(sql, (prof, vault) =>
      {
        vault.Creator = prof;
        return vault;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal Vault Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vault
      (name, description, isPrivate, creatorId)
      VALUES
      (@Name, @Description,@isPrivate, @CreatorId);
      SELECT LAST_INSERT_ID();";
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return GetById(newVault.Id);
    }


    public Vault Edit(Vault updatedData)
    {
      string sql = @"
      UPDATE vault
      SET
        name = @Name, description = @Description,
        isPrivate = @isPrivate 
      WHERE id = @Id
      ;";
      _db.Execute(sql, updatedData);
      return updatedData;
    }
    internal void Delete(int vaultId)
    {
      string sql = "DELETE FROM vault WHERE id = @vaultId LIMIT 1;";
      _db.Execute(sql, new { vaultId });

    }

    internal List<Vault> GetPublicVaultsByProfileId(string profileId)
    {
      string sql = @"
      SELECT
       a.*,
       v.* 
      FROM vault v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.creatorId = @profileId
      AND v.isPrivate = false;";
      return _db.Query<Profile, Vault, Vault>(sql, (prof, vault) =>
      {
        vault.Creator = prof;
        return vault;
      }, new { profileId }, splitOn: "id").ToList();
    }

    internal List<Vault> GetPrivateVaultsByProfileId(string id)
    {
      string sql = @"
      SELECT
       a.*,
       v.* 
      FROM vault v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.creatorId = @id;";
      return _db.Query<Profile, Vault, Vault>(sql, (prof, vault) =>
      {
        vault.Creator = prof;
        return vault;
      }, new { id }, splitOn: "id").ToList();
    }
  }
}