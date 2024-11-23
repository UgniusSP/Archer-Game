using UnityEngine;
// ReSharper disable All

public class RotateBow : MonoBehaviour
{
    public void Update()
    {
        try
        {
            RotateBowToMouse();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error in Update method: {ex.Message}");
        }
    }

    private void RotateBowToMouse()
    {
        try
        {
            if (Camera.main != null)
            {
                Vector2 mousePosition = GetMouseWorldPosition();
                Vector2 bowPosition = transform.position;
                Vector2 direction = mousePosition - bowPosition;
                
                float angle = CalculateAngle(direction);
                RotateBowToDirection(direction);
            }
            else
            {
                Debug.LogError("Main camera not found");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error rotating bow: {ex.Message}");
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        try
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error getting mouse world position: {ex.Message}");
            return Vector2.zero;
        }
    }

    private float CalculateAngle(Vector2 direction)
    {
        try
        {
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error calculating angle: {ex.Message}");
            return 0f; 
        }
    }

    private void RotateBowToDirection(Vector2 direction)
    {
        try
        {
            transform.right = direction;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error rotating bow to direction: {ex.Message}");
        }
    }
}
