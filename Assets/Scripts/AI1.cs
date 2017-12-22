using UnityEngine;
using System.Collections;
namespace WZQ{
public class AI1 : MonoBehaviour{

	public Player player = Player.white;
	public int depth;

	public const int LIVE_ONE =20;
	public const int DASH_ONE =4;
	public const int LIVE_TWO =400;
	public const int DASH_TWO =90;
	public const int LIVE_THREE =6000;
	public const int DASH_THREE =800;
	public const int LIVE_FOUR =20000;
	public const int DASH_FOUR =10000;
	public const int FIVE = 50000;
	
	public  void AiSelect(int[,]board,out int m,out int n,out int score){

	
		int theValue = 0;
		int Alpha = -100000;
		int Beta =  100000;

		int[,]newBoard = new int[15,15];
		for(int ii=0; ii<15; ii++)
			for(int jj=0; jj<15; jj++)
				newBoard[ii,jj] = board[ii,jj];

			m = 0; n = 0;score = 0;

		if(player == Player.black){
			for(int i=0; i<15; i++)
				for(int j=0; j<15; j++)
					if(board[i,j] == 0){
						newBoard[i,j] = 1;
						theValue =AlphaBeta(newBoard,depth-1,Alpha,Beta,Player.white,0);//player = Player.white;
						newBoard[i,j] = 0;
						if(theValue > Alpha){
							Alpha = theValue;
							m = i;
							n = j;
						}
					}
		}else{
				for(int i=0; i<15; i++)
					for(int j=0; j<15; j++)
					if(board[i,j] == 0){
						newBoard[i,j] = -1;
						theValue = AlphaBeta(newBoard,depth-1,Alpha,Beta,Player.black,0);//player = Player.white;
						newBoard[i,j] = 0;
						if(theValue < Beta){
							Beta = theValue;
							m = i;
							n = j;
						}

					}
				print(Beta);
				score = Beta;
			}
	}
	int AlphaBeta(int[,]board,int depth,int alpha ,int beta,Player player,int value){

			if(player == Player.black){
			if(depth > 0){
			for(int i=0; i<15; i++)
				for(int j=0; j<15; j++)
				if(board[i,j] == 0){
					if(alpha >= beta) return alpha;			
						board[i,j] = 1;
						//int value;
						int theValue =Evaluate(board,i,j,Player.black);
						value += theValue;
						value += AlphaBeta(board,depth-1,alpha,beta,Player.white,value);
						if(alpha < value)
							alpha = value;
						board[i,j] = 0;
						value -= theValue;
					}
				}else{
							for(int i=0; i<15; i++)
								for(int j=0; j<15; j++)
								if(board[i,j] == 0){
									value += Evaluate(board,i,j,Player.black);
								if(alpha < value)
									alpha = value;
								}

				}
				return alpha;
			}
			else{
			 if(depth > 0){
					for(int i=0; i<15; i++)
						for(int j=0; j<15; j++)
						if(board[i,j] == 0){
								if(alpha >= beta) return alpha;
								board[i,j] = -1;
								//int value;
								int theValue =Evaluate(board,i,j,Player.white);
								value += theValue;
								value += AlphaBeta(board,depth-1,alpha,beta,Player.black,value);
								if(value < beta)
									beta = value;
								board[i,j] = 0;
								value -= theValue;
							}
					}else{
						for(int i=0; i<15; i++)
							for(int j=0; j<15; j++)
							if(board[i,j] == 0){
								value += Evaluate(board,i,j,Player.white);
								if(alpha < value)
									alpha = value;
							}
							
					}
			return beta;
		}
	}
	int Evaluate(int[,]board,int m, int n,Player player){
		int chessturn = (player == Player.black) ? 1 : -1;
		int deltaValue=0;
		//judge '|'
		int line_start = 0, line_end = 0;
		int extendible = 0;
		
		for (int i = 1; i < 5; i++)
		{
			if (m-i<0 ||board[(m - i),n] != chessturn)
			{
				line_start = -i + 1;
				if(m-i>=0 && board[(m-i),n] ==0 )extendible++;
				else extendible--;
				break;
			}
			else line_start--;
		}
		for (int i = 1; i < 5; i++)
		{
			if (m+i>14 || board[(m + i), n] != chessturn)

			{
				line_end = i - 1;
				if(m+i<=14 && board[(m+i),n] ==0 )extendible++;
				else extendible--;
				break;
			}
			else line_end++;
		}
		switch (line_end - line_start) {
		case  0: 
			if(extendible == 2) {deltaValue += LIVE_ONE;} 
			else if(extendible == 0) {deltaValue += DASH_ONE;}
			else deltaValue += 0;
			break;
		case  1: 
			if(extendible == 2) {deltaValue += LIVE_TWO;} 
			else if(extendible == 0) {deltaValue += DASH_TWO;}
			else deltaValue += 0;
			break;
		case  2:    
			if(extendible == 2) {deltaValue += LIVE_THREE;} 
			else if(extendible == 0) {deltaValue += DASH_THREE;}
			else deltaValue += 0;
			break;
		case  3:    
			if(extendible == 2) {deltaValue += LIVE_FOUR;} 
			else if(extendible == 0) {deltaValue += DASH_FOUR;}
			else deltaValue += 0;
			break;
		case 4 :
			deltaValue += FIVE;
			break;
		default : break;
		}	//judge '-'
		line_start = line_end = 0;
		extendible = 0;
		for (int i = 1; i < 5; i++)
		{
			if (n-i<0 || board[m,n-i] != chessturn)
			{
				line_start = -i + 1;
				if(n-i>=0 && board[m,n-i] ==0 )extendible++;
				else extendible--;
				break;
			}
			else line_start--;
		}
		for (int i = 1; i < 5; i++)
		{
			if ( n+i>14 || board[m, n + i] != chessturn)
			{
				line_end = i - 1;
				if(n+i<=14 && board[m,n+i] ==0 )extendible++;
				else extendible--;
				break;
			}
			else line_end--;
		}
		switch (line_end - line_start) {
		case  0: 
			if(extendible == 2) {deltaValue += LIVE_ONE;} 
			else if(extendible == 0) {deltaValue += DASH_ONE;}
			else deltaValue += 0;
			break;
		case  1: 
			if(extendible == 2) {deltaValue += LIVE_TWO;} 
			else if(extendible == 0) {deltaValue += DASH_TWO;}
			else deltaValue += 0;
			break;
		case  2:    
			if(extendible == 2) {deltaValue += LIVE_THREE;} 
			else if(extendible == 0) {deltaValue += DASH_THREE;}
			else deltaValue += 0;
			break;
		case  3:    
			if(extendible == 2) {deltaValue += LIVE_FOUR;} 
			else if(extendible == 0) {deltaValue += DASH_FOUR;}
			else deltaValue += 0;
			break;
		case 4 :
			deltaValue += FIVE;
			break;
		default : break;
		}
		
		//judge '\'
		line_start = line_end = 0;
		extendible = 0;
		for (int i = 1; i < 5; i++)
		{
			if (m-i <0||n-i <0 || board[(m - i), n - i] != chessturn)
			{
				line_start = -i + 1;
				if(m-i>=0 && n-i>=0 &&board[(m-i),n-i] ==0 )extendible++;
				else extendible--;
				break;
			}
			else line_start--;
		}
		for (int i = 1; i < 5; i++)
		{
			if (m+i>14 || n+i >14|| board[(m + i), n + i] != chessturn)
			{
				line_end = i - 1;
				if(m+i<=14 && n+i<=14 && board[(m+i),n+i] ==0 )extendible++;
				else extendible--;
				break;
			}
			else line_end++;
		}
		switch (line_end - line_start) {
		case  0: 
			if(extendible == 2) {deltaValue += LIVE_ONE;} 
			else if(extendible == 0) {deltaValue += DASH_ONE;}
			else deltaValue += 0;
			break;
		case  1: 
			if(extendible == 2) {deltaValue += LIVE_TWO;} 
			else if(extendible == 0) {deltaValue += DASH_TWO;}
			else deltaValue += 0;
			break;
		case  2:    
			if(extendible == 2) {deltaValue += LIVE_THREE;} 
			else if(extendible == 0) {deltaValue += DASH_THREE;}
			else deltaValue += 0;
			break;
		case  3:    
			if(extendible == 2) {deltaValue += LIVE_FOUR;} 
			else if(extendible == 0) {deltaValue += DASH_FOUR;}
			else deltaValue += 0;
			break;
		case 4 :
			deltaValue += FIVE;
			break;
		default : break;
		}
		
		//judge '/'
		line_start = line_end = 0;
		extendible = 0;
		for (int i = 1; i < 5; i++)
		{
			if (m-i<0 || n+i>14 || board[(m - i), n + i] != chessturn)
			{
				line_start = -i + 1;
				if(m-i>=0 && n+i<=14 && board[(m-i),n+i] ==0 )extendible++;
				else extendible--;
				break;
			}
			else line_start--;
		}
		for (int i = 1; i < 5; i++)
		{
			if (m+i>14 || n-i<0 || board[(m + i),  n - i] != chessturn)	
			{
				line_end = i - 1;
				if(m+i<=14 && n-i>=0 && board[(m+i),n-i] ==0 )extendible++;
				else extendible--;
				break;
			}
			else line_end++;
		}
		switch (line_end - line_start) {
		case  0: 
			if(extendible == 2) {deltaValue += LIVE_ONE;} 
			else if(extendible == 0) {deltaValue += DASH_ONE;}
			else deltaValue += 0;
			break;
		case  1: 
			if(extendible == 2) {deltaValue += LIVE_TWO;} 
			else if(extendible == 0) {deltaValue += DASH_TWO;}
			else deltaValue += 0;
			break;
		case  2:    
			if(extendible == 2) {deltaValue += LIVE_THREE;} 
			else if(extendible == 0) {deltaValue += DASH_THREE;}
			else deltaValue += 0;
			break;
		case  3:    
			if(extendible == 2) {deltaValue += LIVE_FOUR;} 
			else if(extendible == 0) {deltaValue += DASH_FOUR;}
			else deltaValue += 0;
			break;
		case 4 :
			deltaValue += FIVE;
			break;
		default : break;
		}
			if (player == Player.white)
				return -deltaValue;
			return deltaValue;
	}
}
}