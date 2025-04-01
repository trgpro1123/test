using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject arrowhead;
    private Animator animator;
    public bool playerChoosed=false;
    public bool canTrigger=false;
    private NPCManager nPCManager;
    protected Player player;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        nPCManager=GetComponentInParent<NPCManager>();
        player=PlayerManager.instance.player;
        arrowhead.SetActive(false);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)&&playerChoosed==false&&canTrigger){
            this.Interact();

        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Player>()!=null&&playerChoosed==false){
            arrowhead.SetActive(true);
            canTrigger=true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.GetComponent<Player>()!=null&&playerChoosed==false){
            arrowhead.SetActive(false);
            canTrigger=false;
        }
    }
    public void NPCDisappear(){
        animator.SetTrigger("Disappear");
        playerChoosed=true;
        arrowhead.SetActive(false);
    }
    public virtual void Interact(){
        nPCManager.NPCsDisappear();
    }

}
