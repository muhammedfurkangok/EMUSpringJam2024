using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Managers
{
    public class SpittingZombiePool : MonoBehaviour
    {
        public static SpittingZombiePool Instance { get; private set; }
        private int ZOMBIE_POOL_SIZE = 100;
        private Queue<GameObject> spittingZombieQueue = new Queue<GameObject>();

        public int CurrentZombieCount => spittingZombieQueue.Count;

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
        private void Start()
        {
            CreateZombiePool();
        }
        #region Object Pooling Methods

        public void CreateZombiePool()
        {
            for (int i = 0; i < ZOMBIE_POOL_SIZE; i++)
            {
                GameObject zombie = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
                zombie.SetActive(false);
                spittingZombieQueue.Enqueue(zombie);
            }
        }

        public GameObject GetZombieFromPool()
        {
            if (spittingZombieQueue.Count > 0)
            {
                GameObject zombie = spittingZombieQueue.Dequeue();
                zombie.SetActive(true);
                return zombie;
            }

            return null;
        }

        public void ReturnZombieToPool(GameObject zombie)
        {
            zombie.SetActive(false);
            spittingZombieQueue.Enqueue(zombie);
        }

        #endregion

    }
}

