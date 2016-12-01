using UnityEngine;
using System.Collections;

public class UIPositioning : MonoBehaviour {

	public Transform target;
	public Vector3 offset = Vector3.zero;
	public float distance;
	public float panelSpacing;
	public bool isObjectBackwards = false;

	Vector3 currentPosition;
	Vector3 previousPosition;

	// Use this for initialization
	void Start () {
		previousPosition = transform.position;
		if(target == null)
			Debug.LogError("OVAL/UIPositioning: The target of component UIPositioning is not initialized!");
		else
			AdjustDistance();
	}

	void OnEnable() {
		AdjustHorizontalSpacing();
	}
	
	// Update is called once per frame
	void Update () {
		currentPosition = transform.position;

		AdjustRotation(isObjectBackwards);

		if(currentPosition != previousPosition)
			AdjustDistance();

		previousPosition = transform.position;
	}

	void AdjustRotation(bool isObjectBackwards) {
		if(isObjectBackwards)	
			transform.LookAt(2 * transform.position - (target.position + offset), target.up);
		else
			transform.LookAt(target.position + offset, target.up);
	}

	public void AdjustDistance() {
		Ray objectToTarget = new Ray(target.position + offset, (transform.position - (target.position + offset)).normalized);
		transform.position = objectToTarget.GetPoint(distance);
	}

	public void AdjustHorizontalSpacing() {
		//Get this object's sibling index
		int mySiblingIndex = transform.GetSiblingIndex();
		//Holds this object's closest sibling
		GameObject closestSibling = null;

		//Check for activated siblings with lower sibling indexes
		for(int i = 0; i < mySiblingIndex; i++) {
			GameObject sibling = transform.parent.GetChild(i).gameObject;
			if(sibling.activeInHierarchy) {
				closestSibling = sibling;
			}
		}

		//If closest sibling is not null
		if(closestSibling != null) {
			float myWidth = GetComponent<RectTransform>().sizeDelta.x * 0.001f;
			float siblingWidth = closestSibling.GetComponent<RectTransform>().sizeDelta.x * 0.001f;

			//Set this object's position equal to its sibling's position
			transform.localPosition = closestSibling.transform.localPosition;
			//Swing ui panel to the right by (0.5*siblingWidth + 0.5*thisWidth + spacing)
			float separation = (siblingWidth/2 + myWidth/2 + panelSpacing);
			float degreesToSwing = Mathf.Rad2Deg * Mathf.Acos((2*Mathf.Pow(distance, 2f) - Mathf.Pow(separation, 2f))/(2*Mathf.Pow(distance, 2f)));
			transform.RotateAround(target.position + offset, target.up, degreesToSwing);
		}
	}
}
