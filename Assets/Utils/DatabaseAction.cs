using Assets.Models;
using Assets.ModelsRequest;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Utils
{
    public static class DatabaseAction<T> where T : class
    {
        public static IEnumerator GetData(string uri, Action<T> callback)
        { 
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(Config.baseUrl + uri);
            yield return unityWebRequest.SendWebRequest();
            switch (unityWebRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + unityWebRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + unityWebRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("Received: " + unityWebRequest.downloadHandler.text);
                    break;
            }
            ApiResult<T> apiResult = JsonConvert.DeserializeObject<ApiResult<T>>(unityWebRequest.downloadHandler.text);
            callback(apiResult.Result);
        }
    }
}
