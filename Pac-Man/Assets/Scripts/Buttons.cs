using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PacMan
{
    public class Retry : MonoBehaviour
    {
        public void RetryButton() => SceneManager.LoadScene("Level 1");

        public void ExitButton() => Application.Quit();
    }
}
