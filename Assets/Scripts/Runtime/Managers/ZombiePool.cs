using System.Collections.Generic;
using UnityEngine;

public class ZombiePool : MonoBehaviour
{
    public static ZombiePool Instance { get; private set; }
    [SerializeField] private GameObject _zombie;
    private int ZOMBIE_POOL_SIZE = 100;
    private Queue<GameObject> zombieQueue = new Queue<GameObject>();

    public int CurrentZombieCount => zombieQueue.Count;

    private void Awake()
    {
        Instance = this;
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
            GameObject zombie = Instantiate(_zombie, Vector3.zero, Quaternion.identity);
            zombie.SetActive(false);
            zombieQueue.Enqueue(zombie);
        }
    }

    public GameObject GetZombieFromPool()
    {
        if (zombieQueue.Count > 0)
        {
            GameObject zombie = zombieQueue.Dequeue();
            zombie.SetActive(true);
            return zombie;
        }
        return null;
    }

    public void ReturnZombieToPool(GameObject zombie)
    {
        zombie.SetActive(false);
        zombieQueue.Enqueue(zombie);
    }
    #endregion

}