using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup UIGroup;

    public void ShowUI()
    {
        UIGroup.alpha = 1;
    }

    public void HideUI()
    {
        UIGroup.alpha = 0;
    }
}
