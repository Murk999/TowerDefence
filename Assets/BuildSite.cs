using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSite : MonoBehaviour, IPointerDownHandler
{
    public static Action<Transform> OnClickEvent;

    protected void InvokeNullEvent()
    {
        OnClickEvent(null);
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnClickEvent(transform.root);
    }
}
