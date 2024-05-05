using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Ozgur.Scripts;
using Runtime.Interfaces;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Managers
{
    public class SpittingZombieManager : MonoBehaviour, IZombie, IDamageable
    {
 
        private Transform target;
        [SerializeField] private NavMeshAgent zombieAgent;
        [SerializeField] private float chaseSpeed = 5f;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private float attackCooldown = 0.5f;



        [Header("Health Info")]
        [SerializeField] private int currentHealth = 35;
        [SerializeField] private int maxHealth = 35;
        [SerializeField] private uint level = 1;


        private bool isAttacking = false;
        private void OnDestroy()
        {
            TimerSignals.Instance.OnThirtySecondsPassed -= () => LevelUpZombie(1);
        }
        private void OnEnable()
        {
            TimerSignals.Instance.OnThirtySecondsPassed += () => LevelUpZombie(1);
        }
        private void Start()
        {
            target = Player.Instance.transform;

            maxHealth += 10 * (int)level;
            chaseSpeed += .5f * level;
            attackDamage += 3 * (int)level;
            currentHealth = maxHealth;
        }
        public void LevelUpZombie(uint levelMultiplier)
        { 
            level += levelMultiplier;
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
            Attack();
        }

        public void ChasePlayer()
        {
            if (zombieAgent != null)
            {
                zombieAgent.SetDestination(target.position);
                zombieAgent.speed = chaseSpeed;
            }
        }

        private async void Attack()
        {
            print("Attacking");
            isAttacking = true;
            await UniTask.WaitForSeconds(attackCooldown);
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
           SpittingZombiePool.Instance.ReturnZombieToPool(gameObject);
        }      
    }
}

