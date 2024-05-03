// using Runtime.Interfaces;
// using UnityEngine;
// using UnityEngine.AI;
//
// namespace Runtime.StateMachine
// {
//     public class ChaseState : IState
//     {
//         public void OnEnterState(ZombieManager zombieManager)
//         {
//             
//             zombieManager.zombieAgent.speed = zombieManager.chaseSpeed;
//             zombieManager.zombieAgent.stoppingDistance = zombieManager.attackRange;
//         }
//
//         public void OnUpdateState(ZombieManager zombieManager)
//         {
//              zombieManager.zombieAgent.SetDestination(zombieManager.target.position);
//              if(zombieManager.zombieAgent.remainingDistance <= zombieManager.attackRange)
//              {
//                  zombieManager.stateMachine.ChangeState(zombieManager.attackState);
//              }
//         }
//         
//     }
// }