using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Managers
{
    public class SpittingZombieManager : MonoBehaviour, IZombie, IDamageable
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform target;
        [SerializeField] private NavMeshAgent zombieAgent;
        [SerializeField] private float chaseSpeed = 5f;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private float attackCooldown = 0.5f;

        #endregion

        #region Private Variables

        private int currentHealth = 100;
        private int maxHealth = 100;
        private int ZOMBIE_POOL_SIZE = 100;
        private Queue<GameObject> spittingZombieQueue = new Queue<GameObject>();

        #endregion

        #endregion



        private bool isAttacking = false;

        private void Start()
        {
            CreateZombiePool();
        }

        private void Update()
        {
            if (target != null && !isAttacking)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, target.position);
                if (distanceToPlayer > attackRange)
                {
                    ChasePlayer();
                }
                else
                {
                    AttackPlayer();
                }
            }
        }

        public void AttackPlayer()
        {
            transform.LookAt(target);
            StartCoroutine(Attack());
        }

        public void ChasePlayer()
        {
            Debug.Log("Chasing Player");
            if (zombieAgent != null)
            {
                zombieAgent.SetDestination(target.position);
                zombieAgent.speed = chaseSpeed;
            }
        }

        private IEnumerator Attack()
        {
            print("Attacking");
            isAttacking = true;
            yield return new WaitForSeconds(attackCooldown);
            //Animasyon oynatılabilir
            if (Vector3.Distance(transform.position, target.position) <= attackRange)
            {

                var playerManager = target.GetComponent<PlayerManager>();
                if (playerManager != null)
                {
                    playerManager.TakeDamage(attackDamage);
                }
            }

            isAttacking = false;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            ReturnZombieToPool(gameObject);
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

        public void LevelUpZombies(uint levelMultiplier)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
