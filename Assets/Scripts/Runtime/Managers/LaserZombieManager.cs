using System;
using System.Collections;
using Ozgur.Scripts;
using Runtime.Interfaces;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Managers
{
    public class LaserZombieManager : MonoBehaviour, IZombie, IDamageable
    {
        private Transform target;
        [SerializeField] private NavMeshAgent zombieAgent;
        [SerializeField] private float chaseSpeed = 5f;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private float attackCooldown = 0.5f;

        [Header("Health Info")]
        [SerializeField]private int currentHealth = 35;
        [SerializeField]private int maxHealth = 35;
        [SerializeField]private uint level = 1;
      
        private bool isAttacking = false;

        private void OnEnable()
        {
            TimerSignals.Instance.OnThirtySecondsPassed += () => LevelUpZombie(1);  
        }
        public void LevelUpZombie(uint levelMultiplier)
        {
            level += levelMultiplier;
        }

        private void Start()
        {
            target = Player.Instance.transform;
            maxHealth += 10 * (int)level;
            chaseSpeed += .5f * level;
            attackDamage += 3 * (int)level;
            currentHealth = maxHealth;
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
                    playerManager.TakeDamage(attackDamage, transform.forward);
                }
            }

            isAttacking = false;
        }

        public void TakeDamage(int damage, Vector3 hitDirection)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
            Debug.Log("Laser Zombie Health: " + currentHealth);
        }

        private void Die()
        {
           LaserZombiePool.Instance.ReturnZombieToPool(gameObject);
        }

        private void OnDestroy()
        {
            TimerSignals.Instance.OnThirtySecondsPassed -= () => LevelUpZombie(1);
        }

    }
}
