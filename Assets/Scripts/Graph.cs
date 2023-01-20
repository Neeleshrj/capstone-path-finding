using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
   public Node[,] nodes;

   public List<Node> walls = new List<Node>();

   int[,] m_mapData;
   int m_width; //map width
   int m_height; //map height

   public static readonly Vector2[] allDirections = 
   {
    new Vector2(0f, 1f),
    new Vector2(1f, 1f),
    new Vector2(1f, 0f),
    new Vector2(1f, -1f),
    new Vector2(0f, -1f),
    new Vector2(-1f, -1f),
    new Vector2(-1f, 0f),
    new Vector2(-1f, 1f),
   }; //storing all 8 compass directions in array

   public void Init(int[,] mapData)
   {
    m_mapData = mapData;
    m_width = mapData.GetLength(0);
    m_height = mapData.GetLength(1);

    nodes = new Node[m_width, m_height];

    for(int y=0; y < m_height; y++)
    {
        for(int x=0; x < m_width; x++)
        {
            NodeType type = (NodeType)mapData[x, y];
            Node newNode = new Node(x, y, type);
            nodes[x, y] = newNode;
            
            newNode.position = new Vector3(x, 0 ,y);

            if(type == NodeType.Blocked)
            {
                walls.Add((newNode));
            }
        }
    }

    for(int y = 0; y < m_height; y++)
    {
        for(int x = 0; x < m_width; x++)
        {
            if(nodes[x,y].nodeType != NodeType.Blocked)
            {
                nodes[x, y].neighbours = GetNeighbours(x, y);
            }
        }
    }
   }

   public bool IsWithinBounds(int x, int y)
   {
    return (x >=0 && x< m_width && y >= 0 && y < m_height);
   } //function to check whether a node is within bounds or not

   List<Node> GetNeighbours(int x, int y, Node[,] nodeArray, Vector2[] directions)
   {
    List<Node> neighbourNodes = new List<Node>();

    foreach (Vector2 dir in directions)
    {
        int newX = x + (int)dir.x;
        int newY = y + (int)dir.y;

        if(IsWithinBounds(newX, newY) && nodeArray[newX, newY] != null && nodeArray[newX, newY].nodeType != NodeType.Blocked)
        {
            neighbourNodes.Add(nodeArray[newX, newY]);
        }
    }
    return neighbourNodes;
   } //gives the neighbour nodes of a node by looping through all nodes and checking if within bounds, not blocked and not null to see if neighbour node or not

   List<Node> GetNeighbours(int x, int y)
   {
    return GetNeighbours(x, y, nodes, allDirections);
   } //Overloaded version of GetNeighbours

}
