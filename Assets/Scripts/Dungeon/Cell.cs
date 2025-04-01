
using UnityEngine;

public class Cell 
{
    public Transform position;
    public bool visited=false;
    public bool canCreateExRoom=false;
    public bool startingRoom=false;
    public bool lastRoom=false;
    public bool[] status=new bool[4];
    
}
