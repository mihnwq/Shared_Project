using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Node
{

    public bool traversed = false;

    public bool open = false;

    public float gCost;
    public float hCost;
    public float fCost;

    public bool solid = false;

    public Node parent;

    public Node(Vector3 position)
    {
        this.position = position;
    }

    public Vector3 position;

    public void setChecked()
    {
        traversed = true;
    }

    public void setOpen()
    {
        open = true;
    }

    public void setSolid()
    {
        solid = true;
    }

    public void SetCosts(float gCost, float hCost, float fCost)
    {
        this.gCost = gCost;
        this.hCost = hCost;
        this.fCost = fCost;
    }


}
