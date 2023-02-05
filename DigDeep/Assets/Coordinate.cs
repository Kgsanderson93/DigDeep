using UnityEngine;




namespace Assets
{


    public class Coordinate :ScriptableObject

     {
    private int x;
    private int y;
    bool _itemoOccupy;
    private GameObject _itemtohold;
    private GameObject _tile;

    public Coordinate()
    {
    }

    public void setX(int x)
    {
        this.x = x;
    }

    public void setY(int y)
    {
        this.y = y;
    }

    public void setTile(GameObject tile)
    {
        this._tile=tile;
    }

    public void GiveItem(GameObject item)
    {
        _itemoOccupy = true;
        _itemtohold = item;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
        
    }

    public GameObject GetTile()
    {
        return _tile;
    }
    }
}

