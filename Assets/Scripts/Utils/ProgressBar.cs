using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class ProgressBar : MonoBehaviour
    {
        public static ProgressBar Instance { get; private set; }

        [SerializeField] private Slider progressBar;
        [SerializeField] private int maxPoints;

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

            if (progressBar == null)
            {
                Debug.LogError("ProgressBar is not assigned in the inspector.");
            }
        }

        private void Start()
        {
            if (progressBar != null)
            {
                progressBar.maxValue = maxPoints;
                progressBar.value = 0;
            }
        }

        public void UpdateProgressBar(int points)
        {
            if (progressBar != null)
            {
                progressBar.value = Mathf.Clamp(points, 0, maxPoints); 
            }
        }

        public int GetMaxPoints()
        {
            return maxPoints;
        }
        
    }
}