using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{

    [SerializeField] private GameObject fullDrop, emptyDrop, canvasReference;
    private int startingHealth = 6;
    private int totalHealth = 12;
    //[SerializeField] private Image fullSprite, emptySprite;
    private Image[] imageComponents;
    private GameObject[] rainDropsArray, emptyRainDropsArray;
    private int currentHealth;
    private int currentHealthCap = 6;
    [SerializeField] private Vector3 startingPosition;
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
                currentHealth++;
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
        
    }

    //private helper method to determine placement of new droplet
    private Vector3 placementPosition(int totalHealth, int currentHealth)
    {

        //we're going to start at level zero for simplicity sake
        //int ylevel = (totalHealth % 10);
        int xlevel = currentHealth;
        

        Vector3 newPosition = startingPosition + new Vector3(dropUIspacingInX * currentHealth, 0, 0);
        

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
        
        currentHealth--;
        if (currentHealth <= 0 )
        {
            rainDropsArray[0].SetActive(false); 
            //lose game
            Debug.Log("water is zero so player has lost");
            return;
        }
        else{
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
            currentHealth++;
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
            currentHealth--;
            currentHealthCap--;
            return;
        }
        if (currentHealthCap == currentHealth)
        {
            //Debug.Log(totalHealth + "totald, " + currentHealth + "current, " + currentHealthCap + " cap");
            emptyRainDropsArray[currentHealthCap - 1].SetActive(false);
            rainDropsArray[currentHealthCap - 1].SetActive(false);
            currentHealth--;
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



}
