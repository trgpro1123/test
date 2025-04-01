using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTreeMoveState : MonsterTreePatroldState
{
    // Transform destination;

    public MonsterTreeMoveState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName, Enemy_MonsterTree _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName, _enemy)
    {
    }
    public override void Enter()
    {
        base.Enter();
        if(enemy.charaterStats.isForzerTime) return;
        // enemy.navMeshAgent.isStopped=false;
        enemy.CreateDestination(enemy.RandomPosition());
        enemy.navMeshAgent.stoppingDistance=0.3f;
        // stateTimer=enemy.enemyAIPathfinding.aiUpdateDelay;
        // Vector2 positionToMove;
        //     while(true){
        //     positionToMove=new Vector2(Random.Range(-6,6),Random.Range(-6,6));
        //     //float randomLength=Random.Range(3,5);
        //     Vector2 vectorToMove=positionToMove-(Vector2)enemy.transform.position;
        //         RaycastHit2D hit = 
        //         Physics2D.Raycast(enemy.transform.position,vectorToMove,vectorToMove.magnitude,LayerMask.GetMask("Obstacle"));
        //         Collider2D obCollider = 
        //         Physics2D.OverlapCircle(enemy.transform.position, 0.2f, LayerMask.GetMask("Obstacle"));
                
        //         if(hit==false&&obCollider==false){
        //             enemy.enemyAIPathfinding.seekBehaviour.SetCustomTargetPosition(positionToMove);
        //             enemy.SetCurrentTarget(player.transform);
        //             break;
        //         }

        //     }
        


    }
    public override void Exit()
    {
        base.Exit();

        enemy.ZeroVelocity();
        enemy.navMeshAgent.stoppingDistance=0;
        // enemy.navMeshAgent.stoppingDistance=enemy.attackDistance;
        // enemy.navMeshAgent.updatePosition=false;
        // enemy.navMeshAgent.isStopped=true;
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
        // if(enemy.navMeshAgent.remainingDistance<=enemy.navMeshAgent.stoppingDistance){
        //     stateMachine.ChangeState(enemy.idleState);
        // }
        if(enemy.IsPlayerDetected()){
            stateMachine.ChangeState(enemy.chasingState);
        }
        // if(stateTimer<0){
            
        //     enemy.MoveToTarget();
        //     stateTimer=enemy.enemyAIPathfinding.aiUpdateDelay;
        // }
        // if (enemy.enemyAIPathfinding.aiData.currentTarget == null)
        // {  
        //     stateMachine.ChangeState(enemy.idleState);
        // }
        
        

    }

    // public Vector2 RandomPosition(){
    //     // Vector2 positionToMove;
    //         while(true){
    //         // positionToMove=new Vector2(Random.Range(-6,6),Random.Range(-6,6));
    //         //float randomLength=Random.Range(3,5);
    //         // Vector2 vectorToMove=positionToMove-(Vector2)enemy.transform.position;
    //         float angle = Random.Range(0, Mathf.PI * 2); // Góc ngẫu nhiên 
    //         float distance = Random.Range(3, 6);
    //         // float x=enemy.transform.position.x+Random.Range(-3,3);
    //         // float y=enemy.transform.position.y+Random.Range(-3,3);
    //         float x = enemy.attackCheck.position.x + Mathf.Cos(angle) * distance; 
    //         float y = enemy.attackCheck.position.y + Mathf.Sin(angle) * distance;
    //         Vector2 vectorToMove=new Vector2(x,y);
    //             RaycastHit2D hit = 
    //             Physics2D.Raycast(enemy.attackCheck.position,vectorToMove - (Vector2)enemy.attackCheck.position,distance,LayerMask.GetMask("Obstacle"));
    //             Collider2D obCollider = 
    //             Physics2D.OverlapCircle(vectorToMove, 0.4f, LayerMask.GetMask("Obstacle"));
                
    //             if(hit==false&&obCollider==false){
    //                 // enemy.enemyAIPathfinding.seekBehaviour.SetCustomTargetPosition(positionToMove);
    //                 // enemy.SetCurrentTarget(player.transform);
    //                 // break;



    //                 // destination.position=vectorToMove;
    //                 return vectorToMove;
    //             }

    //         }
    // }
    
}
