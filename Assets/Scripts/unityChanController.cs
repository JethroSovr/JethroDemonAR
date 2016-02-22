using UnityEngine;
using System.Collections;

public class unityChanController : MonoBehaviour {

	// Use this for initialization
	public Camera cam;
	private Vector3 hitPoint;

	public GameObject chan;
	public float moveSpeed = 30.0f;
	public float rotationSpeed = 1.0f;
	private bool isNeedMove = false;

	private Animator ani;

	void Start () {
		cam = this.GetComponent<Camera> ();
		ani = chan.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		//TODO get touch pos 
		if(Input.GetMouseButtonDown(0)){
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit)){
				Debug.Log("first check hit pos :" +hit.point);
				if(hit.collider.gameObject.name == "Plane"){
					hitPoint = hit.point;
					isNeedMove  =  hitPoint != chan.transform.position;
						
					Debug.Log(hitPoint);
				}

			}
		}

		if (isNeedMove) {
			ani.SetBool ("IsWalk", true);
			unityChanMove ();
		} else {
			ani.SetBool("IsWalk",false);
		}
		//Move unityChan

	
	}


	void unityChanMove(){
		//MOVE 
		chan.transform.position = Vector3.MoveTowards (chan.transform.position, hitPoint, Time.deltaTime * moveSpeed);
		//ROTATION
		if (Vector3.SqrMagnitude (hitPoint - chan.transform.position) > 0.01f) {
			chan.transform.rotation = Quaternion.Lerp (chan.transform.rotation, Quaternion.LookRotation (hitPoint - chan.transform.position), Time.deltaTime * rotationSpeed); 
		} else {
			isNeedMove =false;
		}

	}
}
