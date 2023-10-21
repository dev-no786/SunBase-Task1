using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public static class ApiHelper
{
    public static IEnumerator FetchData<T>(string url, Action<T> OnSucces, Action<string> OnError)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                OnError?.Invoke(webRequest.error);
            }
            else
            {
                string json = webRequest.downloadHandler.text;
                OnSucces?.Invoke(JsonConvert.DeserializeObject<T>(json));
            }
        }
    }
}
