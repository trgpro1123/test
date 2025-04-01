using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenTimeZoneEffect : MonoBehaviour
{
    private float timeUseSkill;
    private float growSpeed;
    private bool canGrow;
    private bool canShrink;
    private float maxSize;




    private List<Collider2D> hitEnemies;
    private Vector2 currentSize;
    private float timer;
    private void Start() {
        hitEnemies = new List<Collider2D>();
        canGrow=true;
    }

    private void Update() {
        timer-=Time.deltaTime;
        if(timer<=0) canShrink=true;
        if(canGrow&&!canShrink){
            currentSize=Vector2.Lerp(transform.localScale,new Vector2(maxSize,maxSize),growSpeed*Time.deltaTime);
            transform.localScale=currentSize;
        }
        if(canShrink){
            currentSize=Vector2.Lerp(transform.localScale,new Vector2(-1,-1),growSpeed*Time.deltaTime);
            transform.localScale=currentSize;
            if(transform.localScale.x<0&&this.gameObject!=null) Destroy(gameObject);
        }
    }
    public void SetFrozenTimeZoneEffect(float _timeUseSkill,float _growSpeed,float _maxSize)
    {
        timeUseSkill = _timeUseSkill;
        timer = timeUseSkill;
        growSpeed = _growSpeed;
        maxSize = _maxSize;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Enemy>()&& !hitEnemies.Contains(other))
        {
            hitEnemies.Add(other);
            other.GetComponent<Enemy>().FreezeTimer(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.GetComponent<Enemy>()!=null)
        {
            other.GetComponent<Enemy>().FreezeTimer(false);
        }
    }
    public void DestroyAreaAttack()
    {
        Destroy(gameObject);
        Debug.Log("Destroy");
    }
    public List<Collider2D> GetEnemyInArea()
    {
        return hitEnemies;
    }
}
