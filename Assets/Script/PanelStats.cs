using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelStats : MonoBehaviour {

    public GameObject playerLevel;
    public GameObject lifeLevel;
    public GameObject staminaLevel;
    public GameObject damageLevel;
    public GameObject ammoLevel;
    public GameObject candenceLevel;
    public GameObject reloadLevel;
    public GameObject aimLevel;

    public GameObject currentLife;
    public GameObject currentLifeMax;
    public GameObject currentStamina;
    public GameObject currentStaminaMax;
    public GameObject currentDamage;
    public GameObject currentAmmo;
    public GameObject currentAmmoMax;
    public GameObject currentCandence;
    public GameObject currentReload;
    public GameObject currentAim;
    public GameObject currentMoney;
    public GameObject currentExp;
    public GameObject currentExpMax;

    private static PanelStats instance = null;

    public static PanelStats GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateStats(GameObject player)
    {
        StatPlayer p = player.GetComponent<StatPlayer>();
        playerLevel.GetComponent<Text>().text = p.level.ToString();
        lifeLevel.GetComponent<Text>().text = p.LevelLife.ToString();
        staminaLevel.GetComponent<Text>().text = p.LevelStamina.ToString();
        damageLevel.GetComponent<Text>().text = p.LevelDamage.ToString();
        ammoLevel.GetComponent<Text>().text = p.LevelAmmo.ToString();
        candenceLevel.GetComponent<Text>().text = p.LevelCadence.ToString();
        reloadLevel.GetComponent<Text>().text = p.LevelReload.ToString();
        aimLevel.GetComponent<Text>().text = p.LevelAim.ToString();

        currentLife.GetComponent<Text>().text = player.GetComponent<Player>().life.ToString();
        currentLifeMax.GetComponent<Text>().text = player.GetComponent<Player>().maxLife.ToString();
        currentStamina.GetComponent<Text>().text = (Mathf.Round(player.GetComponent<DeplacementPlayer>().stamina * 100f) / 100f).ToString();
        currentStaminaMax.GetComponent<Text>().text = player.GetComponent<DeplacementPlayer>().staminaMax.ToString();
        currentDamage.GetComponent<Text>().text = "value";//p.LevelDamage.ToString();
        currentAim.GetComponent<Text>().text = "value";//p.LevelDamage.ToString();
        currentAmmo.GetComponent<Text>().text = player.GetComponent<Player>().gun.GetComponent<Gun>().currentAmmo.ToString();
        currentAmmoMax.GetComponent<Text>().text = player.GetComponent<Player>().gun.GetComponent<Gun>().maxAmmo.ToString();
        currentCandence.GetComponent<Text>().text = player.GetComponent<Player>().gun.GetComponent<Gun>().fireRate.ToString();
        currentReload.GetComponent<Text>().text = player.GetComponent<Player>().gun.GetComponent<Gun>().reloadTime.ToString();
        currentMoney.GetComponent<Text>().text = p.money.ToString();
        currentExp.GetComponent<Text>().text = p.experience.ToString();
        currentExpMax.GetComponent<Text>().text = p.experienceToNextLevel.ToString();
    }
}
