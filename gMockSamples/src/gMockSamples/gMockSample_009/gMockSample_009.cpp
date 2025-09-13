#include <iostream>
#include "gtest/gtest.h"
#include "gmock/gmock.h"
#include "Called_mock.h"

//テスト対象関数
int Caller(int x);

TEST(CalledTest, mockTest_001)
{
	int x = 1;

	Called mock;
	calledMock = &mock;

	EXPECT_CALL(mock, DoWhenCalled(1))
		.WillOnce(testing::Return(2));

	int ret_val = Caller(x);

	ASSERT_EQ(2, ret_val);
}
