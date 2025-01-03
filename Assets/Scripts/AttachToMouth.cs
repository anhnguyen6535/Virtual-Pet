using UnityEngine;
using System.Collections;

public class AttachToMouth : MonoBehaviour
{
    public bool touchMouth = false;
    [SerializeField] PickUp dog;
    [SerializeField] Transform socket;
    [SerializeField] GameObject socketParent;
    [SerializeField] SequenceHandler sequenceHandler;
    private GameObject gameObject;
    private bool grabbed = false;

    void Start()
    {
    
    }

    void Update(){
        if(touchMouth){
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Mouth touch");
        if(collider.gameObject.CompareTag("ball") && !touchMouth)
        {
            touchMouth = true;
            gameObject = collider.gameObject;
            StartCoroutine(Comeback());

        }else if(collider.gameObject.CompareTag("bone") && !touchMouth && !grabbed){
            gameObject = collider.gameObject;
            grabbed = true;
            touchMouth = true;
            socketParent.SetActive(false);
            Debug.Log("Bone touches mouth");
        }
    }

    IEnumerator Comeback(){
        yield return new WaitForSeconds(1);
        dog.SetBackToStartPos();
    }

    public void DetachBallFromMouth(){
        touchMouth=false;
    }

    public void AttachBoneToSocket(){
        Debug.Log("attack bone to socket ATM");
        socketParent.SetActive(true);
        touchMouth=false;
        gameObject.transform.position = socket.position;
        StartCoroutine(PromptPetting());
    }

    IEnumerator PromptPetting(){
        yield return new WaitForSeconds(1);
        sequenceHandler.PromptPetting();
    }
}