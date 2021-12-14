using UnityEngine;

public class PuzzlePlacement : MonoBehaviour
{
    [Inject(InjectFrom.Anywhere)]
    public globalStateStore globalState;

    public Vector2[] puzzleFitLocalPositions; //"correct" local positions for puzzle pieces
    public float errorAllowedPuzzles, errorAllowedFlowers; //error for puzzle auto-fit
    public GameObject brokenVase;

    public bool[] puzzleFit; //fit status for puzzle pieces
    public int puzzleCheckNum;

    public PuzzlePiece[] puzzlePieces;

    public int puzzleType; //0 vase, 1 abstract

    private void Awake()
    {

        if (globalState == null) globalState = FindObjectOfType<globalStateStore>();

        if (puzzleType == 0)
        {
            puzzleFit = new bool[puzzleFitLocalPositions.Length];
            puzzleCheckNum = 4;

            if (puzzlePieces.Length < 7) Debug.LogError("num puzzles wrong");

            for (int p = 4; p < 7; p++)
            {
                puzzlePieces[p].gameObject.SetActive(false);
            }

            if (brokenVase == null) brokenVase = globalState.vaseScene.transform.Find("broken_vase").gameObject;
        }
        else if (puzzleType == 1)
        {
            puzzleFit = new bool[puzzleFitLocalPositions.Length];
            puzzleCheckNum = puzzleFitLocalPositions.Length;
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
        float errorAllowed = ((puzzleType == 0 && puzzleID <= 4) || puzzleType == 1) ? errorAllowedPuzzles : errorAllowedFlowers;

        if (puzzleID > puzzleFitLocalPositions.Length) Debug.LogError("invalid puzzle id");

        if (Vector2.Distance(puzzlePiece.transform.localPosition, puzzleFitLocalPositions[puzzleID - 1]) <= errorAllowed)
        {
            puzzlePiece.transform.localPosition = puzzleFitLocalPositions[puzzleID - 1]; //fitting

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

        if (puzzleType == 0)
        {

            if (puzzleCheckNum == 4) //puzzle pieces fitted
            {
                puzzleCheckNum = 7; //check for flowers next
                                    //show flowers
                for (int p = 4; p < 7; p++)
                {
                    puzzlePieces[p].gameObject.SetActive(true);
                    puzzlePieces[p].GetComponent<Animator>().SetTrigger("fadeIn");
                }

            }
            else if (puzzleCheckNum == 7) //flowers also fitted
            {
                GetComponent<Animator>().SetTrigger("fadeOut"); //puzzle override fadeOut
                transform.Find("f1").GetComponent<Animator>().SetTrigger("fadeOut");
                transform.Find("f2").GetComponent<Animator>().SetTrigger("fadeOut");
                transform.Find("f3").GetComponent<Animator>().SetTrigger("fadeOut");

                globalState.vaseScene.transform.Find("soccer").GetComponent<Collider2D>().enabled = true;

                StartCoroutine(Global.Chain(this, Global.WaitForSeconds(2),
                    Global.Do(() =>
                    {
                        brokenVase.GetComponent<imgSwitcher>().switchToNextImgState();
                        brokenVase.GetComponent<Animator>().SetTrigger("fadeIn");
                    })));
            }
            else
            {
                Debug.LogError("something wrong");
            }

        }
        else if (puzzleType == 1)
        {
            //abstract puzzle complete, fade out collage 
            animEventLink flat = globalState.parkScene.transform.Find("flat").GetComponent<animEventLink>();
            flat.gameObject.SetActive(true);
            flat.flat();

        }

    }
}
