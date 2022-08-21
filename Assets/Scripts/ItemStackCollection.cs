using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



/// <summary>
/// A wrapper for a List<ItemStack>. Adds functionality to change items and retrieve info
/// </summary>


[Serializable]
public class ItemStackCollection 
{
    [SerializeField]
    private int collectionLength;

    //  ItemStack[] collection;


    //[SerializeField]
    //private List<ItemStack> itemList = new List<ItemStack>();
    //public List<ItemStack> ItemList { get => itemList; }
    ////



    //public Action OnCollectionChange = delegate { };  //invoked everytime the collection changes.(an item is added or removed)  

    //public Action<ItemBase, int> OnItemAmountChanged = delegate { };

    //public Action OnCollectionPopulated = delegate { }; //Invoked when the collection is populated


    ////shouldn't be needed when we move to a different save system
    ////Initializes and populates the collection
    //public void PopulateCollection(List<ItemStack> collection)
    //{
    //    itemList = new List<ItemStack>();

    //    for (int i = 0; i < collection.Count && i < collectionLength; i++)
    //    {
    //        itemList.Add(collection[i]);
    //    }



    //    //array
    //    //this.collection = new ItemStack[collectionLength];
    //    //for(int i=0;i<collectionLength;i++)
    //    //{
    //    //    if(i > collection.Length - 1)
    //    //        break;

    //    //    this.collection[i] = collection[i];
    //    //}

    //    //OnCollectionPopulated.Invoke();
    //}


    ////using tuples to avoid stuff like return -1 that will lead to confusion when called from outside
    //private (bool hasFreeSlot, int freeSlotIndex) GetFirstFreeSlot()
    //{
    //    int index = -1;


    //    for (int i = 0; i < collectionLength; i++)
    //    {
    //        if (itemList.Count > i)
    //        {
    //            if (itemList[i] == null)
    //            {
    //                index = i;
    //                break;
    //            }
    //        }
    //    }

    //    return (index > -1, index);
    //}





    ////add item if already in there or has a free slot. Could also add method to cap amount later
    //public bool TryAddAmount(ItemBase itemBase, int amount)
    //{
    //    ItemStack stack = GetItemStackByItemBase(itemBase);

    //    //itemBase already in the list, just add amount
    //    if(stack != null)
    //    {
    //        stack.AddToStack(amount);
    //        return true;
    //    }

    //    //check if it has a free slot, if so make a new itemstack
              
    //    if (itemList.Count < collectionLength)
    //    {
    //        itemList.Add(new ItemStack(itemBase, amount));
    //        return true;
    //    }

    //    return false;
    //}


    ////technically int rest contains the info of the bool, but using a tuple adds the naming which should reduce misunderstandings
    //public (bool fullAmountSubtracted, int rest) TrySubtractAmount(ItemBase itemBase, int amount)
    //{



    //    return (true, 0);
    //}


    

    //public IReadOnlyCollection<ItemStack> GetCollection()
    //{
    //    return itemList as IReadOnlyCollection<ItemStack>;
    //}

 

    //public int GetAmount(ItemBase itemBase)
    //{

    //    ItemStack stack = GetItemStackByItemBase(itemBase);

    //    return stack == null ? 0 : stack.GetAmount();
    //}



    //private ItemStack GetItemStackByItemBase(ItemBase itemBase)
    //{
    //    //returns null if there is not itemStack with this itemBase
    //    return itemList.Find((s => s.itemBase == itemBase));
    //}



    ////This function only gives the reference to the item and doesn't remove from collection
    //public ItemStack GetItemAtIndex(int index)
    //{
    //    return itemList[index];
    //}



    ////Removes and returns whatever the collection has at the given index
    //public ItemStack RetrieveItemStackAtIndex(int index)
    //{
    //    if (index < 0 || index >= collectionLength)
    //        return null;

    //    ItemStack itemStack = itemList[index];
    //    itemList[index] = null;
    //    OnCollectionChange.Invoke();
    //    return itemStack;
    //}

    ////If the collection is empty at the given index, adds the itemstack to the collection at that index.
    //public bool AddItemStackAtIndex(ItemStack itemStack, int index)
    //{
    //    if (index < 0 || index >= collectionLength || itemList[index] != null)
    //        return false;

    //    itemList[index] = itemStack;
    //    OnCollectionChange.Invoke();
    //    return true;
    //}



    ////Returns the quantity of the item in the collection with the given name
    ////public int GetItemQuantity(string itemName)
    ////{
    ////    foreach(ItemStack itemStack in collection)
    ////    {
    ////        if(itemStack == null || itemStack.GetItemName() != itemName)
    ////            continue;

    ////        return itemStack.GetAmount();
    ////    }

    ////    return 0;
    ////}


    ////public bool Add(ItemStack itemStack)
    ////{
    ////    int availableIndex = -1;
    ////    bool itemWasFound = false;
    ////    for(int i=0;i<collection.Length;i++)
    ////    {
    ////        if(collection[i] == null)
    ////        {
    ////            if(availableIndex == -1)
    ////                availableIndex = i;

    ////            continue;
    ////        }

    ////        if(collection[i].GetItemName() != itemStack.GetItemName())
    ////            continue;

    ////        collection[i].AddToStack(itemStack.GetAmount());
    ////        OnCollectionChange.Invoke();
    ////        itemWasFound = true;
    ////        break;
    ////    }

    ////    if(itemWasFound)
    ////        return true;

    ////    if(availableIndex == -1)
    ////        return false;

    ////    collection[availableIndex] = itemStack.Clone();
    ////    OnCollectionChange.Invoke();
    ////    return true;
    ////}


    ////public void Subtract(string itemName, int quantity)
    ////{
    ////    for(int i=0;i<collection.Length;i++)
    ////    {
    ////        if(collection[i] == null || collection[i].GetItemName() != itemName)
    ////            continue;

    ////        collection[i].SubtractFromStack(quantity);

    ////        if(collection[i].GetAmount() == 0)
    ////        {
    ////            collection[i] = null;
    ////        }
    ////        OnCollectionChange.Invoke();
    ////        break;
    ////    }        
    ////}
}
