using UnityEngine;
using System.Collections;
namespace WZQ{
public enum Player{
	black,white
};
public class Qipan : MonoBehaviour {

	public GameObject black;
	public GameObject white;

	public AI ai;

	int[,]board=new int[15,15];
	public Vector3 lefttop = new Vector3(-5.0f,0.0f,5.0f);
	public float xOffset = 0.7f;
	public float zOffset = 0.7f;
	public float tolerence = 0.35f;
	
	public float coldTime = 1.0f;

	private float time = 0;
	private float lastTime = 0 ;

	private Player player;
//	private int curValue = 0;

	void Start () {
		lastTime = time = 0;
		player = Player.black;
		for (int i=0; i<15; i++)
				for (int j=0; j<15; j++){
						board [i, j] = 0;
		}
		StartCoroutine (AiWork());
	}	

	void Update () {
		time += Time.deltaTime;
		if (player == Player.black && time - lastTime > coldTime && Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
				if(Physics.Raycast(ray,out hit,100f)){
				int j = Mathf.RoundToInt((hit.point.x- lefttop.x)/xOffset);
				int i = Mathf.RoundToInt((hit.point.z - lefttop.z)/zOffset);
				if(board[Mathf.Abs(i),Mathf.Abs (j)] == 0){
					Instantiate(black,new Vector3(lefttop.x + j * xOffset,0,lefttop.z + i * zOffset),Quaternion.identity);
					board[Mathf.Abs(i),Mathf.Abs (j)]= 1;
					CheckWin(Mathf.Abs(i),Mathf.Abs(j));
					switchPlayer();
					lastTime = time;
				}
				
			}
			
		}
	}
	IEnumerator AiWork(){
		while(true){
			if (player == Player.white) {
				int i,j,score;
				ai.AiSelect(board,out i,out j,out score);
				//print (score);
				//i = Random.Range(0,14);
				//j = Random.Range(0,14);
				Instantiate(white,new Vector3(lefttop.x + j * xOffset,0,lefttop.z + -i * zOffset),Quaternion.identity);
				board[i,j]= -1;
				CheckWin(i,j);
				switchPlayer();
			}
			yield return null;
		}
	}

		void switchPlayer(){
			if (player == Player.black)
				player = Player.white;
			else
				player = Player.black;
		}

		bool CheckWin(int m, int n){
			int chessturn = (player == Player.black) ? 1 : -1;
			//have Won?
			//judge '|'
			int line_start = 0, line_end = 0;
			for (int i = 1; i < 5; i++)
			{
				if (m-i<0 ||board[(m - i),n] != chessturn)
				{
					line_start = -i + 1;
					break;
				}
				else line_start--;
			}
			for (int i = 1; i < 5; i++)
			{
				if (m+i>14 || board[(m + i), n] != chessturn)
				{
					line_end = i - 1;
					break;
				}
				else line_end++;
			}
			if (line_end - line_start >= 4)
			{
				print("Congratulations You have won!");
				return true;
			}
			
			//judge '-'
			line_start = line_end = 0;
			for (int i = 1; i < 5; i++)
			{
				if (n-i<0 || board[m,n-i] != chessturn)
				{
					line_start = -i + 1;
					break;
				}
				else line_start--;
			}
			for (int i = 1; i < 5; i++)
			{
				if ( n+i>14 || board[m, n + i] != chessturn)
				{
					line_end = i - 1;
					break;
				}
				else line_end--;
			}
			if (line_end - line_start >= 4)
			{
				print (line_start +","+line_end);
				print("Congratulations You have won!");
				return true;
			}
			
			//judge '\'
			line_start = line_end = 0;
			for (int i = 1; i < 5; i++)
			{
				if (m-i <0||n-i <0 || board[(m - i), n - i] != chessturn)
				{
					line_start = -i + 1;
					break;
				}
				else line_start--;
			}
			for (int i = 1; i < 5; i++)
			{
				if (m+i>14 || n+i >14|| board[(m + i), n + i] != chessturn)
				{
					line_end = i - 1;
					break;
				}
				else line_end++;
			}
			if (line_end - line_start >= 4)
			{
				print (line_start +","+line_end);
				print("Congratulations You have won!");
				return true;
			}
			
			//judge '/'
			line_start = line_end = 0;
			for (int i = 1; i < 5; i++)
			{
				if (m-i<0 || n+i>14 || board[(m - i), n + i] != chessturn)
				{
					line_start = -i + 1;
					break;
				}
				else line_start--;
			}
			for (int i = 1; i < 5; i++)
			{
				if (m+i>14 || n-i<0 || board[(m + i),  n - i] != chessturn)
					
				{
					line_end = i - 1;
					break;
				}
				else line_end++;
			}
			if (line_end - line_start >= 4)
			{
				print (line_start +","+line_end);
				print("Congratulations You have won!");
				return true;
			}
			
			return false;
		}
	
}
}
