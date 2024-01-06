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

int SubFunction_001(int input1, int input2);

/**
 * @fn	main
 * @brief	Program entry point.
 */
int main()
{
	int input1 = 1;
	int input2 = 2;

	int result = SubFunction_001(input1, input2);

	_tprintf(_T("Result of SubFunction_001 is %d\n"), result);

	return 0;
}
