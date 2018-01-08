using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Resistance.Data;

namespace Resistance.Game
{
	public class Test : MonoBehaviour {
		
		public Transform t1;
		
		public Text label;
		
		public GameObject voteGroup;
		
		[SerializeField]
		private Transform t2;
		
		private Transform[] transfArr;
		public List<Transform> tList;
		
		public float speed = 1f;

		private TestData data;
		
		// Use this for initialization
		void Start () { 
			data=new TestData();
			data.brol = true;
			
			label.text = "test";
			
			
		}
		
		// Update is called once per frame
		void Update () {
			t1.position += new Vector3(1f,0f,0f) * speed / Time.deltaTime;
		}
		
		
		public void DoStuff()
		{
			print("stuff");
			
			voteGroup.SetActive(false);
		}
		
		
		
		private int bNr = 0;
		public void MakeButton()
		{
			GameObject g = new GameObject("Button " + (++bNr));
			
			g.AddComponent<RectTransform>();
			Image i = g.AddComponent<Image>();
			//i.sprite = 
			Button b = g.AddComponent<Button>();
			int localNr = bNr;
			/*
			b.onClick.AddListener(()=>
			{ 
				print(localNr.ToString());
			});
			*/
			b.onClick.AddListener(MakeButton);
			g.transform.SetParent(voteGroup.transform);
			
			
		}
		
		public void ToGame()
		{
			SceneManager.LoadScene(1);
		}
	}
}