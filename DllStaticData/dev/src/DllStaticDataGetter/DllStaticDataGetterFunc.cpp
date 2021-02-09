#include "pch.h"
#include "DllStatic.h"

VOID GetStaticData()
{
	BYTE buffer[100] = { 0 };
	BYTE* dstBuffer = GetStaticBuffer(buffer, 100);
	
	_tprintf(_T("SHOW STATIC BUFFER :\n"));
	for (int index = 0; index < 100; index++) {
		_tprintf(_T("0x%02X"), dstBuffer[index]);
		if ((0 != index) && (0 == ((index + 1) % 16))) {
			_tprintf(_T("\n"));
		}
		else {
			_tprintf(_T(", "));
		}
	}
	_tprintf(_T("\n"));
}

VOID DllStaticDataGetterFunc()
{
	for (int index = 0; index < 100; index++) {
		GetStaticData();
		Sleep(500);
	}
	FinalizeDll();
}
