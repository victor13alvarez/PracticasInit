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
    public Image image;
    void Start()
    {
        archeryGameSetupManager = FindObjectOfType<ArcheryGameSetupManager>();
        string playerName = this.gameObject.name;
        playerNum = int.Parse(playerName.Substring(playerName.Length - 1));
        placeHolder.text = "Player " + playerNum;
        image.color = Color.red;
    }

    public void ChangeColorOfPlayer(Slider slider)
    {
        string playerNumS = this.gameObject.name.Substring(this.gameObject.name.Length - 1).ToString();
        playerNum = int.Parse(playerNumS);
        switch (slider.value)
        {
            default:
                break;
            case 0:
                archeryGameSetupManager.AddColorToPlayer("red", playerNum);
                image.color = Color.red;
                break;
            case 1:
                archeryGameSetupManager.AddColorToPlayer("blue", playerNum);
                image.color = Color.blue;
                break;
            case 2:
                archeryGameSetupManager.AddColorToPlayer("green", playerNum);
                image.color = Color.green;
                break;
            case 3:
                archeryGameSetupManager.AddColorToPlayer("pink", playerNum);
                image.color = Color.magenta;
                break;
            case 4:
                archeryGameSetupManager.AddColorToPlayer("yellow", playerNum);
                image.color = Color.yellow;
                break;
        }
    }
    public void SetPlayerName(TMP_Text text)
    {
        archeryGameSetupManager.SetNameOfPlayer(text.text.ToString(), playerNum);
    }
}
