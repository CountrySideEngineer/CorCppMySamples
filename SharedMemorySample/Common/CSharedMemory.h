#pragma once

class CSharedMemory
{
public:
	CSharedMemory(int data_byte_size);
	virtual ~CSharedMemory();
	
	BYTE* Read(BYTE* data, int data_size);
	BYTE* Write(BYTE* data, int data_size);

protected:
	VOID	Initialize(int data_byte_size);

protected:
	HANDLE	shared_memory_handle_;
	BYTE* shared_memory_;

	int shared_memory_size_;
};
