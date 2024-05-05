//using System;
//using System.Collections.Generic;
//using System.Data;
//using JetBrains.Annotations;
//using Mono.Data.Sqlite;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.SocialPlatforms.Impl;

//public static class DatabaseController
//{
//    private static readonly string Path = Application.persistentDataPath + "/" + "userData.db";
//    private static readonly string Connection = "URI=file:" + Path;
//    private static readonly IDbConnection DbConnection = new SqliteConnection(Connection);
    
//    public static void InitializeDatabase()
//    {
//        DbConnection.Open();
//        IDbCommand dbCommand;
//        dbCommand = DbConnection.CreateCommand();
//        dbCommand.CommandText =
//            "CREATE TABLE IF NOT EXISTS scoreboard(" +
//            "    id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
//            "    timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL ," +
//            "    score BIGINT," +
//            "    `by` VARCHAR," +
//            "    is_user BOOLEAN default true NOT NULL" +
//            ");";
//        dbCommand.ExecuteNonQuery();
//        var vals = new LeaderboardScore[]
//        {
//            // Below are fixed scores for the user
//            new LeaderboardScore().SetScore(100000).SetIsUser(false).SetId(1).SetBy("Alex"),
//            new LeaderboardScore().SetScore(90000).SetIsUser(false).SetId(2).SetBy("Emily"),
//            new LeaderboardScore().SetScore(80000).SetIsUser(false).SetId(3).SetBy("John"),
//            new LeaderboardScore().SetScore(70000).SetIsUser(false).SetId(4).SetBy("Jane"),
//            new LeaderboardScore().SetScore(60000).SetIsUser(false).SetId(5).SetBy("Doe"),
//            new LeaderboardScore().SetScore(50000).SetIsUser(false).SetId(6).SetBy("Smith"),
//            new LeaderboardScore().SetScore(40000).SetIsUser(false).SetId(7).SetBy("Johnson"),
//            new LeaderboardScore().SetScore(30000).SetIsUser(false).SetId(8).SetBy("Doe"),
//            new LeaderboardScore().SetScore(20000).SetIsUser(false).SetId(9).SetBy("Smith"),
//            new LeaderboardScore().SetScore(10000).SetIsUser(false).SetId(10).SetBy("Johnson"),
//        };
         
//        foreach(var val in vals)
//        {
//            InsertLeaderboard(val);
//        }
//    }

//    public static void InsertLeaderboard(LeaderboardScore leaderboardScore)
//    {
//        var command = DbConnection.CreateCommand();
//        // Insert with id if it has one and insert with timestamp if it has one
//        command.CommandText = $"INSERT OR REPLACE INTO" +
//                              $" scoreboard" +
//                              $" (id, score, timestamp, `by`, is_user)" +
//                              $" VALUES " +
//                              $"($id, $score, $timestamp, $by, $isUser);";
//        command.Parameters.Add(
//            new SqliteParameter
//            {
//                ParameterName = "$id",
//                Value = leaderboardScore.Id
//            });
//        command.Parameters.Add(
//            new SqliteParameter
//            {
//                ParameterName = "$score",
//                Value = leaderboardScore.Score
//            });
//        command.Parameters.Add(
//            new SqliteParameter
//            {
//                ParameterName = "$timestamp",
//                Value = leaderboardScore.Timestamp
//            });
//        command.Parameters.Add(
//            new SqliteParameter
//            {
//                ParameterName = "$by",
//                Value = leaderboardScore.By
//            });
//        command.Parameters.Add(
//            new SqliteParameter
//            {
//                ParameterName = "$isUser",
//                Value = leaderboardScore.IsUser
//            }
//        );
//        command.ExecuteNonQuery();
//    }
//    public static List<LeaderboardScore> GetLeaderboard()
//    {
//        var command = DbConnection.CreateCommand();
//        command.CommandText = "SELECT score, timestamp, `by`, is_user, id   FROM scoreboard;";
//        var reader = command.ExecuteReader();
//        var leaderboardScores = new List<LeaderboardScore>();
//        while (reader.Read())
//            leaderboardScores.Add(
//                new LeaderboardScore()
//                    .SetScore(int.Parse(reader[0].ToString()))
//                    .SetTimestamp(DateTime.Parse(reader[1].ToString()))
//                    .SetBy(reader[2].ToString())
//                    .SetIsUser(bool.Parse(reader[3].ToString()))
//                    .SetId(int.Parse(reader[4].ToString()))
//            );
//        return leaderboardScores;
//    }

//    public static int GetHighScore()
//    {
//        var command = DbConnection.CreateCommand();
//        command.CommandText = "SELECT score FROM scoreboard WHERE is_user = true ORDER BY score desc LIMIT 1";
//        var reader = command.ExecuteReader();
//        reader.Read();
//        if (reader.FieldCount == 0) return 0;
//        return (int)reader[0];
//    }
//}

//public class LeaderboardScore
//{
//    public DateTime Timestamp
//    {
//        set
//        {
//            // Check if valid datetime
//            try
//            {
//                var dt = value;
//                this._timestamp = dt;
//            }
//            catch (Exception e)
//            {
//                Debug.LogError(e);
//                this._timestamp = null;
//            }
//        }
//        get
//        {
//            // Check if not null
//            if (this._timestamp != null)
//            {
//                return (DateTime) this._timestamp;
//            }
//            else
//            {
//                return DateTime.Now;
//            }
//        }
//    }

//    public int? Id
//    {
//        get
//        {
//            return this._id;
//        }
//        set
//        {
//            this._id = value;
//        }
        
//    }

//    public string By
//    {
//        get
//        {
//            if (this._by == null)
//            {
//                return "You";
//            }
//            else
//            {
//                return this._by;
//            }
//        }
//        set
//        {
//            this._by = value;
//        }
//    }
    
//    public bool IsUser
//    {
//        get
//        {
//            return this._isUser;
//        }
//        set
//        {
//            this._isUser = value;
//        }
//    }

//    public int Score
//    {
//        get 
//        {
//            return this._score;
//        }
//        set
//        {
//            this._score = value;
//        }
//    }
//    // Setter functions for ease of use. 
//    public LeaderboardScore SetScore(int score)
//    {
//        this.Score = score;
//        return this;
//    }
//    public LeaderboardScore SetTimestamp(DateTime timestamp)
//    {
//        this.Timestamp = timestamp;
//        return this;
//    }
//    public LeaderboardScore SetBy(string by)
//    {
//        this.By = by;
//        return this;
//    }
//    public LeaderboardScore SetIsUser(bool isUser)
//    {
//        this.IsUser = isUser;
//        return this;
//    }
//    public LeaderboardScore SetId(int id)
//    {
//        this.Id = id;
//        return this;
//    }
    
//    // nullable datetime parameter for timestamp
//    private DateTime? _timestamp;
//    private int? _id;
//    private string _by;
//    private int _score;
//    private bool _isUser = true;
//}
