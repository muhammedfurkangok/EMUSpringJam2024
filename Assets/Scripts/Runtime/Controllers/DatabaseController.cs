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
        var vals = new LeaderboardScore[]
        {
            // Below are fixed scores for the user
            new LeaderboardScore().SetScore(100000).SetIsUser(false).SetId(1).SetBy("Alex"),
            new LeaderboardScore().SetScore(90000).SetIsUser(false).SetId(2).SetBy("Emily"),
            new LeaderboardScore().SetScore(80000).SetIsUser(false).SetId(3).SetBy("John"),
            new LeaderboardScore().SetScore(70000).SetIsUser(false).SetId(4).SetBy("Jane"),
            new LeaderboardScore().SetScore(60000).SetIsUser(false).SetId(5).SetBy("Doe"),
            new LeaderboardScore().SetScore(50000).SetIsUser(false).SetId(6).SetBy("Smith"),
            new LeaderboardScore().SetScore(40000).SetIsUser(false).SetId(7).SetBy("Johnson"),
            new LeaderboardScore().SetScore(30000).SetIsUser(false).SetId(8).SetBy("Doe"),
            new LeaderboardScore().SetScore(20000).SetIsUser(false).SetId(9).SetBy("Smith"),
            new LeaderboardScore().SetScore(10000).SetIsUser(false).SetId(10).SetBy("Johnson"),
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
        var command = DbConnection.CreateCommand(commandText, new object[] {leaderboardScore.Id, leaderboardScore.Score, leaderboardScore.Timestamp, leaderboardScore.By, leaderboardScore.IsUser});

        // Insert with id if it has one and insert with timestamp if it has one
        command.ExecuteNonQuery();
    }
    public static List<LeaderboardScore> GetLeaderboard()
    {
        var commandText = "SELECT score, timestamp, `by`, is_user, id FROM scoreboard;";
        var command = DbConnection.CreateCommand(commandText);
        var scoreboardTables = command.ExecuteQuery<ScoreboardTable>();
        var leaderboardScores = new List<LeaderboardScore>();
        foreach (var row in scoreboardTables)
            leaderboardScores.Add(
                new LeaderboardScore()
                    .SetScore(row.Score)
                    .SetTimestamp(row.Timestamp)
                    .SetBy(row.By)
                    .SetIsUser(row.IsUser)
                    .SetId(row.Id)
            );
        return leaderboardScores;
    }

    public static int GetHighScore()
    {
        var commandText = "SELECT score FROM scoreboard WHERE is_user = true ORDER BY score desc LIMIT 1";
        var command = DbConnection.CreateCommand(commandText);
        var qRes = command.ExecuteQuery<ScoreboardTable>();
        if (qRes.Count == 0) return 0;
        return qRes[0].Score;
    }
}

public class ScoreboardTable
{
    public int Id { get; set; }
    public int Score { get; set; }
    public DateTime Timestamp { get; set; }
    public string By { get; set; }
    public bool IsUser { get; set; }
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
