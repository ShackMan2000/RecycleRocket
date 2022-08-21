using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//This scriptableobject is what outside classes will access that wish to add or remove items from the inventory or check for hotbar selected item.
[CreateAssetMenu(menuName = "InventorySystem2.0/Inventory/PlayerInventory")]
public class PlayerInventory : ScriptableObject
{
    [SerializeField] private ItemStackCollection hotbarCollection;
    [SerializeField] private ItemStackCollection inventoryCollection;

    [SerializeField] private List<ItemStackCollection> inventoryColleion;


    //public ItemStack HotbarSelectedItem { get; private set; }
    //public int HotbarSelectedItemIndex { get; private set; }

    //public Action OnInventoryChange = delegate { };
    //public Action OnHotbarSelectionChange = delegate { };





    ////should have all the methods of itemstackcollection and pass it on and return same values



    ////public bool AddItem(ItemStack itemStack)
    ////{
    ////    if (hotbarCollection.Add(itemStack))
    ////    {
    ////        OnInventoryChange.Invoke();
    ////        return true;
    ////    }

    ////    if (inventoryCollection.Add(itemStack))
    ////    {
    ////        OnInventoryChange.Invoke();
    ////        return true;
    ////    }

    ////    return false;
    ////}

    ////public void RemoveItem(ItemStack itemStack)
    ////{
    ////    hotbarCollection.Subtract(itemStack.GetItemName(), itemStack.GetAmount());
    ////    OnInventoryChange.Invoke();
    ////}

    //public List<ItemStack> itemStacks;

 

    //public void ClearItem(ItemBase itemBase)
    //{
    //    //remove whatever amount an item has


    //}


    ////public int GetItemQuantity(string itemName)
    ////{
    ////    return hotbarCollection.GetItemQuantity(itemName);
    ////}




    //public int GetAmount(ItemBase itemBase, bool fromHotbarOnly = false)
    //{

    //    int amount = hotbarCollection.GetAmount(itemBase);

    //    if (!fromHotbarOnly && amount == 0)
    //    {
    //        amount = inventoryCollection.GetAmount(itemBase);
    //    }

    //    return amount;
    //}







    //// if only used for hotbar, should use inheritance
    //public void SetHotbarSelectedItem(int index)
    //{
    //    HotbarSelectedItem = hotbarCollection.GetItemAtIndex(index);
    //    HotbarSelectedItemIndex = index;
    //    OnHotbarSelectionChange.Invoke();
    //}
}
