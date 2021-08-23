using UnityEngine;
using TMPro;

public class InformationManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI descriptionText;

    void Start()
    {
        ItemMousePotition.mouseOn += ShowInformations;
        ItemMousePotition.mouseOff += ResetInformation;
        descriptionText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        nameText = transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnDestroy()
    {
        ItemMousePotition.mouseOn -= ShowInformations;
        ItemMousePotition.mouseOff -= ResetInformation;
    }

    private void ShowInformations(string nameItem, string description)
    {
        nameText.text = nameItem;
        descriptionText.text = description;
    }

    private void ResetInformation()
    {
        nameText.text = string.Empty;
        descriptionText.text = string.Empty;
    }
}
