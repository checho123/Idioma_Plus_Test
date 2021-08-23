using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Invetary : MonoBehaviour
{
    [SerializeField]
    private Item[] items;
    public List<Item> listInventary { get; private set; }

    void Awake()
    {
        listInventary = new List<Item>();
        listInventary = items.OrderBy(i => i.name).ToList();
        ItemMousePotition.isClicked += RemoveItem;
    }

    private void OnDestroy()
    {
        ItemMousePotition.isClicked -= RemoveItem;
    }

    public void AddItem(Item item)
    {
        if(item != null)
        {
            listInventary.Add(item);
        }
    }

    public void RemoveItem(Item item)
    {
        if (item != null)
        {
            listInventary.Remove(item);
        }
    }
}
