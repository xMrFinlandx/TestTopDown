using System;
using Systems.Save;
using Systems.Save.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MenuScreen : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _scoreTextMesh;

        public void LoadData(GameData data)
        {
            _scoreTextMesh.text = $"Лучший результат: {data.BestScore}";
        }

        public void SaveData(GameData data)
        {
        }
        
        private void Start()
        {
            DataPersistenceManager.Load();
            _startButton.onClick.AddListener(LoadGameplayScene);
        }

        private void LoadGameplayScene()
        {
            SceneManager.LoadScene(1);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveAllListeners();
        }
    }
}