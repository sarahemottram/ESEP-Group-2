using System;
using UnityEngine;

namespace PacMan
{
    public class PlayerLife : MonoBehaviour
    {
        public event Action<int> OnLivesChanged;

        private int lives = 3;
        public int Lives
        {
            get => lives;
            private set
            {
                lives = value;
                OnLivesChanged?.Invoke(lives);
            }
        }
        private void Start() => UIManager.Instance.SubscribeLives(this);

        public void LoseLife() => Lives--;

    }
}
