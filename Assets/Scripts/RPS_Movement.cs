using UnityEngine;

public class RPS_Movement : MonoBehaviour
{
    //NOTE: RPS has 3 types (0:Rock, 1:Paper, 2:Scissors)

    //Get refrences to other scripts
    private GameData gameData;
    private LevelManager levelManager;
    private RPS rps;

    //Checks if we have a target
    public bool isTargetLocked;

    //Used to calculate the position we want to go to
    private Vector2 position;
    [SerializeField] Vector3 targetLocation;    
    [SerializeField] Transform enemyTransform;

    [SerializeField] private float ourY;


    //Used to calculate the size and borders of the screen
    private float upBoarder;
    private float downBoarder;
    private float leftBoarder;
    private float rightBoarder;

    // Start is called before the first frame update
    void Start()
    {
        //Get references
        rps = GetComponent<RPS>();
        gameData = GameData.instance;
        levelManager = LevelManager.instance;

        //Add this script to the list in gameData (used to remove old targets)
        gameData.AddRps_Movements(this);

        position = transform.position;
        isTargetLocked = false;

        GetScreenSizes();
    }

    // Update is called once per frame
    void Update()
    {
        //If the game is not paused
        if (levelManager.isGameOn)
        {
            //Check if we have a target or not (If not, then try to find)
            if (!isTargetLocked)
            {
                FindNewTarget();
            }

            MoveAround();
        }
       
    }


    void MoveAround()
    {
        //If we have a target then we will try to go to it (While jittering)
        if (isTargetLocked)
        {
            targetLocation = enemyTransform.position - transform.position;

            ourY = transform.position.y;

            //Wierd movement of jittering while also moving to a target. 
            //The number on the right (currently 35) is used to determine the distance of jitterness. Mess around with it to see the effect
            //To make it jittery we used random between 0 and the desired value
            if (targetLocation.x > 0)
            {
                position.x = position.x + (float)Random.Range(0, 2) / 35;
            }
            else
            {
                position.x = position.x + (float)Random.Range(-1, 1) / 35;
            }

            if (targetLocation.y > 0)
            {
                position.y = position.y + (float)Random.Range(0, 2) / 35;
            }
            else
            {
                position.y = position.y + (float)Random.Range(-1, 1) / 35;
            }

        }
        //If we dont have a target then we will try to move randomly (While jittering)
        else
        {
            //move around when you have no target
            //make sure you don't pass the screen borders
            position = position + new Vector2((float)Random.Range(-1, 2) / 20, (float)Random.Range(-1, 2) / 45);
            position = new Vector3(Mathf.Clamp(position.x, leftBoarder, rightBoarder) , Mathf.Clamp(position.y, downBoarder, upBoarder), 0);
        }

        transform.position = position;
    }


    //Find a new target (enemy) based on your type
    //Rock:0 finds Scissors, Paper:1 finds Rocks, Scissors:2 find Papers
    void FindNewTarget()
    {
        if(rps.rpsType == 0)
        {
            enemyTransform = gameData.GetScissor(transform.position);
            if(enemyTransform != null)
            {
                isTargetLocked = true;
            }
            else
            {
                isTargetLocked = false;
            }
        }
        else if(rps.rpsType == 1)
        {
            enemyTransform = gameData.GetRock(transform.position);
            if (enemyTransform != null)
            {
                isTargetLocked = true;
            }
            else
            {
                isTargetLocked = false;
            }
        }
        else
        {
            enemyTransform = gameData.GetPaper(transform.position);
            if (enemyTransform != null)
            {
                isTargetLocked = true;
            }
            else
            {
                isTargetLocked = false;
            }
        }
    }


    //Calculate the values of our screen to find each boarder
    //Here we just get it from the values of gameData because we did the same there too
    private void GetScreenSizes()
    {
        upBoarder = gameData.upBoarder;
        downBoarder = gameData.downBoarder;
        leftBoarder = gameData.leftBoarder;
        rightBoarder = gameData.rightBoarder;
    }
}
