using UnityEngine;

public class GlobalReferencesContainer : MonoBehaviour
{
    public static GlobalReferencesContainer Instance;

    [field: SerializeField] public DataPersistenceHandlerBase DataPersistenceHandlerBase { get; private set; }
    [field: SerializeField] public AudioHandler AudioHandler { get; private set; }
    [field: SerializeField] public InputController InputController { get; private set; }
    [field: SerializeField] public SceneLoader SceneLoader { get; private set; }
    [field: SerializeField] public GraphicsHandler GraphicsHandler { get; private set; }

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
