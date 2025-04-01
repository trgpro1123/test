using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform positionSpawn;
    private BoxCollider2D boxCollider;
    public Room mainRoom;
    public Room roomConnected;
    private LayerMask interactableLayer;
    private void Start() {
        mainRoom=GetComponentInParent<Room>();
        boxCollider=GetComponent<BoxCollider2D>();
    }

    public void GetDoor(Transform _positionSpawn){
        positionSpawn=_positionSpawn;
    }
    public void GetRoom(Room _room){
        roomConnected=_room;
    }
    public void ActivateConnectedRoom(){
        if(roomConnected==null) return;
        roomConnected.gameObject.SetActive(true);
        roomConnected.ResetMinimapIcon();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Player>()&&boxCollider.IsTouchingLayers()){
            //player.tranform.position=positionSpawn.position;
            other.GetComponent<Player>().transform.position=positionSpawn.position;

            mainRoom.ResetMinimapIcon();
            mainRoom.ActivateConnectedRooms();
            roomConnected.ActiveMiniMapIcon();
            roomConnected.ActivateConnectedRooms();

        }
        
    }
    
    
}
