using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDoor : MonoBehaviour
{
    public Animator animator;
    void Awake()
    {
        animator=GetComponent<Animator>();
    }

    public void OpenWallAnimation(){
        OpenWall();
        animator.SetTrigger("Open");
    }

    public void CloseWallAnimation(){
        animator.SetTrigger("Close");
    }
    public void OpenWall(){
        gameObject.SetActive(true);
    }
    public void CloseWall(){
        gameObject.SetActive(false);
    }
}
