using System;
using UnityEngine;
using UnityEngine.Rendering;


public class Screenshot : MonoBehaviour
{
    public bool isAlphaBackground = true;
    public Camera cam;
    public int width = 640;
    public int height = 480;
    public GameObject listObjects;
    public float shotDelay;

    private bool startShot = false;
    private int index;
    private float wait;

    private void Start()
    {
        if(cam == null)
            cam = Camera.main;
    }

    private void FixedUpdate()
    {
        if (!startShot && Input.GetKeyDown(KeyCode.Space))
        {
            index = 0;
            startShot = true;
            wait = 0;
        }
        if (startShot)
        {
            if (index < listObjects.transform.childCount)
            {
                if (wait > shotDelay)
                {
                    Debug.Log(wait);
                    Transform transform = listObjects.transform.GetChild(index);
                    transform.gameObject.SetActive(true);
                    SaveFile(transform.gameObject.name);
                    transform.gameObject.SetActive(false);
                    index++;
                    wait = 0;
                } else
                {
                    wait += Time.deltaTime;
                }

            } else
            {
                startShot = false;
            }
        }
    }

    protected void SaveFile(string filename)
    {
        Texture2D screenShot = Shot();

        byte[] bytes = screenShot.EncodeToPNG();
        var dirPath = Application.dataPath + "/Thumbnails";
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        System.IO.File.WriteAllBytes(dirPath + "/" + filename + ".png", bytes);
        //System.IO.File.WriteAllBytes(dirPath + "/R_" + DateTime.Now.ToString("yyy-MM-dd-HH-mm-ss") + ".png", bytes);
    }

    protected Texture2D Shot()
    {

        int w = width;
        int h = height;
        RenderTexture rt = new RenderTexture(w, h, 32);
        cam.targetTexture = rt;
        Texture2D screenShot = new Texture2D(w, h, TextureFormat.ARGB32, false);
        CameraClearFlags clearFlags = cam.clearFlags;
        if (isAlphaBackground)
        {
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = Color.clear;
        }
        cam.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, w, h), 0, 0);
        screenShot.Apply();
        cam.targetTexture = null;
        RenderTexture.active = null;
        DestroyImmediate(rt);
        cam.clearFlags = clearFlags;
        return screenShot;
    }

}
