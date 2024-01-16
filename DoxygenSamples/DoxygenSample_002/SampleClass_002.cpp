#include <iostream>
#include <Windows.h>
#include "SampleClass_002.h"

/**
 * @fn	SubFunction_001_01
 * @brief	Sample function No.001.
 *
 * @param	input1	Input value 1.
 * @param	input2	Input value 2.
 * @retval	This function returns the reuslt of add.
 */
int SampleClass_002::SubFunction_001_01(int input1, int input2)
{
	int baseRet = SampleClass_001::SubFunction_001_01(input1, input2);

	return (baseRet + input1);
}
