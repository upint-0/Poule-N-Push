using UnityEngine;

namespace AgriLife
{
    public class GameManager : MonoBehaviour
    {
        public Camera MainCamera { get; private set; }
        public bool IsPaused { get; set; }
        public static GameManager Instance { get; private set; }

        private SceneLoader _sceneLoader;

        public void LoadMainMenu()
        {
            _sceneLoader.LoadLevel(0);
        }

        public void StartNewGame()
        {
            _sceneLoader.LoadLevel(1);
        }

        public void LoadQuitMenu()
        {
            _sceneLoader.LoadLevel(2);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Pause()
        {
            IsPaused = true;
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            IsPaused = false;
            Time.timeScale = 1f;
        }

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            MainCamera = Camera.main;
            _sceneLoader = GetComponent<SceneLoader>();
        }
    }
}