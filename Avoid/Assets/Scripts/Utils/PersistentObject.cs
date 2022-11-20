using UnityEngine;


namespace Avoidance
{
    /// <summary>
    /// Ensures object persists between scenes. 
	/// Requires object to have a unique tag.
    /// </summary>
    public class PersistentObject : MonoBehaviour 
    {
        
        void Awake()
        {
           GameObject[] others = GameObject.FindGameObjectsWithTag(gameObject.tag);

            foreach (var o in others)
            {
                if (o.GetInstanceID() < this.gameObject.GetInstanceID())
                {
                    DestroyImmediate(gameObject);
                    return;
                }
            }   

           DontDestroyOnLoad(gameObject);
            
        }
    }
}