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


        private int currentHealth = 100;
        private int maxHealth = 100;
       
      
        private bool isAttacking = false;

        private void Awake()
        {
            TimerSignals.Instance.OnThirtySecondsPassed += () => LevelUpZombie(1);
        }
        public void LevelUpZombie(uint levelMultiplier)
        {
            maxHealth += 10 * (int)levelMultiplier;
            chaseSpeed += .5f * levelMultiplier;
            attackDamage += 3 * (int)levelMultiplier;
            attackCooldown -= 0.01f * (int)levelMultiplier;
            currentHealth = maxHealth;
            Debug.Log("Zombie Leveled up!!");
        }

        private void Start()
        {
            target = Player.Instance.transform;
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
