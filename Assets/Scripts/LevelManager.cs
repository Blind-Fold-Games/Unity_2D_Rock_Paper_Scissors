using System.Collections;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    //Public instance of the LevelManager
    public static LevelManager instance;

    //isGameOn used to Pause/Unpause the game
    public bool isGameOn = false;

    //The below fields are used to show the countdown when the game is started
    [SerializeField] private GameObject startPannel;
    [SerializeField] private int countDown;
    [SerializeField] private TextMeshProUGUI countDownText;

    private void Awake()
    {
        instance = this;
        PauseGame();

        //We set the framerate on 20 on purpose because we want the items to jitter slowly
        Application.targetFrameRate = 20;            
    }

    // Start is called before the first frame update
    void Start()
    {     
        countDownText.text = countDown.ToString();
        StartCoroutine(DoCountDown());
    }

    //Countdown from the integer countDown till it gets to 0
    private IEnumerator DoCountDown()
    {
        yield return new WaitForSeconds(1);
        countDown--;
        countDownText.text = countDown.ToString();

        if (countDown > 0)
        {
            StartCoroutine(DoCountDown());
        }
        else
        {
            startPannel.SetActive(false);
            ResumeGame();
        }

    }


    //Pause the game
    public void PauseGame()
    {
        isGameOn = false;
    }

    //Resume the game
    public void ResumeGame()
    {
        isGameOn = true;
        //Time.timeScale = 1;
    }
}
