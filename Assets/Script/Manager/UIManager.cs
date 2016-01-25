using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject canvasGame;
    //public GameObject canvasMainMenu;
    public GameObject canvasOptions;
    public GameObject canvasCredits;
    public GameObject canvasGameOver;
    public GameObject CanvasPause;
    //public GameObject canvasSplash;

    public GameObject tabInfo;

    public GameObject moneyText;
    public GameObject lifeText;
    public GameObject staminaText;
    public GameObject lifeBar;
    public GameObject staminaBar;

    private static UIManager instance = null;

    public static UIManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowCanvasGame()
    {
        HideAll();
        canvasGame.SetActive(true);
    }

    public void SetUIText(GameObject theText, string t)
    {
        theText.GetComponent<Text>().text = t;
    }

    public void ShowStats(bool state)
    {
        tabInfo.SetActive(state);
    }

    public void SetUIBar(GameObject theBar, float v1, float v2)
    {
        theBar.GetComponent<Slider>().value = v1 / v2;
    }

    /* public void ShowCanvasMenu()
     {
         HideAll();
         canvasMainMenu.SetActive(true);
     }*/

    public void ShowCanvasOptions()
    {
        HideAll();
        canvasOptions.SetActive(true);
    }

   /* public void ShowCanvasSplash()
    {
        HideAll();
        canvasSplash.SetActive(true);
    }*/

    public void ShowCanvasCredits()
    {
        HideAll();
        canvasCredits.SetActive(true);
    }

    public void ShowCanvasGameOver()
    {
        HideAll();
        canvasGameOver.SetActive(true);
    }

    public void ShowCanvasPause()
    {
        HideAll();
        CanvasPause.SetActive(true);
    }

    public void HideAll()
    {
        CanvasPause.SetActive(false);
        canvasGameOver.SetActive(false);
        canvasCredits.SetActive(false);
        //canvasSplash.SetActive(false);
        canvasOptions.SetActive(false);
        //canvasMainMenu.SetActive(false);
        canvasGame.SetActive(false);
    }
}
