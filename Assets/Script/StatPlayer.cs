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
}

    public void RemoveMoney(int amount)
    {
        if (money - amount > 0)
            money -= amount;
    }

    public void AddXP(float amount)
    {
        if(experience + amount < experienceToNextLevel)
            experience += amount;
        else
        {
            float much = experienceToNextLevel - amount;
            experienceToNextLevel = 100.0f;
            level++;
            experience = much;
        }
    }
}
