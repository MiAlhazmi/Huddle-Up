using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This class for visual effect for a serius of objects that you can select one only
 * So this class highlighted the selected one
 */
public class ObjectsSelection : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects;
    [SerializeField] private int _selectedObjectIndex;

    public enum EffectType
    {
        Color,
        Appearance,
        Size
    };

    [SerializeField] private EffectType effectType; 
    
    private void Awake()
    {
        _selectedObjectIndex = -1;
    }
    

    private void DoEffect()
    {
        Debug.Log("Doing effect");
        // Turn off other effects: 
        for (int i = 0; i < _objects.Count; i++)
        {
            if (i == _selectedObjectIndex)
            {
                continue;
            }
            _objects[i].gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        
        _objects[_selectedObjectIndex].gameObject.GetComponent<MeshRenderer>().material.color = Color.green;

        
        // switch (effectType)
        // {
        //     case EffectType.Color:
        //         for (int i = 0; i < _objects.Count; i++)
        //         {
        //             if (i == _selectedObjectIndex)
        //             {
        //                 _objects[i].gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        //                 continue;
        //             }
        //             _objects[i].gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        //         }
        //         break;
        // }
    }
    
    public void SelectObject(GameObject objectSelectObject)
    {
        // Debug.Log("SelectObject(); found: " + _objects.Contains(objectSelectObject));
        // if (_objects[0].GetInstanceID() == objectSelectObject.GetInstanceID())
        // {
        //     Debug.Log("ObjectSelect found!");
        //     _selectedObjectIndex = 0;
        // } else if (_objects[1].GetInstanceID() == objectSelectObject.GetInstanceID())
        // {
        //     Debug.Log("ObjectSelect found!");
        //     _selectedObjectIndex = 1;
        // }

        for (var i = 0; i < _objects.Count; i++)
        {
            _selectedObjectIndex = _objects[i].GetInstanceID() == objectSelectObject.GetInstanceID()? i : _selectedObjectIndex;
        }
        
        DoEffect();

        // for (int i = 0; i < _objects.Count; i++)
        // {
        //     if (_objects[i].GetInstanceID() == _objects[_selectedObjectIndex].GetInstanceID())
        //     {
        //         Debug.Log("ObjectSelect is the same!");
        //         return;
        //     }
        //     if (_objects[i].GetInstanceID() == objectSelectObject.GetInstanceID())
        //     {
        //         Debug.Log("ObjectSelect found!");
        //         _selectedObjectIndex = i;
        //     }
        // }
        DoEffect();
        
        // objectSelect.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
    }
}
