// ServerSide.cpp : このファイルには 'main' 関数が含まれています。プログラム実行の開始と終了がそこで行われます。
//
#include <processenv.h>
#include "pch.h"
#include "framework.h"
#include "ServerSide.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// 唯一のアプリケーション オブジェクトです

CWinApp theApp;

using namespace std;

typedef struct _PIPE_HANDLES {
    HANDLE  hPipe;
    HANDLE  hEvent;
} PIPE_HANDLES, * PPIPE_HANDLES;


DWORD   WINAPI  ServerThreadProc(LPVOID threadParam);

int main()
{
    int nRetCode = 0;

    PIPE_HANDLES pipeHandles;
    HMODULE hModule = ::GetModuleHandle(nullptr);

    if (hModule != nullptr)
    {
        // MFC を初期化して、エラーの場合は結果を出力する
        if (!AfxWinInit(hModule, nullptr, ::GetCommandLine(), 0))
        {
            // TODO: アプリケーションの動作を記述するコードをここに挿入してください。
            wprintf(L"致命的なエラー: MFC の初期化が失敗しました\n");
            nRetCode = 1;
        }
        else
        {
            int recvCount = 0;
            while (recvCount < 10) {
                pipeHandles.hPipe = INVALID_HANDLE_VALUE;
                pipeHandles.hPipe = CreateNamedPipe(_T("\\\\.\\pipe\\sample_pipe"),
                    PIPE_ACCESS_DUPLEX,
                    PIPE_TYPE_BYTE | PIPE_WAIT,
                    1,
                    0,
                    0,
                    100,
                    NULL);
                if (INVALID_HANDLE_VALUE == pipeHandles.hPipe) {
                    _tprintf(_T("The pipe can not open.\r\n"));
                }
                else {
                    _tprintf(_T("The pipe can open.\r\n"));
                }
                DWORD threadId = 0;
                pipeHandles.hEvent = CreateEvent(NULL, FALSE, FALSE, _T("IPC_SAMPLE"));
                HANDLE threadHandle = (HANDLE)_beginthreadex(NULL, 0, (_beginthreadex_proc_type)ServerThreadProc, (LPVOID)((PPIPE_HANDLES)(&pipeHandles)), 0, (unsigned int*)&threadId);
                if (NULL == threadId) {
                    _tprintf(_T("Thread can not create.\r\n"));
                    return (-1);
                }
                else {
                    _tprintf(_T("Thread created.\r\n"));
                }
                WaitForSingleObject(threadHandle, INFINITE);
                if (CloseHandle(threadHandle)) {
                    _tprintf(_T("Thread (and its handle) closed.\r\n"));
                }
                _tprintf(_T("Program end.\r\n"));
                Sleep(100);
                recvCount++;
            }
        }
    }
    else
    {
        // TODO: 必要に応じてエラー コードを変更してください
        wprintf(L"致命的なエラー: GetModuleHandle が失敗しました\n");
        nRetCode = 1;
    }

    return nRetCode;
}

DWORD WINAPI    ServerThreadProc(LPVOID threadParam)
{
    BYTE    readBuffer[256];
    BYTE    sendBuffer[256];
    DWORD   readBufferSizeInByte = sizeof(readBuffer);
    ZeroMemory(readBuffer, readBufferSizeInByte);
    ZeroMemory(sendBuffer, sizeof(sendBuffer));

    PPIPE_HANDLES   pipeHandles = (PPIPE_HANDLES)threadParam;
    int loopCount = 0;

    while (1)
    {
        WaitForSingleObject(pipeHandles->hEvent, INFINITE);
        DWORD   readDataSize = 0;
        BOOL readResult = ReadFile(pipeHandles->hPipe, readBuffer, readBufferSizeInByte, (LPDWORD)&readDataSize, NULL);
        ResetEvent(pipeHandles->hEvent);
        if (FALSE == readResult) {
            _tprintf(_T("Can not read data.\r\n"));
            break;
        }

        //Copy read data into a buffer to keep send(response) data.
        for (int index = 0; index < readDataSize; index++) {
            sendBuffer[index] = readBuffer[index];
        }

        //Update response data.
        LPWORD bufferToSend = (LPWORD)sendBuffer;
        (*bufferToSend)++;

        //Sleep(10);

        SetEvent(pipeHandles->hEvent);
        DWORD writtenDataSize = 0;
        BOOL writeResult = WriteFile(pipeHandles->hPipe, sendBuffer, readDataSize, (LPDWORD)&writtenDataSize, NULL);

        //Show received and sent data in buffers.
        _tprintf(_T("Rcv : "));
        for (int index = 0; index < readDataSize; index++) {
            _tprintf(_T("0x%02X "), readBuffer[index]);
        }
        _tprintf(_T("\r\n"));
        _tprintf(_T("Snd : "));
        for (int index = 0; index < writtenDataSize; index++) {
            _tprintf(_T("0x%02X "), sendBuffer[index]);
        }
        _tprintf(_T("\r\n"));

        WORD topData = (WORD)(*(LPBYTE)(&readBuffer[0]));
        if (0 == topData) {
            _tprintf(_T("End data received.(0x%04x)\r\n"), topData);
            break;
        }
    }

    _tprintf(_T("Exit loop\r\n"));
    SetEvent(pipeHandles->hEvent);

    if (CloseHandle(pipeHandles->hPipe)) {
        _tprintf(_T("Pipe close.\r\n"));
        _tprintf(_T("Exit loop thread.\r\n"));
    }
    return 0;
}