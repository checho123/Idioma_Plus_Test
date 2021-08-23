using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeElement
{
    Posion_De_Curar,
    Posion_De_Subir_Nivel
}
[CreateAssetMenu(fileName = "New Item", menuName = "Create Item")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public string nameItem;
    public string description;
    public TypeElement type;

    public int ID { get; private set; }
    public int count
    {
        get 
        {
            return GameObject.FindGameObjectWithTag("InventoryController").GetComponent<Invetary>().listInventary.FindAll
                ( x => x.ID == this.ID).Count;
        }
    }

    private void OnEnable() =>
        ID = this.GetInstanceID();
}
