using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAndMenuButton : MonoBehaviour
{
    Animator _cAnim;
    SceneChange _sceneChange;
    string _sceneName;

    private void Awake()
    {
        _cAnim = GetComponent<Animator>();
        _sceneChange = GetComponent<SceneChange>();
    }

    public void PlayButton()
    {
        _cAnim.SetTrigger("PlayButton");
        _sceneName = "ArcheryGame";
    }

    public void MenuButton()
    {
        _cAnim.SetTrigger("MenuButton");
        _sceneName = "MainMenu";
    }

    void OnButtonAnimEnded()
    {
        _cAnim.SetTrigger("FadeOut");
    }

    void OnFadeOutTransitionComplete()
    {
        if (SceneChange.sceneName.Equals("ArcheryGame_GameOver"))
            GetComponent<MainCanvas_EndGame>().FadeOutTransitionComplete();
        _sceneChange.changeScene(_sceneName);

    }
    void OnFadeInTransitionComplete()
    {
        if (SceneChange.sceneName.Equals("ArcheryGame_GameOver"))
            GetComponent<MainCanvas_EndGame>().FadeInTransitionComplete();
    }
}
