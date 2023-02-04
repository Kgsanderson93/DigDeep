using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.Tilemaps;

namespace Assets

{

    public class GridSystem
    {
        LinkedList
        private GridSystem current;


        private GridSystem()
        {
            _coords = new LinkedList<Coordinate>();
            
            
        }

        private void start()
        {
            getInstance();
        }

        public GridSystem getInstance()
        {
            if (current == null)
            {
                this.current= new GridSystem();
            }
            return this.current;
        }

        public void AddRow(int x, int y)
        {
            Coordinate c = _coords.Last();
            if (c.getY == y)
            {
                return;
            }

            for (int i = x; i < x + 30; i++)
            {
                
               _c
            }
        }

        public void removeRow(int x, int y)
        {
            

        }

    }

   
}
