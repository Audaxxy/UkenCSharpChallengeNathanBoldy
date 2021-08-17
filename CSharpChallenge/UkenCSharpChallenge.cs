//Nathan Boldy 
//C# Script that loads in a series of .txt files, and returns the number with the lowest frequency in each file.
using System;
using System.IO;
namespace CSharpChallenge
{
	class globals
	{
		
		public static int[] fileContentIntegers; //Array to hold the file data once converted to integers
		public static string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\src\","*.txt"); //Array to hold the file names, automatically finds the current directory, locates the "src" folder, and reads in files paths of all .txt files as a string to be accessed by LoadFile()
	}
	static class UkenCSharpChallenge
	{
		//Method for loading and sorting files, reads in the file path strings from files[]
		static void LoadFile(string file)
		{
			string[] fileContents = File.ReadAllLines(file); //Reads all of the lines of the passed file, stores them in an array of strings: filecontents[]

			globals.fileContentIntegers = new int[fileContents.Length]; //Resets the global array to a blank integer array  an element for each line of the file
			int index = 0; //temp var to ensure all ints are given their own index within the new array
			foreach (string line in fileContents) //for every line in the file
			{
					int lineInt = Convert.ToInt32(line); //convert line to int

					globals.fileContentIntegers[index] = lineInt; //Populates the designated index of the global array to the current line
					index++; //increment index
			}
			Array.Sort(globals.fileContentIntegers); //sort the array from lowest to highest
		}


		//Logic for Main loop
		//Given the contents of each file are sorted and stored as an array in ascending order, by parsing front-to-back every identical integer will be adjacent.
		//We simply iterate through the array and count the occurrances until there is a mismatch between the current integer and the next integer.
		//If the number of occurances of that given interger is less than the record of the previous least popular integer, it becomes the new record.
		//As we are parsing in ascending order, and only checking if the current occurance streak is LESS THAN the record, even if a higher number occurs the same number of times as a lower number, the lower number will still hold the record.
		static void Main()
		{
			
			for (int i = 0; i < globals.files.Length ; i++)//For as many files as are detected
			{
				LoadFile(globals.files[i]); //Load the corresponding file

				int[] fileContentIntegers = globals.fileContentIntegers; //Create a local copy of the global integer array
				int timesOccurred = 0; //Keeps track of the number of times a number has occurred consecutively
				int currentInt = fileContentIntegers[0]; //The current integer, defaulted to the first integer in the array
				int occuranceRecord = int.MaxValue; //The current record for the least number of times an integer has occurred, defaulted to the maximum possible integer
				int leastPopular = int.MaxValue; //The current integer value of the integer that holds the occurance record
				
				for (int j = 0; j < fileContentIntegers.Length - 1; j++)//For every integer in the array
				{
					if (fileContentIntegers[j] == currentInt) //If the next integer is equal to the current integer, add to the streak
					{
						timesOccurred++;
					}
					else//Otherwise, 
					{
						if (timesOccurred < occuranceRecord) //If the current integer streak was less than the last
						{
							occuranceRecord = timesOccurred; //Set the record to the current streak
							leastPopular = currentInt; //Set the leastpopular number to the current integer value
						}
						currentInt = fileContentIntegers[j]; //Set the current integer to the next integer in the array
						timesOccurred = 1; //reset the streak
					}
				}
				Console.WriteLine((i+1)+": File: "+globals.files[i] + " Number: " + leastPopular + ", Repeated " + occuranceRecord + " time(s)");// Output the result of the file
			}
			Console.Read();//Keep console open
		}
	}
}
