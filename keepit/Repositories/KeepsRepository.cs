using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepit.Models;

namespace keepit.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Keep Create(Keep keepData)
    {
      string sql = @"
     INSERT INTO keeps
     (name, description, img, views,kept, creatorId)
     VALUES
     (@Name , @Description, @Img , @Views,@Kept, @CreatorId);
     SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, keepData);
      keepData.Id = id;
      return keepData;
    }

    internal Keep GetById(int id)
    {
      string sql = @"
      SELECT 
        k.*,
        a.* 
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.id = @id;
      ";
      return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
      {

        k.Creator = a;
        return k;
      }, new { id }).FirstOrDefault();
    }

    internal List<Keep> GetAll()
    {
      string sql = @"
    SELECT
        k.*,
        a.*
    FROM keeps k
    JOIN accounts a WHERE a.id = k.creatorId;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }).ToList();
    }

    internal void Edit(Keep og)
    {
      string sql = @"
      UPDATE keeps
      SET
       name = @Name,
       description = @Description,
       img= @Img,
       kept = @Kept,
       views = @Views
      WHERE id = @Id;";
      _db.Execute(sql, og);
    }

    internal string Remove(int id)
    {
      string sql = @"
        DELETE FROM keeps WHERE id = @id LIMIT 1;
      ";
      int rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected > 0)
      {
        return "Deleted Keep";
      }
      throw new Exception("could not delete");
    }
  }
}