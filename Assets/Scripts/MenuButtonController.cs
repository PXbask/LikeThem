using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour {

	// Use this for initialization
	[SerializeField] GameObject[] buttons;
	[SerializeField] GameObject backButton;

	private int currentIndex;
	private int maxIndex;

	AudioManager audioManager;
	PauseMenu pauseMenu;
	WaitForSecondsRealtime wait;

	private readonly string DEFAULT_BUTTON = "Default MenuButton";
	private readonly string SIDE_BUTTON = "Side MenuButton";


	private void Start()
	{
		Reset();
		pauseMenu = FindObjectOfType<PauseMenu>();
		audioManager = AudioManager.instance;
		maxIndex = buttons.Length - 1;
		wait = new WaitForSecondsRealtime(.1f);


		// TODO: Pegar o Animator e Button components de cada
	}

	private void OnMove(InputValue value)
	{
		Vector2 movement = value.Get<Vector2>();
		if (movement == Vector2.down)
		{
			if (currentIndex < maxIndex) currentIndex++;
			else currentIndex = 0;

			// TODO: ARRUMAR ISSO C URGENCIA
			SelectCurrentButton();
			UnselectOtherButtons();
			AudioManager.instance.Play("Select");
		}
		else if (movement == Vector2.up)
		{
			if (currentIndex > 0) currentIndex--;
			else currentIndex = maxIndex;

			// TODO: ARRUMAR ISSO C URGENCIA
			SelectCurrentButton();
			UnselectOtherButtons();
			AudioManager.instance.Play("Select");
		}
		else if (movement == Vector2.left && buttons[currentIndex].CompareTag(SIDE_BUTTON))
		{
			buttons[currentIndex].GetComponentInChildren<Button>().onClick.Invoke();
		}

		else if (movement == Vector2.right && buttons[currentIndex].CompareTag(SIDE_BUTTON))
		{
			buttons[currentIndex].GetComponentsInChildren<Button>()[1].onClick.Invoke();
		}
	}

	private void OnSelect(InputValue value)
	{
		if (buttons[currentIndex].CompareTag(SIDE_BUTTON)) return;

		buttons[currentIndex].GetComponent<Animator>().SetTrigger("pressed");
		audioManager.Play("Press");
		StartCoroutine(DelayButtonPress(buttons[currentIndex].GetComponent<Button>()));
	}

	private void OnUnpause(InputValue value)
	{
		if (backButton == null || pauseMenu == null) return;
		AudioManager.instance.Play("Pause");
		pauseMenu.Resume();
	}

	private void OnGoBack(InputValue value)
	{
		if (backButton == null) return;
		AudioManager.instance.Play("Back");
		StartCoroutine(DelayButtonPress(backButton.GetComponent<Button>()));
	}


	private void Reset()
	{
		currentIndex = 0;
		SelectCurrentButton();
	}

	private void OnEnable()
	{
		//GetComponent<PlayerInput>().DeactivateInput();
		Reset();
	}

	private void OnDisable()
	{
		//GetComponent<PlayerInput>().ActivateInput();
		Reset();
	}

	public void SetActiveWithDelay(bool isActive)
	{
		StartCoroutine(DelayToShowHide(isActive));
	}

	private IEnumerator DelayToShowHide(bool isActive)
	{
		yield return new WaitForSeconds(1f);
		gameObject.SetActive(isActive);
		
	}

	private void SelectCurrentButton()
	{
		buttons[currentIndex].GetComponent<Animator>().SetBool("selected", true);
	}

	private void UnselectOtherButtons()
	{
		for (int i = 0; i <= maxIndex; i++)
		{
			if (i != currentIndex)
			{
				buttons[i].GetComponent<Animator>().SetBool("selected", false);
			}
		}
	}

	private IEnumerator DelayButtonPress(Button button)
	{
		yield return wait;
		button.onClick.Invoke();
	}
}