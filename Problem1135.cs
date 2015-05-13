/********************************************************************************
** auth： Tan Yong
** date： 5/13/2015 11:43:06 AM
** desc： The circus clown Sunny has a magic box. When the circus is performing, Sunny puts some balls into the box one by one. The balls are in three colors: red(R), yellow(Y) and blue(B). Let Cr, Cy, Cb denote the numbers of red, yellow, blue balls in the box. Whenever the differences among Cr, Cy, Cb happen to be x, y, z, all balls in the box vanish. Given x, y, z and the sequence in which Sunny put the balls, you are to find what is the maximum number of balls in the box ever.

For example, let's assume x=1, y=2, z=3 and the sequence is RRYBRBRYBRY. After Sunny puts the first 7 balls, RRYBRBR, into the box, Cr, Cy, Cb are 4, 1, 2 respectively. The differences are exactly 1, 2, 3. (|Cr-Cy|=3, |Cy-Cb|=1, |Cb-Cr|=2) Then all the 7 balls vanish. Finally there are 4 balls in the box, after Sunny puts the remaining balls. So the box contains 7 balls at most, after Sunny puts the first 7 balls and before they vanish.

输入
Line 1: x y z

Line 2: the sequence consisting of only three characters 'R', 'Y' and 'B'.

For 30% data, the length of the sequence is no more than 200.

For 100% data, the length of the sequence is no more than 20,000, 0 <= x, y, z <= 20.

输出
The maximum number of balls in the box ever.

提示
Another Sample

Sample Input	Sample Output
0 0 0
RBYRRBY            	4





样例输入
1 2 3
RRYBRBRYBRY
样例输出
7
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
    class Problem1135
    {
        static List<int> XYZ = new List<int>();
        public static void MyMain(string[] args)
        {
            string[] tokens = Console.ReadLine().Split(' ');
            for (int i = 0; i < tokens.Length; i++)
            {
                XYZ.Add(int.Parse(tokens[i]));
            }
            XYZ.Sort();
            int Cr = 0, Cy = 0, Cb = 0;
            int maxball = 0, curball = 0;
            string balls = Console.ReadLine();
            foreach (var item in balls)
            {
                curball++;
                maxball = maxball < curball ? curball : maxball;
                switch (item)
                {
                    case 'R':
                        Cr++;
                        break;
                    case 'Y':
                        Cy++;
                        break;
                    case 'B':
                        Cb++;
                        break;
                    default:
                        break;
                }
                if (CanValish(Cr,Cy,Cb))
                {
                    curball = 0;
                    Cr = 0;
                    Cb = 0;
                    Cy = 0;
                }
            }
            Console.WriteLine(maxball);
        }

        private static bool CanValish(int Cr, int Cy, int Cb)
        {
            List<int> _xyz = new List<int>();
            _xyz.Add(Math.Abs(Cr - Cy));
            _xyz.Add(Math.Abs(Cb - Cy));
            _xyz.Add(Math.Abs(Cr - Cb));
            _xyz.Sort();
            bool match = true;
            for (int i = 0; i < 3; i++)
            {
                if (XYZ[i] != _xyz[i])
                {
                    match = false;
                    break;
                }
            }
            return match;
        }
    }
}
