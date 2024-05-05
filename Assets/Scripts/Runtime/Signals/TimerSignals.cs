using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class TimerSignals : MonoBehaviour
    {
        #region Singleton

        public static TimerSignals Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion
        
        public UnityAction OnTimerReset = delegate {  };
        public UnityAction OnThirtySecondsPassed = delegate { };
        public UnityAction OnOneMinutePassed = delegate { };
        public UnityAction OnFiveMinutesPassed = delegate { };
        public UnityAction OnSixMinutesPassed = delegate { }; // 6 minutes to reset the timer

    }
    
}