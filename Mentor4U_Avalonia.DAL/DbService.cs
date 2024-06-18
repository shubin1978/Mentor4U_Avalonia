﻿using Dapper;
using Logger;
using Logger.File;
using Mentor4U_Avalonia.Models;
using Npgsql;

namespace Mentor4U_Avalonia.DAL;

public static class DbService<TEntity> where TEntity : IModel
{
   private static readonly ILogger _logger = new LogToFile();
   private static NpgsqlConnection? _connection;

   public static async Task ConnectAsync(string connectionString)
   {
      if (string.IsNullOrEmpty(connectionString)) 
         throw new EmptyStringException(nameof(connectionString));
      
      Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
      //Эта строчка позволит сопоставлять имена с нижним подчеркиванием
      //с названиями свойств класса без подчеркивания
      _connection = new NpgsqlConnection(connectionString);
      await _connection.OpenAsync();

      /*ExceptionsExtensions.LoggingIfException(
         logger:_logger,
         action: async () =>
            {
              
            }, 
         moduleName: nameof(DbService<TEntity>),
         methodName: nameof(ConnectAsync));
      return Task.CompletedTask;*/
   }


   public static async Task<IEnumerable<TEntity>> GetAllAsync()
   {
      try
      {
         var sql = $"SELECT * FROM {DbHelper.TableNames[typeof(TEntity)]}";
         return await _connection?.QueryAsync<TEntity>(sql);
      }
      catch (Exception e)
      {
         _logger?.Error($"Module:{nameof(DbService<TEntity>)}. Method:{nameof(GetAllAsync)}. Message:{e.Message}");
         throw;
      }
   }

   public static async Task<TEntity?> GetByIdAsync(int id)
   {
      try
      {
         var sqlRaw = $"SELECT * FROM {DbHelper.TableNames[typeof(TEntity)]} WHERE id = @Id";
         var sqlParameters = new { Id = id };
         return await _connection?.QuerySingleOrDefaultAsync<TEntity>(sqlRaw, sqlParameters);
      }
      catch (Exception e)
      {
         _logger?.Error($"Module:{nameof(DbService<TEntity>)}. Method:{nameof(GetByIdAsync)}. Message:{e.Message}");
         throw;
      }
   }
   
 
   public static async Task DisconnectAsync()
   {
      if (_connection == null) return;
      
      await _connection.CloseAsync();
      await _connection.DisposeAsync();
      
      /*ExceptionsExtensions.LoggingIfException(
         logger: _logger,
         action: async () =>
         {
            
         },
         moduleName: nameof(DbService<TEntity>),
         methodName: nameof(DisconnectAsync));*/
      
   }
}