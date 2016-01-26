using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;

    public GameObject MainMenuCanvas;
    public GameObject OptionCanvas;
    public GameObject PauseCanvas;
    public GameObject UICanvas;

    public bool isInPause;

    enum gameState
    {
        Menu,
        SplashScreen,
        Pause,
        GameOver,
        Playing
    };

    gameState currentGamestate;

    public static GameManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        currentGamestate = gameState.SplashScreen;
        
        //StartCoroutine(LoadMenu());
    }

    void Start()
    {
        isInPause = false;
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        Application.LoadLevel("Test");
        currentGamestate = gameState.Playing;
    }

    public void MainMenu()
    {
        Application.LoadLevel("Menu");
        currentGamestate = gameState.Menu;

    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape) && isInPause == false)
        {
            isInPause = true;
            PauseCanvas.SetActive(true);
            UICanvas.SetActive(false);
            Time.timeScale = 0;
            return;
    
        }

        if(Input.GetKeyDown(KeyCode.Escape) && isInPause == true)
        {
            PauseCanvas.SetActive(false);
            UICanvas.SetActive(true);
            Time.timeScale = 1;
            isInPause = false;
            return;

        }

    }

    public void exitPause()


    {
        Debug.Log("cc");
        PauseCanvas.SetActive(false);
        UICanvas.SetActive(true);
        Time.timeScale = 1;
        isInPause = false;

    }


    public void Options()
    {
        OptionCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }

    public void BackMainMenu()
    {
        OptionCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(1.0f);
        Application.LoadLevel("Menu");
       
    }
}
