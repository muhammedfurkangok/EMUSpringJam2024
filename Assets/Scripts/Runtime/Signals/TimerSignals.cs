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
        public UnityAction OnFiveMinutesPast = delegate {  };
    }
    
}