using System;
using System.Collections.Generic;
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

    public Vault GetById(int id, bool userSignIn, string userId)
    {
      Vault vfound = _vrepo.GetById(id);
      // in vFound check if it is private - remove it
      if (vfound != null)
      {
        if (userSignIn)
        {
          if (vfound.CreatorId == userId)
          {
            return vfound;
          }
          else
          // if (vfound.CreatorId != userId)
          {
            if (vfound.isPrivate == true)
            {
              throw new Exception("Vault - unauthorized");
            }
            else
            // if (vfound.isPrivate == false)
            {
              return vfound;
            }
          }
        }
        else
        // if (!userSignIn)
        {
          if (vfound.isPrivate == true)
          {
            throw new Exception("Cannot access - unauthorized");
          }
          else
          {
            return vfound;
          }
        }
      }
      else { throw new Exception(" Vault not found"); }
    }

    internal Vault Create(Vault newVault)
    {
      return _vrepo.Create(newVault);
    }

    internal Vault Edit(Vault updatedVault)
    {
      Vault original = _vrepo.GetById(updatedVault.Id);
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
      Vault vtoDelete = _vrepo.GetById(VaultId);
      if (vtoDelete.CreatorId != userId)
      {
        throw new Exception("Thats not your Keep");
      }
      _vrepo.Delete(VaultId);
    }

    internal List<Vault> GetVaultsByProfileId(string id, bool userSignIn, string userId)
    {
      if (id == userId)
      {
        // user vaults
        List<Vault> vaults = _vrepo.GetPrivateVaultsByProfileId(id);
        return vaults;
      }
      else
      { // non-private vaults
        List<Vault> vaults = _vrepo.GetPublicVaultsByProfileId(id);
        return vaults;
      }
    }
  }
}