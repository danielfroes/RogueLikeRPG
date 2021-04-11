using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionBar : MonoBehaviour {
    //Text component of the number of actions
    [SerializeField] private TextMeshProUGUI numActionsUI = null;
    //Fillable image 
    [SerializeField] public Image fillImg = null;
    //Background of the slider
    [SerializeField] private Image backgroundImg = null;

    //Color of the action bars; the first Color is the initial background Img;
    [SerializeField]private List<Color> barColors = null;
    [SerializeField] private float fillSpeed = 0.2f;
    public static ActionBar instance;
    private int colorIndex = 0;
    public bool _isFull;
    //quantity of actions charged
    private int _numActions;
    public int numActions
    {
        get { return _numActions;}
        set 
        { 
            //whenever the property value is changed, the UI text changes along with it;
            _numActions = value;
            numActionsUI.text = numActions.ToString(); 
        }   
    }
    
    
    // Start is called before the first frame update
    private void Awake() {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void Start() {
        numActions = 0;
        _isFull = false;
        backgroundImg.color = barColors[0];
        fillImg.color = barColors[1];
        colorIndex = 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        fillImg.fillAmount += fillSpeed*Time.deltaTime;

        if (!(Math.Abs(fillImg.fillAmount - 1) < 0.01f) || _isFull) return;
        
        //changes the intermediary
        if(colorIndex < barColors.Count)
        {

            ChangeBarColor();
            fillImg.fillAmount = 0; //slider value goes to minimun
            numActions++;
        }
        //Last bar is full
        else if(colorIndex == barColors.Count)
        {
            numActions++;
            _isFull = true;
        }

    }


    public void SpendAction(int amount)
    {
        numActions -= amount;
        
        if(_isFull)
        {
            _isFull = false;
            fillImg.fillAmount = 0; //slider value goes to minimun
            colorIndex -= amount-1;
        }
        else
        {
            colorIndex -= amount+1;
            ChangeBarColor();
        }
    }


    private void ChangeBarColor()
    {
        backgroundImg.color = barColors[colorIndex - 1]; //Changes the color of the background to look that the action bars is overlayed
        
        fillImg.color = barColors[colorIndex]; //fill value changes to the next color to seems another bar
        colorIndex++;
        // numActions++;
        // colorIndex++;
    }

    void OnDestroy()
    {
        instance = null;
    }
}
