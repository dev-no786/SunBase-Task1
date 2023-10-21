using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ApiController : MonoBehaviour
{
    
    [SerializeField] private string _endpoint = "https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";
    
    public IEnumerator GetClientData(Action<ResponseData> OnSuccess)
    {
        
        yield return ApiHelper.FetchData(_endpoint, OnSuccess, GetError);
    }

    void GetError(string msg)
    {
        print("Failed to get api data - " + msg);
        UIManager.Instance.ShowErrorAlert(msg);
    }
}
