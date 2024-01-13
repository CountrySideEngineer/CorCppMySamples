/**
 * @file	SubCode_002_01.cpp
 * @brief	Sample file which implements sub functions.
 * @author	CountrySideEngineer
 * @copyright	Copyright 2024 CountrySideEngineer. All right reserved.
 *				This program is released under MIT license.
 */
#include <iostream>
#include <Windows.h>
#include <tchar.h>

 /**
  * @fn	SubFunction_002_01
  * @brief	Sample function No.002_01
  *
  * @param	input1	Input value 1.
  * @param	input2	Input value 2.
  * @retval	Returns pow of input.
  */
int SubFunction_002_01(int input_002_01)
{
	return (input_002_01 * input_002_01);
}

/**
 * @fn	SubFunction_002_02
 * @brief	Sample function which has no argument and returns no value.
 */
void SubFunction_002_02(void)
{
	_tprintf(_T("This is sample functino No. 2."));
	return;
}
