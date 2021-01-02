// ClientSide.cpp : このファイルには 'main' 関数が含まれています。プログラム実行の開始と終了がそこで行われます。
//

#include "pch.h"
#include "framework.h"
#include "ClientSide.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// 唯一のアプリケーション オブジェクトです

CWinApp theApp;

using namespace std;

int main()
{
     int nRetCode = 0;

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
            HANDLE hEvent = OpenEvent(EVENT_ALL_ACCESS, FALSE, _T("IPC_SAMPLE"));
            if (NULL == hEvent) {
                _tprintf(_T("Error event open.\r\n"));
                return (-1);
            }
            HANDLE hPipe = CreateFile(_T("\\\\.\\pipe\\sample_pipe"),
                GENERIC_WRITE | GENERIC_READ,
                0, 
                NULL, 
                OPEN_EXISTING, 
                FILE_ATTRIBUTE_NORMAL, 
                NULL);
            if (INVALID_HANDLE_VALUE == hPipe) {
                 _tprintf(_T("Can not create file, as PIPE\r\n"));
            }
            else {
                _tprintf(_T("Can create file, as PIPE\r\n"));
                int loopCount = 0;
                int maxLoopNum = 200;

                while (1) {
                    BYTE sendBuffer[256];
                    ZeroMemory(sendBuffer, sizeof(sendBuffer));

                    BOOL isExitFlag = FALSE;

                    //Set data to send.
                    LPWORD bufferToSet = (LPWORD)sendBuffer;
                    if (loopCount < maxLoopNum) {
                        *bufferToSet = (WORD)((loopCount * 2) + 1);
                        isExitFlag = FALSE;
                    }
                    else {
                        *bufferToSet = (WORD)0;
                        isExitFlag = true;
                    }
                    bufferToSet++;
                    *bufferToSet = (WORD)0xFFFF;
                    bufferToSet++;
                    *bufferToSet = (WORD)0xFFFF;

                    DWORD writtenByteSize = 0;
                    DWORD numberOfByteToWrite = 6;
                    SetEvent(hEvent);
                    BOOL writeRes = WriteFile(hPipe, sendBuffer, numberOfByteToWrite, (LPDWORD)&writtenByteSize, NULL);
                    if (!writeRes) {
                        _tprintf(_T("Can not write data into file.\r\n"));
                    }
                    WaitForSingleObject(hEvent, INFINITE);
                    BYTE recvBuffer[256] = { 0 };
                    DWORD readDataSize = 0;
                    BOOL readRes = ReadFile(hPipe, recvBuffer, sizeof(recvBuffer), (LPDWORD)&readDataSize, NULL);
                    ResetEvent(hEvent);
                    if (FALSE == readRes) {
                        _tprintf(_T("Can not receive da ta.\r\n"));
                        break;
                    }
                    else {
                        //Show sent and received data buffers.
                        _tprintf(_T("Snd : "));
                        for (int index = 0; index < writtenByteSize; index++) {
                            _tprintf(_T("0x%02X "), sendBuffer[index]);
                        }
                        _tprintf(_T("\r\n"));
                        _tprintf(_T("Rcv : "));
                        for (int index = 0; index < readDataSize; index++) {
                            _tprintf(_T("0x%02X "), recvBuffer[index]);
                        }
                        _tprintf(_T("\r\n"));
                    }

                    if (FALSE != isExitFlag) {
                        break;
                    }

                    Sleep(1);
                    loopCount++;
                }

                CloseHandle(hPipe);
                _tprintf(_T("Wait for event.\r\n"));
                WaitForSingleObject(hEvent, INFINITE);

                _tprintf(_T("Event is activated.\r\n"));
            }
            CloseHandle(hEvent);
            // TODO: アプリケーションの動作を記述するコードをここに挿入してください。
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
