/********************************************************************************
** auth： Tan Yong
** date： 5/13/2015 11:21:04 AM
** desc： 

在上次安排完相亲之后又过了挺长时间，大家好像都差不多见过面了。不过相亲这个事不是说那么容易的，所以Nettle的姑姑打算收集一下之前的情况并再安排一次相亲。所以现在摆在Nettle面前的有2个问题：

1.姑姑想要了解之前所有相亲的情况。对于任一个一次相亲，只要跟参与相亲的两人交流就可以得到这次相亲的情况。如果一个人参加了多次相亲，那么跟他交流就可以知道这几次相亲的情况。那么问题来了，挖掘技术到底哪家强姑姑最少需要跟多少人进行交流可以了解到所有相亲的情况。

问题1解答

2.因为春节快要结束了，姑姑打算给这些人再安排一次集体相亲。集体相亲也就是所有人在一起相亲，不再安排一对一对的进行相亲。但是姑姑有个条件，要求所有参与相亲的人之前都没有见过。也就是说在之前的每一次相亲中的两人不会被同时邀请来参加这次集体相亲。那么问题又来了，姑姑最多可以让多少人参与这个集体相亲。

问题2解答

输入

第1行：2个正整数，N,M(N表示点数 2≤N≤1,000，M表示边数1≤M≤5,000)
第2..M+1行：每行两个整数u,v，表示一条无向边(u,v)

输出

第1行：1个整数，表示最小点覆盖数
第2行：1个整数，表示最大独立集数

样例输入
5 4
3 2
1 3
5 4
1 5
样例输出
2
3
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
    class Problem1127
    {
        static int VertexNum;
        static int EdgeNum;
        static List<int>[] Graph;
        static bool[] flag;
        static Dictionary<int, int> dicB = new Dictionary<int, int>();
        static bool[] testedTagArr;
        static int[] match;
        public static void MyMain(string[] args)
        {
            string[] tokens = Console.ReadLine().Split(' ');
            VertexNum = int.Parse(tokens[0]);
            EdgeNum = int.Parse(tokens[1]);
            Graph = new List<int>[VertexNum];
            flag = new bool[VertexNum];
            match = new int[VertexNum];
            for (int i = 0; i < VertexNum; i++)
            {
                Graph[i] = new List<int>();
                flag[i] = false;
                match[i] = -1;
            }
            for (int i = 0; i < EdgeNum; i++)
            {
                string[] nodes = Console.ReadLine().Split(' ');
                int head = int.Parse(nodes[0]) - 1;
                int tail = int.Parse(nodes[1]) - 1;
                Graph[head].Add(tail);
                Graph[tail].Add(head);
            }

            testedTagArr = new bool[VertexNum];
            int count = 0;
            for (int i = 0; i < VertexNum; i++)
            {
                if (match[i]==-1)
                {
                    Array.Clear(testedTagArr, 0, testedTagArr.Length);

                    if (FindMatch(i))
                    {
                        count++;
                    }
                }
               
            }

            Console.WriteLine(count);
            Console.WriteLine(VertexNum-count);
        }

        private static bool FindMatch(int v)
        {
            foreach (var item in Graph[v])
            {
                if (!testedTagArr[item])
                {
                    testedTagArr[item] = true;
                    if (match[item] == -1 || FindMatch(match[item]))
                    {
                        match[item] = v;
                        match[v] = item;
                        
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
