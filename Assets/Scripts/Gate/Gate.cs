using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    
    [SerializeField] private GameObject arrowhead;
    protected Animator animator;
    protected bool canStart=false;
    protected BoxCollider2D boxCollider2D;
    protected Player player;
    void Awake()
    {
        animator=GetComponent<Animator>();
        boxCollider2D=GetComponent<BoxCollider2D>();
    }
    protected virtual void Update() {
        if(Input.GetKeyDown(KeyCode.F)&&canStart){
            StartCoroutine(DungeonManager.instance.LoadRoundWithDelay(2f));
        }
    }
    protected void OnTriggerEnter2D(Collider2D other) {
        if(player==null) player=PlayerManager.instance.player;
        if(other.GetComponent<Player>()&&player.canUseGate){
            canStart=true;
            arrowhead.SetActive(true);
            animator.SetBool("Connect",true);
        }
    }
    protected void OnTriggerExit2D(Collider2D other) {
        if(player==null) player=PlayerManager.instance.player;
        if(other.GetComponent<Player>()&&player.canUseGate){
            canStart=false;
            arrowhead.SetActive(false);
            animator.SetBool("Connect",false);
        }
    }

    public void ActiveBoxCollider(){
        boxCollider2D.enabled=true;
    }


    
}
