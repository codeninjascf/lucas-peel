using UnityEngine;
using System.Collections;

namespace Avoidance
{
	/// <summary>
	/// Disable object on android platform.
	/// </summary>
    public class DisableObjectOnAndroid : MonoBehaviour
    {
        void Start()
        {
#if UNITY_ANDROID
            gameObject.SetActive(false);
#endif
        }
    }
}