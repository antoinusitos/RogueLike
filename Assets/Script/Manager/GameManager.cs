using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;

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

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        Application.LoadLevel("Game");
        currentGamestate = gameState.Playing;
    }

    public void MainMenu()
    {
        Application.LoadLevel("MainMenu");
        currentGamestate = gameState.Menu;

    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(1.0f);
        Application.LoadLevel("MainMenu");
    }
}
