using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;


public class GridManager : MonoBehaviour
{
    private const int _rows = 25;
    private const int _cols = 25;
    private const int _tilesize = 1;
    private Vector3 cameraVector2Int;
    private ArrayList tiles=null;
    bool inversex = false;
    bool inversey = false;
    private int rangexLow;
    private int rangexHigh;
    private int rangeyLow;
    private int rangeyHigh;

    void Start()
    {
        //initialize camera
        cameraVector2Int=GetCameraPos();
        UpdateRange();
        SwapRange();
        GenerateGrid();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        //check if camera has moved, if so update grid
        Vector3 newposVector2Int = GetCameraPos();
        if (newposVector2Int != cameraVector2Int)
        {
            cameraVector2Int = newposVector2Int;
            UpdateGrid();
            
        }
    }

    private void UpdateGrid()
    {
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


        foreach (GameObject tile in tiles)
        {
            //attempt to say that if not in bounds destroy. This doesn't appear to work at all
            if (tile.transform.position.x < rangexLow || tile.transform.position.x > rangexHigh|| tile.transform.position.y < rangeyLow || tile.transform.position.y > rangeyHigh)
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

        for (int row = 0; row<_rows; row++)
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
        Vector3 cameraVector3 = Camera.main.transform.position;
        if (cameraVector3.x < 0)
        {
            inversex = true;
        }

        if (cameraVector3.y < 0)
        {
            inversey = true;
        }

        return cameraVector3;
    }

    //Math is probably sketchy here.
    //In theory I'm trying to account for the grid system.
    //Honestly if i could just move the camera spawn to the bottom right quad
    //this math would probably be easier
    private void SwapRange()
    {
        if (inversex)
        {
            int temp = rangeyHigh;
            rangexHigh = -rangexLow;
            rangeyLow = -temp;
        }

        if (inversey)
        {
            int temp = rangeyHigh;
            rangeyHigh = -rangeyLow;
            rangeyLow = -temp;
        }
    }

    private void UpdateRange()
    {
        Vector3 camera = GetCameraPos();
        rangexLow = (int)(camera.x - _rows * _tilesize / 2);
        rangexHigh = (int)(camera.x + _rows * _tilesize / 2 + 1);

        rangeyLow = (int)(camera.y - _cols * _tilesize / 2);
        rangeyHigh = (int)(camera.y + _cols * _tilesize / 2 + 1);
    }
}
