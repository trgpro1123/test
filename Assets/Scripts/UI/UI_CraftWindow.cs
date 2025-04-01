using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CraftWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private  TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button craftButton;
    [SerializeField] private Image[] materialImage;

    private void Start()
    {
        ResetCraftWindown();
    }

    private void ResetCraftWindown()
    {
        for (int i = 0; i < materialImage.Length; i++)
        {
            materialImage[i].color = Color.clear;
            materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        }
        itemIcon.sprite = null;
        itemIcon.color = Color.clear;
        itemName.text = "";
        itemDescription.text = "";
        craftButton.gameObject.SetActive(false);
    }

    public void SetUpCraftWindow(ItemData_Equipment _item){
        craftButton.gameObject.SetActive(true);
        craftButton.onClick.RemoveAllListeners();
        for (int i = 0; i < materialImage.Length; i++)
        {
            materialImage[i].color=Color.clear;
            materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color=Color.clear;
        }

        for (int i = 0; i < _item.craftingMaterials.Count; i++)
        {
            if(_item.craftingMaterials.Count>materialImage.Length)
                Debug.LogWarning("You have more materials than materials slot you have");

            materialImage[i].sprite=_item.craftingMaterials[i].itemData.icon;
            materialImage[i].color=Color.white;

            TextMeshProUGUI materialTextSlot=materialImage[i].GetComponentInChildren<TextMeshProUGUI>();
            materialTextSlot.text=_item.craftingMaterials[i].stackSize.ToString();
            materialTextSlot.color=Color.white;

        }
        itemIcon.sprite=_item.icon;
        itemIcon.color=Color.white;
        itemName.text=_item.itemName;
        itemDescription.text=_item.GetDesciptrion();
        craftButton.onClick.AddListener(()=>Inventory.instance.CanCraft(_item,_item.craftingMaterials));
    }

    private void OnDisable() {
        
        ResetCraftWindown();
    }
}
