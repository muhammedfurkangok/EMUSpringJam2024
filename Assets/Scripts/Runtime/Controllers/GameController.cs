using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DatabaseController.InitializeDatabase();
        var leaderboard = DatabaseController.GetLeaderboard();
        DatabaseController.InsertLeaderboard(new LeaderboardScore().SetScore(210000));
        for (int i = 0; i < leaderboard.Count; i++)
        {
            Debug.Log(leaderboard[i].By);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
