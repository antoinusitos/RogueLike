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
    public int LevelAmmo = 0;

    public int LevelStamina = 0;
    public int LevelLife = 0;

    public float upgradeReload;
    public float upgradeCadence;
    public int upgradeAmmo;

    public AudioSource buyShop;

    void Start()
    {
        money = 100;
        experienceToNextLevel = 100.0f;
        level = 1;
        experience = 0.0f;

        LevelDamage = 0;
        LevelCadence = 0;
        LevelReload = 0;
        LevelAmmo = 0;

        LevelStamina = 0;
        LevelLife = 0;

        upgradeAmmo = 5;
        upgradeReload = 0.05f;
        upgradeCadence = 0.05f;

        buyShop = SoundManager.instance.buyShop.GetComponent<AudioSource>();

        RefreshUI();
    }

    public void RefreshUI()
    {
        UIManager.GetInstance().SetUIText(UIManager.GetInstance().moneyText, " : " + money);
    }

    public void RemoveMoney(int amount)
    {
        if (money - amount >= 0)
            money -= amount;

        buyShop.Play();
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
        if (GetComponent<Player>().gun.GetComponent<Gun>().reloadTime < 0)
            GetComponent<Player>().gun.GetComponent<Gun>().reloadTime = 0;
    }

    public void AddStatCadence()
    {
        LevelCadence++;
        GetComponent<Player>().gun.GetComponent<Gun>().fireRate -= upgradeCadence;
        if (GetComponent<Player>().gun.GetComponent<Gun>().fireRate < 0)
            GetComponent<Player>().gun.GetComponent<Gun>().fireRate = 0;
    }

    public void AddStatAmmo()
    {
        LevelAmmo++;
        GetComponent<Player>().gun.GetComponent<Gun>().maxAmmoLoaded += upgradeAmmo;
    }

    public void Load(int TheLevelReload, int TheLevelCadence, int TheLevelAmmo, int TheLevel, int TheMoney,
        float TheExperienceNextLevel, float TheExperience, int TheLevelDamage, int TheLevelLife, int TheMaxLife, 
        float TheStaminaMax, float TheDamage)
    {
        LevelDamage = TheLevelDamage;
        LevelReload = TheLevelReload;
        LevelStamina = TheLevelReload;
        LevelCadence = TheLevelCadence;
        LevelAmmo = TheLevelAmmo;
        LevelLife = TheLevelLife;

        for (int i = 0; i < TheLevelReload; ++i)
        {
            GetComponent<Player>().gun.GetComponent<Gun>().reloadTime -= upgradeReload;
            if (GetComponent<Player>().gun.GetComponent<Gun>().reloadTime < 0)
                GetComponent<Player>().gun.GetComponent<Gun>().reloadTime = 0;
        }
        for (int j = 0; j < TheLevelCadence; ++j)
        {
            GetComponent<Player>().gun.GetComponent<Gun>().fireRate -= upgradeCadence;
            if (GetComponent<Player>().gun.GetComponent<Gun>().fireRate < 0)
                GetComponent<Player>().gun.GetComponent<Gun>().fireRate = 0;
        }
        for (int k = 0; k < TheLevelAmmo; ++k)
        {
            GetComponent<Player>().gun.GetComponent<Gun>().maxAmmoLoaded += upgradeAmmo;
        }

        GetComponent<Player>().gun.GetComponent<Gun>().MaxAmmo();

        level = TheLevel;
        money = TheMoney;
        experienceToNextLevel = TheExperienceNextLevel;
        experience = TheExperience;

        GetComponent<Player>().SetMaxLife(TheMaxLife);
        GetComponent<Player>().SetLife(TheMaxLife);

        GetComponent<DeplacementPlayer>().SetStaminaMax(TheStaminaMax);
        GetComponent<DeplacementPlayer>().SetStamina(TheStaminaMax);

        GetComponent<Player>().gun.GetComponent<Gun>().SetDamage(TheDamage);
    }
}
