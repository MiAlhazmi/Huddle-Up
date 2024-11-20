using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapSelectionControl : MonoBehaviour
{
    
    public String selectedMap;
    
    public enum Maps
    {
        Lab,
        Camp,
        Village,
        Playground
    };


    private void Awake()
    {
        SelectMap(Random.Range(1, 3));
    }

    public void SelectMap(Maps map)
    {
        switch (map)
        {
            case Maps.Lab:
                selectedMap = "Map1";
                break;
            case Maps.Camp:
                selectedMap = "Map2";
                break;
        }
    }
    
    public void SelectMap(int mapNum)
    {
        switch (mapNum)
        {
            case 1:
                selectedMap = "Playground";
                break;
            case 2:
                selectedMap = "Camp";
                break;
            case 3:
                selectedMap = "Village";
                break;
            default: 
                selectedMap = "Playground";
                break;
        }
    }
}
