#include <iostream>
#include <Windows.h>
#include <tchar.h>

SHORT Sample_Max(SHORT input1, SHORT input2)
{
	SHORT max = 0;
	if (input1 < input2) {
		max = input2;
	}
	else {
		max = input1;
	}

	return max;
}

SHORT Sample_Pow(SHORT input1)
{
	SHORT pow = input1 * input1;

	return pow;
}
