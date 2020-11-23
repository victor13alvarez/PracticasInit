using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPanel_ArcherySetUp : MonoBehaviour
{
    public static Dictionary<string, bool> _colorsDic = new Dictionary<string, bool>(); //Just to be sure that every player has a different color inGame
    public static Dictionary<string, List<Toggle>> _toggles = new Dictionary<string, List<Toggle>>();
    public static string[] colorNames = { "RED", "BLUE", "GREEN", "PINK", "ORANGE" };

    public TMP_Text placeHolder; //In order a player does not want to introduce its name

    int playerNum;

    const int _totalColors = 5;
    const string playerPanelName = "Player ";

    Toggle[] toggles;

    private void Awake()
    {
        GetComponent<Animator>().SetTrigger("PlayerPanelSpawn");
        toggles = GetComponentsInChildren<Toggle>();

        if (_colorsDic.Count == 0)
            CreateDictionaries();
 
        placeHolder.text = this.gameObject.name = playerPanelName + (ArcheryGameManager._players.Count +1);
        playerNum = ArcheryGameManager._players.Count;
        
    }

    private void CreateDictionaries()
    {
        for (int i = 0; i < _totalColors; i++)
        {
            _colorsDic.Add(colorNames[i], false);
            _toggles.Add(colorNames[i], new List<Toggle>());
        }
    }

    private void Start()
    {
        GenerateFreeColor();
    }

    void GenerateFreeColor() //Set the panel color to the first available color
    {
        int currentColor = 0;
        while (_colorsDic[colorNames[currentColor]])
            currentColor += 1;

        toggles[currentColor].isOn = true;
    }

    public void SetPlayerColor(string color)
    {
        switch (color)
        {
            case "RED":
                ArcheryGameManager._players[playerNum].playerColor = Color.red;
                break;
            case "BLUE":
                ArcheryGameManager._players[playerNum].playerColor = Color.blue;
                break;
            case "GREEN":
                ArcheryGameManager._players[playerNum].playerColor = Color.green;
                break;
            case "PINK":
                ArcheryGameManager._players[playerNum].playerColor = Color.magenta;
                break;
            case "ORANGE":
                ArcheryGameManager._players[playerNum].playerColor = new Color(1f, .5f, 0f, 1f);
                break;
            default:
                break;
        }
    }

    public void SetPlayerName(TMP_Text text)
    {
        ArcheryGameManager._players[playerNum].playerName = text.text;
    }

    public void DestroyPanel()
    {
        GetComponent<Animator>().SetTrigger("PlayerPanelDestroy");
    }

    public void DestroyPanelCallback()
    {
        Destroy(this.gameObject);
    }
}