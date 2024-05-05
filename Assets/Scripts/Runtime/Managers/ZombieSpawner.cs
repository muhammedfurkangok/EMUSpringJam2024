using Cysharp.Threading.Tasks;
using Runtime.Managers;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public bool canSpawnZombies = true;
    public bool canSpawnSpitters = true;
    public bool canSpawnLasers = true;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

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
                zombie.transform.SetPositionAndRotation(player.transform.position + 
                    (new Vector3(UnityEngine.Random.insideUnitCircle.x, 0, UnityEngine.Random.insideUnitCircle.y) * 10f), transform.rotation);
                zombie.SetActive(true);
            }
            await UniTask.WaitForSeconds(1.5f);
            if(ZombiePool.Instance.CurrentZombieCount >= 99)
            {
                canSpawnZombies = false;
            }
        }
    }
    private async void SpawnSpitters()
    {
        while (canSpawnSpitters)
        {
            GameObject spitter = SpittingZombiePool.Instance.GetZombieFromPool();
            if (spitter != null)
            {
                spitter.transform.SetPositionAndRotation(player.transform.position +
                    (new Vector3(UnityEngine.Random.insideUnitCircle.x, 0, UnityEngine.Random.insideUnitCircle.y) * 10f), transform.rotation);
                spitter.SetActive(true);
            }
            await UniTask.WaitForSeconds(1.5f);
            if (SpittingZombiePool.Instance.CurrentZombieCount >= 25)
            {
                //canSpawnZombies = false;
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
                laser.transform.SetPositionAndRotation(player.transform.position +
                    (new Vector3(UnityEngine.Random.insideUnitCircle.x, 0, UnityEngine.Random.insideUnitCircle.y) * 10f), transform.rotation);
                laser.SetActive(true);
            }
            await UniTask.WaitForSeconds(1.5f);
            if (LaserZombiePool.Instance.CurrentZombieCount >= 10)
            {
                canSpawnZombies = false;
            }
        }
    }
}