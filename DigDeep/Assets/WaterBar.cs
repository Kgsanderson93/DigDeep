using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{

    [SerializeField] private GameObject fullDrop, emptyDrop, canvasReference;
    public int startingHealth = 6;
    private static int totalHealth = 12;
    //[SerializeField] private Image fullSprite, emptySprite;
    private Image[] imageComponents;
    private static GameObject[] rainDropsArray, emptyRainDropsArray;
    public int currentHealth;
    public int currentHealthCap = 6;
    [SerializeField] private Vector2 startingPosition;
    [SerializeField] private float dropUIspacingInX, dropUIspacingInY;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 0;
        rainDropsArray = new GameObject[totalHealth + 1];
        emptyRainDropsArray = new GameObject[totalHealth + 1];
        imageComponents = new Image[totalHealth + 1];
        for (int i = 0; i < totalHealth; i++)
        {
            Debug.Log("drip");
            var newEmptyDrop = Instantiate(emptyDrop, placementPosition(totalHealth, i), Quaternion.identity);
            newEmptyDrop.transform.SetParent(canvasReference.transform, false);
            var newDrop = Instantiate(fullDrop, placementPosition(totalHealth, i), Quaternion.identity);
            newDrop.transform.SetParent(canvasReference.transform, false);
            if (currentHealth >= startingHealth)
            {
                newDrop.SetActive(false); 
                newEmptyDrop.SetActive(false); 
                
            }
            else{
                addHealth();
            }
            

            
            
            rainDropsArray[i] = newDrop;
            emptyRainDropsArray[i] = newEmptyDrop;


        }
        //current health and total health should be the same
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            takeDamage();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            heal();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            increaseMax();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            decreaseMax();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            moveWaterDown();
        }
        
    }

    //private helper method to determine placement of new droplet
    private Vector2 placementPosition(int totalHealth, int currentHealth)
    {

        //we're going to start at level zero for simplicity sake
        //int ylevel = (totalHealth % 10);
        int xlevel = currentHealth;
        

        Vector2 newPosition = startingPosition + new Vector2(dropUIspacingInX * currentHealth, 0);
        

        return newPosition;
    }

    // private void updateHealth(int totalHealth, int currentHealth)
    // {
    //     //Debug.Log(totalHealth + " " + currentHealth);
    //     if (totalHealth == currentHealth)
    //     {
    //         return;
    //     }
    //     else
    //     {
    //         Debug.Log(totalHealth + " " + currentHealth);
    //         // imageComponents = GetComponentsInChildren<Image>();
    //         //rainDropsArray[i].sprite = 
            

    //     }
    // }

    public void takeDamage()
    {
        
        
        if (currentHealth <= 0 )
        {
            rainDropsArray[0].SetActive(false); 
            //lose game
            Debug.Log("water is zero so player has lost");
            return;
        }
        else{
            removeHealth();
            rainDropsArray[currentHealth].SetActive(false); 
            emptyRainDropsArray[currentHealth].SetActive(true);
        }
        
        
        
    }

    public void heal()
    {
        if (currentHealth < currentHealthCap)
        {
            rainDropsArray[currentHealth].SetActive(true);
            emptyRainDropsArray[currentHealth].SetActive(false);
            addHealth();
            return;
            
        }
        //having problem at end so I'll single it out
        else if (currentHealthCap == totalHealth)
        {
            // rainDropsArray[totalHealth - 1].SetActive(true);
            // emptyRainDropsArray[totalHealth - 1].SetActive(false);
            // currentHealth++;
            return;
        }
        //any other case
        
        
    }

    public void increaseMax()
    {
        //Debug.Log(totalHealth + "total, " + currentHealth + "current, " + currentHealthCap + " cap");
        //don't add another heart if already at max
        if (currentHealthCap >= totalHealth)
        {
            heal();
            return;
        }
        //if at one before was having problems so I singled it out
        if (currentHealthCap == totalHealth - 1)
        {
            emptyRainDropsArray[currentHealthCap].SetActive(true);
            currentHealthCap++;
            heal();
            return;
        }
        //adds another max heart and heals one
        if (currentHealthCap < totalHealth)
        {
            emptyRainDropsArray[currentHealthCap++].SetActive(true);
            heal();
        }
        
        

        
    }



    public void decreaseMax()
    {
        
        if (currentHealthCap <= startingHealth)
        {
            return;
        }
        //consider if cap and current are equal then the current also needs to get decremented
        //but if current isn't at cap we don't want to decrement
        if (currentHealthCap == currentHealth && currentHealth == 12)
        {
            emptyRainDropsArray[currentHealthCap - 1].SetActive(false);
            rainDropsArray[currentHealthCap - 1].SetActive(false);
            removeHealth();
            currentHealthCap--;
            return;
        }
        if (currentHealthCap == currentHealth)
        {
            //Debug.Log(totalHealth + "totald, " + currentHealth + "current, " + currentHealthCap + " cap");
            emptyRainDropsArray[currentHealthCap - 1].SetActive(false);
            rainDropsArray[currentHealthCap - 1].SetActive(false);
            removeHealth();
            currentHealthCap--;
            return;
        }
        //so if the cap is larger than the current then don't decrement health but still remove water droplet
        if (currentHealthCap > currentHealth)
        {
            emptyRainDropsArray[currentHealthCap - 1].SetActive(false);
            currentHealthCap--;
            return;
        }
        
        
        
    }

//this method will be called in player movement when going down not into obstacle
    public static void moveWaterDown()
    {
        for (int i = 0; i < totalHealth; i++)
        {
            //access both arrays and transform the raindrops down
            rainDropsArray[i].transform.position = rainDropsArray[i].transform.position + new Vector3(0, -1, 0);
            emptyRainDropsArray[i].transform.position = emptyRainDropsArray[i].transform.position + new Vector3(0, -1, 0);


        }
    }

    public static void moveWaterRight()
    {
        for (int i = 0; i < totalHealth; i++)
        {
            //access both arrays and transform the raindrops down
            rainDropsArray[i].transform.position = rainDropsArray[i].transform.position + new Vector3(1, 0, 0);
            emptyRainDropsArray[i].transform.position = emptyRainDropsArray[i].transform.position + new Vector3(1, 0, 0);


        }
    }

    public static void moveWaterLeft()
    {
        for (int i = 0; i < totalHealth; i++)
        {
            //access both arrays and transform the raindrops down
            rainDropsArray[i].transform.position = rainDropsArray[i].transform.position + new Vector3(-1, 0, 0);
            emptyRainDropsArray[i].transform.position = emptyRainDropsArray[i].transform.position + new Vector3(-1, 0, 0);


        }
    }


    public void addHealth()
    {
        currentHealth = currentHealth + 1;
    }

    public void removeHealth()
    {
        currentHealth = currentHealth - 1;
    }



}
