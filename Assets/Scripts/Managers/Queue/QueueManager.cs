using System;
using UnityEngine;
using Utilities;

namespace Managers.Queue
{
    public class QueueManager : Singleton<QueueManager>
    {
        [SerializeField] private QueueElement[] _queue;

        public static event Action RestartAction;
        
        public void Restart()
        {
            RestartAction?.Invoke();
            
            foreach (var element in _queue)
            {
                element.Enable();
            }
        }

        private void Start()
        {
            Restart();
        }
    }
}