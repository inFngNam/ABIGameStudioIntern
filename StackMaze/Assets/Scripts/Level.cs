using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
    private List<List<List<int>>> maps = new List<List<List<int>>>();

    private void Start()
    {
        SelectMaps();
    }

    private void Update()
    {
    }
    
    private void SelectMaps()
    {
        while (maps.Count < 3)
        {
            int random = UnityEngine.Random.Range(0, 6);
            maps.Add(LoadMap(random));
        }
    }

    private List<List<int>> LoadMap(int id)
    {
        List<List<int>> map = new List<List<int>>();
        StreamReader reader = new StreamReader(String.Format("Assets/Maps/map{0}.txt", id));
        while (!reader.EndOfStream)
        {
            List<int> line = new List<int>();
            string lineString = reader.ReadLine();
            for (int index = 0; index < lineString.Length; index++)
            {
                line.Add(int.Parse(lineString[index].ToString()));
            }
            map.Add(line);
        }
        reader.Close();
        return map;
    }
}
