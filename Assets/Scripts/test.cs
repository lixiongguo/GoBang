using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour {
	public class Coord{
		public int i;
		public int j;
		public Coord(int i, int j){
			this.i = i;
			this.j = j;
		}
	}
	Stack<Coord> coords = new Stack<Coord> ();
	void Start () {
		coords.Push (new Coord (1, 2));
		coords.Push (new Coord (3, 4));

		print (coords.Pop().i);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
