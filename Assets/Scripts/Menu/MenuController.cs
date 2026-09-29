using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject _activeMenu;

    public List<KeyCode> _increaseVert;
    public List<KeyCode> _decreaseVert;
    public List<KeyCode> _increaseHoriz;
    public List<KeyCode> _decreaseHoriz;
    public List<KeyCode> _confirmButtons;

    private MenuDefinition _activeMenuDefinition;
    private int _activeButton = 0;

    public void Start()
    {
        //update active menu definition at start
        UpdateActiveMenuDefinition();
    }

    public void Update()
    {
        switch(_activeMenuDefinition.GetMenuType())
        {
            case MenuType.HORIZONTAL:
                MenuInput(_increaseHoriz, _decreaseHoriz);
                break;
            case MenuType.VERTICAL:
                MenuInput(_increaseVert, _decreaseVert);
                break;
        }
    }

    private void MenuInput(List<KeyCode> increase, List<KeyCode> decrease)
    {
        int newActive = _activeButton;

        for (int i = 0; i < increase.Count; i++)
        {
            if(Input.GetKeyDown(increase[i]))
            {
                newActive = SwitchCurrentButton(1);
            }
        }

        for (int i = 0; i < decrease.Count; i++)
        {
            if (Input.GetKeyDown(decrease[i]))
            {
                newActive = SwitchCurrentButton(-1);
            }
        }

        for (int i = 0; i < _confirmButtons.Count ; i++)
        {
            if (Input.GetKeyDown(_confirmButtons[i]))
            {
                ClickCurrentButton();
            }
        }

        _activeButton = newActive;
    }

    private int SwitchCurrentButton(int increment)
    {
        if (!_activeMenuDefinition.GetButtonDefinitions()[_activeButton].GetDisableControls())
        {
            int newActive = Utility.WrapAround(_activeMenuDefinition.GetButtonCount(), _activeButton, increment);

            _activeMenuDefinition.GetButtonDefinitions()[_activeButton].SwappedOff();
            _activeMenuDefinition.GetButtonDefinitions()[newActive].SwappedTo();

            return newActive;
        }

        return _activeButton;
    }

    private void ClickCurrentButton()
    {
        if (!_activeMenuDefinition.GetButtonDefinitions()[_activeButton].GetDisableControls())
        {
            StartCoroutine(_activeMenuDefinition.GetButtonDefinitions()[_activeButton].ClickButton());
        }
    }

    public void UpdateActiveMenuDefinition()
    {
        _activeMenuDefinition = _activeMenu.GetComponent<MenuDefinition>();

        if (_activeMenuDefinition != null)
        {
            // Only update music if we are NOT continuing previous music
            if (!_activeMenuDefinition._continuePrevMusic)
            {
                AudioManager.Instance.PlayMusic(_activeMenuDefinition._menuMusic);
            }
            // If _continuePrevMusic is true, do nothing (keep previous music)
        }
        else
        {
            // If there's no valid menu, stop the music
            AudioManager.Instance.StopMusic();
        }
    }

    public void SetActiveMenu(GameObject activeMenu)
    {
        //set active menu
        _activeMenu = activeMenu;

        //make sure to update definition
        UpdateActiveMenuDefinition();

        _activeButton = 0;
    }
}
