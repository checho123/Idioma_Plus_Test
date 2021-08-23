using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMenu : MonoBehaviour
{
    [SerializeField]
    private ItemReferece itemElements;

    [SerializeField]
    private List<Item> itemsInventory;

    void Start()
    {
        itemsInventory = new List<Item>();
        itemsInventory = GameObject.FindGameObjectWithTag("InventoryController").GetComponent<Invetary>().listInventary;
        InstanteElements();
    }

    public void InstanteElements()
    {
        for (int i = 0; i < itemsInventory.Count; i++)
        {
            if (isRepeated(i))
            {
                continue;
            }
            (Instantiate(itemElements, transform) as ItemReferece).SetValue(itemsInventory[i]);
        }
    }

    private bool isRepeated(int element)
    {
        if (element == 0)
        {
            return false;
        }
        return itemsInventory[element].ID == itemsInventory[element - 1].ID;
    }
}
