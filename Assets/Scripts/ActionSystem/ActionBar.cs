using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionBar : MonoBehaviour
{    
    //Action slider
    [SerializeField]private Slider actionSlider = null; 
    //Text component of the number of actions
    [SerializeField] private TextMeshProUGUI numActionsUI = null;
    //Fill of the slider
    [SerializeField] private Image fillImg = null;
    //Background of the slider
    [SerializeField] private Image backgroundImg = null;

    //Color of the action bars; the first Color is the initial background Img;
    [SerializeField]private List<Color> barColors = null;
    private int colorIndex = 0;

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
        // barColors = new List<Color>(); 
    }

    private void Start() {
        numActions = 0;
        backgroundImg.color = barColors[0];
        fillImg.color = barColors[1];
        colorIndex = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            actionSlider.value += 0.1f;
        }

        if(actionSlider.value == actionSlider.maxValue && colorIndex < barColors.Count)
        {
            
            backgroundImg.color = fillImg.color; //Changes the color of the background to look that the action bars is overlayed
            actionSlider.value = actionSlider.minValue; //slider value goes to minimun
            fillImg.color = barColors[colorIndex]; //fill value changes to the next color to seems another bar
            
            numActions++;
            colorIndex++;

        }
    }
}
