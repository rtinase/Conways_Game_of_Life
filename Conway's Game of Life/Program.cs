using System;
using System.Data;
using System.Drawing;

namespace Program{
    class Program{
        static void Main(string[] args){
            Console.WriteLine("How many columns do you want?");
            int amtColumns = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How many rows do you want?");
            int amtRows = Convert.ToInt32(Console.ReadLine());

            Game myGame = new Game();
            for(int i = 0; i < 10; i++){
                bool[,] fields = myGame.make2DArray(amtRows, amtColumns);
                myGame.makeGrid(fields);
                Thread.Sleep(1000);
                Console.Clear();
            }
            
        
            // bool[,] arr = myGame.make2DArray(4,4);
            // foreach(bool i in arr){
            //     Console.WriteLine(i);
            // }
        }
        
    }

    class Game{
        private int rowsCou = 0;
        private int colsCou = 0;  // разделить конструктор и метод
        public bool[,] make2DArray(int rows = 50, int cols = 50){ //creates an array of fields 
            rowsCou = rows;
            colsCou = cols;
            bool[,] arr = new bool[rowsCou, colsCou];  //gives a size to the grid
            Random rnd = new Random();
            for(int i = 0; i < rowsCou; i++){ // loop that gives values to the array
                for(int j = 0; j < colsCou; j++){
                    arr[i,j] = (rnd.Next(2) == 0) ? false: true; // Math.Round(rnd.NextDouble()) as an option
                }
            }
            return arr;
        }
        public void makeGrid(bool[,] inputArray){ // draws a grid 
            for(int i = 0; i < inputArray.GetLength(0); i++){ 
                for(int j = 0; j < inputArray.GetLength(1); j++){  
                    if (inputArray[i,j] == true ) Console.Write("@"); // what sign will be displayd on the place alive cell
                    else Console.Write("_");    // what sign will be displayd on the place dead cell
                }
                Console.WriteLine();
            }
        }

        private int countLiveNeighbors(bool[,] inputArray, int x, int y){ // counting alive neighbors around one cell
            int sum = 0;
             
            for(int i = -1; i < 2; i++){
               for(int k = -1; k < 2; k++){

                if(inputArray[ (x + i + rowsCou) % rowsCou, (y + k +colsCou) % colsCou]) sum++; // checks cells around and adds 1 they are true
               }
            }
            return sum--; // return amount of neigbors and minus to not cound itself
        }

        public int computingNextGeneration(bool[,] inputArray){ // next generation
            bool[,] nextGeneration = new bool[inputArray.GetLength(0), inputArray.GetLength(1)]; // the next move array

            for(int i = 0; i < inputArray.GetLength(0); i++){
                for(int k = 0; k < inputArray.GetLength(1); k++){

                    // if(i == 0 || i == inputArray.GetLength(0)-1 || k == 0 || k == inputArray.GetLength(1)-1){// Checking collision of the edjes

                    // } 
                    
                    int neighbors = this.countLiveNeighbors(inputArray, i, k); // checking amount of cells around that are alive
                    bool isAlive = inputArray[i,k]; // checking if this cell alive

                    if( !isAlive && neighbors == 3){
                        nextGeneration[i,k] = true;
                    }
                    else if(isAlive && (neighbors < 2 || neighbors > 3)){
                        nextGeneration[i,k] = false;
                    }
                    else{
                        nextGeneration[i,k] = inputArray[i,k];
                    }
                    
                }
            }
            return 0;
        }
    }
}
