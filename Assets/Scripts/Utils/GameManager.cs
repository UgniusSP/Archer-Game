using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private TextMeshProUGUI score;
        private static int _points;

        public static int Points
        {
            get => _points;
            private set
            {
                _points = value;
                Instance?.UpdateScoreDisplay();
            }
        }

        static GameManager()
        {
            _points = 0;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void UpdateScoreDisplay()
        {
            score.text = _points.ToString();
            ProgressBar.Instance.UpdateProgressBar(_points);
        }

        public void UpdatePoints(int point)
        {
            Points += point;
        }

        public void GameOver()
        {
            Points = 0;
            LoadScene("GameOver");
        }

        public void LevelComplete()
        {
            Points = 0;
            LoadScene("LevelCompleted");
        }

        private void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public int GetPoints()
        {
            return _points;
        }
    }
}