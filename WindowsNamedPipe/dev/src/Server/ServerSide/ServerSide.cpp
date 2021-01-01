// ServerSide.cpp : このファイルには 'main' 関数が含まれています。プログラム実行の開始と終了がそこで行われます。
//

#include "pch.h"
#include "framework.h"
#include "ServerSide.h"

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
            HANDLE hPipe = INVALID_HANDLE_VALUE;
            hPipe = CreateNamedPipe(_T("\\\\.\\pipe\\sample_pipe"),
                PIPE_ACCESS_INBOUND,
                PIPE_TYPE_BYTE | PIPE_WAIT,
                1,
                0,
                0,
                100,
                NULL);
            if (INVALID_HANDLE_VALUE == hPipe) {
                _tprintf(_T("The pipe can not open.\r\n"));
            }
            else {
                _tprintf(_T("The pipe can open.\r\n"));
            }
            if (0 == ConnectNamedPipe(hPipe, NULL)) {
                _tprintf(_T("Can not connect named pipe"));
            }
            else {
                _tprintf(_T("Can connect named pipe"));
            }

            SHORT failedCount = 0;
            do {
                _TCHAR buffer[256];
                ZeroMemory(buffer, 256);
                DWORD readDataSize = 0;
                BOOL readRes = ReadFile(hPipe, buffer, sizeof(buffer), (LPDWORD)&readDataSize, NULL);
                if (!readRes) {
                    //_tprintf(_T("Can not read data.\r\n"));
                    failedCount++;
                }
                else {
                    _tprintf(_T("Can read data : \r\n"));
                    _tprintf(_T("Read data size : %d\r\n"), readDataSize);
                    _tprintf(_T("Read data : %s\r\n"), buffer);
                    failedCount = 0;
                }
                Sleep(10);
            } while (failedCount < 2000);   //Max 20 second

            FlushFileBuffers(hPipe);
            DisconnectNamedPipe(hPipe);
            CloseHandle(hPipe);
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
