/********************************************************************************
** auth： Tan Yong
** date： 5/8/2015 11:11:37 AM
** desc： Little Hi gets lost in the city. He does not know where he is. He does not know which direction is north.

Fortunately, Little Hi has a map of the city. The map can be considered as a grid of N*M blocks. Each block is numbered by a pair of integers. The block at the north-west corner is (1, 1) and the one at the south-east corner is (N, M). Each block is represented by a character, describing the construction on that block: '.' for empty area, 'P' for parks, 'H' for houses, 'S' for streets, 'M' for malls, 'G' for government buildings, 'T' for trees and etc.

Given the blocks of 3*3 area that surrounding Little Hi(Little Hi is at the middle block of the 3*3 area), please find out the position of him. Note that Little Hi is disoriented, the upper side of the surrounding area may be actually north side, south side, east side or west side.

输入
Line 1: two integers, N and M(3 <= N, M <= 200).
Line 2~N+1: each line contains M characters, describing the city's map. The characters can only be 'A'-'Z' or '.'.
Line N+2~N+4: each line 3 characters, describing the area surrounding Little Hi.

输出
Line 1~K: each line contains 2 integers X and Y, indicating that block (X, Y) may be Little Hi's position. If there are multiple possible blocks, output them from north to south, west to east.

样例输入
8 8
...HSH..
...HSM..
...HST..
...HSPP.
PPGHSPPT
PPSSSSSS
..MMSHHH
..MMSH..
SSS
SHG
SH.
样例输出
5 4
** Ver.:  V1.0.0
** Feedback: mailto:tanyong@cyou-inc.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hihocoder
{
    class Problem1094
    {
        const int LOCATION_NUM = 3;
        const int CENTER_OF_LOCATION = 1;
        static int Row, Col;
        static char[,] CityMap;
        static char[,] Location;
        public static void MyMain(string[] args)
        {

            string[] tokens = Console.ReadLine().Split(' ');
            Row = int.Parse(tokens[0]);
            Col = int.Parse(tokens[1]);
            CityMap = new char[Row, Col];
            for (int i = 0; i < Row; i++)
            {
                char[] curline = Console.ReadLine().ToCharArray();
                for (int j = 0; j < Col; j++)
                {
                    CityMap[i,j] = curline[j];
                }
            }
            Location = new char[LOCATION_NUM, LOCATION_NUM];
            for (int i = 0; i < LOCATION_NUM; i++)
            {
                char[] curline = Console.ReadLine().ToCharArray();
                for (int j = 0; j < LOCATION_NUM; j++)
                {
                    Location[i, j] = curline[j];
                }
            }
            List<int[]> matchlist = GetAllMatch();
            foreach (var item in matchlist)
            {
                Console.WriteLine("{0} {1}", item[0]+1, item[1]+1);
            }
        }

        private static List<int[]> GetAllMatch()
        {
            List<int[]> result = new List<int[]>();
            List<char[,]> locations = GenerateAllLocations();


            for (int i = CENTER_OF_LOCATION; i < Row-CENTER_OF_LOCATION; i++)
            {
                for (int j = CENTER_OF_LOCATION; j < Col - CENTER_OF_LOCATION; j++)
                {
                    bool matched = false;
                    if (CityMap[i, j] == Location[CENTER_OF_LOCATION, CENTER_OF_LOCATION])
                    {
                        foreach (var curlocation in locations)
                        {
                            if (HasMatch(i, j, curlocation))
                            {
                                matched = true;
                                break;
                            }
                        }
                    }
                    if (matched)
                    {
                        result.Add(new int[] { i, j });
                    }
                }
            }
            return result;
        }

        private static List<char[,]> GenerateAllLocations()
        {
            List<char[,]> locations = new List<char[,]>();
            locations.Add(Location);
            for (int i = 0; i < 3; i++)
            {
                char[,] newloc = new char[LOCATION_NUM, LOCATION_NUM];
                for (int j = 0; j < LOCATION_NUM; j++)
                {
                    for (int k = 0; k < LOCATION_NUM; k++)
                    {
                        newloc[j, k] = locations[i][LOCATION_NUM - k - 1, j];
                    }
                }
                locations.Add(newloc);
            }
            return locations;
        }

        private static bool HasMatch(int centerX, int centerY, char[,] curlocation)
        {
            for (int i = 0; i < LOCATION_NUM; i++)
            {
                for (int j = 0; j < LOCATION_NUM; j++)
                {
                    if (curlocation[i,j] != CityMap[centerX+i-1,centerY+j-1])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
