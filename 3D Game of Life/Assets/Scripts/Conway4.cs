using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Conway4 : MonoBehaviour {


	private System.Random random;
	public GameObject cube0,cube1,cube2,cube3,cube4,cube5,cube6,cube7,cube8,cube9;
	public bool seedIsSetable; 
	public int seed;
	public int width;
	public int height;
	public int depth;
	public int colors;
	public int fill;
	bool[,,,] world; 
	float genDelay;


	// Use this for initialization
	void Start () {
		genDelay = 0;
		world = new bool[width,height,depth,colors];
		seedWorld();
	}

	void draw(){
		for (int i = 0; i < world.GetLength(0); i++){
			for (int j = 0; j < world.GetLength(1); j++){
				for (int k = 0; k < world.GetLength(2); k++){
					for (int l = 0; l < world.GetLength(3); l++){
						if(world[i,j,k,l]){
							if(l == 0){
								GameObject myCube = Instantiate(cube0, new Vector3(i,j,k), Quaternion.identity);
								Destroy(myCube,.09f);
							}else if(l == 1){
								GameObject myCube = Instantiate(cube1, new Vector3(i,j,k), Quaternion.identity);
								Destroy(myCube,.09f);
							}else if(l == 2){
								GameObject myCube = Instantiate(cube2, new Vector3(i,j,k), Quaternion.identity);
								Destroy(myCube,.09f);
							}else if(l == 3){
								GameObject myCube = Instantiate(cube3, new Vector3(i,j,k), Quaternion.identity);
								Destroy(myCube,.09f);
							}else if(l == 4){
								GameObject myCube = Instantiate(cube4, new Vector3(i,j,k), Quaternion.identity);
								Destroy(myCube,.09f);
							}else if(l == 5){
								GameObject myCube = Instantiate(cube5, new Vector3(i,j,k), Quaternion.identity);
								Destroy(myCube,.09f);
							}else if(l == 6){
								GameObject myCube = Instantiate(cube6, new Vector3(i,j,k), Quaternion.identity);
								Destroy(myCube,.09f);
							}else if(l == 7){
								GameObject myCube = Instantiate(cube7, new Vector3(i,j,k), Quaternion.identity);
								Destroy(myCube,.09f);
							}else if(l == 8){
								GameObject myCube = Instantiate(cube8, new Vector3(i,j,k), Quaternion.identity);
								Destroy(myCube,.09f);
							}else if(l == 9){
								GameObject myCube = Instantiate(cube9, new Vector3(i,j,k), Quaternion.identity);
								Destroy(myCube,.09f);
							}
						}
					}
				}
			}
		}
	}


	void seedWorld(){
		if(seedIsSetable){
			random = new System.Random(seed);
		}else{
			seed = DateTime.Now.Millisecond;
			random = new System.Random(seed);   //https://msdn.microsoft.com/en-us/library/system.random(v=vs.110).aspx       https://msdn.microsoft.com/en-us/library/system.datetime.millisecond(v=vs.110).aspx
		}


		for (int i = 0; i < world.GetLength(0); i++){
			for (int j = 0; j < world.GetLength(1); j++){
				for (int k = 0; k < world.GetLength(2); k++){
					for (int l = 0; l < world.GetLength(3); l++){
						if(random.Next(0,100) >= fill){
							world[i,j,k,l] = false;
						} else{
							world[i,j,k,l] = true;	
						}
					}
				}
				
			}
		}

	}


	void nextGeneration(){
		bool[,,,] tempWorld = new bool[width,height,depth,colors];
		for (int i = 0; i < world.GetLength(0); i++){
			for (int j = 0; j < world.GetLength(1); j++){
				for (int k = 0; k < world.GetLength(2); k++){
					for (int l = 0; l < world.GetLength(3); l++){
						tempWorld[i,j,k,l] = world[i,j,k,l];
					}
				}
			}
		}
		for (int i = 0; i < world.GetLength(0); i++){
			for (int j = 0; j < world.GetLength(1); j++){
				for (int k = 0; k < world.GetLength(2); k++){
					for (int l = 0; l < world.GetLength(3); l++){
						int neighbors = getMooreNeighborhood4D(i,j,k,l);
						if(neighbors < 2 && tempWorld[i,j,k,l]){
							tempWorld[i,j,k,l] = false;
						}else if(neighbors > 2 && tempWorld[i,j,k,l]){
							tempWorld[i,j,k,l] = false;
						}else if(world[i,j,k,l] == false && (neighbors == 3 || neighbors == 4)){
							tempWorld[i,j,k,l] = true;
						}
					}
				} 
			}
		}
		world = tempWorld;
	}
	




	int getMooreNeighborhood4D(int x, int y, int z, int a){
		int sum = 0;
		for(int i = -1; i <= 1; i++){
			for(int j = -1; j <= 1; j++){
				for(int k = -1; k <= 1; k++){
					for(int l = -1; l <= 1; l++){
						if(x+i>-1 && y+j>-1 && z+k>-1 && a+l>-1 && x+i < world.GetLength(0) && y+j < world.GetLength(1) && z+k < world.GetLength(2) && a+l < world.GetLength(3)){           ///this is real bad, need to fix
							if(world[x+i,y+j,z+k, a+l]){
								sum += 1;
							}
						}
					}
				}
			}
		}
		if (world[x,y,z,a]){
			sum-=1;
		}
		return sum;
	}


	// Update is called once per frame
	void Update () {
		if(Time.time> genDelay){
			draw();
			nextGeneration();
			genDelay = Time.time + .08f;
		}

	}
}

			//12-13
/*                      24
........................O           35     0
......................O.O                  1     
............OO......OO............OO       2
...........O...O....OO............OO       3
OO........O.....O...OO                     4
OO........O...O.OO....O.O                  5
..........O.....O.......O                  6
...........O...O                           7
............OO                             8
              
*/