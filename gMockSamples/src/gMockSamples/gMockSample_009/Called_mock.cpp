#include <iostream>
#include "gtest/gtest.h"
#include "gmock/gmock.h"
#include "Called_mock.h"

ICalled* calledMock;

int DoWhenCalled(int x)
{
	return calledMock->DoWhenCalled(x);
}