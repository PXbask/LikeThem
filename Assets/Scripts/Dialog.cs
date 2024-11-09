using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField] GameObject controlHint;
    [SerializeField] string[] sentences;
    [SerializeField] float typingSpeed = .05f;

    Player player;
    TextMeshProUGUI textDisplay;
    Button continueButton;
    Animator textAnimator;
    GameObject dialogBox;

    private int index;
    char[] currentSentence;
    IEnumerator typeCoroutine;

    private void Start()
    {
        dialogBox = transform.GetChild(0).gameObject;
        textAnimator = dialogBox.transform.Find("Text Content").GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        player.gameObject.GetComponent<PlayerInput>().enabled = false;
        player.gameObject.GetComponent<PlayerMovement>().enabled = false;

        textDisplay = dialogBox.GetComponentInChildren<TextMeshProUGUI>();
        continueButton = dialogBox.GetComponentInChildren<Button>();
        typeCoroutine = Type();
        StartCoroutine(typeCoroutine);
    }
    IEnumerator Type()
    {
        yield return new WaitForSeconds(.4f);
        dialogBox.SetActive(true);

        currentSentence = sentences[index].ToCharArray();
        foreach (char letter in currentSentence)
        {
            textDisplay.text += letter;
            AudioManager.instance.Play("Dialog");

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void ContinueText()
    {
        if (textDisplay.text.Length < currentSentence.Length - 5)
            CompleteCurrentSentence();

        else if (index < sentences.Length - 1)
            NextSentence();

        else CloseDialogAndGiveControlToPlayer();
    }


    private void CompleteCurrentSentence()
    {
        StopCoroutine(typeCoroutine);
        textDisplay.text = sentences[index];
    }

    private void NextSentence()
    {
        textAnimator.SetTrigger("change");
        StopCoroutine(typeCoroutine);
        index++;
        textDisplay.text = "";
        typeCoroutine = Type();
        StartCoroutine(typeCoroutine);
    }

    private void CloseDialogAndGiveControlToPlayer()
    {
        dialogBox.GetComponent<PlayerInput>().enabled = false;
        dialogBox.GetComponent<MenuButtonController>().enabled = false;
        player.gameObject.GetComponent<PlayerInput>().enabled = true;
        player.gameObject.GetComponent<PlayerMovement>().enabled = true;
        controlHint.SetActive(true);
        dialogBox.SetActive(true);
        gameObject.SetActive(false);
    }
}
