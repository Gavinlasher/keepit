using System;
using System.Collections.Generic;
using keepit.Models;
using keepit.Repositories;

namespace keepit.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository p_repo;
    private readonly VaultsService _vs;

    public ProfilesService(ProfilesRepository p_repo, VaultsService vs)
    {
      this.p_repo = p_repo;
      _vs = vs;
    }

    internal Profile GetProfileById(string id)
    {
      Profile found = p_repo.GetProfileById(id);
      if (found == null)
      {
        throw new Exception("This is an Invaild profile Id");
      }
      return found;
    }

    internal List<Keep> GetAllKeeps(string id)
    {
      Profile profile = p_repo.GetProfileById(id);
      return p_repo.GetAllKeeps(profile.Id);

    }

    internal List<Vault> GetAllVaults(string profileId, string userId)
    {
      Profile profile = p_repo.GetProfileById(profileId);
      Vault vaultcheck = p_repo.GetVaultByProfile(profile.Id);
      if (vaultcheck.IsPrivate == true && vaultcheck.CreatorId != userId)
      {
        throw new Exception("these vault are privite");
      }

      return p_repo.GetAllVaults(profile.Id);
    }
  }
}