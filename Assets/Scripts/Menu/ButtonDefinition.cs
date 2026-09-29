using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class ButtonDefinition : MonoBehaviour
{
    public bool _animated = false;
    public Color _unselectedTint = Color.grey;
    public Color _selectedTint = Color.white;
    public bool _selected = false;
    private Button _button;
    private Image _image;
    private Animator _animator;

    public string _swapToSFX;
    public string _confirmSFX;
    public float _confirmTime;

    private bool _disableControls = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();

        _animated = TryGetComponent<Animator>(out _animator);

        if (!_animated)
        {
            if (_selected)
            {
                _image.color = _selectedTint;
            }
            else
            {
                _image.color = _unselectedTint;
            }
        }
    }

    public void SwappedTo()
    {
        _selected = true;

        if (!string.IsNullOrEmpty(_swapToSFX))
        {
            AudioManager.Instance.Play(_swapToSFX);
        }

        _image.color = _selectedTint;

        if (_animated)
        {
            _animator.SetBool("Selected", _selected);
        }
        else
        {
            _image.color = _selectedTint;
        }
    }

    public void SwappedOff()
    {
        _selected = false;

        if (_animated)
        {
            _animator.SetBool("Selected", _selected);
        }
        else
        {
            _image.color = _selectedTint;
        }

        _image.color = _unselectedTint;
    }

    public IEnumerator ClickButton()
    {
        if(!_disableControls)
        {
            _disableControls = true;

            _button.onClick.Invoke();

            if (!string.IsNullOrEmpty(_confirmSFX))
            {
                AudioManager.Instance.Play(_confirmSFX);
            }

            yield return new WaitForSeconds(_confirmTime);

            _disableControls = false;
        }
    }

    public bool GetDisableControls()
    {
        return _disableControls;
    }
}
