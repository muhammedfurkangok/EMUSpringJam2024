using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;


public static class DatabaseController
{
    private static readonly string Path = Application.persistentDataPath + "/" + "userData.db";
    // private static readonly string Connection = "URI=file:" + Path;
    private static readonly SQLiteConnection DbConnection = new SQLiteConnection(Path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    
    public static void InitializeDatabase()
    {
        
        string command = "CREATE TABLE IF NOT EXISTS scoreboard(" +
            "   id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
            "    timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL ," +
            "    score BIGINT," +
            "    `by` VARCHAR," +
            "    is_user BOOLEAN default true NOT NULL" +
            ");";
        var dbCommand = DbConnection.CreateCommand(command);
        dbCommand.ExecuteNonQuery();
        var vals = new[]
        {
            new LeaderboardScore { Score = 100000, IsUser = false, Id = 1, By = "Alex" },
            new LeaderboardScore { Score = 90000, IsUser = false, Id = 2, By = "Emily" },
            new LeaderboardScore { Score = 80000, IsUser = false, Id = 3, By = "John" },
            new LeaderboardScore { Score = 70000, IsUser = false, Id = 4, By = "Jane" },
            new LeaderboardScore { Score = 60000, IsUser = false, Id = 5, By = "Doe" },
            new LeaderboardScore { Score = 50000, IsUser = false, Id = 6, By = "Smith" },
            new LeaderboardScore { Score = 40000, IsUser = false, Id = 7, By = "Johnson" },
            new LeaderboardScore { Score = 30000, IsUser = false, Id = 8, By = "Doe" },
            new LeaderboardScore { Score = 20000, IsUser = false, Id = 9, By = "Smith" },
            new LeaderboardScore { Score = 10000, IsUser = false, Id = 10, By = "Johnson" },
        };

         
        foreach(var val in vals)
        {
            InsertLeaderboard(val);
        }
    }

    public static void InsertLeaderboard(LeaderboardScore leaderboardScore)
    {
        var commandText = $"INSERT OR REPLACE INTO" +
                          $" scoreboard" +
                          $" (id, score, timestamp, `by`, is_user)" +
                          $" VALUES " +
                          $"(?, ?, ?, ?, ?);";
        var command = DbConnection.CreateCommand(
            commandText, 
            new object[] {leaderboardScore.Id, leaderboardScore.Score, leaderboardScore.Timestamp, leaderboardScore.By, leaderboardScore.IsUser});

        // Insert with id if it has one and insert with timestamp if it has one
        command.ExecuteNonQuery();
    }
    public static List<LeaderboardScore> GetLeaderboard()
    {
        var commandText = "SELECT score, timestamp, `by`, is_user, id FROM scoreboard;";
        var command = DbConnection.CreateCommand(commandText);
        // Create a query based on this table.
        // CREATE TABLE IF NOT EXISTS scoreboard(
        //     id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
        //     timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL 
        //     score BIGINT
        //     `by` VARCHAR
        //     is_user BOOLEAN default true NOT NUL
        // );
        var scoreboardTables = command.ExecuteQuery<ScoreboardTable>();
        
        var leaderboardScores = new List<LeaderboardScore>();
        
        foreach (var row in scoreboardTables)
        {
            Debug.Log(row);
            leaderboardScores.Add(
                new LeaderboardScore()
                    .SetScore(row.score)
                    .SetTimestamp(row.timestamp)
                    .SetBy(row.by)
                    .SetIsUser(row.is_user)
                    .SetId(row.id)
            );
        }
        return leaderboardScores;
    }

    public static int GetHighScore()
    {
        var commandText = "SELECT score FROM scoreboard WHERE is_user = true ORDER BY score desc LIMIT 1";
        var command = DbConnection.CreateCommand(commandText);
        var qRes = command.ExecuteQuery<ScoreboardTable>();
        if (qRes.Count == 0) return 0;
        return qRes[0].score;
    }
}

// Scoreboard table class to use with queries
// public class ScoreboardTable
// {
//     public int score;
//     public DateTime timestamp;
//     public string by;
//     public bool is_user;
//     public int id;
// }

public class ScoreboardTable
{
    public int id { get; set; }
    public DateTime timestamp { get; set; }
    public int score { get; set; }
    public string by { get; set; }
    public bool is_user { get; set; }
}

public class LeaderboardScore
{
    public DateTime Timestamp
    {
        set
        {
            // Check if valid datetime
            try
            {
                var dt = value;
                this._timestamp = dt;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                this._timestamp = null;
            }
        }
        get
        {
            // Check if not null
            if (this._timestamp != null)
            {
                return (DateTime) this._timestamp;
            }
            else
            {
                return DateTime.Now;
            }
        }
    }

    public int? Id
    {
        get
        {
            return this._id;
        }
        set
        {
            this._id = value;
        }
        
    }

    public string By
    {
        get
        {
            if (this._by == null)
            {
                return "You";
            }
            else
            {
                return this._by;
            }
        }
        set
        {
            this._by = value;
        }
    }
    
    public bool IsUser
    {
        get
        {
            return this._isUser;
        }
        set
        {
            this._isUser = value;
        }
    }

    public int Score
    {
        get 
        {
            return this._score;
        }
        set
        {
            this._score = value;
        }
    }
    // Setter functions for ease of use. 
    public LeaderboardScore SetScore(int score)
    {
        this.Score = score;
        return this;
    }
    public LeaderboardScore SetTimestamp(DateTime timestamp)
    {
        this.Timestamp = timestamp;
        return this;
    }
    public LeaderboardScore SetBy(string by)
    {
        this.By = by;
        return this;
    }
    public LeaderboardScore SetIsUser(bool isUser)
    {
        this.IsUser = isUser;
        return this;
    }
    public LeaderboardScore SetId(int id)
    {
        this.Id = id;
        return this;
    }
    
    // nullable datetime parameter for timestamp
    private DateTime? _timestamp;
    private int? _id;
    private string _by;
    private int _score;
    private bool _isUser = true;
}
