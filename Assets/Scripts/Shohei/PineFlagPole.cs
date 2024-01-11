using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineFlagPole : MonoBehaviour
{
    //public SceneController sceneController;
    [SerializeField] Animator transitionAnim;

    public Transform pineFlag;
    public Transform poleBottom;
    public Transform house;
    public float speed = 5f;
    public int world = 1;
    public int stage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(pineFlag, poleBottom.position));
            StartCoroutine(LevelCompleteSequence(other.transform));
        }
    }

    private IEnumerator LevelCompleteSequence(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;

        yield return MoveTo(player, poleBottom.position);
        yield return MoveTo(player, player.position + Vector3.right);
        yield return MoveTo(player, player.position + Vector3.right + Vector3.down);
        yield return MoveTo(player, house.position);

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        //transitionAnim.SetTrigger("End");
        GameManager.Instance.LoadLevel(world, stage);
        //transitionAnim.SetTrigger("Start");
    }

    private IEnumerator MoveTo(Transform subject, Vector3 destination)
    {
        while (Vector3.Distance(subject.position, destination) > 0.025f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        subject.position = destination;
    }

}
