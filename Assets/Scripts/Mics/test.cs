using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{

    // NavMeshAgent navMeshAgent;
    // public NavMeshSurface Surface2D;
    // public GameObject testEffect;

    // private Animator animator;
    public Transform target;
    public float knockbackForce;
    public float duration;
    public Rigidbody2D rb;
    public TextMeshProUGUI text;
    public float timer=-1;

    private void Start() {
        rb=GetComponent<Rigidbody2D>();
        // navMeshAgent=GetComponent<NavMeshAgent>();
        // navMeshAgent.updateRotation=false;
        // navMeshAgent.updateUpAxis=false;
        // animator=GetComponent<Animator>();
        // navMeshAgent.speed=2f;
    }
    private void Update() {
        if(timer>0){
            timer-=Time.deltaTime;
            text.text=timer.ToString("0.0");
        }
        else if(timer<0){
            timer=0;
            text.text="";
        }
        

        // if(Input.GetKeyDown(KeyCode.P)){
            
        // }
        // if(Input.GetKeyDown(KeyCode.R))
        // {
        //     // Surface2D.BuildNavMesh();
        //     // Surface2D.BuildNavMeshAsync();
        // Surface2D.UpdateNavMesh(Surface2D.navMeshData);
        // }
        // navMeshAgent.SetDestination(PlayerManager.instance.player.transform.position);
    }

    public void StopKnockback(){
        rb.velocity=Vector2.zero;
    }




    // public GameObject obj;
    // public float duration;
    // public float heightY;
    // public AnimationCurve animationCurve;

    // private void Update() {
    //     if(Input.GetKeyDown(KeyCode.P))
    //     {
    //         CreateObject();
    //     }
    // }

    // public void CreateObject()
    // {
    //     // GameObject newObj = Instantiate(obj, transform.position, Quaternion.identity);
    //     StartCoroutine(ProjecttileCurveRoutine(transform.position, PlayerManager.instance.player.transform.position));
    // }
    // IEnumerator ProjecttileCurveRoutine(Vector3 startPosition,Vector3 endPosition){
    //     float timePassed=0f;
    //     GameObject newObj = Instantiate(obj, transform.position, Quaternion.identity);
    //     while(timePassed<duration){
    //         timePassed+=Time.deltaTime;
    //         float linearT=timePassed/duration;
    //         float heightT=animationCurve.Evaluate(linearT);
    //         float height=Mathf.Lerp(0f,heightY,heightT);

    //         newObj.transform.position=Vector2.Lerp(startPosition,endPosition,linearT)+new Vector2(0f,height);


    //         yield return null;
    //     }
    //     Debug.Log("Destroy");
    //     Destroy(newObj.gameObject);
    // }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.GetComponent<Player>())
    //     {
    //         Debug.Log("Player");
    //     }
    // }
    
}
