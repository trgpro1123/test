using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private List<NPC> nPCs;


    public void NPCsDisappear()
    {
        foreach (var npc in nPCs)
        {
            npc.NPCDisappear();
        }
    }
}
