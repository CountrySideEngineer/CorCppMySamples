#include <iostream>
#include <Windows.h>
#include <tchar.h>
#include "SampleClass_001.h"

/**
 * @fn	SubFunction_001_01
 * @brief	Sample function No.001.
 *
 * @param	input1	Input value 1.
 * @param	input2	Input value 2.
 * @retval	This function returns the reuslt of add.
 */
int SampleClass_001::SubFunction_001_01(int input1, int input2)
{
	return (input1 + input2);
}

/**
 * @fn	SubFunction_001_02
 * @brief	Sample function which has no argument and returns no value.
 */
void SampleClass_001::SubFunction_001_02(void)
{
	_tprintf(_T("This is sample functino No. 2."));
	return;
}

/**
 * @fn	SubFunction_001_03
 * @brief	Sample function which has an argument.
 *
 * @param	input_001_003	Input value to this method.
 * @retval
 */
int SampleClass_001::SubFunction_001_03(int input_001_003)
{
	return input_001_003 * 2;
}

/**
 * @fn	SubFunction_001_04
 * @brief	Sample function which has an argument.
 *
 * @param	input2	Input value 1.
 * @param	input1	Input value 2.
 * @retval
 */
int SampleClass_001::SubFunction_001_04(int input2, int input1)
{
	return (input1 + input2);
}

/**
 * @fn	SubFunction_001_05
 * @brief	Sample function which has an argument.
 *
 * @param	input_001_05_01	Input value 1.
 * @param	input_001_05_02	Input value 2.
 * @retval
 */
int SampleClass_001::SubFunction_001_05(int input_001_05_01, int input_001_05_02)
{
	return (input_001_05_01 + input_001_05_02);
}

/**
 * @fn	SubFunction_001_06
 * @brief	Sample function which has an argument.
 *
 * @param	input_001_06_01	Input value 1.
 * @param	input_001_06_02	Input value 2.
 * @retval
 */
int SampleClass_001::SubFunction_001_06(int input_001_06_01, int input_001_06_02)
{
	return (input_001_06_01 + input_001_06_02);
}

/**
 * @fn	SubFunction_001_07
 * @brief	Sample function which has an argument.
 *
 * @param	input_001_07_01	Input value 1.
 * @param	input_001_07_02	Input value 2.
 * @retval
 */
int SampleClass_001::SubFunction_001_07(int input_001_07_01, int input_001_07_02)
{
	return (input_001_07_01 + input_001_07_02);
}
