using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillListType : MonoBehaviour
{
    public List<Transform> gameObjectIgnores;
    public void SwitchTo(GameObject _menu){

        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.activeSelf&&gameObjectIgnores.Contains(transform.GetChild(i))){
                continue;
            }
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if(_menu!=null){
            _menu.gameObject.SetActive(true);
        }
   }
}
