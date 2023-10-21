using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class ButtonModel : MonoBehaviour
{
    public TextMeshProUGUI LabelText;
    public TextMeshProUGUI PointText;
    public int id;
    public Action<int> NotifyId;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        NotifyId?.Invoke(id);
    }
    
    private void OnDestroy()
    {
        
        foreach (var VARIABLE in NotifyId.GetInvocationList())
        {
            
        }
    }
}
