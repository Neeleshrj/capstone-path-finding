using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MapData : MonoBehaviour
{
    public int width = 10;
    public int height = 5;

    public TextAsset textAsset;
    public string resourcePath = "MapData";

    public List<string> GetTextFromFile(TextAsset tAsset)
    {
        List<string> lines = new List<string>();

        if(tAsset != null)
        {
            string textData = tAsset.text;
            string[] delimiters = { "\r\n", "\n"};
            lines.AddRange(textData.Split(delimiters, System.StringSplitOptions.None));
            lines.Reverse();
        }
        else{
            Debug.LogWarning("Invalid asset from file");
        }

        return lines;
    }// read obstacle course through text made maze

    public List<string> GetTextFromFile()
    {
        if(textAsset == null)
        {
            string levelName = SceneManager.GetActiveScene().name;
            textAsset = Resources.Load(resourcePath + "/" + levelName) as TextAsset;
        }
        return GetTextFromFile(textAsset);
    }

    public void SetDimensions(List<string> textLines)
    {
        height = textLines.Count;

        foreach(string line in textLines)
        {
            if(line.Length > width)
            {
                width = line.Length;
            }
        }
    } //set height and width according to text file

    public int[,] MakeMap()
    {
        List<string> lines = new List<string>();
        lines = GetTextFromFile();
        SetDimensions(lines);

        int[,] map = new int[width, height];
        for(int y=0; y < height; y++){
            for(int x=0; x < width; x++){
                if(lines[y].Length > x)
                {
                    map[x, y] = (int)Char.GetNumericValue(lines[y][x]);
                }
            }
        }
        
        return map;
    }
}
