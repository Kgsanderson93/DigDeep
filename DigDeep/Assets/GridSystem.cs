
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using Random = System.Random;


namespace Assets

{

    public class GridSystem : ScriptableObject
    {
        private LinkedList<Coordinate> _coords;
        [SerializeField] private float waterSpawnChance=0.5f;
        [SerializeField] private float TimeSinceFloat = 0;
        [SerializeField] private float success = .75f;
        [SerializeField] private float anotherSquare=.1f;


        public GridSystem()
        {
            _coords = new LinkedList<Coordinate>();
            

        }


        public void AddRow(int x, int y, GameObject tile)
        {
            
            
            if (_coords.Count > 0)
            {
                Coordinate c = _coords.Last();
                if (c.GetY() == y)
                {
                    return;
                }
            }

            for (int i = x; i < x + 30; i++)
            {

                GameObject tileClone = (GameObject)Instantiate(tile
                );
                
                tile.transform.position = new Vector2(i, y);
                Coordinate toCoordinate = ScriptableObject.CreateInstance<Coordinate>();
                toCoordinate.setTile(tileClone);
                toCoordinate.setX(i);
                toCoordinate.setY(y);
                bool waterSpawn = SpawnWaterChance();
                bool shouldSpawn = false;
                GameObject spawnedGameObject;
                if (!waterSpawn)
                {
                    shouldSpawn = SpawnChance();
                   
                }

                if (waterSpawn)
                {
                    spawnedGameObject=SpawnWater();
                }

                if (shouldSpawn)
                {
                    spawnedGameObject=SpawnObj();
                    toCoordinate.GiveItem(spawnedGameObject);
                }
                _coords.AddLast(toCoordinate);
            }
            
        }

        private GameObject SpawnObj()
        {
            throw new System.NotImplementedException();
        }

        private GameObject SpawnWater()
        {
            float chance = RandomNumberGenerator.GetInt32(0, 120);


        }

        private bool SpawnWaterChance()
        {

            float chance = RandomNumberGenerator.GetInt32(0, 120);
            if (chance / 10 > success)
            {
                return true;
            }
            TimeSinceFloat = TimeSinceFloat + anotherSquare;
            if (TimeSinceFloat > 1)
            {
                TimeSinceFloat--;
                return true;
            }
            return false;
        }


        public void RemoveRow(int x, int y)
        { 
            while(_coords.First().GetY() == y)
            {
                Destroy(_coords.First.Value.GetItem());
                Destroy(_coords.First.Value.GetTile());
                _coords.RemoveFirst();
            }
        }

        public bool IsEmpty()
        {
            return _coords.Count == 0;
        }

        private bool SpawnChance()
        {
            float chance = RandomNumberGenerator.GetInt32(0, 100);
            if (chance / 10 > success)
            {
                return true;
            }

            TimeSinceFloat=TimeSinceFloat + anotherSquare;
            if (TimeSinceFloat > 1)
            {
                TimeSinceFloat--;
                return true;
            }
            return false;
            
        }


    }

   
}
