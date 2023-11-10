#include <iostream>
#include <Windows.h>
#include <tchar.h>

#define BLOCK_SIZE      (128)       //128byte
#define BLOCK_NUM       (100)       //100block
#define DATA_SIZE       (BLOCK_SIZE * BLOCK_NUM)    //Sample data size.

BOOL    AppearTable[DATA_SIZE] = { false };

BOOL HasDisappear()
{
    BOOL hasDisappear = FALSE;

    for (LONG index = 0; index < DATA_SIZE; index++) {
        if (!AppearTable[index]) {
            hasDisappear = TRUE;
            break;
        }
    }
    return hasDisappear;
}

int main()
{
    ULONG loopCount = 0;

    srand((unsigned int)time(NULL));

    do {
        LONG index = rand() % DATA_SIZE;
        if (AppearTable[index]) {
            _tprintf(_T("DUPLICATED : %5d\n"), index);
            continue;
        }
        AppearTable[index] = TRUE;

        if (!HasDisappear()) {
            break;
        }
        loopCount++;
    } while (1);

    _tprintf(_T("LOOP COUNT = %5d\n"), loopCount);

    for (int index = 0; index < DATA_SIZE; index++) {
        if ((0 != index) && (0 == (index % 16)))
        {
            _tprintf(_T("\n%5d : "), index);
        }
        _tprintf(_T("%s "), AppearTable[index] ? _T("TRUE ") : _T("FALSE"));
    }
    _tprintf(_T("\n"));

    std::cout << "Hello World!\n";
}
