#include <iostream>
#include <Windows.h>
#include <tchar.h>
#include "gtest/gtest.h"

SHORT Sample_Pow(SHORT input1);

typedef struct _POW_TEST_PARAM {
	SHORT	input1;
	SHORT	expect;
} POW_TEST_PARAM;

POW_TEST_PARAM	testParams[] = {
	{  1, 1 },
	{  2, 4 },
	{ -1, 1 },
	{ -2, 4 },
};

class Sample_PowTest : public ::testing::TestWithParam<POW_TEST_PARAM> {
public:

};

TEST_P(Sample_PowTest, PositiveTest)
{
	ASSERT_EQ(GetParam().expect, Sample_Pow(GetParam().input1));
}

INSTANTIATE_TEST_SUITE_P(
	Sample_PowTestTes,
	Sample_PowTest,
	::testing::ValuesIn(testParams));
