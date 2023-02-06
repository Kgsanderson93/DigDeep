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
       
        [SerializeField] private float success = 9f;
        
        //[SerializeField] private int spawnBig = 2;
        [SerializeField] private int spawnMed = 6;





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

                GameObject tileClone = (GameObject)Instantiate(tile);
                
                tile.transform.position = new Vector2(i, y);
                Coordinate toCoordinate = ScriptableObject.CreateInstance<Coordinate>();
                toCoordinate.setTile(tileClone);
                toCoordinate.setX(i);
                toCoordinate.setY(y);
                
                GameObject spawnedGameObject= SpawnWaterChance(i,y);
                //if that doesnt spawn something
                if (spawnedGameObject==null)
                {
                    Debug.Log("tried to spawn obj");
                    spawnedGameObject=SpawnChance(i, y);
                }
                //if either spawned give the item to the Coord
                if (spawnedGameObject != null)
                {
                    if (y <= 0)
                    {
                        toCoordinate.GiveItem(spawnedGameObject);
                    }
                }
                //either way add the coord
                _coords.AddLast(toCoordinate);
            }
            
        }

        private GameObject SpawnObj(int x, int y)
        {
            //would like to add more obstacles here but not now :/

            float chance = RandomNumberGenerator.GetInt32(0, 20);
            if (chance>2) {
                GameObject rockGameObject = (GameObject)Instantiate(Resources.Load("Rock"));
                rockGameObject.AddComponent<BoxCollider2D>();
                rockGameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                rockGameObject.tag = "Rock";
                rockGameObject.transform.position = new Vector2(x, y);
                return rockGameObject;
            }
            else
            {
                GameObject fertilizerGameObject = (GameObject)Instantiate(Resources.Load("Fertilizer"));
                fertilizerGameObject.AddComponent<BoxCollider2D>();
                fertilizerGameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                fertilizerGameObject.tag = "Fertilizer";
                fertilizerGameObject.transform.position = new Vector2(x, y);
                return fertilizerGameObject;
            }
        }

        private GameObject SpawnWater(int x, int y)
        {
            
            float chance = RandomNumberGenerator.GetInt32(0, 10);
            
            //if (_coords.Count>20&& chance <spawnBig && _coords.Last().ReturnEmpty()&& _coords.Last().GetY()==y)
            //{
            //    Debug.Log("SpawnBig"+_coords.Last().ReturnEmpty());
              //  GameObject leftWaterGameObject = (GameObject)Instantiate(Resources.Load("LeftBigWater"));
                //leftWaterGameObject.transform.position=new Vector2(x-1,y);
                //leftWaterGameObject.AddComponent<BoxCollider2D>();
                //leftWaterGameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                //leftWaterGameObject.tag = "BigWaterLeft";

//                _coords.Last.Value.GiveItem(leftWaterGameObject);
  //              
    //            GameObject rightWaterGameObject = (GameObject)Instantiate(Resources.Load("RightBigWater"));
      //          rightWaterGameObject.transform.position = new Vector2(x , y);
        //        rightWaterGameObject.AddComponent<BoxCollider2D>();
          //      rightWaterGameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            //    rightWaterGameObject.tag = "BigWaterRight";

              //  return rightWaterGameObject;
            //}
            if(chance<spawnMed)
            {
                GameObject medWaterGameObject = (GameObject)Instantiate(Resources.Load("MedWater"));
                medWaterGameObject.transform.position = new Vector2(x, y);
                medWaterGameObject.AddComponent<BoxCollider2D>();
                medWaterGameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                medWaterGameObject.tag = "MedWater";
                return medWaterGameObject;
            }
            else
            {
                GameObject smallWaterGameObject = (GameObject)Instantiate(Resources.Load("SmallWater"));
                smallWaterGameObject.transform.position = new Vector2(x, y);
                smallWaterGameObject.AddComponent<BoxCollider2D>();
                smallWaterGameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                smallWaterGameObject.tag = "SmallWater";
                return smallWaterGameObject;
            }



        }

        private GameObject SpawnWaterChance(int i, int y)
        {

            float chance = RandomNumberGenerator.GetInt32(0, 100);
            
            Debug.Log("chance"+chance);
            if (chance / 10 > success+.5)
            {
                return SpawnWater(i,y);
            }
            
            
            return null;
        }


       

        private GameObject SpawnChance(int x, int y)
        {
            float chance = RandomNumberGenerator.GetInt32(0, 100);
            Debug.Log("RolledChance" + chance);
            if (chance/10> success)
            {
                return SpawnObj(x, y);
            }

            
            return null;
            
        }

        public void RemoveRow(int x, int y)
        {
            while (_coords.First().GetY() == y)
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
    }

   
}
