#include <stdio.h>

int DoWhenCalled(int x);

int Caller(int x)
{
	return DoWhenCalled(x);
}



