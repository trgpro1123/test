using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSummon : MonoBehaviour
{
    public System.Action<bool> OnSummon;
    public void ActivateEnemy(){
        OnSummon?.Invoke(true);
    }
    
}
