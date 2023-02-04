using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;




namespace Assets
{


    public class Coordinate

     {
    private int x;
    private int y;
    bool layerOccupy;
    bool itemoOccupy;
    private GameObject itemtohold;

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.layerOccupy = true;

    }

    public void giveItem(GameObject item)
    {
        layerOccupy = true;
        itemtohold = item;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
        
    }
    }
}

