using Cysharp.Threading.Tasks;
using Runtime.Interfaces;
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
        private void FireEvents()
        {
            var result = elapsedTime % 30;
            Debug.Log(result);
            Debug.LogWarning((int)elapsedTime);
            if ((int)elapsedTime % 30 == 0 && elapsedTime % 60 != 0 && elapsedTime % 300 != 0)
            {
                TimerSignals.Instance.OnThirtySecondsPassed();
                Debug.Log("30 seconds passed");
            }
            if ((int)elapsedTime % 60 == 0 && elapsedTime % 300 != 0)
            {
                TimerSignals.Instance.OnOneMinutePassed();
                Debug.Log("60 seconds passed");
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
