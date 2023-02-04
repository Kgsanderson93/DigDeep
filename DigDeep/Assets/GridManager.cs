using System;
using UnityEngine;

namespace Assets
{
    public class GridManager : MonoBehaviour
    {
        private const int Rows = 30;
        private const int Cols = 30;
        private static GridSystem _current;
        private Vector3 _cameraVector3;

        private int _rangexLow;
        private int _rangeyHigh;
        private int _rangeyLow;
        private GameObject referenceTile;
        private float ysincelastupdate;

        private void Start()
        {
            referenceTile = (GameObject)Instantiate(Resources.Load("layer3"));
            _cameraVector3 = Camera.main.transform.position;
            Debug.Log(_cameraVector3.x);
            _current = ScriptableObject.CreateInstance<GridSystem>();
            _rangexLow = (int)_cameraVector3.x - Rows / 2;
            _rangeyLow = (int)_cameraVector3.y - Cols / 2;
            _rangeyHigh = (int)_cameraVector3.y + Cols / 2;
            GenerateGrid();
            ysincelastupdate = 0;
        }

        // Update is called once per frame
        private void Update()
        {
            float camVar = Math.Abs(_cameraVector3.y - Camera.main.transform.position.y);
            ysincelastupdate=ysincelastupdate + camVar;
            Debug.Log("Cam"+camVar);
            Debug.Log("Oldy" + _cameraVector3.y);
            Debug.Log("newy" + Camera.main.transform.position.y);
            if (ysincelastupdate > 1)
            {
                for (int i = (int)camVar; i > 0; i--)
                {
                    
                    UpdateGrid();
                }
                
            }
            _cameraVector3 = Camera.main.transform.position;
        }

        private void UpdateGrid()
        {
            _current.AddRow(_rangexLow, _rangeyLow--,referenceTile);
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
                for (row = _rangeyHigh; row >= _rangeyLow; row--) _current.AddRow(_rangexLow, row,referenceTile);

                _rangeyLow = row;
            }

            
        }
    }
}