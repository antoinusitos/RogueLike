using UnityEngine;
using System.Collections;

public class StatPlayer : MonoBehaviour {

    public float experience = 0.0f;
    public float experienceToNextLevel = 100.0f;
    public int level = 1;
    public int money = 0;

    public int LevelDamage = 0;
    public int LevelCadence = 0;
    public int LevelReload = 0;
    public int LevelAim = 0;
    public int LevelAmmo = 0;

    public int LevelStamina = 0;
    public int LevelLife = 0;

    public float upgradeReload;
    public float upgradeCadence;
    public int upgradeAmmo;

    void Start()
    {
        money = 100;
        experienceToNextLevel = 100.0f;
        level = 1;
        experience = 0.0f;

        LevelDamage = 0;
        LevelCadence = 0;
        LevelReload = 0;
        LevelAim = 0;
        LevelAmmo = 0;

        LevelStamina = 0;
        LevelLife = 0;

        upgradeAmmo = 5;
        upgradeReload = 0.05f;
        upgradeCadence = 0.05f;

        RefreshUI();
    }

    public void RefreshUI()
    {
        UIManager.GetInstance().SetUIText(UIManager.GetInstance().moneyText, "money : " + money);
    }

    public void RemoveMoney(int amount)
    {
        if (money - amount >= 0)
            money -= amount;
        RefreshUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        RefreshUI();
    }

    public int GetMoney()
    {
        return money;
    }

    public void AddXP(float amount)
    {
        if(experience + amount < experienceToNextLevel)
            experience += amount;
        else
        {
            float much = experienceToNextLevel - experience + amount;
            experienceToNextLevel = 100.0f;
            level++;
            experience = much;
        }
    }

    public void AddStatLife(int value)
    {
        LevelLife++;
        GetComponent<Player>().AddMaxLife(value);
    }

    public void AddStatStamina(int value)
    {
        LevelStamina++;
        GetComponent<DeplacementPlayer>().AddMaxStamina(value);
    }

    public void AddStatDamage(int value)
    {
        LevelDamage++;
        GetComponent<Player>().gun.GetComponent<Gun>().damage += value;
    }

    public void AddStatReload()
    {
        LevelReload++;
        GetComponent<Player>().gun.GetComponent<Gun>().reloadTime -= upgradeReload;
    }

    public void AddStatCadence()
    {
        LevelCadence++;
        GetComponent<Player>().gun.GetComponent<Gun>().fireRate -= upgradeCadence;
    }

    public void AddStatAmmo()
    {
        LevelAmmo++;
        GetComponent<Player>().gun.GetComponent<Gun>().maxAmmoLoaded += upgradeAmmo;
    }
}
