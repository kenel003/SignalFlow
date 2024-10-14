using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericControl : MonoBehaviour
{
    
    private MouseSelectionController mouseSelection;
    [SerializeField] private string controlName;
    [SerializeField] private float dialMin = 0f;
    [SerializeField] private float dialMax = 359f;
    private float dialSpeed = 90f;
    [SerializeField] private float toggleUpHeight = 0.1f;
    [SerializeField] private float toggleDownHeight = 0.015f;
    [SerializeField] private float sliderMin = 0f;
    [SerializeField] private float sliderMax = 1.25f;
    [SerializeField] private float sliderSpeed = .15f;
    private float dialRot = 0;
    private float sliderValue = -3f, sliderPercent = 0f, dialPercent = 0f;
    private bool toggled = false;
    private float controlValue = 0;
    void Start()
    {
        mouseSelection = GameObject.Find("Main Camera").GetComponent<MouseSelectionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseSelection.clickedObject == gameObject )
        {
            if (mouseSelection.clickedObject.CompareTag("Dial"))
            {
                if (dialRot < dialMin)
                {
                    dialRot = dialMin;
                }
                if (dialRot > dialMax)
                {
                    dialRot = dialMax;
                }
                if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) &&(dialRot >= dialMin && dialRot <= dialMax))
                {
                    controlValue = Input.GetAxis("Horizontal");
                    if (controlValue < 0)
                    {
                        dialRot -= dialSpeed * Time.deltaTime;
                    }
                    if (controlValue > 0)
                    {
                        dialRot += dialSpeed * Time.deltaTime;
                    }
                    //Sets dial rotation based on dialRot
                    transform.localEulerAngles = new Vector3(mouseSelection.clickedObject.transform.localEulerAngles.x, dialRot, mouseSelection.clickedObject.transform.localEulerAngles.z);
                }/*If the dialRot is inside the bounds, the user can change it with the arrow keys
                  * The dialRot is then used to set a new Y axis EulerAngle*/
            }//If the game object is a dial, then it can move.

            else if (mouseSelection.clickedObject.CompareTag("Slider"))
            {
                if (sliderValue < sliderMin)
                {
                    sliderValue = sliderMin;
                }
                if (sliderValue > sliderMax)
                {
                    sliderValue = sliderMax;
                }
                if (sliderValue >= sliderMin && sliderValue <= sliderMax)
                {
                    controlValue = Input.GetAxis("Vertical");
                    if ( controlValue > 0)
                    {
                        if (sliderMax - sliderValue >= sliderSpeed * controlValue * Time.deltaTime) sliderValue += sliderSpeed * controlValue * Time.deltaTime;
                        else sliderValue = sliderMax; //gets rid of the 'hop' when maxed out
                    }

                    if (controlValue < 0)
                    {
                        if (sliderValue - sliderMin >= sliderSpeed * controlValue * Time.deltaTime) sliderValue -= -sliderSpeed * controlValue * Time.deltaTime;
                        else sliderValue = sliderMin;
                    }
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, sliderValue);
                }
            }

            else if (mouseSelection.clickedObject.CompareTag("Toggle") && mouseSelection.newClick) //needed newClick to make sure toggles stay
            {
                //play click sound
                mouseSelection.newClick = false; //keep toggle from being toggled until the next new click of the mouse
                if (transform.localPosition.y >= toggleUpHeight) //toggle currently off
                {
                    toggled = true; //turns toggle on
                    Debug.Log("Toggled ON at original Position: " + transform.localPosition.y);
                    transform.localPosition = new Vector3(transform.localPosition.x, toggleDownHeight, transform.localPosition.z);
                }
                else if (transform.localPosition.y <= toggleDownHeight) //toggle is currently on
                {
                    Debug.Log("Toggled OFF at position: " + transform.localPosition.y);
                    toggled = false; //turns toggle off
                    transform.localPosition = new Vector3(transform.localPosition.x, toggleUpHeight, transform.localPosition.z);
                }
            }

        }
    }
}
