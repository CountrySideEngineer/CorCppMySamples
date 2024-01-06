/**
 * @file	DoxygenSample_001.cpp
 * @brief	Sample file to use doxygen.
 * @author	CountrySideEngineer
 * @cpoyright	Copyright 2024 CountrySideEngineer. All right reserved.
 * @license	This program is released under MIT license.
 */
#include <iostream>
#include <Windows.h>
#include <tchar.h>

/**
 * @fn	SubFunction_001
 * @brief	Sample function No.001.
 * 
 * @param	input1	Input value 1.
 * @param	input2	Input value 2.
 * @retval	This function returns the reuslt of add.
 */
int SubFunction_001(int input1, int input2)
{
	return (input1 + input2);
}

/**
 * @fn	SubFunction_002
 * @brief	Sample function which has no argument and returns no value.
 */
void SubFunction_002(void)
{
	_tprintf(_T("Thsi is sample functino No. 2."));
	return;
}
