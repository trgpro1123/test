using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,ISaveManager
{
    public static PlayerManager instance;
    public Player player;
    public int currency;
    private void Awake() {
        if(instance!=null&&this.gameObject!=null){
            Destroy(this.gameObject);
        }
        else{
            instance=this;
        }
        if(!gameObject.transform.parent){
            DontDestroyOnLoad(gameObject);
        }
    }
    public bool HaveEnoughMoney(int _price){
        if(_price>currency) return false;

        currency-=_price;
        return true;
    }
    public int GetCurrency()=>currency;
    public void SaveData(ref GameData _data)
    {
        _data.currency=this.currency;
    }

    public void LoadData(GameData _data)
    {
        this.currency=_data.currency;  
    }


}
