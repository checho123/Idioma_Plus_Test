using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemReferece : MonoBehaviour
{
    public Image iconImg;
    public TextMeshProUGUI countText;

    public Item _item { get; private set; }

    public void SetValue(Item item)
    {
        _item = item;
        iconImg.sprite = item.icon;
        UpdateCount();
    }

    public void UpdateCount()
    {
        countText.text = "x " + _item.count.ToString();
    }
}
