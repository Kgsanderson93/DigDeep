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
        GenerateGrid();
        
        cameraVector2Int =Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newposVector2Int = GetCameraPos();
        if (newposVector2Int != cameraVector2Int)
        {
            cameraVector2Int = newposVector2Int;
            UpdateGrid();
            
        }
    }

    private void UpdateGrid()
    {
        GenerateGrid();
        DestroyOutOfBounds();
        
        
    }

    private void DestroyOutOfBounds()
    {
        GetCameraPos();
       
       
       rangexLow = (int)(Camera.main.transform.position.x -_rows*_tilesize / 2);
       rangexHigh = (int)(Camera.main.transform.position.x +_rows*_tilesize / 2+1);

        rangeyLow = (int)(Camera.main.transform.position.y- _cols*_tilesize / 2); 
        rangeyHigh = (int)(Camera.main.transform.position.y + _cols*_tilesize / 2+1);

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

        foreach (GameObject tile in tiles)
        {
            
            if (tile.transform.position.x < rangexLow || tile.transform.position.x > rangexHigh|| tile.transform.position.y < rangeyLow || tile.transform.position.y > rangeyHigh)
            {
                Destroy(tile);
            }
        }
       
    }

    private Vector3 GetCameraPos()
    {
        Vector3 cameraVector3= Camera.main.transform.position;
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

    private void GenerateGrid()
    {
        tiles = new ArrayList();
        int posX;
        int posY;
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("layer3"));
        Vector3 camera=GetCameraPos();

        rangexLow = (int)(camera.x - _rows * _tilesize /2);
        rangexHigh = (int)(camera.x + _rows * _tilesize /2+1);

        rangeyLow = (int)(camera.y - _cols * _tilesize / 2);
        rangeyHigh = (int)(camera.y + _cols*_tilesize/2+1);

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

        posX = rangexLow;
        posY = rangeyLow;
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
}
