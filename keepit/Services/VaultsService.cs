using System;
using System.Collections.Generic;
using keepit.Models;
using keepit.Repositories;

namespace keepit.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository vs_repo;
    private readonly VaultKeepsRepository vk_repo;

    public VaultsService(VaultsRepository vs_repo, VaultKeepsRepository vk_repo)
    {
      this.vs_repo = vs_repo;
      this.vk_repo = vk_repo;
    }

    internal Vault Create(Vault vaultData)
    {
      return vs_repo.Create(vaultData);
    }

    internal Vault GetById(int id, string userId)
    {
      Vault found = vs_repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invaild vault Id");
      }
      if (found.IsPrivate == true && found.CreatorId != userId)
      {
        throw new Exception("This is a privite Vault!");
      }

      return found;

    }

    internal Vault Edit(Vault updates, Account userInfo)
    {
      Vault og = vs_repo.GetById(updates.Id);
      if (og.CreatorId != userInfo.Id)
      {
        throw new Exception("Hey this is not you Vault Silly");
      }
      og.IsPrivate = updates.IsPrivate;
      og.Description = updates.Description ?? og.Description;
      og.Name = updates.Name ?? og.Name;
      vs_repo.Edit(og);
      return og;
    }



    internal List<Vault> GetAllMyVaults(string id)
    {
      return vs_repo.GetAllMyVaults(id);
    }

    internal string Remove(int id, Account userInfo)
    {
      Vault vault = vs_repo.GetById(id);
      if (vault.CreatorId != userInfo.Id)
      {
        throw new Exception("Not Your Vault!!");
      }
      return vs_repo.Remove(id);
    }



    internal List<KeepsViewModal> GetKeepsByVaultId(int id, string userId)
    {
      Vault foundvault = vs_repo.GetById(id);
      if (foundvault.IsPrivate == true && foundvault.CreatorId != userId)
      {
        throw new Exception("This is vault is private!");
      }
      return vk_repo.GetKeepsByVaultId(foundvault.Id);
    }

  }
}