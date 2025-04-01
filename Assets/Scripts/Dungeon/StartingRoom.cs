using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRoom : MonoBehaviour
{
    [SerializeField] private GameObject positionSpawnPlayer;

    private Player player;
    private void Start() {
        StartCoroutine(SetupRoom());
    }
 

    public IEnumerator SetupRoom()
    {
        player = PlayerManager.instance.player;
        player.transform.position = positionSpawnPlayer.transform.position;

        yield return new WaitForSeconds(1f);
        GameManager.instance.SetCinemachineVirtualCamera();
    }


}
