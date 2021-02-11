// SharedMemoryReader.cpp : このファイルには 'main' 関数が含まれています。プログラム実行の開始と終了がそこで行われます。
//

#include "pch.h"
#include "framework.h"
#include "SharedMemoryReader.h"
#include "CSharedMemory.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#define BUFFER_SIZE     (100)

//外部関数
VOID    ShowBuffer(BYTE* data, int data_size);

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
            CSharedMemory shared_memory(BUFFER_SIZE);
            BYTE read_data_buffer[BUFFER_SIZE];

            for (int index = 0; index < 20; index++) {
                Sleep(1000);
                shared_memory.Read(read_data_buffer, BUFFER_SIZE);
                _tprintf(_T("Read data(%5d) :\n"), index);
                ShowBuffer(read_data_buffer, BUFFER_SIZE);
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
