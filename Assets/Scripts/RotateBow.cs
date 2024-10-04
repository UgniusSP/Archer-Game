using UnityEngine;
// ReSharper disable All

public class RotateBow : MonoBehaviour
{
    public void Update()
    {
        if (Camera.main != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 bowPosition = transform.position;
            Vector2 direction = mousePosition - bowPosition;
        
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.right = direction;
            
        }
    }
}
