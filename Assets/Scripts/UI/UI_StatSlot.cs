using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private string statName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private TextMeshProUGUI statNameText;
    [TextArea]
    public string descriptionStat;
    public string descriptionStatKey;


    private UI ui;

    private void OnValidate() {
        gameObject.name="Stat - "+statName;
        if(statNameText!=null){
            statNameText.text=statName;
        }

    }

    private void Start() {
        UpdateStatValueUI();
        ui=GetComponentInParent<UI>();   
    }
    public void UpdateStatValueUI(){
        PlayerStats playerStats=PlayerManager.instance.player.GetComponent<PlayerStats>();
        if(playerStats!=null){
            statValueText.text=playerStats.GetType(statType).GetValue().ToString();
        }
        if(statType==StatType.health){
            statValueText.text=playerStats.GetMaxHealth().ToString();
        }
        if(statType==StatType.strength){
            statValueText.text=playerStats.strength.GetValue().ToString();
        }
        if(statType==StatType.armor){
            statValueText.text=playerStats.armor.GetValue().ToString();
        }
        if(statType==StatType.agility){
            statValueText.text=playerStats.agility.GetValue().ToString();
        }
        if(statType==StatType.Lifesteal){
            statValueText.text=playerStats.Lifesteal.GetValue().ToString();
        }
        if(statType==StatType.moveSpeed){
            statValueText.text=playerStats.moveSpeed.GetValue().ToString();
        }
        if(statType==StatType.critChance){
            int critChance=Mathf.Clamp(playerStats.critChance.GetValue()+PlayerManager.instance.player.charaterStats.luck.GetValue(),0,50);
            statValueText.text=critChance.ToString();
        }
        if(statType==StatType.magicPower){
            statValueText.text=playerStats.magicPower.GetValue().ToString();
        }
        if(statType==StatType.magicResistance){
            statValueText.text=playerStats.magicResistance.GetValue().ToString();
        }
        if(statType==StatType.magicRegeneration){
            statValueText.text=playerStats.magicRegeneration.GetValue().ToString();
        }
        if(statType==StatType.luck){
            statValueText.text=playerStats.luck.GetValue().ToString();
        }
        if(statType==StatType.coolDown){
            statValueText.text=playerStats.coolDown.GetValue().ToString();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.statTooltip.ShowStatTooltip(descriptionStatKey);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.statTooltip.HideStatTooltip();
    }
}
