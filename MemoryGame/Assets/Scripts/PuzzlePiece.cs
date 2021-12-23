using UnityEngine;

public class PuzzlePiece : interactable
{
    public PuzzlePlacement puzzlePlacement;
    public int puzzleID;

    public bool selected, changeSpriteAfterPlacement;

    public imgSwitcher imgSwitch;
    public Vector2 placementOffset; //if changeSpriteAfterPlacement, might need to be offset from placement position 

    private Vector2 originalPosition; //local pos
    private bool isMobile;

    protected override void Awake()
    {
        base.Awake(); //calls interactable awake stuff

        if (puzzlePlacement == null) puzzlePlacement = FindObjectOfType<PuzzlePlacement>();
        if (changeSpriteAfterPlacement) imgSwitch = GetComponent<imgSwitcher>();

        isMobile = enabler.isMobile();
    }

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (selected)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            transform.position = newPos;

            if ((!isMobile && Input.GetMouseButtonDown(1)) ||  (isMobile && Input.GetTouch(0).tapCount == 2)  )
            {
                //if right click, release
                selected = false;

                transform.localPosition = originalPosition;
                globalState.holdingPuzzlePiece = false;

            }

        }
    }

    public override void onClick()
    {
        //   print("clicked: " + eventData.pointerPress.name);
        if (clickable && !globalState.holdingPuzzlePiece && !selected)
        {
            if (name[0] != 'f') globalState.audio.playSFX(0, 2);
            selected = true;
            globalState.holdingPuzzlePiece = true;

            if (!globalState.puzzleRightClickHintShown && globalState.parkScene != null)
            {
                globalState.puzzleRightClickHintShown = true;
                Transform hint = globalState.parkScene.transform.Find("collage/rightClickHint");


                hint.gameObject.SetActive(true);
                hint.GetComponent<Animator>().SetTrigger("fadeInText");

                StartCoroutine(Global.Chain(this, Global.WaitForSeconds(2),
                            Global.Do(() =>
                            {
                                hint.GetComponent<Animator>().SetTrigger("fadeOutText");
                            })));

            }

        }
        else if (clickable && selected) //waiting to be placed
        {
            bool success = puzzlePlacement.checkForPlacement(puzzleID, this);
            if (success)
            {
                //sfx
                if (name[0] == 'f') globalState.audio.playSFX(2, 6);
                else globalState.audio.playSFX(0, 3);

                clickable = false; //disable clicking
                selected = false;
                transform.GetComponent<Collider2D>().enabled = false;

                if (changeSpriteAfterPlacement) imgSwitch.switchToNextImgState();

                transform.localPosition += (Vector3)placementOffset;

                globalState.holdingPuzzlePiece = false;
            }
            else
            {

            }
        }
    }

    public override void onEnter()
    {
        // if (clickable) mouseControl.toHand();
    }

    public override void onExit()
    {
        //  if (clickable) mouseControl.toMouse();

        if (clickable && selected && globalState.holdingPuzzlePiece) //waiting to be placed
        {
            bool success = puzzlePlacement.checkForPlacement(puzzleID, this);
            if (success)
            {
                clickable = false; //disable clicking
                selected = false;

                if (changeSpriteAfterPlacement) imgSwitch.switchToNextImgState();

                transform.localPosition += (Vector3)placementOffset;
                globalState.holdingPuzzlePiece = false;
            }
            else
            {

            }
        }
    }
}
