using UnityEngine;
using System.Collections;
using System.IO;

public class ShareScreenShot : MonoBehaviour
{
    public static ShareScreenShot Instance;

    private bool _isProcessing;

    private string _shareScore;
    private float _width, _height;

    private string _destination;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ButtonShare()
    {
        _shareScore = GameManager.Instance.currentScore.ToString();

        if (!_isProcessing)
        {
            StartCoroutine(ShareScreenshot());
        }
    }


    private IEnumerator ShareScreenshot()
    {
        _width = Screen.width;
        _height = Screen.height;

        _isProcessing = true;

        yield return new WaitForEndOfFrame();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        // create the texture
        Texture2D screenTexture = new ((int)_width, (int)_height, TextureFormat.RGB24, true);
        // put buffer into texture
        screenTexture.ReadPixels(new Rect(0, 0, _width, _height), 0, 0);
        // apply
        screenTexture.Apply();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        byte[] dataToSave = screenTexture.EncodeToPNG();
        _destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
        File.WriteAllBytes(_destination, dataToSave);
        ShareMethod();
        _isProcessing = false;
    }

    private void ShareMethod()
    {
        if (Application.isEditor) return;

        AndroidJavaClass intentClass = new ("android.content.Intent");
        AndroidJavaObject intentObject = new ("android.content.Intent");
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        AndroidJavaClass uriClass = new ("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + _destination);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Beat my score of " + _shareScore + " in JUMP ROBOT");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Jump Robot");

        intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
        AndroidJavaClass unity = new("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

        currentActivity.Call("startActivity", intentObject);
    }
}