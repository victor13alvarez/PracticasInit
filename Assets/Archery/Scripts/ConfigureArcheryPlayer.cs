using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ConfigureArcheryPlayer : MonoBehaviour
{
    private ArcheryGameSetupManager archeryGameSetupManager;
    static Dictionary<int, bool> selectColor = new Dictionary<int, bool>();
    int playerNum;
    public TMP_Text placeHolder;
    public Image image;
    int currentColor = 0;
    Color[] colors = { Color.red ,Color.blue , Color.green , Color.magenta , Color.yellow };
    string playerNumS;

    bool onValueChangedIgnore;

    [SerializeField] Slider playerSlider;

    private void Awake()
    {
        if(selectColor.Count == 0)
            for (int i = 0; i < 5; i++)
                selectColor.Add(i, false);
        onValueChangedIgnore = false;

    }
    void Start()
    {
        archeryGameSetupManager = FindObjectOfType<ArcheryGameSetupManager>();
        playerNumS = this.gameObject.name.Substring(this.gameObject.name.Length - 1).ToString();
        playerNum = int.Parse(playerNumS);
        placeHolder.text = "Player " + playerNum;
        GenerateFreeColor();
    }

    void GenerateFreeColor()
    {
        while (selectColor[currentColor])
            currentColor += 1;

        selectColor[currentColor] = true;
        image.color = colors[currentColor];
        onValueChangedIgnore = !onValueChangedIgnore;
        playerSlider.value = currentColor;
        onValueChangedIgnore = !onValueChangedIgnore;
    }

    public void ChangeColorOfPlayer(Slider slider)
    {
        if (onValueChangedIgnore)
            return;
        if (CheckIfColorIsAlreadyPicked((int)slider.value))
        {
            selectColor[currentColor] = false;
            currentColor = (int)slider.value;
            selectColor[currentColor] = true;
            archeryGameSetupManager.AddColorToPlayer(colors[currentColor], playerNum);
            image.color = colors[currentColor];
        }
        else
        {
            onValueChangedIgnore = !onValueChangedIgnore;
            playerSlider.value = currentColor;
            onValueChangedIgnore = !onValueChangedIgnore;
        }
    }

    private bool CheckIfColorIsAlreadyPicked(int value)
    {
        if (selectColor[value] == true)
            return false;
        return true;
    }

    public void SetPlayerName(TMP_Text text)
    {
        archeryGameSetupManager.SetNameOfPlayer(text.text.ToString(), playerNum);
    }
}
