using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
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

    #endregion
 
    #endregion


    private bool isAttacking = false;

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

    private void AttackPlayer()
    {
        transform.LookAt(target);
        StartCoroutine(Attack());
    }

    private void ChasePlayer()
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
        //animation
        Destroy(gameObject,2f);
    }
}