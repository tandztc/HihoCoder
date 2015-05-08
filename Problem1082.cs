/********************************************************************************
** auth： Tan Yong
** date： 5/8/2015 2:48:52 PM
** desc： fjxmlhx每天都在被沼跃鱼刷屏，因此他急切的找到了你希望你写一个程序屏蔽所有句子中的沼跃鱼(“marshtomp”，不区分大小写)。为了使句子不缺少成分，统一换成 “fjxmlhx” 。

输入
输入包括多行。

每行是一个字符串，长度不超过200。

一行的末尾与下一行的开头没有关系。

输出
输出包含多行，为输入按照描述中变换的结果。

样例输入
The Marshtomp has seen it all before.
marshTomp is beaten by fjxmlhx!
AmarshtompB
样例输出
The fjxmlhx has seen it all before.
fjxmlhx is beaten by fjxmlhx!
AfjxmlhxB
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
    class Problem1082
    {
        public static void MyMain(string[] args)
        {
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                string result = MyReplace(line, "Marshtomp", "fjxmlhx");
                Console.WriteLine(result);
            }
        }

        private static string MyReplace(string source, string oldValue, string newValue)
        {
            int IncreaseNum = newValue.Length - oldValue.Length > 0 ? newValue.Length - oldValue.Length : 0;
            IncreaseNum *= source.Length / oldValue.Length;
            char[] chars = new char[IncreaseNum + source.Length];
            string upperSource = source.ToUpper();
            string upperOldValue = oldValue.ToUpper();
            int curpos = 0;
            int nexpos = 0;
            int curnewindex = 0;
            while (true)
            {
                nexpos = upperSource.IndexOf(upperOldValue, curpos);
                if (nexpos < 0)
                {
                    break;
                }
                for (int i = curpos; i < nexpos; i++)
                {
                    chars[curnewindex++] = source[i];
                }
                for (int i = 0; i < newValue.Length; i++)
                {
                    chars[curnewindex++] = newValue[i];
                }
                curpos = nexpos + oldValue.Length;
            }
            if (curpos == 0)
            {
                return source;
            }
            for (int i = curpos; i < source.Length; i++)
            {
                chars[curnewindex++] = source[i];
            }
            return new string(chars, 0, curnewindex);
        }
    }
}
