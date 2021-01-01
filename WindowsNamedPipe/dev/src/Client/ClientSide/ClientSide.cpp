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
            HANDLE hPipe = CreateFile(_T("\\\\.\\pipe\\sample_pipe"),
                GENERIC_WRITE,
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
                int maxLoopNum = 5;
                do {
                    _TCHAR buffer[256];
                    ZeroMemory(buffer, sizeof(buffer));
                    _stprintf_s(buffer, _T("CLIENT SEND DATA = %d"), loopCount);
                    DWORD writtenWordSize = 0;
                    DWORD numberOfByteToWrite = _tcsclen(buffer) * sizeof(TCHAR);
                    _tprintf(_T("Send data len : %d\r\n"), numberOfByteToWrite);
                    BOOL writeRes = WriteFile(hPipe, buffer, numberOfByteToWrite, (LPDWORD)&writtenWordSize, NULL);
                    if (!writeRes) {
                        _tprintf(_T("Can not write data into file.\r\n"));
                    }
                    else {
                        _tprintf(_T("Can write data into file.(data size = %d)\r\n"), writtenWordSize);
                    }
                    loopCount++;

                    Sleep(100);
                } while (loopCount < maxLoopNum);
            }
            CloseHandle(hPipe);
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
