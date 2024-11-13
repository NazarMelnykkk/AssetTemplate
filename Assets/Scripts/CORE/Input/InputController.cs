using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public List<InputActionConfig> InputActions;
    private Dictionary<CharacterAction, InputActionConfig> _actionDictionary;

    private void OnEnable()
    {
        _actionDictionary = new Dictionary<CharacterAction, InputActionConfig>();
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

    public InputActionConfig GetActionByType(CharacterAction action)
    {
        _actionDictionary.TryGetValue(action, out var config);
        return config;
    }
}
