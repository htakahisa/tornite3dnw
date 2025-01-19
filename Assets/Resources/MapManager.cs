using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public static MapManager mapmanager;
    private static string mapName;

    // Start is called before the first frame update
    void Start()
    {
        mapmanager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MapSelect(string map)
    {
        mapName = map;
    }

    public string GetMapName()
    {
        return mapName;
    }

}
