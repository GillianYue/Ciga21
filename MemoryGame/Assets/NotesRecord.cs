using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesRecord : MonoBehaviour
{
    public Animator[] notes;
    public string currSequence = ""; //e.g. "1352"

    public static string[] guitarSolutions = { "1357", "2468" },
        drumSolutions = { "1231", "2341" }, accordionSolutions = { "1352", "3476" };

    public int instrumentIndex, sequenceIndex; //guitar 0, drum 1, accordion 2
    public static int currActiveInstrument;


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void recordNote(int noteIndex)
    {
        //trigger animation for note
        notes[noteIndex - 1].Play("noteSelect");
        //TODO trigger animation for instrument
        //brief play

        currSequence += noteIndex.ToString();

        int compare = compareSolutions();

        if (compare == 1)
        {
            //pass
            StartCoroutine(notesPassEffectCoroutine());

        }else if(compare == -1) //reaches maximum length
        {
            //reset
            //false sfx

            resetAllNoteStatus();
        }
        else
        {
            //regular non-match

        }


    }

    public IEnumerator notesPassEffectCoroutine()
    {
        //correct sfx

        for(int i=0; i<notes.Length; i++)
        {
            notes[i].Play("notePass");
            yield return new WaitForSeconds(0.2f * i);
        }

        if (sequenceIndex > 1)
        {
            //curr instrument done
            //TODO add in instrument bg layer

            //TODO fade out the notes
        }
        else
        {
            sequenceIndex += 1; //increment one, move to next puzzle (if assuming each instrument gets 2 puzzles assigned)
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

                if(record.instrumentIndex != currActiveInstrument)
                {
                    record.gameObject.SetActive(false); //deactivate
                }
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

        if (solution.Equals("")) Debug.LogError("invalid solution");

        if (currSequence.Equals(solution)) return 1;
        if(currSequence.Length >= solution.Length)
        {
            return -1;
        }
        return 0; //a regular non-match
    }

    public void resetSequence()
    {
        //reset selected notes to blank state
        foreach(char c in currSequence)
        {
            notes[int.Parse(c.ToString())].Play("noteIdle");
        }

        currSequence = "";
    }
}
