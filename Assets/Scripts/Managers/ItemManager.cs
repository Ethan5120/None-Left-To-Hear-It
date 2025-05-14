using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public pickUp_SO itemsStates;
    public void InitializeItems()
    {
        if(items.Count != itemsStates.itemActiveState.Count)
        {
            Debug.LogError("NotCompatibleLists");
            return;
        }

        for(int i = 0; i < items.Count; i++)
        {
            if(itemsStates.itemActiveState[i] == true)
            {
                items[i].SetActive(false);
            }
        }
    }

    public void ResetItems()
    {
        for(int i = 0; i < itemsStates.itemActiveState.Count; i++)
        {
            itemsStates.itemActiveState[i] = false;
        }
    }
}
