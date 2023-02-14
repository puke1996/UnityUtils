using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Plugins.Puke.UnityUtilities.UnityNetworkUtils
{
    public delegate void StringHandler(string str);

    public delegate void BytesHandler(byte[] bytes);

    public delegate void AssetBundleHandler(AssetBundle assetBundle);

    public delegate void RawBytesHandler(byte[] bytes);

    public delegate void Texture2DHandler(Texture2D texture2D);

    /// <summary>
    /// UnityWebRequest具有更好的平台兼容性,HttpClient在WebGl平台甚至不会编译
    /// </summary>
    public class HTTPUtil
    {
        /// <summary>
        /// 获取流文件夹的Uri
        /// </summary>
        public static string GetFileUriInStreamingAssets(string path)
        {
            return Application.streamingAssetsPath + "/" + path;
        }

        public void Demo()
        {
            // if (Input.GetKeyDown(KeyCode.G))
            // {
            //     HttpClient httpClient = new HttpClient();
            //     // var result = httpClient.GetAsync("https://www.google.com").Result;
            //     // HttpClient httpClient = new HttpClient();
            //     var result = httpClient.GetAsync("https://www.google.com").Result;
            //     Debug.Log(result);
            // }
            //
            // if (Input.GetKeyDown(KeyCode.B))
            // {
            //     HttpClient httpClient = new HttpClient();
            //     var result = httpClient.GetAsync("https://www.baidu.com").Result;
            //     Debug.Log(result);
            // }
        }

        /// <summary>
        /// 下载AssetBundle
        /// </summary>
        public static IEnumerator GetAssetBundle(string uri, AssetBundleHandler assetBundleHandle)
        {
            // UnityWebRequestAssetBundle vs UnityWebRequest
            UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(uri);
            yield return webRequest.SendWebRequest();
            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                // 如果请求失败或网络错误
                Debug.Log(webRequest.error);
            }
            else
            {
                AssetBundle downloadedAssetBundle =
                    ((DownloadHandlerAssetBundle) webRequest.downloadHandler).assetBundle;
                assetBundleHandle(downloadedAssetBundle);
            }
        }

        /// <summary>
        /// 下载文本
        /// </summary>
        public static IEnumerator GetText(string uri, StringHandler stringHandle)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(uri);
            yield return webRequest.SendWebRequest();
            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                // 如果请求失败或网络错误
                Debug.Log(webRequest.error);
            }
            else
            {
                string downloadedString = webRequest.downloadHandler.text;
                stringHandle(downloadedString);
            }
        }

        public static IEnumerator GetData(string uri, RawBytesHandler rawBytesHandle)
        {
            //string uri = "http://127.0.0.1:80/3dmodels/头骨2/293022/骷髅头_FBX/kl.fbx";
            //uri = "http://127.0.0.1:80/3dmodels/头骨4/skull/skull.ZPR";
            //uri = "http://127.0.0.1:80/3dmodels/心脏3/xinzangtiaodong/xinzangtiaodong/xinzangtiaodong.fbx";
            ////  string uri = "http://127.0.0.1:80/3dmodels/TriLib/test1.zip";
            UnityWebRequest webRequest = UnityWebRequest.Get(uri);
            yield return webRequest.SendWebRequest();
            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                // 如果请求失败或网络错误
                Debug.Log(webRequest.error);
            }
            else
            {
                var data = webRequest.downloadHandler.data;
                rawBytesHandle(data);
                // var assetLoader = new AssetLoader();
                // assetLoader.LoadFromMemoryWithTextures(data, ".fbx", null, null, null, null, null);
            }
        }

        public static IEnumerator GetTexture(string uri, Texture2DHandler texture2DHandle)
        {
            UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(uri);
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                var texture2D = ((DownloadHandlerTexture) webRequest.downloadHandler).texture;
                texture2DHandle(texture2D);
            }
        }

        public static IEnumerator PostForm3<T>(string url, WWWForm wwwForm, CommonResultHandler<T> commonResultHandler)
        {
            UnityWebRequest www = UnityWebRequest.Post(url, wwwForm);
            // www.SetRequestHeader("Authorization", token);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                // 不能这样
                // var result = (CommonResult<T>) JsonConvert.DeserializeObject(www.downloadHandler.text);
                // 只能这样
                var commonResult = JsonConvert.DeserializeObject<CommonResult<T>>(www.downloadHandler.text);
                commonResultHandler(commonResult);
                // bytes
                // www.downloadHandler.data
            }
        }

        public static IEnumerator PostForm2(string token, string url, WWWForm wwwForm, BytesHandler bytesHandler)
        {
            UnityWebRequest www = UnityWebRequest.Post(url, wwwForm);
            www.SetRequestHeader("Authorization", token);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                bytesHandler(www.downloadHandler.data);
            }
        }

        public static IEnumerator PostJson(string url, object obj, StringHandler stringHandler)
        {
            string str = JsonConvert.SerializeObject(obj);
            // 转字节流
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            UnityWebRequest www = new UnityWebRequest(url, "POST");
            www.uploadHandler = new UploadHandlerRaw(bytes);
            www.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string recv = www.downloadHandler.text;
                stringHandler(recv);
            }
        }

        public static IEnumerator PostJson(string token, string url, object obj, StringHandler stringHandler)
        {
            string str = JsonConvert.SerializeObject(obj);
            // 转字节流
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            UnityWebRequest www = new UnityWebRequest(url, "POST");
            www.uploadHandler = new UploadHandlerRaw(bytes);
            www.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
            www.SetRequestHeader("Authorization", token);
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string recv = www.downloadHandler.text;
                stringHandler(recv);
            }
        }

        public static IEnumerator PostBytes(string url, byte[] bytes, StringHandler stringHandler)
        {
            UnityWebRequest www = new UnityWebRequest(url, "POST");
            www.uploadHandler = new UploadHandlerRaw(bytes);
            www.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string recv = www.downloadHandler.text;
                stringHandler(recv);
            }
        }

        public static IEnumerator PostBytes2(string url, byte[] bytes, BytesHandler bytesHandler)
        {
            UnityWebRequest www = new UnityWebRequest(url, "POST");
            www.uploadHandler = new UploadHandlerRaw(bytes);
            www.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                bytesHandler(www.downloadHandler.data);
            }
        }
    }
}