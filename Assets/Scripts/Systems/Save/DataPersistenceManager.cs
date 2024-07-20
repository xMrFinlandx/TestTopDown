using System.Collections.Generic;
using System.Linq;
using Systems.Save.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.Save
{
    public static class DataPersistenceManager
    {
        private const string _FILENAME = "GameProgress.json";
        
        private static readonly FileDataHandler _fileDataHandler = new(Application.persistentDataPath, _FILENAME);
        
        private static GameData _gameData = new();
        private static List<IDataPersistence> _dataPersistenceObjects = new();

        public static void Save()
        {
            _dataPersistenceObjects = GetAllPersistenceObjects();

            foreach (var dataPersistence in _dataPersistenceObjects)
            {
                dataPersistence.SaveData(_gameData);
            }

            _fileDataHandler.Save(_gameData);
        }

        public static void Load()
        {
            _gameData = _fileDataHandler.Load();
            
            _dataPersistenceObjects = GetAllPersistenceObjects();
            
            foreach (var dataPersistence in _dataPersistenceObjects)
            {
                dataPersistence.LoadData(_gameData);
            }
        }
        
        private static List<IDataPersistence> GetAllPersistenceObjects()
        {
            return Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>().ToList();
        }
    }
}