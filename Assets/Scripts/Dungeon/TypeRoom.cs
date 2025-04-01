using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeRoom : MonoBehaviour
{
    [SerializeField] private List<GameObject> types;
    [SerializeField] private GameObject baseType;
    [SerializeField] [Range(0f,1f)]  private float  rateSpawnBaseType;

    public int ChooseRandomType(){
        int randomType=Random.Range(0,types.Count);
        if(baseType==null){
            if(types.Count<=0){
                Debug.Log("Map Empty");
                return -1;
            }
            types[randomType].SetActive(true);
            return -1;
        }
        if(Random.value<=rateSpawnBaseType){
            baseType.SetActive(true);
            baseType.GetComponent<TypeRoom>()?.ChooseRandomType();
            return -1;
        }else{
            if(types.Count<=0){
                Debug.Log("Map Empty");
                baseType.SetActive(true);
                return -1;
            }
            
            types[randomType].SetActive(true);
            types[randomType].GetComponent<TypeRoom>()?.ChooseRandomType();
            // if(baseType!=null)
            //     types.RemoveAt(randomType);
        }
        return randomType;
        
        
    }

    public void RemoveType(int _value){
        if(_value>=0)
            types.RemoveAt(_value);
    }
}
