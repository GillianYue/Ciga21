using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotesRecord : MonoBehaviour
{
    public Animator[] notes;
    public string currSequence = ""; //e.g. "1352"

    public static string[] guitarSolutions = { "54343", "2346753", "14123" },
        drumSolutions = { "3312", "1242", "44334312" }, accordionSolutions = { "1352", "5173", "1265243" };

    public int instrumentIndex, sequenceIndex; //guitar 0, drum 1, accordion 2
    public static int currActiveInstrument;

    private AudioSource[] guitarNotes, accordionNotes, drumNotes;
    public GameObject notesSoundSources;

    public AudioManager audioManager;
    public globalStateStore globalState;

    private void Awake()
    {
        guitarNotes = notesSoundSources.transform.Find("guitar").GetComponents<AudioSource>();
        accordionNotes = notesSoundSources.transform.Find("accordion").GetComponents<AudioSource>();
        drumNotes = notesSoundSources.transform.Find("drums").GetComponents<AudioSource>();

        if (audioManager == null) audioManager = FindObjectOfType<AudioManager>();
        if (globalState == null) globalState = FindObjectOfType<globalStateStore>();

        transform.Find("notes").gameObject.SetActive(false);

    }
    void Start()
    {

    }

    void Update()
    {

    }

    public void giveHint()
    {
        if (currActiveInstrument == -1) return;

        NotesRecord nr = null; string sol = "";

        switch (currActiveInstrument)
        {
            case 0:
                nr = globalState.bandScene.transform.Find("guitar").GetComponent<NotesRecord>();
                sol = guitarSolutions[nr.sequenceIndex];
                break;
            case 1:
                nr = globalState.bandScene.transform.Find("drums").GetComponent<NotesRecord>();
                sol = drumSolutions[nr.sequenceIndex];
                break;
            case 2:
                nr = globalState.bandScene.transform.Find("accordion").GetComponent<NotesRecord>();
                sol = accordionSolutions[nr.sequenceIndex];
                break;

        }

        nr.playSolution();

        StartCoroutine(nr.playHintNotes(sol));

    }

    public IEnumerator playHintNotes(string sol)
    {
        globalState.globalClickable = false;

        foreach (char c in sol)
        {
            int n = int.Parse(c.ToString());
            n -= 1;

            for (int nt = 0; nt < notes.Length; nt++)
            {
                if (nt == n)
                {
                    notes[nt].Play("noteOpaque");
                }
                else
                {
                    notes[nt].Play("noteHalfTransparent");
                }
            }
            yield return new WaitForSeconds(0.5f);
        }

        foreach (Animator n in notes)
        {
            n.gameObject.SetActive(true);
            StartCoroutine(Global.Chain(this, Global.WaitForSeconds(Random.Range(0f, 0.5f)), Global.Do(() =>
            {
                n.Play("noteIdle");
            }))
                );
        }

        yield return new WaitForSeconds(0.3f);

        globalState.globalClickable = true;
    }

    public void enableNotes()
    {
        GameObject nts = transform.Find("notes").gameObject;

        if (!nts.activeSelf)
        {

            nts.SetActive(true);

            foreach (Animator n in notes)
            {
                n.gameObject.SetActive(true);
                StartCoroutine(Global.Chain(this, Global.WaitForSeconds(Random.Range(0f, 0.5f)), Global.Do(() =>
                {
                    n.Play("noteOut");
                }))
                    );
            }
        }
    }

    public void enableHintButton()
    {
        globalState.bandScene.transform.Find("HintButton").GetComponent<Button>().interactable = true;
    }

    public void recordNote(int noteIndex)
    {
        //trigger animation for note
        notes[noteIndex - 1].Play("noteSelect");
        GetComponent<Animator>().SetTrigger("action2"); //brief play action

        switch (instrumentIndex)
        {
            case 0: guitarNotes[noteIndex - 1].Play(); break;
            case 1: drumNotes[noteIndex - 1].Play(); break;
            case 2: accordionNotes[noteIndex - 1].Play(); break;
        }

        currSequence += noteIndex.ToString();

        int compare = compareSolutions();

        if (compare == 1)
        {
            //pass
            StartCoroutine(notesPassEffectCoroutine());

        }
        else if (compare == -1) //reaches maximum length
        {
            //reset
            //false sfx

            currSequence = currSequence.Substring(1);
        }
        else
        {
            //regular non-match

        }

    }

    public void playSolution()
    {
        int playIndex = -1;

        GetComponent<Animator>().SetTrigger("action1");

        switch (instrumentIndex)
        {
            case 0: //guitar
                switch (sequenceIndex)
                {
                    case 0:
                        playIndex = 7;
                        break;
                    case 1:
                        playIndex = 14;
                        break;
                    case 2:
                        playIndex = 15;
                        break;
                }
                break;
            case 1: //tabla
                switch (sequenceIndex)
                {
                    case 0:
                        playIndex = 6;
                        break;
                    case 1:
                        playIndex = 16;
                        break;
                    case 2:
                        playIndex = 17;
                        break;
                }
                break;
            case 2: //accordion
                switch (sequenceIndex)
                {
                    case 0:
                        playIndex = 8;
                        break;
                    case 1:
                        playIndex = 12;
                        break;
                    case 2:
                        playIndex = 13;
                        break;
                }
                break;
        }

     //   if (playIndex == -1) Debug.LogError("something wrong");

        audioManager.playSFX(4, playIndex);

    }

    public IEnumerator notesPassEffectCoroutine()
    {
        globalState.globalClickable = false; //will be set back to true either after note fadeout, or after solution has been played

        //TODO correct/bingo sfx
     //   print("match");

        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].Play("notePass");
            yield return new WaitForSeconds(i * 0.1f);
        }

        if (sequenceIndex > 1) //no more on curr instrument
        {
            GetComponent<Collider2D>().enabled = false;
            currActiveInstrument = -1;
            globalState.bandScene.transform.Find("HintButton").GetComponent<Button>().interactable = false;

            //curr instrument done
            GetComponent<interactable>().instrumentStartPlaying();

            foreach (Animator n in notes)
            {
                n.Play("noteFadeout");
            }

            yield return new WaitForSeconds(2);
            globalState.globalClickable = true;

        }
        else
        {
            currSequence = "";
            sequenceIndex += 1; //increment one, move to next puzzle (if assuming each instrument gets 2 puzzles assigned)
            yield return new WaitForSeconds(1);
            playSolution();
        }

    }

    //resets notes in all instruments
    public void resetAllNoteStatus()
    {

        NotesRecord[] records = FindObjectsOfType<NotesRecord>();

        foreach (NotesRecord record in records)
        {

            if (record.currSequence.Length > 0)
            {
                foreach (Animator n in record.notes)
                {
                    n.Play("noteIdle");
                }

                currSequence = "";

            }

            if (record.instrumentIndex != currActiveInstrument)
            {
                record.transform.Find("notes").gameObject.SetActive(false); //deactivate
            }
        }

    }

    /// <summary>
    /// compares curr sequence to solution.
    ///
    /// Returns:
    /// 1 if match
    /// 0 if no match (including length difference)
    /// -1 if curr sequence reaches maximum length and is not a match
    /// 
    /// </summary>
    /// <returns></returns>
    public int compareSolutions()
    {
        string solution = "";

        //getting my solution
        switch (instrumentIndex)
        {
            case 0: solution = guitarSolutions[sequenceIndex]; break;
            case 1: solution = drumSolutions[sequenceIndex]; break;
            case 2: solution = accordionSolutions[sequenceIndex]; break;
        }

      //  if (solution.Equals("")) Debug.LogError("invalid solution");

        if (currSequence.Equals(solution)) return 1;
        if (currSequence.Length >= solution.Length)
        {
            return -1;
        }
        return 0; //a regular non-match
    }

    public void resetSequence()
    {
        //reset selected notes to blank state
        foreach (char c in currSequence)
        {
            notes[int.Parse(c.ToString())].Play("noteIdle");
        }

        currSequence = "";
    }
}
