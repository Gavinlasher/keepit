using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepit.Models;

namespace keepit.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetProfileById(string id)
    {
      string sql = "SELECT * FROM accounts WHERE id = @id";
      return _db.Query<Profile>(sql, new { id }).FirstOrDefault();
    }

    internal List<Keep> GetAllKeeps(string id)
    {
      string sql =
      @"
      SELECT * FROM keeps WHERE creatorId = @id
      ";
      return _db.Query<Keep>(sql, new { id }).ToList();
    }

    internal List<Vault> GetAllVaults(string id)
    {
      string sql = "SELECT * FROM vaults WHERE creatorId = @id";
      return _db.Query<Vault>(sql, new { id }).ToList();
    }

    internal Vault GetVaultByProfile(string id)
    {
      string sql = @"
      SELECT * FROM vaults WHERE vaults.creatorId = @id;
      ";
      return _db.Query<Vault>(sql, new { id }).FirstOrDefault();
    }
  }
}