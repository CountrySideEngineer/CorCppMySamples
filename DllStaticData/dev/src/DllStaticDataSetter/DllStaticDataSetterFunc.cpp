#include "pch.h"
#include "DllStatic.h"

VOID SetStaticData(int startIndex)
{
	BYTE buffer[100] = { 0 };
	
	for (int index = 0; index < 100; index++) {
		buffer[index] = (startIndex + index);
	}

	SetStaticBuffer(buffer, 100);

	_tprintf(_T("SHOW STATIC BUFFER :\n"));
	for (int index = 0; index < 100; index++) {
		_tprintf(_T("0x%02x"), buffer[index]);
		if ((0 != index) && (0 == ((index + 1) % 16))) {
			_tprintf(_T("\n"));
		}
		else {
			_tprintf(_T(", "));
		}
	}
	_tprintf(_T("\n"));
}

VOID SetNoneStaticData(int startIndex)
{
	BYTE buffer[100] = { 0 };

	for (int index = 0; index < 100; index++) {
		buffer[index] = startIndex + index;
	}

	SetNoneStaticBuffer(buffer, 100);

	_tprintf(_T("SHOW NONE STATIC BUFFER :\n"));
	for (int index = 0; index < 100; index++) {
		_tprintf(_T("0x%02x"), buffer[index]);
		if ((0 != index) && (0 == ((index + 1) % 16))) {
			_tprintf(_T("\n"));
		}
		else {
			_tprintf(_T(", "));
		}
	}
	_tprintf(_T("\n"));
}

VOID DllStaticDataSetterFunc()
{
	for (int index = 0; index < 10; index++) {
		SetStaticData(index);
		Sleep(510);
		SetNoneStaticData(index);
		Sleep(510);
	}
}
