using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    private GameManager manager;
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI progressText;

    void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void MainMenu(string menu)
    {
        SceneManager.LoadScene(menu);
        manager.state = StateGame.menu; 
    }

    public void GamePlayScene(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
        manager.state = StateGame.tutorial;
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100 + "%";
            yield return null;
        }
    }
}
