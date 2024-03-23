using UnityEngine;
using UnityEngine.UI;
namespace PacMan
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }

        public Text livesText;

        public Text scoreText;

        public void SubscribeLives(PlayerLife playerLife)
        {
            playerLife.OnLivesChanged += lives =>
            {

                livesText.text = lives.ToString();
            };

            livesText.text = playerLife.Lives.ToString();
        }

        public void SubscribeScore(PelletController pelletController)
        {
            pelletController.OnScoreChanged += score =>
            {
                scoreText.text = score.ToString();
            };
            scoreText.text = pelletController.Score.ToString();
        }
    }
}