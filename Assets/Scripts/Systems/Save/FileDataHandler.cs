using System;
using System.IO;
using Systems.Save.Data;
using UnityEngine;

namespace Systems.Save
{
    public class FileDataHandler
    {
        private readonly string _path;
        private readonly string _name;

        public FileDataHandler(string path, string name)
        {
            _path = path;
            _name = name;
        }

        public GameData Load()
        {
            var fullPath = Path.Combine(_path, _name);
            var gameData = new GameData();

            if (!File.Exists(fullPath)) 
                return gameData;
            
            try
            {
                using var stream = new FileStream(fullPath, FileMode.Open);
                using var reader = new StreamReader(stream);
                    
                var dataToLoad = reader.ReadToEnd();
                gameData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occured when trying to load data from {fullPath}, \n {e}");
            }

            return gameData;
        }

        public void Save(GameData data)
        {
            var fullPath = Path.Combine(_path, _name);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                var dataToStore = JsonUtility.ToJson(data, true);

                using var stream = new FileStream(fullPath, FileMode.Create);
                using var writer = new StreamWriter(stream);
                
                writer.Write(dataToStore);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occured when trying to save data to {fullPath}, \n {e}");
            }
        }

    }
}