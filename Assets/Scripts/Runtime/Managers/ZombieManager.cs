using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent zombieAgent;
    [SerializeField] private float chaseSpeed = 5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 0.5f;

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
}