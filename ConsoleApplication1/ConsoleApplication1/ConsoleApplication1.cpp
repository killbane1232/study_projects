#include <iostream>
#include <cstdio>
#include <cstring>

#define  _CRT_SECURE_NO_WARNINGS

struct stack {
	int idx = -1;
	char* arr = new char[255];
	int push(char a) {
		idx++;
		arr[idx] = a;
		return 0;
	}
	char pop() {
		if (idx == -1)
			return ' ';
		return arr[idx--];
	}
	char peek() {
		if (idx == -1)
			return ' ';
		return arr[idx];
	}
};

int getPriority(char a) {
	switch (a) {
	case '(':
		return -2;
	case ')':
		return -1;
	case '+':
	case '-':
		return 1;
	case '*':
	case '/':
		return 2;
	default:
		return 5;
	}
}

int main()
{
	char* str = new char[255];
	int a = scanf_s("%s", str);
	int max = 0;
	for (int i = 0; i < 255; i++)
		if ((str[i] < 'a' && str[i] > 'Z' || str[i] > 'z' || str[i] < '0' || str[i] > '9' && str[i] < 'A') && str[i] != '-' && str[i] != '+' && str[i] != '/' && str[i] != '*' && str[i] != '(' && str[i] != ')')
		{
			max = i;
			break;
		}
	int j = 0;
	int k = 0;
	stack stk = stack();
	char* res = (char*)malloc(255 * sizeof(char));
	while (j < max) 
	{
		while (getPriority(str[j]) < getPriority(stk.peek()) && stk.peek() != ' ') {
			char c = stk.pop();
			if (c != '(' && c != ')')
				res[k++] = c;
		}
		if (getPriority(str[j]) == getPriority(stk.peek()))
		{
			if (str[j] != '(' && str[j] != ')')
				res[k++] = str[j];
		}
		else 
		{
			stk.push(str[j]);
		}
		j++;
	}
	while (stk.idx >= 0) {
		char c = stk.pop();
		if (c != '(' && c != ')')
			res[k++] = c;
	}
	for (int l = 0; l < k; l++) {
		printf("%c", res[l]);
	}
	return 0;
}