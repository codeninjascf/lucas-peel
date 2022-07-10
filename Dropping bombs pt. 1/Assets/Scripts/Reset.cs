using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    // Called when the object collides with something
    private void OnCollisionEnter(Collision collision)
    {
        // Loads the initial scene, resetting the game
        SceneManager.LoadScene(0);
    }
}
