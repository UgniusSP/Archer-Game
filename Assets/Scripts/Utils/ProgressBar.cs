using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class ProgressBar : MonoBehaviour
    {
        public static ProgressBar Instance;
        
        [SerializeField] private Slider progressBar;
        
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
        
        public void Start()
        {
            progressBar.maxValue = 10;
            progressBar.value = 0;
        }
        
        public void UpdateProgressBar(int points)
        {
            progressBar.value = points;
        }
        
    }
}