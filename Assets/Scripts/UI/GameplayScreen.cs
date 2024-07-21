using Managers;
using Managers.Queue;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameplayScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _child;
        [SerializeField] private TextMeshProUGUI _scoreTextMesh;

        private void Start()
        {
            OnRestart();
            
            QueueManager.RestartAction += OnRestart;
            PlayerStats.PlayerDiedAction += OnPlayerDied;
            ScoreManager.CurrentScoreProperty.ValueChangedEvent += OnScoreChanged;

            OnScoreChanged(0, 0);
        }

        private void OnRestart()
        {
            _child.SetActive(true);
        }

        private void OnPlayerDied()
        {
            _child.SetActive(false);
        }

        private void OnScoreChanged(int previous, int current)
        {
            _scoreTextMesh.text = $"Очков набрано: {current}";
        }
        
        private void OnDisable()
        {
            QueueManager.RestartAction -= OnRestart;
            PlayerStats.PlayerDiedAction -= OnPlayerDied;
            ScoreManager.CurrentScoreProperty.ValueChangedEvent -= OnScoreChanged;
        }
    }
}