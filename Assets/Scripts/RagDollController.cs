using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class RagDollController : MonoBehaviour
{
    private enum RagDollState
    {
        Standing,
        RagDoll,
        StandingUp
    }
    public Animator Anim;
    public bool HasBeenTouchedRecently;

    private RagDollState _currentState = RagDollState.Standing;
    private float _timeToGetUpTimer;
    public float TimeToGetUp;
    public Transform hipsBone;
    public CapsuleCollider MainCollider;
    public GameObject MainRig;
    //public Animator MainAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _timeToGetUpTimer = TimeToGetUp;
        GetRagdollThings();
        ragdollModeOff();
        Anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        hipsBone = hipsBone.transform;
        switch (_currentState)
        {
            case RagDollState.Standing:
                StandingBehaviour();
                break;
            case RagDollState.RagDoll:

                break;
            case RagDollState.StandingUp:

                break;
        }
        _timeToGetUpTimer -= Time.deltaTime;

    }
    public void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Hand")
        {
            Debug.Log("a hand touched me");
            HasBeenTouchedRecently = true;
            ragdollModeOn();
            StartCoroutine("GettingUpTime");
        }
    }
    Collider[] ragdollColliders;
    Rigidbody[] limbsRigidbodys;
    void GetRagdollThings()
    {
        //just gets all of the Rigidbodies and Colliders at the start(Ignore)
        ragdollColliders = MainRig.GetComponentsInChildren<Collider>();
        limbsRigidbodys = MainRig.GetComponentsInChildren<Rigidbody>();
    }

    public void ragdollModeOn()
    {
        
        Anim.enabled = false;
        _currentState = RagDollState.RagDoll;
       
        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = true;

        }
        foreach (Rigidbody rigidbody in limbsRigidbodys)
        {
            rigidbody.isKinematic = false;

        }



        MainCollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void ragdollModeOff()
    {

        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = false;

        }
        foreach (Rigidbody rigidbody in limbsRigidbodys)
        {
            rigidbody.isKinematic = true;

        }


        Anim.enabled = true;
        MainCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
    public void StandingBehaviour()
    {

    }
    public void StandingUpBehaviour()
    {

        {

        }
    }
    public void RagdollBehaviour()
    {




    }
    private void AlignPositionToHips()
    {
        Vector3 originalHipsPosition = hipsBone.position;
        transform.position = hipsBone.position;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo))
        {

            transform.position = new Vector3(transform.position.x, hitInfo.point.x, transform.position.z);
        }

        hipsBone.position = originalHipsPosition;
    }

    public IEnumerator GettingUpTime()
    {
      
        _timeToGetUpTimer = TimeToGetUp; ;
        yield return new WaitForSecondsRealtime(_timeToGetUpTimer);
        if (HasBeenTouchedRecently == true) 
        {
            HasBeenTouchedRecently = false;
            Debug.Log("turning off has been touched recently...");
            StartCoroutine("GettingUpTime");
        }
        else
        {
            ragdollModeOff();
            Debug.Log("Turning off ragdoll mode...");
        }


        yield return null;
    }
}
