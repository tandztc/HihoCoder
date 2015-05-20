/********************************************************************************
** auth： Tan Yong
** date： 5/18/2015 10:41:19 AM
** desc： A company plans to recruit some new employees. There are N candidates (indexed from 1 to N) have taken the recruitment examination. After the examination, the well-estimated ability value as well as the expected salary per year of each candidate is collected by the Human Resource Department.

Now the company need to choose their new employees according to these data. To maximize the company's benefits, some principles should be followed:

1. There should be exactly X males and Y females.

2. The sum of salaries per year of the chosen candidates should not exceed the given budget B.

3. The sum of ability values of the chosen candidates should be maximum, without breaking the previous principles. Based on this, the sum of the salary per year should be minimum.

4. If there are multiple answers, choose the lexicographically smallest one. In other words, you should minimize the smallest index of the chosen candidates; If there are still multiple answers, then minimize the second smallest index; If still multiple answers, then minimize the third smallest one; ...

Your task is to help the company choose the new employees from those candidates.

输入
The first line contains four integers N, X, Y, and B, separated by a single space. The meanings of all these variables are showed in the description above. 1 <= N <= 100, 0 <= X <= N, 0 <= Y <= N, 1 <= X + Y <= N, 1 <= B <= 1000.

Then follows N lines. The i-th line contains the data of the i-th candidate: a character G, and two integers V and S, separated by a single space. G indicates the gender (either "M" for male, or "F" for female), V is the well-estimated ability value and S is the expected salary per year of this candidate. 1 <= V <= 10000, 0 <= S <= 10.

We assure that there is always at least one possible answer.

输出
On the first line, output the sum of ability values and the sum of salaries per year of the chosen candidates, separated by a single space.

On the second line, output the indexes of the chosen candidates in ascending order, separated by a single space.

样例输入
4 1 1 10
F 2 3
M 7 6
M 3 2
F 9 9
样例输出
9 9
1 2
** Ver.:  V1.0.0
** Feedback: mailto:tanyong@cyou-inc.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

namespace hihocoder
{
    class Problem1137
    {
        public static void MyMain(string[] args)
        {
            string[] tokens = Console.ReadLine().Split(' ');
            int N = int.Parse(tokens[0]);
            int X = int.Parse(tokens[1]);
            int Y = int.Parse(tokens[2]);
            int B = int.Parse(tokens[3]);

            int[,] dpmale = new int[X + 1, B + 1];  //dpmale[i,j]表示选i名男应聘者在预算为j的情况下的最大分数，下同
            int[,] dpfemale = new int[Y + 1, B + 1];
            BitArray[,] dpmalebit = new BitArray[X + 1, B + 1]; //dpmalebit[i,j]表示入选男性的数组，true表示录用，下同
            BitArray[,] dpfemalebit = new BitArray[X + 1, B + 1]; //dpmalebit[i,j]表示入选男性的数组，true表示录用，下同
            for (int i = 0; i < X+1; i++)
            {
                for (int j = 0; j < B+1; j++)
                {
                    dpmale[i, j] = -1;
                    dpmalebit[i, j] = new BitArray(N);  //这里的大小为n，因为男女可以出现在任何序号，最后的结果有用Or合并即可
                }
            }
            for (int i = 0; i < Y + 1; i++)
            {
                for (int j = 0; j < B + 1; j++)
                {
                    dpfemale[i, j] = -1;
                    dpfemalebit[i, j] = new BitArray(N);
                }
            }
            dpmale[0, 0] = 0;
            dpfemale[0, 0] = 0;
            int malecnt = 0, femalecnt = 0, malebgt = 0, femalebgt = 0;
            for (int i = 0; i < N; i++)
            {
                string[] interviewer = Console.ReadLine().Split(' ');
                string G = interviewer[0];
                int V = int.Parse(interviewer[1]);
                int S = int.Parse(interviewer[2]);
                if (G=="M")
                {
                    malecnt++;
                    malebgt += S;
                    int realcnt = Math.Min(malecnt, X);
                    int realbgt = Math.Min(malebgt, B);
                    for (int curcnt = realcnt; curcnt >0 ; curcnt--)
                    {
                        for (int curbgt = realbgt; curbgt >= S; curbgt--)
                        {
                            if (dpmale[curcnt - 1, curbgt - S] == -1)
                            {
                                continue;
                            }
                            if (dpmale[curcnt - 1, curbgt - S] + V > dpmale[curcnt, curbgt])
                            {
                                dpmale[curcnt, curbgt] = dpmale[curcnt - 1, curbgt - S] + V;
                                dpmalebit[curcnt, curbgt].SetAll(false);
                                dpmalebit[curcnt, curbgt].Or(dpmalebit[curcnt - 1, curbgt - S]);
                                dpmalebit[curcnt, curbgt].Set(i, true);
                            }
                        }
                    }
                }
                else
                {
                    femalecnt++;
                    femalebgt += S;
                    int realcnt = Math.Min(femalecnt, Y);
                    int realbgt = Math.Min(femalebgt, B);
                    for (int curcnt = realcnt; curcnt > 0; curcnt--)
                    {
                        for (int curbgt = realbgt; curbgt >= S; curbgt--)
                        {
                            if (dpfemale[curcnt - 1, curbgt - S] == -1)
                            {
                                continue;
                            }
                            if (dpfemale[curcnt - 1, curbgt - S] + V > dpfemale[curcnt, curbgt])
                            {
                                dpfemale[curcnt, curbgt] = dpfemale[curcnt - 1, curbgt - S] + V;
                                dpfemalebit[curcnt, curbgt].SetAll(false);
                                dpfemalebit[curcnt, curbgt].Or(dpfemalebit[curcnt - 1, curbgt - S]);
                                dpfemalebit[curcnt, curbgt].Set(i, true);
                            }
                        }
                    }
                }
            }

            //遍历每种预算匹配，得到预算不超过B的情况下的最大分数
            int maxpoint = 0, minbudget = B, maleselect = -1, femaleselect = -1;
            for (int i = 0; i <= B; i++)
            {
                if (dpmale[X,i]==-1)
                {
                    continue;
                }
                for (int j = 0; j <= B-i; j++)
                {
                    if (dpfemale[Y,j] == -1)
                    {
                        continue;
                    }
                    if (dpmale[X, i] + dpfemale[Y, j] > maxpoint)
                    {
                        maxpoint = dpmale[X, i] + dpfemale[Y, j];   //这一句提交到hihocoder的时候会runtime error
                        maleselect = i;
                        femaleselect = j;
                        minbudget = i + j;
                    }
                    else
                    {
                        continue;
                        if (dpmale[X, i] + dpfemale[Y, j] == maxpoint)
                        {
                            if (minbudget > i + j)
                            {
                                maleselect = i;
                                femaleselect = j;
                                minbudget = i + j;
                            }
                            else if (minbudget == i + j)
                            {
                                BitArray former = new BitArray(dpmalebit[X, maleselect].Or(dpfemalebit[Y, femaleselect]));
                                BitArray current = new BitArray(dpmalebit[X, i].Or(dpfemalebit[Y, j]));
                                for (int k = 0; k < N; k++)
                                {
                                    if (former[k] ^ current[k])
                                    {
                                        if (current[k])
                                        {
                                            maleselect = i;
                                            femaleselect = j;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("{0} {1}", maxpoint, minbudget);
            if (maleselect==-1 || femaleselect == -1)
            {
                return;
            }
            BitArray result = new BitArray(dpmalebit[X, maleselect].Or(dpfemalebit[Y, femaleselect]));
            bool flag = false;
            for (int i = 0; i < N; i++)
            {
                if (result[i])
                {
                    if (flag)
                    {
                        Console.Write(" ");
                    }
                    flag = true;
                    Console.Write(i+1);
                }
            }
            Console.WriteLine();
        }

        public static void Benchmark(string[] args)
        {
            Random r = new Random();
            var sw = Stopwatch.StartNew();
            int n = 0;
            for (int i = 0; i < 100000000; i++)
            {
                n = r.Next(100) | 100086;
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw = Stopwatch.StartNew();
            BitArray mybitarr = new BitArray(100);
            BitArray abitarr = new BitArray(mybitarr);

            for (int i = 0; i < 100000000; i++)
            {
                abitarr.SetAll(false);
                abitarr.Or(mybitarr);
                abitarr.Set(r.Next(100), true);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
