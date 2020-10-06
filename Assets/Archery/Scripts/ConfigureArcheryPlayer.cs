using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ConfigureArcheryPlayer : MonoBehaviour
{
    private ArcheryGameSetupManager archeryGameSetupManager;
    int playerNum;
    public TMP_Text placeHolder;
    void Start()
    {
        archeryGameSetupManager = FindObjectOfType<ArcheryGameSetupManager>();
        string playerName = this.gameObject.name;
        playerNum = int.Parse(playerName.Substring(playerName.Length - 1));
        placeHolder.text = "Player " + playerNum;
    }

    public void ChangeColorOfPlayer(Slider slider)
    {
        string playerNumS = this.gameObject.name.Substring(this.gameObject.name.Length - 1).ToString();
        playerNum = int.Parse(playerNumS);
        //print(playerNum);
        //print(int.Parse(playerNum));
        switch (slider.value)
        {
            default:
                break;
            case 0:
                archeryGameSetupManager.AddColorToPlayer("red", playerNum);
                break;
            case 1:
                archeryGameSetupManager.AddColorToPlayer("blue", playerNum);
                break;
            case 2:
                archeryGameSetupManager.AddColorToPlayer("green", playerNum);
                break;
            case 3:
                archeryGameSetupManager.AddColorToPlayer("pink", playerNum);
                break;
            case 4:
                archeryGameSetupManager.AddColorToPlayer("yellow", playerNum);
                break;
        }
    }
    public void SetPlayerName(TMP_Text text)
    {
        archeryGameSetupManager.SetNameOfPlayer(text.text.ToString(), playerNum);
    }
}
