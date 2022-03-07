using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    public PlayerStats stats;
    public Text[] statsText;
    public Bar experienceBar;
    public Text experienceText;
    public Text moneyText;

    public void OnLevelUpButtonPressed(int i)
    {
        stats.LevelUp(i);
    }

    public void OnEnable()
    {
        experienceBar.SetOnlyMaxValueOnBar(stats.currentLevel * 15);
        experienceBar.SetValueOnBar(stats.experience);
    }

    public void Update()
    {
        //TODO: make event to avoid calling this in Update
        UpdateTexts();
    }
    private void UpdateTexts()
    {
        statsText[0].text = $"{stats.maxHp}";
        statsText[1].text = $"{stats.maxStamina}";
        statsText[2].text = stats.movementSpeed.ToString("0.0");
        statsText[3].text = $"{stats.damage}";
        experienceText.text = $"Level {stats.currentLevel}, exp {stats.experience} / {stats.currentLevel * 15}";
        moneyText.text = $"{stats.money} coins";
        experienceBar.SetMaxValueOnBar(stats.currentLevel * 15);
        experienceBar.SetValueOnBar(stats.experience);
    }
}
