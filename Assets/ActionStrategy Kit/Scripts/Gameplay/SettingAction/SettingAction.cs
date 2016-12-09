/// <summary>
/// This script use for calling other method from other script
/// Example call attacking method from ActionEventMelee(script) , call Healing method from ActionEventHealer(script)
/// Don't edit code in this script
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class SettingAction : MonoBehaviour {
	
	public enum SendParameterType{
		 Null, Int, Float, Bool, Object	
	}
	
	[System.Serializable]
	public class EventSet{
		public string methodName;
		public SendParameterType sendParameterType;
		public int sendInt;
		public float sendFloat;
		public bool sendBool;
		public Object sendObject;
		public float time;
		public AnimationClip animations;
		[HideInInspector]
		public bool oneTime;
	}
	
	public bool preview;
	public int activeIndex;
	public List<EventSet> eventSet = new List<EventSet>();
	
	private float maxTime;
	
	[HideInInspector]
	public string methodName;
	[HideInInspector]
	public float time;
	[HideInInspector]
	public AnimationClip animations;
	[HideInInspector]
	public SendParameterType sendParameterType;
	
	void Update(){
		if(Application.isPlaying == false){
			if(eventSet.Count > 0){
				if(eventSet[activeIndex].animations != null){
					if(preview){
						eventSet[activeIndex].animations.SampleAnimation(gameObject,eventSet[activeIndex].time);
					}else{
						eventSet[activeIndex].animations.SampleAnimation(gameObject,0);	
					}
				}
			}
		}else{
			for(int i = 0; i < eventSet.Count; i++){
				maxTime = eventSet[i].time+0.1f;
				if(eventSet[i].animations != null && eventSet[i].methodName != ""){
					if(GetComponent<Animation>()[eventSet[i].animations.name].time > eventSet[i].time 
						&& GetComponent<Animation>()[eventSet[i].animations.name].time < maxTime){
						if(eventSet[i].oneTime == false){
							SetSendMassage(i);
							eventSet[i].oneTime = true;
							StartCoroutine(Reset(i));
						}
					}
				}
			}
		}
	}
		
	void SetSendMassage(int i){
		switch(eventSet[i].sendParameterType){
			case SendParameterType.Null :{
				SendMessage(eventSet[i].methodName);
			}
			break;
			
			case SendParameterType.Int :{
				SendMessage(eventSet[i].methodName,eventSet[i].sendInt);
			}
			break;
			
			case SendParameterType.Float :{
				SendMessage(eventSet[i].methodName,eventSet[i].sendFloat);
			}
			break;
			
			case SendParameterType.Bool :{
				SendMessage(eventSet[i].methodName,eventSet[i].sendBool);
			}
			break;
			
			case SendParameterType.Object :{
				SendMessage(eventSet[i].methodName,eventSet[i].sendObject);
			}
			break;
		}
	}
	
	IEnumerator Reset(int index){
		yield return new WaitForSeconds(0.1f);
		eventSet[index].oneTime = false;
	}
}
