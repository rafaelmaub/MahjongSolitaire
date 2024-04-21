using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MaterialHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked piece " + transform.GetSiblingIndex());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered piece " + transform.GetSiblingIndex());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit piece " + transform.GetSiblingIndex());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
