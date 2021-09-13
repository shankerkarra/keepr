using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultKeepsService
  {

    private readonly VaultKeepsRepository _vksrepo;
    private readonly KeepsRepository _krepo;
    private readonly VaultsRepository _vrepo;

    public VaultKeepsService(VaultKeepsRepository vksrepo, KeepsRepository krepo, VaultsRepository vrepo)
    {
      _vksrepo = vksrepo;
      _krepo = krepo;
      _vrepo = vrepo;
    }

    internal Vaultkeep GetById(int id)
    {
      Vaultkeep vkfound = _vksrepo.GetById(id);
      // in vFound check if it is private - remove it
      if (vkfound == null)
      {
        throw new Exception("Invalid Id");
      }
      return vkfound;
    }

    internal Vaultkeep Create(Vaultkeep newVaultkeep)
    {
      //Fetch the Vault Id
      // Fetch the Keep Id and Increment the Keeps
      Vault vault = _vrepo.GetById(newVaultkeep.VaultId);
      if (vault != null && vault.CreatorId == newVaultkeep.CreatorId)
      {
        Vaultkeep vaultkeep = _vksrepo.Create(newVaultkeep);
        if (vaultkeep != null)
        {
          Keep keep = _krepo.GetById(vaultkeep.KeepId);
          keep.Keeps += 1;
          _krepo.EditKeepsCount(keep);
          return vaultkeep;
        }
        throw new Exception("UnAuthorized");
      }
      throw new Exception("UnAuthorized");
    }

    internal List<Keep> GetKeepsByVaultId(int id)
    {
      return _vksrepo.GetKeepsByVaultId(id);

    }

    internal void Delete(int VaultkeepId, string userId)
    {
      Vaultkeep vktoDelete = GetById(VaultkeepId);
      if (vktoDelete.CreatorId != userId)
      {
        throw new Exception("Thats not your Keep");
      }
      if (_vksrepo.Delete(VaultkeepId) > 0)
      {
        // Fetch the Keep record based on Keep Id
        // Reduce the keeps count
        Keep keep = _krepo.GetById(vktoDelete.KeepId);
        keep.Keeps -= 1;
        _krepo.EditKeepsCount(keep);
        // return Ok("Deleted");
      }
      throw new Exception("Thats not your Keep");
    }
  }
}