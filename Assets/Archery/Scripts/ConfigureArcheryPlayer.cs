using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConfigureArcheryPlayer : MonoBehaviour
{
    private ArcheryGameSetupManager archeryGameSetupManager;
    void Start()
    {
        archeryGameSetupManager = FindObjectOfType<ArcheryGameSetupManager>();
    }

    public void ChangeColorOfPlayer(Slider slider)
    {
        string playerName = this.gameObject.name;
        switch (slider.value)
        {
            default:
                break;
            case 0:
                archeryGameSetupManager.AddColorToPlayer("red", int.Parse(playerName.Substring(playerName.Length-1)));
                break;
            case 1:
                archeryGameSetupManager.AddColorToPlayer("blue", int.Parse(playerName.Substring(playerName.Length - 1)));
                break;
            case 2:
                archeryGameSetupManager.AddColorToPlayer("green", int.Parse(playerName.Substring(playerName.Length - 1)));
                break;
            case 3:
                archeryGameSetupManager.AddColorToPlayer("pink", int.Parse(playerName.Substring(playerName.Length - 1)));
                break;
            case 4:
                archeryGameSetupManager.AddColorToPlayer("yellow", int.Parse(playerName.Substring(playerName.Length - 1)));
                break;
        }
    }
}
