using UnityEngine;



public class RPS_Spawner : MonoBehaviour
{
    //NOTE: RPS has 3 types (0:Rock, 1:Paper, 2:Scissors)

    //The rps prefab which we will spawn when the game starts
    [SerializeField] private GameObject rpsPrefab;


    //An Enum that tells us in which way should the rps be spawned?
    //Random: Takes the SpawnNumber below and spawns rpsPrefab that many times, but the rps type is going to be random (randomly chooses rock paper scissor).
    //Even: Takes the SpawnNumber below and spawns rpsPrefab that many times, but the rps type is going to be evenly distributed between rock paper & scissors. EXAMPLE: If SpawnType is even and the SpawnNumber is 21, then rpsPrefab will be spawned 21 times (Rock:7 , Paper:7, Scissors:7).
    //Numbered: Numberd type will not use the SpawnNumber int, instead it's going to read the values of rockNumber,paperNumber & scissorsNumber below and will spawn that type based on the values of these integers.
    private enum SpawnType { Random, Even, Numbered }
    [SerializeField] SpawnType spawnType;


    //SpawnNumber used in (Random & Even) SpawnType. rockNumber,paperNumber & scissorsNumber are used in (Numbered) SpawnType.
    public int SpawnNumber;
    [SerializeField] private int rockNumber;
    [SerializeField] private int paperNumber;
    [SerializeField] private int scissorsNumber;


    //Variables that hold the values of the screen border
    private float upBoarder;
    private float downBoarder;
    private float leftBoarder;
    private float rightBoarder;



    private void Awake()
    {
        CalculateScreen();    
    }

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }


    //Spawns the rpsPrefabs
    private void Spawn()
    {
        //Spawn based on the spawnTypes of (Random, Even, Numbered)
        switch (spawnType)
        {
            //Random: Takes the SpawnNumber below and spawns rpsPrefab that many times, but the rps type is going to be random (randomly chooses rock paper scissor).
            case SpawnType.Random:
                for(int i =0; i < SpawnNumber; i++)
                {
                    //Spawn rpsPrefab at FindSpawnPoint(a random point on screen) without any rotation and make it a child of the gameobject that we put this script on.
                   GameObject instRps = Instantiate(rpsPrefab,FindSpawnPoint() , Quaternion.identity, transform);

                    //Randomly choose the type of the rps (0:Rock, 1:Paper, 2:Scissors)
                    //Remember Random.Range(int1,int2) the int2 is exclusive and is not calculated
                    instRps.GetComponent<RPS>().rpsType = Random.Range(0, 3);
                }
                break;


            //Even: Takes the SpawnNumber below and spawns rpsPrefab that many times, but the rps type is going to be evenly distributed between rock paper & scissors. EXAMPLE: If SpawnType is even and the SpawnNumber is 21, then rpsPrefab will be spawned 21 times (Rock:7 , Paper:7, Scissors:7).
            case SpawnType.Even:

                //Calculate the even value between the 3 types
                int firstSpawn = (int)Mathf.Ceil((float)SpawnNumber /3);
                int secondSpawn = (int)Mathf.Ceil(((float)SpawnNumber / 3)*2);     

                
                for (int i = 1; i <= SpawnNumber; i++)
                {
                    //Spawn... same as above
                    GameObject instRps = Instantiate(rpsPrefab, FindSpawnPoint(), Quaternion.identity, transform);


                    //Spawn evenly
                    if(i <= firstSpawn)
                    {
                        instRps.GetComponent<RPS>().rpsType = 0;
                    }
                    else if(i > firstSpawn && i <= secondSpawn)
                    {
                        instRps.GetComponent<RPS>().rpsType = 1;
                    }
                    else
                    {
                        instRps.GetComponent<RPS>().rpsType = 2;
                    }
                    
                }
                break;


            //Numbered: Numberd type will not use the SpawnNumber int, instead it's going to read the values of rockNumber,paperNumber & scissorsNumber below and will spawn that type based on the values of these integers.
            case SpawnType.Numbered:

                //Spawn based on each typeNumber
                for (int i = 1; i <= rockNumber; i++)
                {
                    GameObject instRps = Instantiate(rpsPrefab, FindSpawnPoint(), Quaternion.identity, transform);
                    instRps.GetComponent<RPS>().rpsType = 0;
                }
                for (int i = 1; i <= paperNumber; i++)
                {
                    GameObject instRps = Instantiate(rpsPrefab, FindSpawnPoint(), Quaternion.identity, transform);
                    instRps.GetComponent<RPS>().rpsType = 1;
                }
                for (int i = 1; i <= scissorsNumber; i++)
                {
                    GameObject instRps = Instantiate(rpsPrefab, FindSpawnPoint(), Quaternion.identity, transform);
                    instRps.GetComponent<RPS>().rpsType = 2;
                }

                break;
        }
    }


    //Randomly finds a place inside the screen borders in order to spawn an rpsPrefab at that point.
    private Vector2 FindSpawnPoint()
    {
        Vector2 position;
        position = new Vector2(Random.Range(leftBoarder, rightBoarder), Random.Range(downBoarder, upBoarder));

        return position;
    }


    //Calculates the borders of the screen
    private void CalculateScreen()
    {
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));


        upBoarder = stageDimensions.y;
        downBoarder = -stageDimensions.y;
        leftBoarder = -stageDimensions.x;
        rightBoarder = stageDimensions.x;
    }
}
