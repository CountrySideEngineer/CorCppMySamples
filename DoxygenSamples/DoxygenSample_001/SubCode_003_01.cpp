/**
 * @file	SubCode_003_01.cpp
 * @brief	Sample file which implements sub functions.
 * @author	CountrySideEngineer
 * @copyright	Copyright 2024 CountrySideEngineer. All right reserved.
 *				This program is released under MIT license.
 */
#include <iostream>
#include <Windows.h>
#include <tchar.h>

 /**
  * @fn	SubFunction_003_01
  * @brief	Sample function No.002_01
  *
  * @param	input1	Input value 1.
  * @param	input2	Input value 2.
  * @retval	Returns pow of input.
  */
int SubFunction_002_01(int* input_003_01)
{
	return ((*input_003_01) * (*input_003_01));
}

/**
 * @fn	SubFunction_003_02
 * @brief	Sample function which has no argument and returns no value.
 */
void SubFunction_003_02(void* input_003_02)
{
	int* _input_003_02 = (int*)input_003_02;

	*_input_003_02 = (*_input_003_02) * 2;
}

/**
 * @fn	SubFunction_003_03
 * @brief	Sample function which has no argument and returns no value.
 */
void SubFunction_003_03(int input_003_02[])
{
	input_003_02[2] = input_003_02[0] + input_003_02[1];
}

/**
 * @fn	SubFunction_003_03
 * @brief	Sample function which has no argument and returns no value.
 */
void SubFunction_003_04(int input_003_04[3])
{
	input_003_04[2] = input_003_04[0] * input_003_04[1];
}
