using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public GameObject boss;
    [SerializeField] private GameObject gate;
    public bool isBossDeath=false;
    public void OnBossDeath(Enemy enemy){
        gate.SetActive(true);
        isBossDeath=true;
    }
}
