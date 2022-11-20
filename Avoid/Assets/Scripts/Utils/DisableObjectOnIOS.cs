using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Disable object on IOS platform.
	/// </summary>
    public class DisableObjectOnIOS : MonoBehaviour
    {
        void Start()
        {
#if UNITY_IOS
            gameObject.SetActive(false);
#endif
        }

    }
}