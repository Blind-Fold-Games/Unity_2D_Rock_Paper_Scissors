using UnityEngine;

public class RPS : MonoBehaviour
{

    //Reference to gameData Script
    private GameData gameData;

    //Audio Source on this game object
    private AudioSource audioSource;
    


    //rpsType: 0 = Rock; 1 = Paper; 2 = Scissors;
    public int rpsType;

    //Images or Sprites of rock paper and scissor
    [SerializeField] private Sprite rockSprite;
    [SerializeField] private Sprite paperSprite;
    [SerializeField] private Sprite scissorsSprite;

    //Reference to our sprite renderer
    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        gameData = GameData.instance;        
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        InitializeTypes();
    }

    //Choose the type of Rock or Paper or Scissor when the game starts
    //rpsType: 0 = Rock; 1 = Paper; 2 = Scissors;
    private void InitializeTypes()
    {

        if (rpsType == 0)
        {
            spriteRenderer.sprite = rockSprite;
            gameData.AddRock(transform);
        }
        else if (rpsType == 1)
        {
            spriteRenderer.sprite = paperSprite;
            gameData.AddPaper(transform);
        }
        else
        {
            spriteRenderer.sprite = scissorsSprite;
            gameData.AddScissor(transform);
        }
    }


    //When something comes in contact with this gameobject
    //PS: Tried OnTriggerEnter and OnTriggerEnter i prefered the OnTriggerStay for this situation as it is more precise and suitable
    private void OnTriggerStay2D(Collider2D other)
    {
        //If the game is not paused
        if (LevelManager.instance.isGameOn)
        {
            //Check if the thing we hit is either rock paper or scissor
            int newType = other.GetComponent<RPS>().rpsType;

            //If thier type is different than our type
            if (newType != rpsType)
            {
                //Check if we need to change our type
                ChangeType(newType);

                //Removes the targets which is used in RPS_Movement script
                //PS:There are better ways to handle this for sure
                gameData.RemoveTargets();
            }
        }
    }


    //Check if we need to change our type based on what we hit
    private void ChangeType(int newType)
    {
        //If we are rock then check if we hit paper, if so then change oursef to paper
        if (rpsType == 0)
        {
            if (newType == 1)
            {
                spriteRenderer.sprite = paperSprite;
                rpsType = newType;
                gameData.AddPaper(transform);
                gameData.RemoveRock(transform);

                //Play Paper Aduio
                audioSource.clip = gameData.PaperSound;
                audioSource.Play();
            }          
        }
        //If we are paper then check if we hit scissor, if so then change oursef to scissor
        else if ( rpsType == 1)
        {
            if (newType == 2)
            {
                spriteRenderer.sprite = scissorsSprite;
                rpsType = newType;
                gameData.AddScissor(transform);
                gameData.RemovePaper(transform);

                //Play Scissor Aduio
                audioSource.clip = gameData.ScissorSound;
                audioSource.Play();
            }
        }
        //If we are scissor then check if we hit rock, if so then change oursef to rock
        else
        {
            if (newType == 0)
            {
                spriteRenderer.sprite = rockSprite;
                rpsType = newType;
                gameData.AddRock(transform);
                gameData.RemoveScissor(transform);

                //Play Rock Aduio
                audioSource.clip = gameData.RockSound;
                audioSource.Play();
            }
        }
    }

}
