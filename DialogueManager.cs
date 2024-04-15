using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text textDialogue;
    [SerializeField] GameObject dialogueList;
    [SerializeField] Text dialogueName;
    private Dialogue dialogue;
    [SerializeField] GameObject npcControl;
    private NpcController npccontroll;

    [SerializeField] float lettersPerSecond;
   
    public static DialogueManager Instance { get; private set; }
    public List<string> stringsforshow;

   
    private void Awake()
    {
        Instance = this;
        dialogue = dialogueList.GetComponent<Dialogue>();
        npccontroll = npcControl.GetComponent<NpcController>();
    }
    
    public void ShowDialogue()
    {

        dialogBox.SetActive(true);
        dialogueName.text = npccontroll.rightname;
        StartCoroutine(MoveThroughDialogue(npccontroll));
    }
    public IEnumerator MoveThroughDialogue(NpcController npcController)
    {
        
        for (int i = 0; i < npccontroll.rightnpc.Count; i++)
        {
            string line = npccontroll.rightnpc[i];
            foreach (var letter in line.ToCharArray())
            {
                textDialogue.text += letter;
                yield return new WaitForSeconds(1f / lettersPerSecond);
            }

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            textDialogue.text = "";
        }
        npccontroll.rightnpc.Clear();
        dialogBox.SetActive(false);
    }
}
