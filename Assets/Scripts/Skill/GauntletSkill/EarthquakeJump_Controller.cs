using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeJump_Controller : MonoBehaviour
{

    private int damage;
    private float percentExtraDamageOfSkill;
    private float size;

    public void SetEarthquakeJump(int _damage, float _percentExtraDamageOfSkill,float _size)
    {
        damage = _damage;
        percentExtraDamageOfSkill = _percentExtraDamageOfSkill;
        size = _size;
        transform.localScale = new Vector3(size, size, 1);
        transform.Rotate(0, 0, Random.Range(0, 360));
        DealDamage();
    }

    private void DealDamage()
    {
        Vector3 position = GetComponent<SpriteRenderer>().bounds.center;
        float radius = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(position, radius);
        foreach (Collider2D enemy in enemies)
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                PlayerManager.instance.player.charaterStats.DoPhysicalDamage(enemyStats, damage, percentExtraDamageOfSkill);
            }
        }
        Destroy(gameObject, 0.5f);
    }
}
