//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.InputSystem;
//using UnityEngine.UI;

//public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
//{
//	[SerializeField] MenuButtonController menuButtonController;
//	[SerializeField] Animator animator;
//	[SerializeField] AnimatorFunctions animatorFunctions;
//	[SerializeField] int thisIndex;

//	Button buttonMe;
//	State currentState;

//	enum State
//	{
//		PRESSED, SELECTED, DESELECTED
//	}

//	// Use this for initialization
//	void Start()
//	{
//		buttonMe = GetComponent<Button>();
//		animator.SetBool("selected", false);
//	}

//	// Update is called once per frame
//	void Update()
//    {
//		if (menuButtonController.currentIndex == thisIndex)
//		{
//			animator.SetBool("selected", true);
//			if (Input.GetAxis("Submit") == 1)
//			{
//				animator.SetBool("pressed", true);
//			}
//			else if (animator.GetBool("pressed"))
//			{
//				animator.SetBool("pressed", false);
//				animatorFunctions.disableOnce = true;

//				buttonMe.onClick.Invoke();
//			}
//		}
//		else
//		{
//			animator.SetBool("selected", false);
//		}
//	}

//	private void OnSelect(InputValue value)
//	{

//	}

//	public void OnPointerEnter(PointerEventData eventData)
//	{
//		animator.SetBool("selected", true);
//	}

//	public void OnPointerExit(PointerEventData eventData)
//	{
//		animator.SetBool("selected", false);
//	}

//	public void OnPointerDown(PointerEventData eventData)
//	{
//		animator.SetBool("pressed", true);
//	}
//}
