using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [field: SerializeField] public List<InputActionConfigBase> InputActions {  get; private set; }

    private Dictionary<CharacterAction, InputActionConfigBase> _actionDictionary;

    private void OnEnable()
    {
        _actionDictionary = new Dictionary<CharacterAction, InputActionConfigBase>();
        foreach (var action in InputActions)
        {
            if (action is IInputAction inputAction)
            {
                inputAction.Initialize();
            }

            if (!_actionDictionary.ContainsKey(action.Action))
            {
                _actionDictionary[action.Action] = action;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var action in InputActions)
        {
            if (action is IInputAction inputAction)
            {
                inputAction.Cleanup();
            }
        }

        _actionDictionary.Clear();
    }

    public InputActionConfigBase GetActionByType(CharacterAction action)
    {
        _actionDictionary.TryGetValue(action, out var config);
        return config;
    }
}
