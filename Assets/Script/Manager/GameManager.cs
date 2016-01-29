using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;

    public GameObject MainMenuCanvas;
    public GameObject OptionCanvas;
    public GameObject PauseCanvas;
    public GameObject UICanvas;

    public bool isInPause;

    public AudioSource buttonPressed;

    enum gameState
    {
        Menu,
        SplashScreen,
        Pause,
        GameOver,
        Playing
    };

    public GameObject player;

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

   


public void SetPlayer(GameObject p )
    {
        player = p;
    } 
    
    void Start()
    {
        isInPause = false;

        buttonPressed = SoundManager.instance.buttonPressed.GetComponent<AudioSource>();
    }    public void Quit()
   
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
        buttonPressed.Play();

        Application.LoadLevel("Menu");
        currentGamestate = gameState.Menu;

    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape) && isInPause == false)
        {
            buttonPressed.Play();

            isInPause = true;
            PauseCanvas.SetActive(true);
            UICanvas.SetActive(false);
            Time.timeScale = 0;
            return;
    
        }

        if(Input.GetKeyDown(KeyCode.Escape) && isInPause == true)
        {
            buttonPressed.Play();

            PauseCanvas.SetActive(false);
            UICanvas.SetActive(true);
            Time.timeScale = 1;
            isInPause = false;
            return;

        }

    }

    public void exitPause()


    {
        buttonPressed.Play();

       // Debug.Log("cc");
        PauseCanvas.SetActive(false);
        UICanvas.SetActive(true);
        Time.timeScale = 1;
        isInPause = false;

    }


    public void Options()
    {
        buttonPressed.Play();

        OptionCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }

    public void BackMainMenu()
    {
        buttonPressed.Play();

        OptionCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(1.0f);

        Application.LoadLevel("Menu");
       
    }
}
