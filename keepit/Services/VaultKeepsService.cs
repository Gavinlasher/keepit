using System;
using keepit.Models;
using keepit.Repositories;

namespace keepit.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository vk_repo;
    private readonly VaultsRepository v_repo;

    public VaultKeepsService(VaultKeepsRepository vk_repo, VaultsRepository v_repo)
    {
      this.vk_repo = vk_repo;
      this.v_repo = v_repo;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData, string userId)
    {
      Vault foundVault = v_repo.GetById(vaultKeepData.VaultId);

      if (foundVault.CreatorId != userId)
      {
        throw new Exception("login in");
      }
      return vk_repo.Create(vaultKeepData);
    }

    internal VaultKeep GetById(int id)
    {
      VaultKeep found = vk_repo.GetById(id);
      if (found == null)
      {
        throw new Exception("This is a INvaild VaultKeep Id");
      }
      return found;
    }

    internal void Remove(int VaultKeepId, string userId)
    {
      VaultKeep found = GetById(VaultKeepId);
      if (found.CreatorId != userId)
      {
        throw new Exception("You dont have permission to Deleted that Vault");
      }
      vk_repo.Remove(VaultKeepId);
    }
  }
}