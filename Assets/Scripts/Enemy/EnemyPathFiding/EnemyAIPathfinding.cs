using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAIPathfinding : MonoBehaviour
{
    public List<SteeringBehaviour> steeringBehaviours;

    [SerializeField]
    private List<Detector> detectors;

    public AIData aiData {get;private set;}

    //public float detectionDelay = 0.05f, aiUpdateDelay = 0.06f;

    public float detectionDelay  = 0.05f;
    public float aiUpdateDelay  = 0.06f;

    // attackDelay = 1f;

    // [SerializeField]
    // private float attackDistance = 0.5f;

    //Inputs sent from the Enemy AI to the Enemy controller
    // public UnityEvent OnAttackPressed;
    // public UnityEvent<Vector2> OnMovementInput, OnPointerInput;

    // [SerializeField]
    // private Vector2 movementInput;

    public ContextSolver contextSolver{get;private set;}





    private Enemy enemy;
    public SeekBehaviour seekBehaviour;



    private void Start()
    {
        //Detecting Player and Obstacles around
        enemy=GetComponent<Enemy>();
        aiData=GetComponent<AIData>();
        contextSolver=GetComponentInChildren<ContextSolver>();
        seekBehaviour=GetComponentInChildren<SeekBehaviour>();
        InvokeRepeating("PerformDetection", 0, detectionDelay);
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
        // float[] danger =new float[8];
        // float[] intesting=new float[8];

        // foreach (var item in steeringBehaviours)
        // {
        //     (danger,intesting)=item.GetSteering(danger,intesting,aiData);
        // }
        
    }
    


    // private void Update()
    // {
    //     //Enemy AI movement based on Target availability
    //     if (aiData.currentTarget != null)
    //     {
    //         //Looking at the Target
    //         OnPointerInput?.Invoke(aiData.currentTarget.position);
    //         if (following == false)
    //         {
    //             following = true;
    //             StartCoroutine(ChaseAndAttack());
    //         }
    //     }
    //     else if (aiData.GetTargetsCount() > 0)
    //     {
    //         //Target acquisition logic
    //         aiData.currentTarget = aiData.targets[0];
    //     }
    //     //Moving the Agent
    //     OnMovementInput?.Invoke(movementInput);
    // }

    // private IEnumerator ChaseAndAttack()
    // {
    //     if (aiData.currentTarget == null)
    //     {
    //         //Stopping Logic
    //         Debug.Log("Stopping");
    //         movementInput = Vector2.zero;
    //         following = false;
    //         yield break;
    //     }
    //     else
    //     {
    //         float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);

    //         if (distance < attackDistance)
    //         {
    //             //Attack logic
    //             movementInput = Vector2.zero;
    //             OnAttackPressed?.Invoke();
    //             yield return new WaitForSeconds(attackDelay);
    //             StartCoroutine(ChaseAndAttack());
    //         }
    //         else
    //         {
    //             //Chase logic
    //             movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviours, aiData);
    //             yield return new WaitForSeconds(aiUpdateDelay);
    //             StartCoroutine(ChaseAndAttack());
    //         }

    //     }

    // }


    // public IEnumerator MoveToTarget(){
    //      if (aiData.currentTarget == null)
    //     {
    //         enemy.ZeroVelocity();
    //         yield break;
    //     }
    //     else{
    //         Vector2 moveDicrection=contextSolver.GetDirectionToMove(steeringBehaviours, aiData);
    //         enemy.SetRigidbody(moveDicrection.x*enemy.moveSpeed,moveDicrection.y*enemy.moveSpeed);
    //         yield return new WaitForSeconds(aiUpdateDelay);
    //         StartCoroutine(MoveToTarget());
    //     }
    // }
}
