
[System.Serializable]
public class InventorySlot
{
    public ItemType typeOfSlot = ItemType.Emty;
    public Item item;
    public int amount = 0;

    public InventorySlot(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void ChangeQuantity(int amountToChange, Database database)
    {
        amount += amountToChange;
        if (amount <= 0)
        {
            ClearSlot(database);
        }
    }

    public void ClearSlot(Database database)
    {
        item = database.items[0];
        amount = 0;
    }

    public void UpdateSlot(Item newItem, int newAmount)
    {
        item = newItem;
        amount = newAmount;
    }

    public bool CanPlaceInSlot(Item item)
    {
        return (typeOfSlot == ItemType.Emty || typeOfSlot == item.type);
    }
}