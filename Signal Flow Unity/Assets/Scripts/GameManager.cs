using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Game Manager provides text for start, end, and restart of game.
    //Game Manager also determines the end of the game.

    //Selector script
    private MouseSelectionController mouseSelection;

    //Dial Scripts
    public LedControl[] ledNodes;
    public GenericControl[] channel1, channel2, channel3, channel4, channel5_6, channel7_8, channel9_10, channel11_12, channelFX, channelSub1_2, channelMain;
    // Variables for Text
    public GameObject selected, titleScreen, instructionScreen, finishText;
    public TextMeshProUGUI selectText, titleText;
    public Button start1Button, restartButton, start2Button;
    public TextMeshProUGUI descriptionText, gameDoneText;

   
    void Start()
    {
        mouseSelection = GameObject.Find("Main Camera").GetComponent<MouseSelectionController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Tells User what Game Object is selected.
        if (mouseSelection.clickedObject.CompareTag("Dial") || mouseSelection.clickedObject.CompareTag("Slider") || mouseSelection.clickedObject.CompareTag("Toggle"))
        {
            selectText.text = "Currently Selected: " + mouseSelection.clickedObject.name;
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        finishText.gameObject.SetActive(true);
        selected.gameObject.SetActive(false);
    }//End of game text when game ends. Stops selected GameObject text. Delays game slightly

    public void Start1()
    {
        titleScreen.gameObject.SetActive(false);
        instructionScreen.gameObject.SetActive(true);
    }//Goes through starts screen and instruction screen. On Button press.

    public void Start2()
    {
        instructionScreen.gameObject.SetActive(false);
        selected.gameObject.SetActive(true);
    }//Ends instruction screen. Starts selected object text. On Button press.

    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }//If the restart button is pressed, restart the game.
}
