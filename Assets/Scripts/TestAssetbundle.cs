using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TestAssetbundle : MonoBehaviour {

    public static readonly string bundleSavePath = "Assets/StreamingAssets/AssetBundles";
    public static readonly string bundleSuffix = ".ab";

    public RawImage rawImage;

    private static readonly List<string> abList = new List<string> { "chatpng", "chatapng", "shaders","materials" };
    [Range(0, 4)]
    public int startLoadPos;
    private int clickCount;
    
	void Start ()
    {
        clickCount = startLoadPos;
    }

    private Rect rect = new Rect(10, 10, 100, 50);
    private void OnGUI()
    {
        if(GUI.Button(rect, "加载"))
        {
            clickCount++;
        }
    }

    public List<List<Object>> ObjectList = new List<List<Object>>();
    void LoadAB()
    {
        if (startLoadPos < abList.Count && clickCount > startLoadPos)
        {
            var abName = abList[startLoadPos]; startLoadPos++;
            Debug.LogError("Load abName:" + abName + ">>>>>>>>>>>>");
            
            string loadPath = Path.Combine(bundleSavePath, string.Format("{0}{1}", abName, bundleSuffix)); ;
            AssetBundle _bundle = AssetBundle.LoadFromFile(loadPath);

            Object[] objects = _bundle.LoadAllAssets();
            ObjectList.Add(new List<Object>(objects));
            
            if(objects.Length > 0 && (objects[0] is Material))
            {
                Show(objects[0] as Material);
            }
        }
        clickCount++;
    }

    private void Show(Material material)
    {
        Texture2D texture2D = (Texture2D)material.mainTexture;

        Texture2D _MainTextexture2D = (Texture2D)material.GetTexture("_MainTex");
        Texture2D _AlphaTextexture2D = (Texture2D)material.GetTexture("_AlphaTex");
        
        rawImage.material = material;
        material.mainTexture = material.mainTexture;
    }

    private void Update()
    {
        LoadAB();
    }
}
