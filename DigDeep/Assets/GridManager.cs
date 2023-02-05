using System;
using System.Collections;
using System.IO;
using UnityEngine;



public class GridManager : MonoBehaviour
{
    private const int _rows = 25;
    private const int _cols = 25;
    private const int _tilesize = 1;
    private Vector3 cameraVector3;
    private ArrayList tiles=null;
    bool inversex = false;
    bool inversey = false;
    private int rangexLow;
    private int rangexHigh;
    private int rangeyLow;
    private int rangeyHigh;
    private int testing = 0;

    void Start()
    {
        //initialize camera
        cameraVector3=GetCameraPos();
        UpdateRange();
        SwapRange();
        GenerateGrid();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        //check if camera has moved, if so update grid
        Vector3 newposVector3 = GetCameraPos();
        if (newposVector3 != cameraVector3)
        {
            cameraVector3 = newposVector3;
            UpdateGrid();
            
        }
    }

    private void UpdateGrid()
    {
        testing = testing + 1;
        Debug.Log("rounds"+testing);
        //make more grid theoretically I want to check if a space has already been generated
        //then what hasn't? or maybe update grid should only generate in front of the camera
        //whatever direction it has moved 
        GenerateGrid();
        //Whatever's out of bounds should be destroyed in an attempt at memory management
        //in theory should be constrained also by players possible moves, want to destroy whats above but not necessarily whats beside

        DestroyOutOfBounds();
        
        
    }

    private void DestroyOutOfBounds()
    {
       
       UpdateRange();
       SwapRange();


        for(int i=tiles.Count-1; i>=0; i--)
        {
            GameObject tile = (GameObject)tiles[i];
            //attempt to say that if not in bounds destroy. This doesn't appear to work at all
            if (Math.Abs(rangexLow) > Math.Abs(tile.transform.position.x)||Math.Abs(tile.transform.transform.position.x)>Math.Abs(rangexHigh)|| Math.Abs(rangeyLow) > Math.Abs(tile.transform.position.y) || Math.Abs(tile.transform.transform.position.y) > Math.Abs(rangeyHigh))
            {
               
                Destroy(tile);
              
            }
        }
       
    }

    private void GenerateGrid()
    {
        tiles = new ArrayList();
        
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("layer3"));
        UpdateRange();
        SwapRange();
       
        
        

        int posX = rangexLow;
        int posY = rangeyLow;

        for (int row = rangexLow; row<rangexHigh; row++)
        {
            posY = (int)(posY + _tilesize);
            posX = rangexLow;
            for (int col = rangeyLow; col < rangeyHigh; col++)
            {
                posX= (int)(posX + _tilesize);
                
                GameObject tile = (GameObject)Instantiate(
                    referenceTile, transform);
                
                    
                    
                    tile.transform.position = new Vector2(posX, posY);
                    
                    tiles.Add(tile);
               
            }

        }
        Destroy(referenceTile);
        
    }
    private Vector3 GetCameraPos()
    {
        Vector3 newcameraVector3 = Camera.main.transform.position;
        
        if (cameraVector3.x < 0)
        {
            inversex = true;
        }

        if (cameraVector3.y < 0)
        {
            inversey = true;
        }

        return newcameraVector3;
    }

    //Math is probably sketchy here.
    //In theory I'm trying to account for the grid system.
    //Honestly if i could just move the camera spawn to the bottom right quad
    //this math would probably be easier
    private void SwapRange()
    {
        if (inversex)
        {
            (rangexHigh, rangexLow) = (rangexLow, rangexHigh);
        }

        if (inversey)
        {
            (rangeyHigh, rangeyLow) = (rangeyLow, rangeyHigh);
        }
    }

    private void UpdateRange()
    {
        Vector3 camera = GetCameraPos();
        Debug.Log("xcam"+camera.x);
        Debug.Log("ycam"+camera.y);

        rangexLow = (int)(camera.x - _rows * _tilesize );
        rangexHigh = (int)(camera.x + _rows * _tilesize);

        rangeyLow = (int)(camera.y - _cols * _tilesize);
        rangeyHigh = (int)(camera.y + _cols * _tilesize);

        Debug.Log("xHigh"+rangexHigh);
        Debug.Log("xLow"+rangexLow);
        Debug.Log("yHigh"+rangeyHigh);
        Debug.Log("yLow"+rangeyLow);
    }
}
