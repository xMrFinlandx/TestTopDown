using Managers;
using Managers.Queue;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class ResultsScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _child;
        [SerializeField] private TextMeshProUGUI _resultTextMesh;
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _mainMenuButton;

        private void Start()
        {
            OnRestart();
            
            PlayerStats.PlayerDiedAction += OnPlayerDied;
            ScoreManager.ScoreSavedAction += OnScoreChanged;
            QueueManager.RestartAction += OnRestart;
            _retryButton.onClick.AddListener(Retry);
            _mainMenuButton.onClick.AddListener(LoadMainMenu);
        }

        private void OnRestart()
        {
            _child.SetActive(false);
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        private void Retry()
        {
            QueueManager.Instance.Restart();;
        }

        private void OnPlayerDied()
        {
            _child.SetActive(true);
        }

        private void OnScoreChanged(int best, int current)
        {
            _resultTextMesh.text = best < current ? $"Новый рекорд: {current}" : $"Счет: {current}";
        }
        
        private void OnDestroy()
        {
            PlayerStats.PlayerDiedAction -= OnPlayerDied;
            ScoreManager.ScoreSavedAction -= OnScoreChanged;
            QueueManager.RestartAction -= OnRestart;
            _retryButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
        }
    }
}