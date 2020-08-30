//Maze Program By Ryan
//Made For YaCodingContest #1

//Mazes Solved and Checked
// 1, 2, 3, b1, b2, b3
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Solver
{
	class Program
	{
		static void Main(string[] args)
		{
			
			var MazeToSolve = "1";			
				String[] MazeS = File.ReadAllLines($"{Directory.GetCurrentDirectory()}/Mazes/{MazeToSolve}.txt");
			
				var StartX = -1;
				var StartY = -1;
				var EndX = -1;
				var EndY = -1;

			String[][] Maze = new String[MazeS.Length][];
			Console.WriteLine("The Maze:");
			for (int i = 0; i < MazeS.Length; i++)
			{
				Console.WriteLine(MazeS[i]);
				Maze[i] = MazeS[i].ToCharArray().Select(c => c.ToString()).ToArray();
			}

			for (int i = 0; i < Maze.Length; i++)
			{
				for (int j = 0; j < Maze[i].Length; j++)
				{
					if(Maze[i][j] == "x")
					{
						StartX = j;
						StartY = i;
					}
					if (Maze[i][j] == "o")
					{
						EndX = j;
						EndY = i;
					}
				}
			}

			if (StartX == -1 || StartY == -1 || EndX == -1 || EndY == -1)
			{
				Console.Error.WriteLine("ERROR: Cant Find Start Or End Point!!!");
				Console.ReadKey();
				return;
			}
			Console.WriteLine("If The Program Gets Stuck Here, The Maze Is Impossible");
			Maze[EndY][EndX] = "0";
			var Num = 0;
			while (Maze[StartY][StartX] == "x")
			{

				for (int i = 0; i < Maze.Length; i++)
				{
					for (int j = 0; j < Maze[i].Length; j++)
					{
						if(Maze[i][j] == Num.ToString())
						{
							try
							{
								if (Maze[i + 1][j] == " " || Maze[i + 1][j] == "x")
								{
									//Console.WriteLine(Num);
									var NPO = Num + 1;
									Maze[i + 1][j] = NPO.ToString();
								}
							}
							catch (System.IndexOutOfRangeException)
							{
							}
							try
							{
								if (Maze[i - 1][j] == " " || Maze[i - 1][j] == "x")
								{
									//Console.WriteLine(Num);
									var NPO = Num + 1;
									Maze[i - 1][j] = NPO.ToString();
								}
							}
							catch (System.IndexOutOfRangeException)
							{
							}
							try
							{
								if (Maze[i][j + 1] == " " || Maze[i][j + 1] == "x")
								{
									//Console.WriteLine(Num);
									var NPO = Num + 1;
									Maze[i][j + 1] = NPO.ToString();
								}
							}
							catch (System.IndexOutOfRangeException)
							{
							}
							try
							{
								if (Maze[i][j - 1] == " " || Maze[i][j - 1] == "x")
								{
									//Console.WriteLine(Num);
									var NPO = Num + 1;
									Maze[i][j - 1] = NPO.ToString();
								}
							}
							catch (System.IndexOutOfRangeException)
							{
							}
						}
					}
				}
				Num++;
			}
			var MazerX = StartX;
			var MazerY = StartY;
			List<String> Dirs = new List<string>();
			List<int> PosX = new List<int>();
			List<int> PosY = new List<int>();
			Console.WriteLine("");
			Console.WriteLine("Solving Instrutions:");
			PosX.Add(MazerX);
			PosY.Add(MazerY);
			while (MazerX != EndX || MazerY != EndY)
			{
				var Number = Int32.Parse(Maze[MazerY][MazerX]);
				//Console.WriteLine(Number);
				try
				{
					if (Maze[MazerY + 1][MazerX] == (Number - 1).ToString())
					{
						//Down
						Dirs.Add("D");
						MazerY++;
						PosX.Add(MazerX);
						PosY.Add(MazerY);
					}
				}
				catch (System.IndexOutOfRangeException)
				{
				}
				try
				{
					if (Maze[MazerY - 1][MazerX] == (Number - 1).ToString())
					{
						//Up
						Dirs.Add("U");
						MazerY--;
						PosX.Add(MazerX);
						PosY.Add(MazerY);
						
					}
				}
				catch (System.IndexOutOfRangeException)
				{
				}
				try
				{
					if (Maze[MazerY][MazerX + 1] == (Number - 1).ToString())
					{
						//Right
						Dirs.Add("R");
						MazerX++;
						PosX.Add(MazerX);
						PosY.Add(MazerY);
						
					}
				}
				catch (System.IndexOutOfRangeException)
				{
				}
				try
				{
					if (Maze[MazerY][MazerX - 1] == (Number - 1).ToString())
					{
						//Left
						Dirs.Add("L");
						MazerX--;
						PosX.Add(MazerX);
						PosY.Add(MazerY);
						
					}
				}
				catch (System.IndexOutOfRangeException)
				{
				}
			}

			var PrevDir = Dirs[0];
			var DirNum = 0;
			var comma = "";
			for (int i = 0;i < Dirs.Count; i++)
			{
				if (Dirs[i] == PrevDir)
				{
					DirNum++;
				}
				else
				{
					if(DirNum == 1)
					{
						Console.Write($"{comma}{Dirs[i - 1]}");
					}
					else
					{
						Console.Write($"{comma}{Dirs[i - 1]}*{DirNum}");
					}
					PrevDir = Dirs[i];
					DirNum = 1;
					comma = ", ";
				}
			}
			
				if (DirNum == 1)
				{
					Console.Write($"{comma}{Dirs[Dirs.Count - 1]}");
				}
				else
				{
					Console.Write($"{comma}{Dirs[Dirs.Count - 1]}*{DirNum}");
				}
			
			Console.WriteLine("");
			Console.WriteLine("Solved Maze:");
			for(int i = 0; i < Maze.Length; i++)
			{
				for (int j = 0; j < Maze[i].Length; j++)
				{
					var MazerPos = false;
					for (int p = 0; p < PosX.Count; p++)
					{
						if(PosX[p] == j && PosY[p] == i)
						{
							MazerPos = true;
						}
					}
					if (MazerPos)
					{
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.Write("+");
					}else if(Maze[i][j] == "#")
					{

						Console.Write("#");
					}
					else
					{
						Console.Write(" ");
					}
					Console.ForegroundColor = ConsoleColor.White;
				}
				Console.WriteLine();
			}
			//End
			Console.WriteLine("Program finished. Press any key to close");
			Console.ReadKey();
		}
	}
}
