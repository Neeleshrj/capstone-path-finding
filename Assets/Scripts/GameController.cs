using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameController : MonoBehaviour
{
    public MapData mapData;
    public Graph graph;

    void Start()
    {
        if (mapData != null && graph != null)
        {
            int[,] mapInstance = mapData.MakeMap();
            graph.Init(mapInstance);

            GraphView graphView = graph.gameObject.GetComponent<GraphView>();

            if(graphView != null)
            {
                graphView.Init(graph);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            Debug.Log("mouse click at"+ mouseWorldPosition);
        }
    }
}
