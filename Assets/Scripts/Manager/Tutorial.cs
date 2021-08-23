using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> tutorialGuide;
    [SerializeField]
    private int indexGuide;
    private GameManager manager;
    private bool starGameplay;
    [SerializeField]
    private GameObject continueButton;

    void Start()
    {
        HideAllGuide();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        tutorialGuide[indexGuide].SetActive(true);
        continueButton.SetActive(false);
    }

    private void HideAllGuide()
    {
        foreach (GameObject guide in tutorialGuide)
        {
            guide.SetActive(false);
        }
    }

    public void NextGuide()
    {
        tutorialGuide[indexGuide].SetActive(false);
        indexGuide++;

        if (indexGuide >= tutorialGuide.Count)
        {
            indexGuide = 0;
            EndTutorial();
        }
        tutorialGuide[indexGuide].SetActive(true);

    }

    public void EndTutorial()
    {
        starGameplay = !starGameplay;
        continueButton.SetActive(starGameplay);
    }

    public void StartGame()
    {
        if (starGameplay)
        {
            manager.state = StateGame.starGame;
        }
    }
}
