using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utils
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private TextMeshProUGUI score;
        private static int _points;
    
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

        public void UpdatePoints(int point)
        {
            _points += point;
            score.text = "" + _points;
            
            ProgressBar.Instance.UpdateProgressBar(_points);
            
        }

        public void GameOver()
        {
            _points = 0;
            
            SceneManager.LoadScene("GameOver");
        }

        public void LevelComplete()
        {
            _points = 0;
            
            SceneManager.LoadScene("LevelCompleted");
        }
        
        public int GetPoints()
        {
            return _points;
        }
    }
}