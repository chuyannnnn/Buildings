/// <summary>
/// This script use for show text damage if hero or enemy attack
/// </summary>

using UnityEngine;
using System.Collections;

public class TextDamage : MonoBehaviour {
	
	public GameObject root;
	
	void Update () {
		
		//show text damage
		if(this.GetComponent<TextMesh>() != null){
			root.transform.LookAt(Camera.main.transform.position);
			this.GetComponent<TextMesh>().GetComponent<Renderer>().material.color -= new Color(0,0,0,2f*Time.deltaTime);
			this.transform.Translate(Vector3.up * 1.5f * Time.deltaTime);
			if(this.GetComponent<TextMesh>().GetComponent<Renderer>().material.color.a <= 0){
				Destroy(root.gameObject);	
			}
		}
	}
	
	public void SetDamage(float damage, Color colotText){
		//recive & send damage variable
		this.GetComponent<TextMesh>().text = damage.ToString();
		this.GetComponent<TextMesh>().GetComponent<Renderer>().material.color = colotText;
	}
}
