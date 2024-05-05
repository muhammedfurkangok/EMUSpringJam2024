using Cysharp.Threading.Tasks;
using Runtime.Interfaces;
using Runtime.Signals;
using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Managers
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        private float elapsedTime = 0f;
        private float totalTime = 0f;

        private void Start()
        {
            TimerSignals.Instance.OnSixMinutesPassed += ResetTimer;
        }

        private void Update()
        {
            UpdateTimer();
        }

        private async void UpdateTimer()
        {
            elapsedTime += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(elapsedTime);

            string timerString = string.Format("{0:00}:{1:00}", (int)time.TotalMinutes, time.Seconds);
            timerText.text = timerString;
            await UniTask.WaitForSeconds(1f);
            FireEvents();
        }
        private void ResetTimer()
        {
            totalTime += elapsedTime;
            elapsedTime = 0f;
            TimerSignals.Instance.OnTimerReset();
        }

        private float lastEventTime = 0f;
        private float eventInterval = 30f; // Interval in seconds for the events
        private void FireEvents()
        {
            float currentTime = Time.time;
            if (currentTime - lastEventTime >= eventInterval)
            {
                lastEventTime = currentTime;

                TimeSpan time = TimeSpan.FromSeconds(elapsedTime);
                if (time.Seconds % 30 == 0 && time.Seconds % 60 != 0 && time.Seconds % 300 != 0)
                {
                    TimerSignals.Instance.OnThirtySecondsPassed();
                }
                if (time.Seconds % 60 == 0 && time.Seconds % 300 != 0)
                {
                    TimerSignals.Instance.OnOneMinutePassed();
                    Debug.Log("60 seconds passed");
                }
                if (time.Seconds % 299 == 0)
                {
                    TimerSignals.Instance.OnFiveMinutesPassed();
                }
                if (time.Seconds % 359 == 0)
                {
                    TimerSignals.Instance.OnSixMinutesPassed();
                }
            }
        }

        private void OnDestroy()
        {
            TimerSignals.Instance.OnSixMinutesPassed -= ResetTimer;
        }
    }
}
