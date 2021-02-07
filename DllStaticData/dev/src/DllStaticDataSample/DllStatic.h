#pragma once

BYTE*	WINAPI	SetNoneStaticBuffer(BYTE* buffer, BYTE bufferSize);
BYTE*	WINAPI	GetNoneStaticBuffer(BYTE* buffer, BYTE bufferSize);
BYTE*	WINAPI	SetStaticBuffer(BYTE* buffer, BYTE bufferSize);
BYTE*	WINAPI	GetStaticBuffer(BYTE* buffer, BYTE bufferSize);
VOID	WINAPI	FinalizeDll();
