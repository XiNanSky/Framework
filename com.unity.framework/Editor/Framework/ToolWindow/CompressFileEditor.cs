using System.IO;
using UnityEditor;
using UnityEngine;

public class CompressFileEditor
{
    private const string pack_android = "/ab/Package Android/";
    private const string pack_out_android = "/ab/Package Out/Android/";

    private const string pack_ios = "/ab/Package IOS/";
    private const string pack_out_ios = "/ab/Package Out/Android/";

    private const string wwwxdcp_android = @"D:\Static Programs\Server-modules\www\qipai\xdcp\xdcp_res\android\";
    private const string wwwxdcp_ios = @"D:\Static Programs\Server-modules\www\qipai\xdcp\xdcp_res\ios\";


    /// <summary> AssetBundleManifestName == 对应AB依赖列表文件 </summary>  

    private static string AssetBundle_BuildDirectory_Path = @Application.streamingAssetsPath + "/../../../" + "AssetBundles";

    private static string AssetBundle_TargetDirectory_Path = @Application.streamingAssetsPath + "/" + "ABFiles";

    [MenuItem("ETool/Asset Bundle/Build Asset Bundles", false, 0)]
    public static void BuildAssetBundleAndroid()
    {
        //Application.streamingAssetsPath对应的StreamingAssets的子目录  
        Debug.Log("path=" + AssetBundle_BuildDirectory_Path);

        DirectoryInfo AB_Directory = new DirectoryInfo(AssetBundle_BuildDirectory_Path);
        if (!AB_Directory.Exists)
        {
            AB_Directory.Create();
        }
        System.IO.FileInfo[] filesAB = AB_Directory.GetFiles();
        foreach (var item in filesAB)
        {
            Debug.Log("******删除旧文件：" + item.FullName + "******");
            item.Delete();
        }
#if UNITY_ANDROID
        BuildPipeline.BuildAssetBundles(AB_Directory.FullName, BuildAssetBundleOptions.ChunkBasedCompression, UnityEditor.BuildTarget.Android);
#elif UNITY_IPHONE
        BuildPipeline.BuildAssetBundles(AB_Directory.FullName, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.iOS); 
#else
        BuildPipeline.BuildAssetBundles(AB_Directory.FullName, BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows64);
#endif
        Debug.Log("****** AssetBundle打包完成 ******");

        Debug.Log("将要转移的文件夹是：" + AssetBundle_TargetDirectory_Path);
        System.IO.FileInfo[] filesAB_temp = AB_Directory.GetFiles();

        DirectoryInfo streaming_Directory = new DirectoryInfo(AssetBundle_TargetDirectory_Path);

        System.IO.FileInfo[] streaming_files = streaming_Directory.GetFiles();
        foreach (var item in streaming_files)
        {
            item.Delete();
        }
        AssetDatabase.Refresh();
        foreach (var item in filesAB_temp)
        {
            if (item.Extension == "")
            {
                item.CopyTo(AssetBundle_TargetDirectory_Path + "/" + item.Name, true);
            }
        }
        AssetDatabase.Refresh();
        Debug.Log("******文件传输完成******");
    }

    private static string _dirName = "";

    /// <summary> 批量命名所选文件夹下资源的AssetBundleName </summary>  
    [MenuItem("ETool/Asset Bundle/Set Asset Bundle Name")]
    private static void SetSelectFolderFileBundleName()
    {
        var selObj = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Unfiltered);
        foreach (var item in selObj)
        {
            string objPath = AssetDatabase.GetAssetPath(item);
            DirectoryInfo dirInfo = new DirectoryInfo(objPath);
            if (dirInfo == null)
            {
                Debug.LogError("******请检查，是否选中了非文件夹对象******");
                return;
            }
            _dirName = dirInfo.Name;

            string filePath = dirInfo.FullName.Replace('\\', '/');
            filePath = filePath.Replace(Application.dataPath, "Assets");
            AssetImporter ai = AssetImporter.GetAtPath(filePath);
            ai.assetBundleName = _dirName;

            SetAssetBundleName(dirInfo);
        }
        AssetDatabase.Refresh();
        Debug.Log("******批量设置AssetBundle名称成功******");
    }

    private static void SetAssetBundleName(DirectoryInfo dirInfo)
    {
        FileSystemInfo[] files = dirInfo.GetFileSystemInfos();
        foreach (FileSystemInfo file in files)
        {
            if (file is System.IO.FileInfo && file.Extension != ".meta" && file.Extension != ".txt")
            {
                string filePath = file.FullName.Replace('\\', '/');
                filePath = filePath.Replace(Application.dataPath, "Assets");
                AssetImporter ai = AssetImporter.GetAtPath(filePath);
                ai.assetBundleName = _dirName;
            }
            else if (file is DirectoryInfo)
            {
                string filePath = file.FullName.Replace('\\', '/');
                filePath = filePath.Replace(Application.dataPath, "Assets");
                AssetImporter ai = AssetImporter.GetAtPath(filePath);
                ai.assetBundleName = _dirName;
                SetAssetBundleName(file as DirectoryInfo);
            }
        }
    }


    /// <summary> 批量清空所选文件夹下资源的AssetBundleName </summary>  
    [MenuItem("ETool/Asset Bundle/Reset Asset Bundle Name")]
    private static void ResetSelectFolderFileBundleName()
    {
        UnityEngine.Object[] selObj = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Unfiltered);
        foreach (UnityEngine.Object item in selObj)
        {
            string objPath = AssetDatabase.GetAssetPath(item);
            DirectoryInfo dirInfo = new DirectoryInfo(objPath);
            if (dirInfo == null)
            {
                Debug.LogError("****** 请检查，是否选中了非文件夹对象 ******");
                return;
            }
            _dirName = null;

            string filePath = dirInfo.FullName.Replace('\\', '/');
            filePath = filePath.Replace(Application.dataPath, "Assets");
            AssetImporter ai = AssetImporter.GetAtPath(filePath);
            ai.assetBundleName = _dirName;

            SetAssetBundleName(dirInfo);
        }
        AssetDatabase.Refresh();
        Debug.Log("****** 批量清除AssetBundle名称成功 ******");
    }

    /// <summary> 移除资源......移除后将不打进发布包中 </summary>
    [MenuItem("ETool/Assets/Remove prefab/RemovePrefab")]
    private static void MoveAsset()
    {
        var str = AssetDatabase.MoveAsset("Assets/img/simpleui/v1_button_exit_2.png", "Assets/test/v1_button_exit_2.png");
        if (str != "") Debug.LogError("str");
        AssetDatabase.Refresh();
    }
}

