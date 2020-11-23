using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    Image _bg;
    Toggle _toggle;
    public PlayerPanel_ArcherySetUp _archerySetUp;
    const float _disAlpha = .25f;
    string _name;


    private void Awake()
    {
        _name = this.gameObject.name;
        _bg = GetComponentInChildren<Image>();
        _toggle = GetComponent<Toggle>();
    }
    private void Start()
    {
        _toggle.interactable = PlayerPanel_ArcherySetUp._colorsDic[_name] ? false : true;
        PlayerPanel_ArcherySetUp._toggles[_name].Add(_toggle);
    }
    public void OnValueChange(bool v)
    {
        if (!v)
        {
            _bg.color = new Color(_bg.color.r, _bg.color.g, _bg.color.b, _disAlpha);
            PlayerPanel_ArcherySetUp._colorsDic[_name] = false;
            PlayerPanel_ArcherySetUp._toggles[_name].ForEach(x => x.interactable = true);

        }

        else
        {
            _bg.color = new Color(_bg.color.r, _bg.color.g, _bg.color.b, 1f);
            PlayerPanel_ArcherySetUp._colorsDic[_name] = true;
            PlayerPanel_ArcherySetUp._toggles[_name].ForEach(x => x.interactable = false);
            _archerySetUp.SetPlayerColor(name);
        }
    }
    private void OnDestroy()
    {
        bool selected = false;
        PlayerPanel_ArcherySetUp._toggles[_name].RemoveAt(PlayerPanel_ArcherySetUp._toggles[_name].Count - 1);
        foreach (Toggle t in PlayerPanel_ArcherySetUp._toggles[_name])
        {
            if (t.isOn)
                selected = true;
        }
        if (!selected && PlayerPanel_ArcherySetUp._colorsDic[_name])
        {
            PlayerPanel_ArcherySetUp._colorsDic[_name] = false;
            PlayerPanel_ArcherySetUp._toggles[_name].ForEach(x => x.interactable = true);
        }

    }
}
