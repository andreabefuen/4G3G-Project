using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class ShareManager : MonoBehaviour
{
    public Image screenshot;
    string ShareSubject = "Picture Share";
    string shareLink = "Test Link" + "\nhttp://stackoverflow.com/questions/36512784/share-image-on-android-application-from-unity-game";
    string textToShare = "Text To share";

    public string shareSubject, shareMessage;
    bool isProcessing = false;
    bool isFocus = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }

    public void OnShareButton()
    {
        shareMessage = "My island has " + Player.instance.pollutionSlider.value + " of pollution and " + Player.instance.happinessSlider.value + " of happiness!\nBecome a tester! \nhttps://play.google.com/apps/testing/com.GasCan.AlternativeEnergy";
        shareSubject = "ALTERNATIVE ENERGY";
        //Invoke("ShareScreenshot", 1f);
        ShareScreenshot();
    }

    private void TextureToScrennshot()
    {
        
    }

    void ShareScreenshot()
    {
        
#if UNITY_ANDROID
        if (!isProcessing)
        {
            StartCoroutine(ShareScreenshotAndroid());
        }
#else
		Debug.Log("No sharing set up for this platform.");
#endif
    }

    public void NormalScreenShot()
    {
        string fileName = "Screenshot" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + "`.png";
        string screenShotPath = Application.persistentDataPath + "/" + fileName;
        ScreenCapture.CaptureScreenshot(fileName, 1);

        RenderTexture renderTexture = Camera.main.targetTexture;

    }

    public IEnumerator ShareScreenshotAndroid()
    {
        Debug.Log("Share");
        isProcessing = true;
        yield return new WaitForSeconds(2f);

        string fileName = "Screenshot" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + "`.png";
        string screenShotPath = Application.persistentDataPath + "/" + fileName;
        ScreenCapture.CaptureScreenshot(fileName, 1);

        yield return new WaitForSeconds(0.5f);
        if (!Application.isEditor)
        {
            //Create intent for action send
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            //create image URI to add it to the intent
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + screenShotPath);

            //put image and string extra
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("setType", "image/png");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your high score");
            currentActivity.Call("startActivity", chooser);
        }
       
        yield return new WaitUntil(() => isFocus);
        isProcessing = false;

    }
    public void TakeAShot()
    {
        StartCoroutine("CaputureScreenShot");
    }

    IEnumerator CaputureScreenShot()
    {
        string fileName = "Screenshot" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second +".png";
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + fileName);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Capture image");
        SaveScreenShot(fileName, GetScreenshotImage(Application.persistentDataPath + "/" + fileName));
        //ShowScreenshot(Application.persistentDataPath +"/"+ fileName);
    }

    Texture2D GetScreenshotImage (string filePath)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if(File.Exists(filePath))
        {
            fileBytes = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
            texture.LoadImage(fileBytes);
        }
        return texture;
    }

    void ShowScreenshot(string filePath)
    {
        Texture2D txt = GetScreenshotImage(filePath);
        Sprite sprite = Sprite.Create(txt, new Rect(0, 0, txt.width, txt.height), new Vector2(0.5f, 0.5f));

        screenshot.sprite = sprite;
        screenshot.gameObject.SetActive(true);
    }

    void SaveScreenShot(string fileName, Texture2D textureScreenshot)
    {
        byte[] imageBytes = textureScreenshot.EncodeToPNG();

        System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/ScreenShot");
        string path = Application.persistentDataPath + "/ScreenShot" + "/" + fileName;
        System.IO.File.WriteAllBytes(path, imageBytes);

        StartCoroutine(ShareScreenshot(path));
    }

    IEnumerator ShareScreenshot (string destination)
    {


        Debug.Log(destination);


        if (!Application.isEditor)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);

            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("setType", "image/png");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), textToShare + shareLink);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), ShareSubject);

            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "YO! I Love Who Lurks!");
            currentActivity.Call("startActivity", jChooser);
        }
        Debug.Log("done!");
        yield return null;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
