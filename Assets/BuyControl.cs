using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyControl : MonoBehaviour
{
    private RectTransform t;
    private void Awake()
    {
        t = GetComponent<RectTransform>();
        BuildSite.OnClickEvent += MoveToTransform;
        gameObject.SetActive(false);
    }
    private void MoveToTransform(Transform targert)
    {
        if (targert)
        {
            var position = Camera.main.WorldToScreenPoint(targert.position);
            print(position);
            t.anchoredPosition = position;
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
