using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlacement : MonoBehaviour
{
    public Vector2[] puzzleFitLocalPositions; //"correct" local positions for puzzle pieces
    public float errorAllowed; //error for puzzle auto-fit

    public bool[] puzzleFit; //fit status for puzzle pieces


    private void Awake()
    {
        puzzleFit = new bool[4];
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void allPuzzlesFitted()
    {

    }
}
