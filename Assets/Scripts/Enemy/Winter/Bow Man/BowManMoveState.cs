using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowManMoveState : BowManPatrolState
{

    public BowManMoveState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName, Enemy_BowMan _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName, _enemy)
    {
    }
    public override void Enter()
    {
        base.Enter();
        if(enemy.charaterStats.isForzerTime) return;
        enemy.CreateDestination(enemy.RandomPosition());
        enemy.navMeshAgent.stoppingDistance=0.3f;
    }
    public override void Exit()
    {
        base.Exit();

        enemy.ZeroVelocity();
        enemy.navMeshAgent.stoppingDistance=0;
        enemy.DestroyDestination();

    }
    public override void Update()
    {
        base.Update();
        if(enemy.charaterStats.isForzerTime) return;
        enemy.FlipController(enemy.destination.position.x-enemy.transform.position.x);
        enemy.MoveToDestination(enemy.destination.position);
        
        if(Vector2.Distance(enemy.attackCheck.position,enemy.destination.position)<0.3f){
            stateMachine.ChangeState(enemy.idleState);
            
        }
        if(enemy.IsPlayerDetected()){
            stateMachine.ChangeState(enemy.chasingState);
        }

    }

    // public Vector2 RandomPosition(){
        
    //     while(true){
    //     float angle = Random.Range(0, Mathf.PI * 2);
    //     float distance = Random.Range(3, 6);

    //     float x = enemy.transform.position.x + Mathf.Cos(angle) * distance; 
    //     float y = enemy.transform.position.y + Mathf.Sin(angle) * distance;
    //     Vector2 vectorToMove=new Vector2(x,y);
    //         RaycastHit2D hit = 
    //         Physics2D.Raycast(enemy.attackCheck.position,vectorToMove - (Vector2)enemy.attackCheck.position,distance,LayerMask.GetMask("Obstacle"));
    //         Collider2D obCollider = 
    //         Physics2D.OverlapCircle(vectorToMove, 0.6f, LayerMask.GetMask("Obstacle"));
    //         // if(hit==false&&obCollider==false){
    //         //     return vectorToMove;
    //         // }
    //         if(hit==false&&obCollider==null){
    //             return vectorToMove;
    //         }

    //     }
    // }
    
    // public Vector2 RandomPosition() {
    //     int count = 0;
    //     while(count < 100) {
    //         count++;
    //         float angle = Random.Range(0, Mathf.PI * 2);
    //         float distance = Random.Range(3, 6);
            
    //         float x = enemy.transform.position.x + Mathf.Cos(angle) * distance; 
    //         float y = enemy.transform.position.y + Mathf.Sin(angle) * distance;
    //         Vector2 vectorToMove = new Vector2(x, y);
            
    //         // Visualize the ray
    //         Debug.DrawRay(enemy.attackCheck.position, vectorToMove - (Vector2)enemy.attackCheck.position, Color.red, 1.0f);
            
    //         // Kiểm tra NavMesh
    //         UnityEngine.AI.NavMeshHit navHit;
    //         bool onNavMesh = UnityEngine.AI.NavMesh.SamplePosition(vectorToMove, out navHit, 1.0f, UnityEngine.AI.NavMesh.AllAreas);
            
    //         if (!onNavMesh) {
    //             Debug.Log("Position not on NavMesh");
    //             continue;
    //         }
            
    //         RaycastHit2D hit = Physics2D.Raycast(enemy.attackCheck.position, vectorToMove - (Vector2)enemy.attackCheck.position, distance, LayerMask.GetMask("Obstacle"));
    //         Collider2D obCollider = Physics2D.OverlapCircle(vectorToMove, 0.3f, LayerMask.GetMask("Obstacle"));
            
    //         if (hit.collider != null) {
    //             Debug.Log($"Hit obstacle: {hit.collider.gameObject.name}");
    //             continue;
    //         }
            
    //         if (obCollider != null) {
    //             Debug.Log($"Overlap with: {obCollider.gameObject.name}");
    //             continue;
    //         }
            
    //         return navHit.position; // Return position on NavMesh
    //     }
        
    //     Debug.LogWarning("Couldn't find valid position after 100 attempts");
    //     return enemy.transform.position;
    // }
}
