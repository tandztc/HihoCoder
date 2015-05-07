/********************************************************************************
** auth： Tan Yong
** date： 5/7/2015 5:21:45 PM
** desc： 时间限制:2000ms
单点时限:1000ms
内存限制:256MB
描述
小Ho给自己定了一个宏伟的目标：连续100天每天坚持在hihoCoder上提交一个程序。100天过去了，小Ho查看自己的提交记录发现有N天因为贪玩忘记提交了。于是小Ho软磨硬泡、强忍着小Hi鄙视的眼神从小Hi那里要来M张"补提交卡"。每张"补提交卡"都可以补回一天的提交，将原本没有提交程序的一天变成有提交程序的一天。小Ho想知道通过利用这M张补提交卡，可以使自己的"最长连续提交天数"最多变成多少天。

输入
第一行是一个整数T(1 <= T <= 10)，代表测试数据的组数。

每个测试数据第一行是2个整数N和M(0 <= N, M <= 100)。第二行包含N个整数a1, a2, ... aN(1 <= a1 < a2 < ... < aN <= 100)，表示第a1, a2, ...  aN天小Ho没有提交程序。

输出
对于每组数据，输出通过使用补提交卡小Ho的最长连续提交天数最多变成多少。

样例输入
3  
5 1  
34 77 82 83 84  
5 2  
10 30 55 56 90  
5 10  
10 30 55 56 90
样例输出
76  
59
100
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
    class Problem1051
    {
        public static void MyMain(string[] args)
        {
            int times = int.Parse(Console.ReadLine());
            for (int i = 0; i < times; i++)
            {
                string[] tokens = Console.ReadLine().Split(' ');
                int LackDayNum = int.Parse(tokens[0]);
                int CardNum = int.Parse(tokens[1]);
                string[] lackdays = Console.ReadLine().Split(' ');
                List<int> LackDayList = new List<int>();
                for (int j = 0; j < LackDayNum; j++)
                {
                    LackDayList.Add(int.Parse(lackdays[j]));
                }

                int result = CalcMaxContinuousDay(LackDayList, CardNum);
                Console.WriteLine(result);
            }
        }

        private static int CalcMaxContinuousDay(List<int> LackDayList, int CardNum)
        {
            int slidenum = LackDayList.Count - CardNum + 1;
            if (slidenum<0)
            {
                return 100;
            }
            int max = 0;
            for (int i = 0; i < slidenum; i++)
            {
                List<int> currentLack = new List<int>();
                for (int j = 0; j < i; j++)
                {
                    currentLack.Add(LackDayList[j]);
                }
                for (int j = i + CardNum; j < LackDayList.Count; j++)
                {
                    currentLack.Add(LackDayList[j]);
                }
                int curvalue = CalcCurrentMax(currentLack);
                max = max < curvalue ? curvalue : max;
            }
            return max;
        }

        private static int CalcCurrentMax(List<int> LackList)
        {
            if (LackList == null || LackList.Count == 0)
            {
                return 100;
            }
            int max = 0;
            for (int i = 0; i < LackList.Count; i++)
            {
                int curvalue = 0;


                if (i == 0)
                {
                    curvalue = LackList[i] - 1;
                }
                else
                {
                    curvalue = LackList[i] - LackList[i - 1] - 1;
                }
                max = max < curvalue ? curvalue : max;
                if (i == LackList.Count - 1)
                {
                    curvalue = 100 - LackList[i];
                    max = max < curvalue ? curvalue : max;
                }

            }
            return max;
        }
    }
}
