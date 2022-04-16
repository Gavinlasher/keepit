using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepit.Models;

namespace keepit.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault Create(Vault vaultData)
    {
      string sql = @"
     INSERT INTO vaults
     (name, description, isPrivate,creatorId)
     VALUES
     (@Name , @Description, @IsPrivate ,@CreatorId);
     SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData.Id = id;
      return vaultData;
    }

    internal Vault GetById(int id)
    {
      string sql = @"
      SELECT 
        v.*,
        a.* 
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.id = @id;
      ";
      return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
      {

        v.Creator = a;
        return v;
      }, new { id }).FirstOrDefault();
    }

    internal void Edit(Vault og)
    {
      string sql = @"
      UPDATE vaults
      SET
       name = @Name,
       description = @Description,
    isPrivate = @IsPrivate
      WHERE id = @Id;";
      _db.Execute(sql, og);
    }

    internal List<Vault> GetAllMyVaults(string id)
    {
      string sql = "SELECT * FROM vaults WHERE vaults.creatorId = @id";
      return _db.Query<Vault>(sql, new { id }).ToList();
    }

    internal string Remove(int id)
    {
      string sql = @"
        DELETE FROM vaults WHERE id = @id LIMIT 1;
      ";
      int rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected > 0)
      {
        return "Deleted Vaults";
      }
      throw new Exception("could not delete");
    }
  }
}