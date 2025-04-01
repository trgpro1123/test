using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEndSummon : MonoBehaviour
{
    [SerializeField] private GameObject boss;

    public void SummonBoss()
    {
        GameObject newBoss = Instantiate(boss, transform.position, Quaternion.identity);
        newBoss.SetActive(true);

    }
}
