using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject loadingMessage;
    [SerializeField] private GameObject errorMessage;
    [SerializeField] private TextMeshProUGUI errorTextMessage;
    [SerializeField] private GameObject popupMessage;
    
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI points;
    [SerializeField] private TextMeshProUGUI address;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ShowErrorAlert(string error)
    {
        errorTextMessage.text = error;
        errorMessage.SetActive(true);
        errorMessage.GetComponent<PopupTransition>().OpenPopup();
    }

    public void ShowLoadingAlert(bool _isActive)
    {
        if (_isActive)
        {
            loadingMessage.transform.localScale = Vector3.zero;
            loadingMessage.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
        }
        else
        {
            loadingMessage.transform.localScale = Vector3.one;
            loadingMessage.transform.DOScale(Vector3.zero, 5f).SetEase(Ease.OutBounce);
        }
        loadingMessage.SetActive(_isActive);
        
    }

    public void ShowClientInfoPopup(int id , DataItem item = null)
    {
        popupMessage.SetActive(true);
        popupMessage.transform.localScale = Vector3.zero;
        popupMessage.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutCubic);
        
        if (item == null)
        {
            name.text = "No Data record for Id " + id;
            points.text = "No Data record for Id " + id;
            address.text = "No Data record for Id " + id;
        }
        else
        {
            name.text = "Name :"+ item.name;
            points.text = "Points :"+item.points.ToString();
            address.text = "Address :"+item.address;
        }
    }
}
