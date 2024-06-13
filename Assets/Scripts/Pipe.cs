using System.Collections;
using UnityEngine;
public class Pipe : MonoBehaviour
{
    public KeyCode enterKeyCode = KeyCode.S;
    public Vector3 enterDir = Vector3.down;
    public Vector3 exitDir = Vector3.zero;
    public Transform connection;
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D other)
    {
        if(connection != null && other.CompareTag("Player"))
        {
            if(Input.GetKey(enterKeyCode) )
            {
                StartCoroutine(enter(other.transform));
            }
        }
    }

    private IEnumerator enter(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        Vector3 enteredPos = transform.position + enterDir;
        Vector3 enteredScale = Vector3.one * 0.5f;
        yield return Move(player, enteredPos, enteredScale);
        yield return new WaitForSeconds(1f);

        bool underground = connection.position.y < 0;
        Camera.main.GetComponent<CameraMovement>().SetUnderground(underground);
        if(exitDir != Vector3.zero)
        {
            player.position = connection.position - exitDir;
            yield return Move(player,connection.position + exitDir, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }
            player.GetComponent<PlayerMovement>().enabled = true;
    }

    private IEnumerator Move(Transform player, Vector3 endPosition,Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 0.6f;

        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while(elapsed < duration)
        {
            float t = elapsed / duration;
            player.position = Vector3.Lerp(startPosition,endPosition,t);
            player.localScale = Vector3.Lerp(startScale,endScale,t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        player.position = endPosition;
        player.localScale = endScale;
    }
}
