using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBegin : Gate
{
    protected override void Update() {
        if(Input.GetKeyDown(KeyCode.F)&&canStart){
            DungeonManager.instance.LoadNameMap();
            player.charaterStats.UpdateBeforeStart();
            StartCoroutine(DungeonManager.instance.LoadMapWithDelay(2f));
            GameManager.instance.enemyInSealedStateGameObject.SetActive(false);
        }
    }
}
