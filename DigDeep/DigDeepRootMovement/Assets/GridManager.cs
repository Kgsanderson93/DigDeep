using System;
using UnityEngine;

namespace Assets
{
    public class GridManager : MonoBehaviour
    {
        private const int Rows = 30;
        private const int Cols = 30;
        private int rowCount;
        private static GridSystem _current;
        private Vector3 _cameraVector3;

        private int _rangexLow;
        private int _rangeyHigh;
        private int _rangeyLow;
        private GameObject referenceTile;
        private GameObject referenceTileFirst;
        private GameObject referenceTileSecond;
        private GameObject referenceTileThird;

        private float ysincelastupdate;

        private void Start()
        {
            referenceTileThird = (GameObject)Instantiate(Resources.Load("layer3"));
            referenceTileFirst = (GameObject)Instantiate(Resources.Load("layer1"));
            referenceTileSecond = (GameObject)Instantiate(Resources.Load("layer2"));
            UpdateTile();
            _cameraVector3 = Camera.main.transform.position;
            //Debug.Log(_cameraVector3.x);
            _current = ScriptableObject.CreateInstance<GridSystem>();
            //Debug.Log(_current + " balls");
            _rangexLow = (int)_cameraVector3.x - Rows / 2;
            _rangeyLow = (int)_cameraVector3.y;
            _rangeyHigh = (int)_cameraVector3.y + Cols / 2;
            GenerateGrid();
            ysincelastupdate = 0;

           
        }

        // Update is called once per frame
        private void Update()
        {
            //Debug.Log("update" + rowCount);
            
            float camVar = Math.Abs(_cameraVector3.y - Camera.main.transform.position.y);
            ysincelastupdate = ysincelastupdate + camVar;
            //Debug.Log("Cam"+camVar);
            //Debug.Log("update" + ysincelastupdate);
            
            if (ysincelastupdate > 1)
            {
                for (int i = (int)ysincelastupdate; i > 0; i--)
                {
                    UpdateTile();
                    UpdateGrid();
                    ysincelastupdate--;
                }
                
            }
            _cameraVector3 = Camera.main.transform.position;
        }

        private void UpdateGrid()
        {
            Debug.Log("updateGrid"+rowCount);
            UpdateTile();
            _current.AddRow(_rangexLow, _rangeyLow--,referenceTile);
            rowCount++;

            DestroyOutOfBounds();
        }

        private void DestroyOutOfBounds()
        {
            _current.RemoveRow(_rangexLow, _rangeyHigh);
            _rangeyHigh--;
        }

        private void GenerateGrid()
        {
            int row;
            if (_current.IsEmpty())
            {
                for (row = _rangeyHigh; row >= _rangeyLow; row--)
                {
                    _current.AddRow(_rangexLow, row,referenceTile);
                    rowCount++;
                    UpdateTile();
                    
                }

                _rangeyLow = row;
            }

            
        }

        private void UpdateTile()
        {
            if (rowCount <= 20)
            {
                referenceTile = referenceTileFirst;
                return;
            }
            else if (rowCount>20 && rowCount <= 40)
            {
                referenceTile = referenceTileSecond;
                return;
            }
            
                referenceTile = referenceTileThird;
            
        }
    }
}
