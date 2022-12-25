using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    //Game data holds bunch of data and functions for each rps object (Rock Paper Scissor)

    //Public instance of the current script
    public static GameData instance;

    //A list that contains all of the transforms for each type of rps
    [SerializeField] private List<Transform> rockTransforms;
    [SerializeField] private List<Transform> paperTransforms;
    [SerializeField] private List<Transform> scissorsTransforms;

    //A list that contains the RPS_Movement script of every rps - to use it in the RemoveTargets() script
    [SerializeField] private List<RPS_Movement> rps_Movements;

    //Shared Sounds
    public AudioClip RockSound;
    public AudioClip PaperSound;
    public AudioClip ScissorSound;


    //Variables that hold the values of the screen border
    public float upBoarder;
    public float downBoarder;
    public float leftBoarder;
    public float rightBoarder;


    private void Awake()
    {      
        instance = this;
        CalculateScreen();
    }

    //Get the nearest enemy's transform
    public Transform GetRock(Vector3 position)
    {
        if (rockTransforms.Count > 0)
        {
            float enemyDistance = rockTransforms[0].position.magnitude;
            Transform enemyTransform = rockTransforms[0];
            foreach (Transform t in rockTransforms)
            {
                float distance = (t.position - position).magnitude;
                if (distance < enemyDistance)
                {
                    enemyTransform = t;
                }
            }
            return enemyTransform;
        }
        else return null;
    }
    public Transform GetPaper(Vector3 position)
    {
        if (paperTransforms.Count > 0)
        {
            float enemyDistance = paperTransforms[0].position.magnitude;
            Transform enemyTransform = paperTransforms[0];
            foreach (Transform t in paperTransforms)
            {
                float distance = (t.position - position).magnitude;
                if (distance < enemyDistance)
                {
                    enemyTransform = t;
                }
            }
            return enemyTransform;
        }
        else return null;
    }
    public Transform GetScissor(Vector3 position)
    {
        if (scissorsTransforms.Count > 0)
        {
            float enemyDistance = scissorsTransforms[0].position.magnitude;
            Transform enemyTransform = scissorsTransforms[0];
            foreach (Transform t in scissorsTransforms)
            {
                float distance = (t.position - position).magnitude;
                if (distance < enemyDistance)
                {
                    enemyTransform = t;
                }
            }
            return enemyTransform;
        }
        else return null;
    }


    //Add items to each of the rps lists we have
    public void AddRock(Transform rock)
    {
        rockTransforms.Add(rock);
    }
    public void AddPaper(Transform paper)
    {
        paperTransforms.Add(paper);
    }
    public void AddScissor(Transform Scissor)
    {
        scissorsTransforms.Add(Scissor);
    }


    //Remove items from each of the rps lists we have
    public void RemoveRock(Transform removedRock)
    {
        foreach (Transform t in rockTransforms)
        {
            if(t == removedRock)
            {
                rockTransforms.Remove(t);
                break;
            }
        }
    }
    public void RemovePaper(Transform removedPaper)
    {
        foreach (Transform t in paperTransforms)
        {
            if (t == removedPaper)
            {
                paperTransforms.Remove(t);
                break;
            }
        }
    }
    public void RemoveScissor(Transform removedScissor)
    {
        foreach (Transform t in scissorsTransforms)
        {
            if (t == removedScissor)
            {
                scissorsTransforms.Remove(t);
                break;
            }
        }
    }


    //Add items to the list of rps_movement scripts
    public void AddRps_Movements(RPS_Movement rps_movement)
    {
        rps_Movements.Add(rps_movement);
    }

    //Remove targets from everyone
    //NOTE: This is heavy and can be done better but it takes time to write better code for it
    public void RemoveTargets()
    {
        foreach (RPS_Movement t in rps_Movements)
        {
            t.isTargetLocked = false;
        }
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
