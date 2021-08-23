using UnityEngine;
using TMPro;

public class ElementInvetary : MonoBehaviour
{
    [SerializeField]
    private Item elementItem;
    [SerializeField]
    private TypeElement typeElement;
    [SerializeField, Range(0f,1f)]
    private float speedRotateElement = 0.3f;
    [SerializeField]
    private Invetary invetary;
    [SerializeField]
    private GameManager manager;

    void Awake()
    {
        invetary = GameObject.FindGameObjectWithTag("InventoryController").GetComponent<Invetary>();
    }

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        typeElement = elementItem.type;
    }

    // Update is called once per frame
    void Update()
    {
        ElementRotate(speedRotateElement);
    }

    private void ElementRotate(float speedRotate)
    {
        transform.Rotate(Vector3.up, speedRotate);
    }

    public void CollectItem()
    {
        invetary.AddItem(elementItem);
        invetary.listInventary.Add(elementItem);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (typeElement == elementItem.type)
            {
                manager.msgText.text = "Nuevo elemento añadido: " + typeElement.ToString();
                manager.msg = true;
                Destroy(gameObject, 0.3f);
                CollectItem();
            }
        }
    }
}
