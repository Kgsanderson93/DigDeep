
using System.Collections.Generic;
using System.Linq;
using UnityEngine;




namespace Assets

{

    public class GridSystem : ScriptableObject
    {
        private LinkedList<Coordinate> _coords;
        


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
                _coords.AddLast(toCoordinate);
            }
            
        }


        public void RemoveRow(int x, int y)
        { 
            while(_coords.First().GetY() == y)
            {
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
