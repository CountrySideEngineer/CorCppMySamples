#include "pch.h"

static	BYTE	StaticBuffer[100];
BYTE	NoneStaticBuffer[100];
#define	SHARED_MEMORY_SIZE		(100)
_TCHAR* SharedMemoryName = _T("SAMPLE_SHARED_MEMORY_NAME");
BYTE* sharedMemory = NULL;
HANDLE	sharedMemoryHandle = NULL;

VOID InitBuffer()
{
	sharedMemoryHandle = ::CreateFileMapping(
		INVALID_HANDLE_VALUE, 
		NULL, 
		PAGE_READWRITE, 
		0, 
		100, 
		SharedMemoryName);
	if (NULL == sharedMemoryHandle) {
		_tprintf(_T("Can not open shared memory\n"));

		return;
	}
	sharedMemory = (BYTE*)::MapViewOfFile(
		sharedMemoryHandle,
		FILE_MAP_WRITE | FILE_MAP_READ,
		0,
		0,
		SHARED_MEMORY_SIZE);
	if (NULL == sharedMemory) {
		_tprintf(_T("Can not create shared memory.\n"));
	}
	else {
		_tprintf(_T("Create shared memory success.\n"));
		_tprintf(_T("Shared memory address : 0x%p\n"), sharedMemory);
		BYTE* _sharedMemory = sharedMemory;
		for (int index = 0; index < SHARED_MEMORY_SIZE; index++) {
			_sharedMemory[index] = 0x00;
		}
	}
}

VOID WINAPI	FinalizeDll()
{
	if (NULL != sharedMemory) {
		::UnmapViewOfFile(sharedMemory);
	}
	if (NULL != sharedMemoryHandle) {
		::CloseHandle(sharedMemoryHandle);
	}
}

BYTE* WINAPI	SetStaticBuffer(BYTE* buffer, BYTE bufferSize)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	if (NULL == sharedMemory) {
		_tprintf(_T("Shared memory not created"));
	}
	else {
		BYTE* dstMemory = sharedMemory;
		for (BYTE index = 0; index < bufferSize; index++) {
			dstMemory[index] = buffer[index];
		}
	}
	return sharedMemory;
}

BYTE* WINAPI	GetStaticBuffer(BYTE* buffer, BYTE bufferSize)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	if (NULL == sharedMemory) {
		_tprintf(_T("Shared memory not created"));
	}
	else {
		BYTE* srcMemory = sharedMemory;
		for (BYTE index = 0; index < bufferSize; index++) {
			buffer[index] = srcMemory[index];
		}
	}

	return sharedMemory;
}
