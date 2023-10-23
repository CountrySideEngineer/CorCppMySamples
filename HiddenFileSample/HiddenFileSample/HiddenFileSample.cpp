#include <iostream>
#include <Windows.h>
#include <tchar.h>
#include <io.h>

int main()
{
    BYTE    byteData[256];
    for (int index = 0; index < 256; index++) {
        byteData[index] = (BYTE)index;
    }

    CONST TCHAR* hiddenFileName = _T("hidden_file.bin");

    TCHAR* currentDir = _tgetcwd(NULL, 0);
    TCHAR   hiddenFilePathBuffer[512] = { 0 };

    _stprintf_s(hiddenFilePathBuffer, _T("%s\\%s"), currentDir, hiddenFileName);

    _tprintf(_T("     CURRENT DIR = %s\n"), currentDir);
    _tprintf(_T("HIDDEN FILE PATH = %s\n"), hiddenFilePathBuffer);

    //Write data to hidden file.
    HANDLE hiddenFile = CreateFile(
        hiddenFilePathBuffer,
        GENERIC_WRITE | GENERIC_READ,
        FILE_SHARE_READ,
        NULL,
        CREATE_NEW,
        FILE_ATTRIBUTE_HIDDEN,
        NULL);
    DWORD writtenByteSize = 0;
    WriteFile(hiddenFile, byteData, 256, &writtenByteSize, 0);
    CloseHandle(hiddenFile);

    BYTE byteDataToRead[256] = { 0 };
    hiddenFile = CreateFile(
        hiddenFilePathBuffer,
        GENERIC_READ,
        0,
        NULL,
        OPEN_EXISTING,
        FILE_ATTRIBUTE_HIDDEN,
        NULL);
    DWORD readByteSize = 0;
    (VOID)ReadFile(hiddenFile, byteDataToRead, 256, &readByteSize, 0);
    CloseHandle(hiddenFile);

    _tprintf(_T("Read data size = %d bytes.\n"), readByteSize);
    for (DWORD index = 0; index < readByteSize; index++) {
        if ((0 != index) && (0 == index % 16)) {
            _tprintf(_T("\n"));
        }
        _tprintf(_T("0x%02X "), byteDataToRead[index]);
    }

    for (DWORD index = 0; index < readByteSize; index++) {
        if (byteData[index] != byteDataToRead[index]) {
            _tprintf(_T("index = %3d - NG"), index);
        }
    }
}
