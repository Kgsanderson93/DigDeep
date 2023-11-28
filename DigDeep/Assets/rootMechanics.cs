using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootMechanics : MonoBehaviour
{

    [SerializeField] private GameObject babyRoot, daddyRoot, rightDownRoot, downLeftRoot, leftDownRoot, downRightRoot, sidewaysRootFull, groundGrid, waterBarHolder;
    private Vector2 rootDirection = Vector2.down;
    private Vector2 previousRootDirection = Vector2.down;
    private Transform position;
    private bool babyRootFlip = true;
    private int distanceLeft, distanceRight, depth;

    private static SpriteRenderer spriteRendererForRoot;
    private GameObject waterMethods;


    // Start is called before the first frame update
    private void Start()
    {
       
        spriteRendererForRoot = gameObject.GetComponent<SpriteRenderer>();
        waterMethods =  gameObject.GetComponent<WaterBar>().gameObject;
        //waterMethods =  waterBarHolder.GetComponentsInChildren(typeof(WaterBar))[0].gameObject;
        

        position = babyRoot.transform;
    }

    void Update()
    {
        // if (currentHealth > 0)
        // {
            if (Input.GetKeyDown(KeyCode.D))
        {
            //check if rock is there and don't move if so



            WaterBar.moveWaterRight();
            distanceRight++;
            MoveRight();
            
            
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            //check if rock is there and don't move if so




            WaterBar.moveWaterLeft();
            distanceLeft++;
            MoveLeft();
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            //check if rock is there and don't move if so



            depth++;

        //need to change water bar reference to use actual script attatched to the canvas and it's methods
        //right now it is just calling the script that exists in the folder I think
        //WaterBar.takeDamage();
            if (depth >= 20)
            {
                //WaterBar.takeDamage();
                if (depth >= 40)
                {
                    //WaterBar.takeDamage();
                }
            }
            
            MoveDown();
            WaterBar.moveWaterDown();
        }
        //}

        
    }
    

    
    void MoveDown()
    {
            //do logic checking to determine where to grow
            Vector3 daddyRootPlacement = new Vector3(this.transform.position.x, this.transform.position.y, 0.0f);
            previousRootDirection = rootDirection;
            rootDirection = Vector2.down;
            this.transform.Translate(rootDirection);
            buildDADDYroot(rootDirection, previousRootDirection, daddyRootPlacement);
            
    }

    void MoveRight()
    {
            //checks if the root can be built, returns if it can't, preventing the build without costing efficiency
            if (invalidRootBuild(Vector2.right, rootDirection, this.transform.position.x))
            {
                return;
            }
            Vector3 daddyRootPlacement = new Vector3(this.transform.position.x, this.transform.position.y, 0.0f);
            previousRootDirection = rootDirection;
            rootDirection = Vector2.right;
            this.transform.Translate(rootDirection);
            //consider putting microscopic delay between move and build to avoid visual overlap
            //yield(.01f);
            buildDADDYroot(rootDirection, previousRootDirection, daddyRootPlacement);
    }

    void MoveLeft()
    {
            //checks if the root can be built, returns if it can't, preventing the build without costing efficiency
            if (invalidRootBuild(Vector2.left, rootDirection, this.transform.position.x))
            {
                return;
            }
            Vector3 daddyRootPlacement = new Vector3(this.transform.position.x, this.transform.position.y, 0.0f);
            previousRootDirection = rootDirection;
            rootDirection = Vector2.left;
            this.transform.Translate(rootDirection);
            buildDADDYroot(rootDirection, previousRootDirection, daddyRootPlacement);
    }


    //this is the big method for adding roots
    void buildDADDYroot(Vector2 rootDirection, Vector2 previousRootDirection, Vector3 daddyRootPlacement)
    {
        // now to allowed behaviors
        //side to side (if previous and current are sideways)
        if ((previousRootDirection == Vector2.left && rootDirection == Vector2.left) || (previousRootDirection == Vector2.right && rootDirection == Vector2.right))
        {
            Instantiate(sidewaysRootFull, daddyRootPlacement, Quaternion.identity);
            //and rotate baby root to face proper direction
            if (rootDirection == Vector2.right && babyRootFlip)
            {
                //change sprite to baby root facing right
            }
            else 
            {
                //rotate baby to face left
            }
            return;
        }
        //dif previous was down and now moving left
        else if (previousRootDirection == Vector2.down && rootDirection == Vector2.left)
        {
            Instantiate(downLeftRoot, daddyRootPlacement, Quaternion.identity);
            return;
        }
        //dif previous was left and now going DEEPER down
        else if (previousRootDirection == Vector2.left && rootDirection == Vector2.down)
        {
            Instantiate(leftDownRoot, daddyRootPlacement, Quaternion.identity);
            return;
        }
        //dif previous was down and now moving right
        else if (previousRootDirection == Vector2.down && rootDirection == Vector2.right)
        {

            Instantiate(downRightRoot, daddyRootPlacement, Quaternion.identity);
            return;
        }
        //dif previous was right and now going DEEPER down
        else if (previousRootDirection == Vector2.right && rootDirection == Vector2.down)
        {
            Instantiate(rightDownRoot, daddyRootPlacement, Quaternion.identity);
            return;
        }
        //if already going down, build another daddy root going DEEPER down
        else if (previousRootDirection == Vector2.down)
        {
            Instantiate(daddyRoot, daddyRootPlacement, Quaternion.identity);
            return;
        }
        
    }


    private bool invalidRootBuild(Vector2 rootDirection, Vector2 previousRootDirection, float currentXcoordinant)
    {
        if ((rootDirection == Vector2.left && previousRootDirection == Vector2.right) || (rootDirection == Vector2.right && previousRootDirection == Vector2.left))
        {
            return true;
        }
        //here is a great place to stop the player from moving past a desired x bounds (on either side) by returning true
        // if (currentXcoordinant >= farXBoundary || currentXcoordinant <= closeXBoundary)
        // {
        //     return true;
        // }
        return false;
    }

}

