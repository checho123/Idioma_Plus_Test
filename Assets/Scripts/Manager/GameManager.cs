using UnityEngine;
using TMPro;

public enum StateGame { menu, tutorial, starGame, dead, victoryGame}

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public StateGame state;

    [Header("Controller Menu Pause")]
    private bool pause = false;
    private bool menuInventary = false;
    public bool msg;
    public bool menuPlayer = true;
    [SerializeField]
    private GameObject menuCanvas, panelMenuPause, panelInventary, msgPanel, playerMenu, menuTutorial, menuMain;

    [Header("Player Controller")]
    [SerializeField]
    private PlayerController playerController;

    [Header("Message Item")]
    [SerializeField, Range(0f, 5f)]
    private float timeMsg = 1.5f;
    private float timeStar;
    public TextMeshProUGUI msgText;

    #region Instance Only
    void Awake()
    {
        if (GameManager.manager == null)
        {
            GameManager.manager = this;
            DontDestroyOnLoad(manager);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    void Start()
    {
        menuCanvas = GameObject.FindGameObjectWithTag("MenuCanvas");
        panelMenuPause = menuCanvas.transform.GetChild(0).gameObject;
        panelInventary = menuCanvas.transform.GetChild(1).gameObject;
        msgPanel = menuCanvas.transform.GetChild(2).gameObject;
        playerMenu = menuCanvas.transform.GetChild(3).gameObject;
        menuTutorial = menuCanvas.transform.GetChild(4).gameObject;
        menuMain = menuCanvas.transform.GetChild(5).gameObject;
        msgText = msgPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        StartGame();
        MenuGame();
        ModeTutorial();
    }

    private void MenuGame()
    {
        if (state == StateGame.menu)
        {
            panelMenuPause.SetActive(false);
            panelInventary.SetActive(false);
            playerMenu.SetActive(false);
        }
    }

    private void ModeTutorial()
    {
        if (state == StateGame.tutorial)
        {
            menuTutorial.SetActive(true);
            menuMain.SetActive(false);
        }
    }

    private void StartGame()
    {
        bool gameStart = (state == StateGame.starGame);
        if (gameStart)
        {
            menuTutorial.SetActive(false); 
            // Player Controller
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerMenu.SetActive(menuPlayer);
            // Pause Menu
            PanelMenuPause();
            panelInventary.SetActive(menuInventary);
            // Panel Invtary
            PanelInventary();
            panelMenuPause.SetActive(pause);
            // Msg Item Add
            AddItem();
        }
    }

    private void AddItem()
    {
        msgPanel.SetActive(msg);
        if (msg)
        {
            timeStar += Time.deltaTime;
        }

        if (timeStar >= timeMsg)
        {
            timeStar = 0;
            msg = false;
        }
    }

    private void PanelMenuPause()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !menuInventary)
        {
            pause = !pause;
            menuPlayer = !menuPlayer;
            Time.timeScale = (pause) ? 0f : 1f;
            playerController.isMoveCharacter = pause;
        }
    }

    private void PanelInventary()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !pause)
        {
            menuInventary = !menuInventary;
            playerController.isMoveCharacter = menuInventary;
            menuPlayer = !menuPlayer;
        }
    }

}
