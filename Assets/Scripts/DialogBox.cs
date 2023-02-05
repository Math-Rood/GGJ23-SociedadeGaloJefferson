using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private string[] dialogList;
    [SerializeField] public int dialogIndex;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI  dialogText;

    [SerializeField] public TextMeshProUGUI charName1;
    [SerializeField] public Image charImg1;
    [SerializeField] public Sprite charSprite1;
    
    [SerializeField] public TextMeshProUGUI charName2;
    [SerializeField] public Image charImg2;
    [SerializeField] public Sprite charSprite2;

    public bool readyToTalk;
    public bool startTalk;
    private GameObject painelBlack1;
    private GameObject painelBlack2;

    

    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && readyToTalk)
        {
            if (!startTalk)
            {
                StartDialogo();
                Debug.Log("Status readyForRun antes");

            }
            else if(dialogText.text == dialogList[dialogIndex])
            {
                NextDialogue();
            }
        }
    }

    void NextDialogue()
    {
        dialogIndex++;

        if (dialogIndex < dialogList.Length)
        {
            painelBlack1 = dialogPanel.transform.GetChild(0).GetChild(1).GameObject();
            painelBlack2 = dialogPanel.transform.GetChild(2).GetChild(1).GameObject();
            
            painelBlack1.SetActive(!painelBlack1.activeSelf);
            painelBlack2.SetActive(!painelBlack2.activeSelf);
            
            Debug.Log("Começando o dialogo");
            StartCoroutine(ShowDialogue());
        }
        else
        {
            dialogPanel.SetActive(false);
            startTalk = false;
            dialogIndex = 0;

        }
    }

    public void StartDialogo()
    {
        Debug.Log("Começando a Corotina de Dialogo");
        charName1.text = "Sanozama";
        charImg1.sprite = charSprite1;
        charName2.text = "Zabuza Momochi";
        charImg2.sprite = charSprite2;
        startTalk = true;
        dialogPanel.SetActive(true);
        StartCoroutine(ShowDialogue());
    }
    
    IEnumerator ShowDialogue()
    {
        dialogText.text = "";
        foreach (char letter in dialogList[dialogIndex])
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            readyToTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            readyToTalk = false;
        }
    }
}
