using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ItemMousePotition : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<string, string> mouseOn;
    public static event Action mouseOff;
    public static event Action<Item> isClicked;

    private ItemReferece referece;
    [SerializeField]
    private PlayerController player;
    [SerializeField, Range(10, 100)]
    private int upLife = 20;
    [SerializeField, Range(5,10)]
    private int levelUp = 5;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        referece = GetComponent<ItemReferece>();
        ChangeColor(Color.gray);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isClicked?.Invoke(referece._item);
        referece.UpdateCount();
        bool noMore = (referece._item.count == 0);

        if (referece._item.type == TypeElement.Posion_De_Curar && !noMore)
        {
            player.PotionLife(upLife);
        }

        if (referece._item.type == TypeElement.Posion_De_Subir_Nivel && !noMore)
        {
            player.PotionLevelUp(levelUp);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeColor(Color.white);
        mouseOn?.Invoke(referece._item.name, referece._item.description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeColor(Color.gray);
        mouseOff?.Invoke();
    }

    private void ChangeColor(Color color)
    {
        referece.iconImg.color = color;
    }
}
