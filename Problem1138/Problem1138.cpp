#pragma warning(disable:4996)
#include <iostream>
#include <vector>
#include <map>
#include <algorithm>
#include <queue>
using namespace std;

struct Point
{
	int x;	//x×ø±ê
	int y;	//y×ø±ê
	int No;	//±àºÅ
	friend bool operator <(const Point &a, const Point &b)
	{
		return a.x < b.x;
	}
};
vector<map<int, int>> graph;
vector<Point> points;
vector<int> distancefromfirst;
vector<int> path;
int pathweight[100000];
int N;
void buildgraph()
{
	sort(points.begin(), points.end());
	int n = points.size();
	for (size_t i = 0; i < N; i++)
	{
		int next = i + 1;
		while (next < N&&points[next].x == points[i].x)
		{
			int a = points[next].No, b = points[i].No;
			graph[a][b] = 0;
			graph[b][a] = 0;
			next++;
		}
		if (i>0)
		{
			int a = points[i - 1].No, b = points[i].No;
			int value = points[i].x - points[i - 1].x;
			if (graph[a].count(b) == 0)
			{
				graph[a][b] = value;
			}
			else
			{
				graph[a][b] = graph[a][b] > value ? value : graph[a][b];
			}
			if (graph[b].count(a) == 0)
			{
				graph[b][a] = value;
			}
			else
			{
				graph[b][a] = graph[b][a] > value ? value : graph[b][a];
			}
		}
		i = next - 1;
	}
}

void spfa()
{
	queue<int> q;
	q.push(0);
	vector<bool> inque(N, false);
	distancefromfirst = vector<int>(N, 2147483647);
	path = vector<int>(N, 0);
	distancefromfirst[0] = 0;
	while (!q.empty()) {
		int idx = q.front();
		q.pop();
		for (map<int, int>::iterator it = graph[idx].begin(); it != graph[idx].end(); it++)
		{
			int No = it->first, value = it->second;
			if (distancefromfirst[idx] + value < distancefromfirst[No])
			{
				distancefromfirst[No] = distancefromfirst[idx] + value;
				//cout << "set " << No << " to " << distancefromfirst[idx] + value;
				path[No] = idx;
				pathweight[No] = value;
				if (!inque[No])
				{
					inque[No] = true;
					q.push(No);
				}
				//cout << "qsize: " << q.size() << endl;
			}
		}
		inque[idx] = false;
	}
}
int main()
{
	//freopen("Input.txt", "r", stdin);
	cin >> N;
	//srand((unsigned)time(0));
	srand(8);
	points = vector<Point>(N);
	graph = vector<map<int, int>>(N);
	for (size_t i = 0; i < N; i++)
	{
		cin >> points[i].x;
		cin >> points[i].y;
		//points[i].x = rand() * rand() % 1000000000;
		//points[i].y = rand() * rand() % 1000000000;
		points[i].No = i;
	}
	//cout << points[N - 1].y << endl;
	buildgraph();

	for (size_t i = 0; i < points.size(); i++)
	{
		swap(points[i].x, points[i].y);
	}
	buildgraph();

	spfa();
	cout << distancefromfirst[N - 1] << endl;

	//system("pause");

	return 0;
}