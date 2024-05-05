using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Managers
{
    public class LaserZombiePool : MonoBehaviour
    {
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
        public static LaserZombiePool Instance { get; private set; }
        private int ZOMBIE_POOL_SIZE = 100;
        private Queue<GameObject> laserZombieQueue = new Queue<GameObject>();
        [SerializeField] private GameObject _laserZombie;


        public int CurrentZombieCount => laserZombieQueue.Count;

        private void Start()
        {
            CreateZombiePool();
        }


        #region Object Pooling Methods

        public void CreateZombiePool()
        {
            for (int i = 0; i < ZOMBIE_POOL_SIZE; i++)
            {
                GameObject zombie = Instantiate(_laserZombie, Vector3.zero, Quaternion.identity);
                zombie.SetActive(false);
                laserZombieQueue.Enqueue(zombie);
            }
        }

        public GameObject GetZombieFromPool()
        {
            if (laserZombieQueue.Count > 0)
            {
                GameObject zombie = laserZombieQueue.Dequeue();
                zombie.SetActive(true);
                return zombie;
            }

            return null;
        }

        public void ReturnZombieToPool(GameObject zombie)
        {
            zombie.SetActive(false);
            laserZombieQueue.Enqueue(zombie);
        }



        #endregion

    }
}
