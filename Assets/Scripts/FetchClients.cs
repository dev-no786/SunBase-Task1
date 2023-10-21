using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FetchClients : MonoBehaviour
{
    [SerializeField]private ApiController _apiController;
    private bool _isQueryRunning;
    private ResponseData _responseData;
    
    [SerializeField] private ButtonModel _clientButtonPrefab;
    [SerializeField] private Transform _targetParent;
    [SerializeField] private TMP_Dropdown _dropdown;
    
    private List<ButtonModel> _clientButtons = new List<ButtonModel>();
    
    // Start is called before the first frame update
    void Start()
    {
        GetData();
        _dropdown.onValueChanged.AddListener(delegate { OnFilterClientData(_dropdown); });
    }

    // Update is called once per frame
    void GetData()
    {
        if (!_isQueryRunning)
        {
            UIManager.Instance.ShowLoadingAlert(true);
            StartCoroutine(FetchData());
        }
    }

    private IEnumerator FetchData()
    {
        _isQueryRunning = true;
        
        //show loading message
        
        //access api for data
        yield return _apiController.GetClientData(FillClientData);
        _isQueryRunning = false;
    }

    private void FillClientData(ResponseData response)
    {
        _responseData = response;
        _isQueryRunning = false;
        UIManager.Instance.ShowLoadingAlert(false);
        
        for (int i = 0; i < response.clients.Length; i++)
        {
            print("label : "+response.clients[i].label);
            ButtonModel button = Instantiate(_clientButtonPrefab,_targetParent);
            button.NotifyId += OnShowInfoPopup;
            _clientButtons.Add(button);
            
            button.id = response.clients[i].id;
            button.LabelText.text = response.clients[i].label;
            
            int indexOfKey = response.clients[i].id;
            
            if (response.data.ContainsKey(indexOfKey))
            {
                print("point :"+ response.data[indexOfKey].points);
                button.PointText.text = response.data[indexOfKey].points.ToString();
            }
            else
            {
                button.PointText.text = "No points found for Client Id " + indexOfKey;
            }
        }
    }

    private void OnShowInfoPopup(int clientId)
    {
        if (_responseData.data.ContainsKey(clientId))
        {
            UIManager.Instance.ShowClientInfoPopup(clientId, _responseData.data[clientId]);
        }
        else
        {
            UIManager.Instance.ShowClientInfoPopup(clientId);
        }
    }

    public void OnFilterClientData(TMP_Dropdown change)
    {
        switch (change.value)
        {
            case 0 : 
                //Filter All Clients
                foreach (var VARIABLE in _clientButtons)
                {
                    VARIABLE.gameObject.SetActive(true);
                }

                break;
            case 1 :
                //Filter check for Managers
                int index = 0;
                foreach (var VARIABLE in _clientButtons)
                {
                    if (_responseData.clients[index].isManager)
                    {
                        VARIABLE.gameObject.SetActive(true);    
                    }
                    else
                    {
                        VARIABLE.gameObject.SetActive(false);
                    }

                    index++;
                }
                break;
            case 2 :
                //Filter check for not Managers
                int index_i = 0;
                foreach (var VARIABLE in _clientButtons)
                {
                    if (_responseData.clients[index_i].isManager)
                    {
                        VARIABLE.gameObject.SetActive(false);    
                    }
                    else
                    {
                        VARIABLE.gameObject.SetActive(true);
                    }

                    index_i++;
                }
                break;
        }
    }
}
