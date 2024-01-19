/**
 * @file	SubCode_001_01.cpp
 * @brief	Sample file to use doxygen.
 * @author	CountrySideEngineer
 * @copyright	Copyright 2024 CountrySideEngineer. All right reserved.
 *				This program is released under MIT license.
 */
#include <iostream>
#include <Windows.h>
#include <tchar.h>
#include "SubCode_002_01.h"

extern int SampleGlobalValue;

int SubFunction_001_01(int input1, int input2);
void SubFunction_001_02(void);
int SubFunction_001_03(int input_001_002);

/**
 * @fn	SubFunction_001_01
 * @brief	Sample function No.001.
 * 
 * @param	input1	Input value 1.
 * @param	input2	Input value 2.
 * @retval	This function returns the reuslt of add.
 */
int SubFunction_001_01(int input1, int input2)
{
	SampleGlobalValue = (input1 + input2);

	return (input1 + input2);
}

/**
 * @fn	SubFunction_001_02
 * @brief	Sample function which has no argument and returns no value.
 */
void SubFunction_001_02(void)
{
	_tprintf(_T("This is sample functino No. 2."));
	return;
}

/**
 * @fn	SubFunction_001_03
 * @brief	Sample function which has an argument.
 * 
 * @param	input_001_002	Input value to this method.
 * @retval	
 */
int SubFunction_001_03(int input_001_002)
{
	int res = SubFunction_002_01(input_001_002);

	return res;
}

int SubFunction_001_04(int input2, int input1)
{
	return (input1 + input2);
}
