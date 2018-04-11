using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildAssetBundles : MonoBehaviour {
    
    [MenuItem("Build Assetbundles/Build")]
    public static void Build()
    {
        List<AssetBundleBuild> assetBundleBuilds = new List<AssetBundleBuild>();
        assetBundleBuilds.Add( GetShaders() );
        assetBundleBuilds.Add( GetChatPng() );
        assetBundleBuilds.Add( GetChatAPng() );
        assetBundleBuilds.Add( GetMaterials() );
        for(int i = 0; i < assetBundleBuilds.Count; i++)
        {
            assetBundleBuilds[i] = HadleAssetBundleBuild(assetBundleBuilds[i]);
        }
        
        AssetBundleManifest assetBundleManifest = BuildPipeline.BuildAssetBundles(TestAssetbundle.bundleSavePath, assetBundleBuilds.ToArray(),
                BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.DeterministicAssetBundle,
                EditorUserBuildSettings.activeBuildTarget);

        EditorUtility.DisplayDialog("AssetBundle Build Finish", "AssetBundle Build Finish", "OK");
    }

    private static AssetBundleBuild HadleAssetBundleBuild(AssetBundleBuild assetBundleBuild)
    {
        assetBundleBuild.assetBundleName += TestAssetbundle.bundleSuffix;
        return assetBundleBuild;
    }

    private static string prePath = "Assets/Resources/";
    private static string[] GetAddressableNames(string[] assetNames)
    {
        string[] addressableNames = new string[assetNames.Length];
        for (int i = 0; i < assetNames.Length; i++)
        {
            addressableNames[i] = assetNames[i].Substring(assetNames[i].IndexOf(prePath) + prePath.Length);
        }
        return addressableNames;
    }
    
    private static AssetBundleBuild GetShaders()
    {
        AssetBundleBuild assetBundleBuild = new AssetBundleBuild();
        assetBundleBuild.assetBundleName = "shaders";
        assetBundleBuild.assetNames = new string[] {
            "Assets/Resources/Shader/UI.shader"
        };
        return assetBundleBuild;
    }

    private static AssetBundleBuild GetChatPng()
    {
        AssetBundleBuild assetBundleBuild = new AssetBundleBuild();
        assetBundleBuild.assetBundleName = "chatpng";
        assetBundleBuild.assetNames = new string[] {
            "Assets/Resources/UI/Chat.png"
        };
        return assetBundleBuild;
    }

    private static AssetBundleBuild GetChatAPng()
    {
        AssetBundleBuild assetBundleBuild = new AssetBundleBuild();
        assetBundleBuild.assetBundleName = "chatapng";
        assetBundleBuild.assetNames = new string[] {
            "Assets/Resources/UI/Chat_A.png"
        };
        return assetBundleBuild;
    }
    private static AssetBundleBuild GetMaterials()
    {
        AssetBundleBuild assetBundleBuild = new AssetBundleBuild();
        assetBundleBuild.assetBundleName = "materials";
        assetBundleBuild.assetNames = new string[] {
            "Assets/Resources/UI/Chat.mat"
        }; 
        return assetBundleBuild;
    }

}
