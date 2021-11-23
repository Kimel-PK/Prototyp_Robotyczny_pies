using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kontrolagracza : MonoBehaviour {

	public Animator anim;
	public Rigidbody fizyka;
	public Collider kolider;
	private float ośY;
	private float ośX;
	private Vector3 ruch;
	private Vector3 skok;
	private float obrót;
	private float odlOdZiemi;
	private bool naZiemi;
	public float prdkobrotu = 10f;
	public float prdkruchu = 10f;
	public float wysskoku = 25f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		fizyka = GetComponent<Rigidbody> ();
		kolider = GetComponent<Collider> ();
		odlOdZiemi = kolider.bounds.extents.y - 4.4f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (naZiemi) {
			Obrót ();
		}
		Ruch ();
		Skok ();
		NaZiemi ();
	}

	void Update() {
		Animuj ();
	}

	void Animuj() {
		anim.SetFloat ("wejH", ośY);
		anim.SetFloat ("wejV", ośX);
	}

	void Ruch() {
		if (naZiemi == true) {
			ośY = Input.GetAxis ("Horizontal");
			ośX = Input.GetAxis ("Vertical");
		}
		Vector3 ruch = transform.forward * ośX * prdkruchu;
		fizyka.MovePosition (fizyka.position + ruch);

	}

	void Obrót() {
		obrót = ośY * prdkobrotu;
		Quaternion qobrót = Quaternion.Euler (0f, obrót, 0f);
		fizyka.MoveRotation (fizyka.rotation * qobrót);
	}

	void Skok() {
		if (Input.GetKeyDown (KeyCode.Space) && naZiemi) {
			fizyka.velocity = new Vector3 (0f, wysskoku, 0f);
		}
	}

	void NaZiemi() {
		if (Physics.Raycast (transform.position, -Vector3.up, odlOdZiemi + 0.1f))
		{
			naZiemi = true;
		}
		else
		{
			naZiemi = false;
		}
	}

}