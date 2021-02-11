#include "pch.h"

VOID ShowBuffer(BYTE* data, int data_size)
{
	for (int index = 0; index < data_size; index++) {
		if ((0 != index) &&
			(0 == index % 16))
		{
			_tprintf(_T("\n"));
		}
		_tprintf(_T("0x%02X "), *(data + index));
	}
	_tprintf(_T("\n"));
}
