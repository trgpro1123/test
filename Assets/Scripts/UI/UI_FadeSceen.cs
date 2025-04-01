using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeSceen : MonoBehaviour
{
    private Animator animator;
    private void Start() {
        animator=GetComponent<Animator>();

    }
    public void FadeIn(){
        animator.SetTrigger("FadeIn");
    }
    public void FadeOut(){
        animator.SetTrigger("FadeOut");
    }
}
