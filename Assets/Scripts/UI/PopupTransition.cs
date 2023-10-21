using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopupTransition : MonoBehaviour
{
    
    [SerializeField] private Ease _closeEase = Ease.InBack;
    [SerializeField] private Ease _openEase = Ease.OutCubic;

    [SerializeField] private float _closingDuration = 0.2f;
    [SerializeField] private float _openingDuration = 0.2f;
    
    public void ClosePopup()
    {
        transform.localScale = Vector3.one;
        transform.DOScale(Vector3.zero, _closingDuration).SetEase(_closeEase);
    }
    
    public void OpenPopup()
    {
        transform.localScale = Vector3.one;
        transform.DOScale(Vector3.one, _openingDuration).SetEase(_openEase);
    }
}
