using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int Id;
    public Vector2 Pos;         
    public Vector3 WorldPos;      
    public List<int> Neighbors = new();
    public Piece OccupiedBy;
    public NodeView View;
}