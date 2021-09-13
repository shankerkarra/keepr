using System;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vrepo;

    public VaultsService(VaultsRepository vrepo)
    {
      _vrepo = vrepo;
    }

    internal Vault GetById(int id)
    {
      Vault vfound = _vrepo.GetById(id);
      // in vFound check if it is private - remove it
      if (vfound == null)
      {
        throw new Exception("Invalid Id");
      }
      return vfound;
    }

    internal Vault Create(Vault newVault)
    {
      return _vrepo.Create(newVault);
    }

    // public Vault GetVaultsByProfileId(string profileId)
    // {
    //   // NOTE Need to look into
    //   // List<Vault> vault =
    //   // return _vrepo.GetVaultsByProfileId(profileId);
    //   // return vault;
    //   throw new Exception();
    // }
    internal Vault Edit(Vault updatedVault)
    {
      Vault original = GetById(updatedVault.Id);
      if (original.CreatorId != updatedVault.CreatorId)
      {
        throw new Exception("You are not the Creator!");
      }
      original.Name = updatedVault.Name ?? original.Name;
      original.Description = updatedVault.Description ?? original.Description;
      original.isPrivate = updatedVault.isPrivate ?? original.isPrivate;
      _vrepo.Edit(original);
      return original;
    }

    // internal Vault GetById(int VaultId, string userId)
    // {
    //   throw new NotImplementedException();
    // }

    internal void Delete(int VaultId, string userId)
    {
      Vault vtoDelete = GetById(VaultId);
      if (vtoDelete.CreatorId != userId)
      {
        throw new Exception("Thats not your Keep");
      }
      _vrepo.Delete(VaultId);
    }
  }
}