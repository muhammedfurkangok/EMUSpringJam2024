// using Runtime.Interfaces;
//
// namespace Runtime.StateMachine
// {
//     public class AttackState : IState
//     {
//         public void OnEnterState(ZombieManager zombieManager)
//         {
//             zombieManager.zombieAgent.speed = zombieManager.attackSpeed;
//             zombieManager.zombieAgent.stoppingDistance = zombieManager.attackRange;
//             
//         }
//
//         public void OnUpdateState(ZombieManager zombieManager)
//         {
//             zombieManager.zombieAgent.SetDestination(zombieManager.target.position);
//             if(zombieManager.zombieAgent.remainingDistance > zombieManager.attackRange)
//             {
//                 zombieManager.stateMachine.ChangeState(zombieManager.chaseState);
//             }
//             
//         }
//         
//     }
// }