using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampInteract : Interactable
{
    private Lamp _lamp;
    // Start is called before the first frame update
    void Start()
    {
        _lamp = GetComponent<Lamp>();
    }
    
    protected override void Interact(GameObject playerGameObj)
    {
        Debug.Log("Interacted with " + gameObject.name);
        _lamp.OnButtonClicked();
    }
}
