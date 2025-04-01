using System;

[Serializable]
public class InventoryItem 
{
    public ItemData itemData;
    public int stackSize;
    public InventoryItem(ItemData _data){
        itemData=_data;
        AddStack();
    }
    public void AddStack()=>stackSize++;
    public void RemoveStack()=>stackSize--;

    public override bool Equals(object obj)
    {
        if (obj is InventoryItem other)
        {
            return itemData == other.itemData;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(itemData);
    }
}
