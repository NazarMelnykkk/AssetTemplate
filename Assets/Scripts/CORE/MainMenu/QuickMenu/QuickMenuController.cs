using UnityEngine;

public class QuickMenuController : MonoBehaviour
{
    [SerializeField] private UIContainerController _uIContainerController;
    private InputActionConfigBase _config;

    private void OnEnable()
    {
        _config = References.Instance.InputController.GetActionByType(CharacterAction.Menu);

        if (_config != null)
        {
            _config.OnPerformedEvent += ToggleQuickMenu;
            _config.Initialize();
        }
    }

    private void OnDisable()
    {
        if (_config != null)
        {
            _config.OnPerformedEvent -= ToggleQuickMenu;
            _config.Cleanup();
        }
    }

    private void ToggleQuickMenu()
    {
        _uIContainerController.Toggle();
    }

}
