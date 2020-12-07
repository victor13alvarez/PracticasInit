using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModels_EndGame : MonoBehaviour
{
    float _playerPositionX_1 = 0f;
    float[] _playerPositionX_2 = { -2f, 2f };
    float[] _playerPositionX_3 = { -4f, 0f,4f };
    float[] _playerPositionX_4 = { -6, -2f, 2f, 6f};
    const float _playerModelYpos = -4f;
    const float _playerModelZposIfLose = 15f;
    const float _playerModeZposIfWin = 11f;

    public GameObject _playerModelPrefab;
    public Material material;


    private void Start()
    {
        SpawnPlayerModels();
    }

    void SpawnPlayerModels()
    {
        GameObject go;
        ArcheryGameManager._players.ForEach(delegate (PlayerInfo currentPlayer)
        {
            go = Instantiate(_playerModelPrefab, this.transform);
            go.name = currentPlayer.playerName;
            switch (ArcheryGameManager._players.Count)
            {
                case 1:
                    go.transform.localPosition = new Vector3(_playerPositionX_1, _playerModelYpos, _playerModeZposIfWin);
                    break;
                case 2:
                    go.transform.localPosition = currentPlayer.playerName.Equals(ArcheryGameManager._playerHasWin) ? new Vector3(_playerPositionX_2[currentPlayer.playerCount - 1], _playerModelYpos, _playerModeZposIfWin) : new Vector3(_playerPositionX_2[currentPlayer.playerCount - 1], _playerModelYpos, _playerModelZposIfLose);
                    break;
                case 3:
                    go.transform.localPosition = currentPlayer.playerName.Equals(ArcheryGameManager._playerHasWin) ? new Vector3(_playerPositionX_3[currentPlayer.playerCount - 1], _playerModelYpos, _playerModeZposIfWin) : new Vector3(_playerPositionX_3[currentPlayer.playerCount - 1], _playerModelYpos, _playerModelZposIfLose);
                    break;
                case 4:
                    go.transform.localPosition = currentPlayer.playerName.Equals(ArcheryGameManager._playerHasWin) ? new Vector3(_playerPositionX_4[currentPlayer.playerCount - 1], _playerModelYpos, _playerModeZposIfWin) : new Vector3(_playerPositionX_4[currentPlayer.playerCount - 1], _playerModelYpos, _playerModelZposIfLose);
                    break;
                default:
                    break;
            }
            Material materialAux = new Material(material)
            {
                color = currentPlayer.playerColor
            };
            go.GetComponentInChildren<SkinnedMeshRenderer>().material = materialAux;
            go.GetComponent<PlayerAnimatorController>().SetTriggerAnim(currentPlayer.playerName.Equals(ArcheryGameManager._playerHasWin) ? "Victory" : "Lose");
            go.transform.LookAt(this.transform.position);

        });
    }

}
