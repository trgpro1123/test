using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSlash_Controller : MonoBehaviour
{
    
    private void Start() {
        transform.Rotate(0,0,Random.Range(0,360));
    }
    public void DestroySelf(){
        Destroy(gameObject);
    }
}
