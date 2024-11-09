using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProgrammedMovement : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();

        playerMovement.Move(Vector2.right);
        StartCoroutine(NextMove(Vector2.down, .85f));
        StartCoroutine(GiveControlBackToPlayer(1.4f));
    }
    private IEnumerator NextMove(Vector2 direction, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerMovement.Move(direction);
    }

    private IEnumerator GiveControlBackToPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerInput.enabled = true;
    }
}
