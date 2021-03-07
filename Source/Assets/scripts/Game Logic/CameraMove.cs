using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    public GameObject trigger1;
    public GameObject trigger2;

    TriggerScript trigger1Ref;
    TriggerScript trigger2Ref;

    public GameObject col;
    public Transform newCameraPos;

    public bool isFinalScene = false;
    public GameObject chest;
    public GameObject chestLid;

    public Image whiteFade;
    public GameObject canvas;
    // Start is called before the first frame update
    void Awake()
    {
        trigger1Ref = trigger1.GetComponent<TriggerScript>();
        trigger2Ref = trigger2.GetComponent<TriggerScript>();

        if (isFinalScene == true)
        {
          whiteFade.canvasRenderer.SetAlpha(0.0f);
        }
        
    }

    

    // Update is called once per frame
    void Update()
    {
        Debug.Log(trigger1Ref.playerReady);
        if (trigger1Ref.playerReady == true && trigger2Ref.playerReady == true)
        {

            if (isFinalScene == true)
            {
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, newCameraPos.position, 75f * Time.deltaTime);
                Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation,Quaternion.Inverse(chest.transform.rotation), 60f * Time.deltaTime);
                LeanTween.rotateX(chestLid, -90f, 5f);
                StartCoroutine(FadeIn());

                if (Vector3.Distance(Camera.main.transform.position, newCameraPos.position) < 2f)
                {
                    trigger1Ref.playerReady = false;
                    trigger2Ref.playerReady = false;

                    GameSparksManager.Instance.PostToLeaderboard();
                    GameSparksManager.Instance.SavePlayer();

                }
            }
             else
            {
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, newCameraPos.position, 75f * Time.deltaTime);

                

                if (Vector3.Distance(Camera.main.transform.position, newCameraPos.position) < 2f)
                {
                    trigger1Ref.playerReady = false;
                    trigger2Ref.playerReady = false;

                    int playerScore = GameSparksManager.Instance.GetPlayerScore();
                    WebSocketService.Instance.SendMessage("OnMessage", playerScore);
                }

            }
               
        }
    }

    IEnumerator FadeIn ()
    {
        yield return new WaitForSeconds(3f);
        canvas.SetActive(true);
        whiteFade.CrossFadeAlpha(1, 2, false);
    }
    
}
