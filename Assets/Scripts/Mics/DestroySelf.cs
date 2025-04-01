using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private bool destroyOnStart=false;
    [SerializeField] private float destroyTime;

    private void Start() {
        if(destroyOnStart){
            Destroy(gameObject,destroyTime);
        }
    }
    public void DestroySelfNow()
    {
        Destroy(gameObject);
    }
    public void DestroyGameObjectParent()
    {
        Destroy(transform.parent.gameObject);
    }
}
