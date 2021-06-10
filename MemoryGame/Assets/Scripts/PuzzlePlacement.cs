using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlacement : MonoBehaviour
{
    public Vector2[] puzzleFitLocalPositions; //"correct" local positions for puzzle pieces
    public float errorAllowedPuzzles, errorAllowedFlowers; //error for puzzle auto-fit

    public bool[] puzzleFit; //fit status for puzzle pieces
    public int puzzleCheckNum;

    public PuzzlePiece[] puzzlePieces;

    private void Awake()
    {
        puzzleFit = new bool[puzzleFitLocalPositions.Length];
        puzzleCheckNum = 4;

        if (puzzlePieces.Length < 7) Debug.LogError("num puzzles wrong");

        for (int p = 4; p < 7; p++)
        {
            puzzlePieces[p].gameObject.SetActive(false);
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public bool checkForPlacement(int puzzleID, PuzzlePiece puzzlePiece)
    {
        float errorAllowed = (puzzleID <= 4) ? errorAllowedPuzzles : errorAllowedFlowers;

        if (puzzleID > puzzleFitLocalPositions.Length) Debug.LogError("invalid puzzle id");

        if(Vector2.Distance(puzzlePiece.transform.localPosition, puzzleFitLocalPositions[puzzleID - 1]) <= errorAllowed)
        {
            puzzlePiece.transform.localPosition = puzzleFitLocalPositions[puzzleID - 1]; //fitting
            //sfx

            puzzleFit[puzzleID - 1] = true;

            //check for all match
            bool intact = true;
            for (int b = 0; b < puzzleCheckNum; b++) if (!puzzleFit[b]) intact = false;
            if (intact) allPuzzlesFitted();

            return true;
        }

        return false;
    }

    public void allPuzzlesFitted()
    {
        
        if (puzzleCheckNum == 4)
        {
            puzzleCheckNum = 7; //check for flowers next
                                //show flowers
            for (int p = 4; p < 7; p++)
            {
                puzzlePieces[p].gameObject.SetActive(true);
            }
        }else if(puzzleCheckNum == 7)
        {
            //flower fit
        }
        else
        {
            Debug.LogError("something wrong");
        }

    }
}
