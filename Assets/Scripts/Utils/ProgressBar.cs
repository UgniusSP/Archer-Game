using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class ProgressBar : MonoBehaviour
    {
        public static ProgressBar Instance;
        
        [SerializeField] private Slider progressBar;
        [SerializeField] private int maxPoints;
        
        private void Awake()
        {
            if (Instance is null)
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
            progressBar.maxValue = maxPoints;
            progressBar.value = 0;
        }
        
        public void UpdateProgressBar(int points)
        {
            progressBar.value = points;
        }

        public int GetMaxPoints()
        {
            return maxPoints;
        }
        
        
    }
}