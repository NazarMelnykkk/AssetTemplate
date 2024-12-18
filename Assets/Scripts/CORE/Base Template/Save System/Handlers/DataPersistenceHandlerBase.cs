using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceHandlerBase : MonoBehaviour
{
    public GameData GameData;
    protected FileDataHandler _fileDataHandler;
    protected string CurrentProfileID = "Plague.json";

    protected virtual void Awake()
    {
        _fileDataHandler = new FileDataHandler(Application.persistentDataPath, CurrentProfileID);
    }

    protected virtual void Start()
    {
        LoadGame();

        GlobalReferencesContainer.Instance.SceneLoader.OnSceneLoadEvent += LoadGame;
        GlobalReferencesContainer.Instance.SceneLoader.OnSceneUnloadEvent += SaveGame;
    }

    protected virtual void NewGame()
    {
        GameData = new GameData();
    }

    public virtual void LoadGame()
    {
        GameData = _fileDataHandler.Load(CurrentProfileID);

        if (GameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded. profile id");
            NewGame();
            return;
        }

        foreach (IDataPersistence dataPersistenceObject in FindAllDataPersistenceObjects())
        {
            dataPersistenceObject.LoadData(GameData);
        }
        Debug.Log("Game Loaded");
    }

    [ContextMenu("SAVE")]
    public virtual void SaveGame()
    {
        if (GameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be saved");
            return;
        }

        foreach (IDataPersistence dataPersistenceObject in FindAllDataPersistenceObjects())
        {
            dataPersistenceObject.SaveData(GameData);
        }

        _fileDataHandler.Save(GameData, CurrentProfileID);

    }

    protected virtual void OnApplicationQuit()
    {
        SaveGame();
    }

    protected virtual List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    protected virtual bool HasGameData()
    {
        return GameData != null;
    }


    protected virtual Dictionary<string, GameData> GetAllprofilesGameData()
    {
        return _fileDataHandler.LoadAllProfiles();
    }
}
