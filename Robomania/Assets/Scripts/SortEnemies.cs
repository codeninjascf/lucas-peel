using UnityEngine;

public class SortEnemies : MonoBehaviour
{
    public Canvas canvas;
    public float distanceBetween = 50f;
    
    private void Update()
    {
        DisplayChildren();
    }

    private void DisplayChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        { 
            transform.GetChild(i).position = new Vector3(transform.position.x, transform.position.y - i * distanceBetween * canvas.scaleFactor, transform.position.z);
        }
    }
}
