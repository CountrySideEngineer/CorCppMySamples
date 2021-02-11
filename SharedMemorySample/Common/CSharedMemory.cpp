#include "pch.h"
#include "CSharedMemory.h"
CString	shared_memory_name = _T("SHARED_MEMORY_NAME");

CSharedMemory::CSharedMemory(int data_byte_size)
	: shared_memory_handle_(NULL)
	, shared_memory_(NULL)
	, shared_memory_size_(data_byte_size)
{
	this->Initialize(this->shared_memory_size_);
}

CSharedMemory::~CSharedMemory()
{
	if (NULL != this->shared_memory_) {
		::UnmapViewOfFile(this->shared_memory_);
		this->shared_memory_ = NULL;
	}
	if (NULL != this->shared_memory_handle_) {
		::CloseHandle(this->shared_memory_handle_);
		this->shared_memory_handle_ = NULL;
	}
}

VOID CSharedMemory::Initialize(int data_byte_size)
{
	HANDLE shared_memory_handle = ::CreateFileMapping(
		INVALID_HANDLE_VALUE,
		NULL,
		PAGE_READWRITE,
		0,
		data_byte_size,
		shared_memory_name);
	if (NULL == shared_memory_handle) {
		_tprintf(_T("Can not create file mapping\n"));

		return;
	}
	BYTE* shared_memory = (BYTE*)::MapViewOfFile(
		shared_memory_handle,
		FILE_MAP_READ | FILE_MAP_WRITE,
		0, 0,
		data_byte_size);
	if (NULL == shared_memory) {
		_tprintf(_T("Can not map file into file.\n"));
		CloseHandle(shared_memory_handle);

		return;
	}

	this->shared_memory_handle_ = shared_memory_handle;
	this->shared_memory_ = shared_memory;
	for (int index = 0; index < this->shared_memory_size_; index++) {
		this->shared_memory_[index] = 0x00;
	}
}

BYTE* CSharedMemory::Read(BYTE* data, const int data_size)
{
	for (int index = 0; index < data_size; index++) {
		*(data + index) = *(this->shared_memory_ + index);
	}
	return this->shared_memory_;
}

BYTE* CSharedMemory::Write(BYTE* data, const int data_size)
{
	for (int index = 0; index < data_size; index++) {
		*(this->shared_memory_ + index) = *(data + index);
	}

	return this->shared_memory_;
}
