using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> breakdown = new Dictionary<string, int>();
            Dictionary<string, int> sortedBD = new Dictionary<string, int>();
            int wordCount = 0;
            List<string> lines = new List<string>();
            string[] words = new string[] { };
            int leastCount = 0;
            string sortKey = "";
            char[] letters = { };
            string fileName = "Jupiter.txt";
            //string fileName = "Odin.txt";
            string outputFile = "";
            // create output file name

            outputFile = fileName.Split('.')[0] + "_WordCount.txt";

            Console.Write("Begin Reading...");

            lines = outputfile(fileName, lines);

            Console.WriteLine("Done!");
            Console.Write("Begin Counting...");

            wordCount = wordcounting(lines, wordCount, words, letters);

            Console.WriteLine("Done!");
            Console.Write("Begin Sorting...");

            breakdown = Letsbreakitdown(lines, wordCount, breakdown, words, letters,  leastCount, sortKey, sortedBD);


            Console.WriteLine("Done!");
            Console.Write("Begin Writing...");

            StreamWriting(sortedBD, outputFile, fileName, wordCount);
            
            Console.WriteLine("Done!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        static List<string> outputfile (string fileName, List<string> lines)
        {
            string line = "";
            using (StreamReader sr = new StreamReader(fileName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length > 0)
                        lines.Add(line);
                }
            }
            return lines;
        }
        static int wordcounting (List<string> lines, int wordCount, string[] words, char[] letters)
        {
            
            foreach (string l in lines)
            {
                words = l.Split(' ');
                for (int x = 0; x < words.Length; x++)
                //foreach(string word in words)
                {
                    if (words[x].Length > 0)
                    {
                        // special character filter
                        if (words[x].Length > 1)
                        {
                            letters = words[x].ToCharArray();
                            if ((int)letters[letters.Length - 1] == 33 // !
                                || (int)letters[letters.Length - 1] == 44 // ,
                                || (int)letters[letters.Length - 1] == 46 // .
                                || (int)letters[letters.Length - 1] == 63 // ?
                                )
                            {
                                words[x] = "";
                                for (int y = 0; y < letters.Length - 1; y++)
                                {
                                    words[x] += letters[y];
                                }
                            }
                        }
                        wordCount++;
                    }
                }
            }
            return wordCount;
        }
        static Dictionary<string, int> Letsbreakitdown (List<string> lines, int wordCount, Dictionary<string, int> breakdown, string[] words, char[] letters, int leastCount, string sortKey, Dictionary<string, int> sortedBD)
        {
            {
                foreach (string l in lines)
                {
                    words = l.Split(' ');
                    for (int x = 0; x < words.Length; x++)
                    //foreach(string word in words)
                    {
                        if (words[x].Length > 0)
                        {
                            // special character filter
                            if (words[x].Length > 1)
                            {
                                letters = words[x].ToCharArray();
                                if ((int)letters[letters.Length - 1] == 33 // !
                                    || (int)letters[letters.Length - 1] == 44 // ,
                                    || (int)letters[letters.Length - 1] == 46 // .
                                    || (int)letters[letters.Length - 1] == 63 // ?
                                    )
                                {
                                    words[x] = "";
                                    for (int y = 0; y < letters.Length - 1; y++)
                                    {
                                        words[x] += letters[y];
                                    }
                                }
                            }


                            if (breakdown.ContainsKey(words[x].ToLower()))
                                breakdown[words[x].ToLower()] += 1;
                            else
                                breakdown[words[x].ToLower()] = 1;

                            wordCount++;
                        }
                    }
                }
                while (breakdown.Count > 0)
                {
                    leastCount = 0;

                    foreach (KeyValuePair<string, int> kvp in breakdown)
                    {
                        if (leastCount < kvp.Value)
                        {
                            leastCount = kvp.Value;
                            sortKey = kvp.Key;
                        }
                    }

                    sortedBD[sortKey] = leastCount;
                    breakdown.Remove(sortKey);
                }
                return breakdown;
            }
        }
        static void StreamWriting (Dictionary<string, int> sortedBD, string outputFile, string fileName, int wordCount)
        {
            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                sw.WriteLine("Word count of {0}.", fileName);
                sw.WriteLine("Total Wordcount is {0}.", wordCount);
                foreach (KeyValuePair<string, int> kvp in sortedBD)
                {
                    sw.WriteLine("{0}-{1}", kvp.Key, kvp.Value);
                }
            }
        }

    }
}
