using Cysharp.Threading.Tasks;
using Runtime.Managers;
using Runtime.Signals;
using System;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        private float elapsedTime = 0f;
        private float totalTime = 0f;

        private void Awake()
        {
            TimerSignals.Instance.OnSixMinutesPassed += ResetTimer;
        }

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
            FireEvents();
        }
        private void ResetTimer()
        {
            totalTime += elapsedTime;
            elapsedTime = 0f;
            TimerSignals.Instance.OnTimerReset();
        }
        private void FireEvents()
        {
            if ((int)elapsedTime % 30 == 0 && elapsedTime % 60 != 0 && elapsedTime % 300 != 0)
            {
                TimerSignals.Instance.OnThirtySecondsPassed();
            }
            if ((int)elapsedTime % 60 == 0 && elapsedTime % 300 != 0)
            {
                TimerSignals.Instance.OnOneMinutePassed();
            }
            if ((int)elapsedTime % 299 == 0)
            {
                TimerSignals.Instance.OnFiveMinutesPassed();
            }
            if ((int)elapsedTime % 359 == 0)
            {
                TimerSignals.Instance.OnSixMinutesPassed();
            }
        }
        private void OnDestroy()
        {
            TimerSignals.Instance.OnSixMinutesPassed -= ResetTimer;
        }
    }
}

public class ZombieSpawner : MonoBehaviour
{

    public bool canSpawnZombies = false;
    public bool canSpawnSpitters = false;
    public bool canSpawnLasers = false;

    private void Start()
    {
        SpawnZombies();
        SpawnSpitters();
        SpawnLasers();
        
    }
    private async void SpawnZombies()
    {
        while (canSpawnZombies)
        {
            GameObject zombie = ZombiePool.Instance.GetZombieFromPool();
            if (zombie != null)
            {
                zombie.transform.position = transform.position;
                zombie.transform.rotation = transform.rotation;
                zombie.SetActive(true);
            }
            await UniTask.WaitForSeconds(1.5f);
            if(ZombiePool.Instance.CurrentZombieCount >= 99)
            {
                canSpawnZombies = false;
                break;
            }
        }
    }
    private async void SpawnSpitters()
    {
        while (canSpawnSpitters)
        {
            GameObject zombie = SpittingZombiePool.Instance.GetZombieFromPool();
            if (zombie != null)
            {
                zombie.transform.position = transform.position;
                zombie.transform.rotation = transform.rotation;
                zombie.SetActive(true);
            }
            await UniTask.WaitForSeconds(1.5f);
            if (SpittingZombiePool.Instance.CurrentZombieCount >= 25)
            {
                canSpawnZombies = false;
                break;
            }
        }
    }
    private async void SpawnLasers()
    {
        while (canSpawnLasers)
        {
            GameObject laser = LaserZombiePool.Instance.GetZombieFromPool();
            if (laser != null)
            {
                laser.transform.position = transform.position;
                laser.transform.rotation = transform.rotation;
                laser.SetActive(true);
            }
            await UniTask.WaitForSeconds(1.5f);
            if (LaserZombiePool.Instance.CurrentZombieCount >= 10)
            {
                canSpawnZombies = false;
                break;
            }
        }
    }
}