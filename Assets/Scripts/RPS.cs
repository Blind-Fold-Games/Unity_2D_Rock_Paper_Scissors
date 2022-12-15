using UnityEngine;

public class RPS : MonoBehaviour
{
    //rpsType: 0 = Rock; 1 = Paper; 2 = Scissors;
    public int rpsType;
    


    [SerializeField] private Sprite rockSprite;
    [SerializeField] private Sprite paperSprite;
    [SerializeField] private Sprite scissorsSprite;

    private SpriteRenderer spriteRenderer;

    private Vector2 position;



    [SerializeField] private float upBoarder;
    [SerializeField] private float downBoarder;
    [SerializeField] private float leftBoarder;
    [SerializeField] private float rightBoarder;


    // Start is called before the first frame update
    void Start()
    {
        rpsType = 0;
        Application.targetFrameRate = 30;
        spriteRenderer = GetComponent<SpriteRenderer>();




        InitializeTypes(Random.Range(0,3));

        position = transform.position;

        CalculateScreen();

    }

    private void InitializeTypes(int initialType)
    {
        rpsType = initialType;

        if (rpsType == 0)
        {
            spriteRenderer.sprite = rockSprite;

        }
        else if (rpsType == 1)
        {
            spriteRenderer.sprite = paperSprite;

        }
        else
        {
            spriteRenderer.sprite = scissorsSprite;
        }
    }

    private void CalculateScreen()
    {
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));


        upBoarder = stageDimensions.y;
        downBoarder = -stageDimensions.y;
        leftBoarder = -stageDimensions.x;
        rightBoarder = stageDimensions.x;
    }


    // Update is called once per frame
    void Update()
    {
        MoveAround();
        
    }

    void MoveAround()
    {
        position = position + new Vector2( (float)Random.Range(-1, 2)/20, (float) Random.Range(-1, 2)/20);
        position = new Vector3( Mathf.Clamp(position.x,leftBoarder, rightBoarder)
            , Mathf.Clamp(position.y, downBoarder, upBoarder), 0);

        transform.position = position;
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        int newType = other.GetComponent<RPS>().rpsType;

        if(newType != rpsType)
        {
            ChangeType(newType);
            print(gameObject.name + " Changing to: " + newType);
        }
    }

    private void ChangeType(int newType)
    {
        if(rpsType == 0)
        {
            if (newType == 1)
            {
                spriteRenderer.sprite = paperSprite;
                rpsType = newType;
            }          
        }
        else if( rpsType == 1)
        {
            if (newType == 2)
            {
                spriteRenderer.sprite = scissorsSprite;
                rpsType = newType;
            }
        }
        else
        {
            if (newType == 0)
            {
                spriteRenderer.sprite = rockSprite;
                rpsType = newType;
            }                     
        }
    }



}
