using System;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        private float elapsedTime = 0f;

        private void Update()
        {
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            elapsedTime += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(elapsedTime);

            string timerString = string.Format("{0:00}:{1:00}", (int)time.TotalMinutes, time.Seconds);
            timerText.text = timerString;
        }
    }
}
public class ZombieLeveller : MonoBehaviour
{
    //Will level up zombies every 30 seconds
    private void Update()
    {
        if ((int)Time.time % 30 == 0)
        {

        }
    }
}
public class ZombieSpawner : MonoBehaviour
{
    //Will spawn zombies every 5 seconds
    private void Update()
    {
        if ((int)Time.time % 5 == 0)
        {
            SpawnZombie();
        }
    }

    private void SpawnZombie()
    {

    }
}