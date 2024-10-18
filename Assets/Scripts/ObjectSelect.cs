using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelect : MonoBehaviour
{
    // public delegate void OnObjectSelect();
    // public static event
    
    public bool selected;

    [SerializeField]
    private ObjectsSelection _objectsSelection;

    public void Select()
    {
        selected = true;
        _objectsSelection.SelectObject(gameObject);
    }

    public void Unselect()
    {
        selected = false;
    }
}
