#include <iostream>
#include <Windows.h>
#include <tchar.h>

#define BLOCK_NUM       (100)           //100block
#define DATA_SIZE       (BLOCK_NUM)     //Sample data size.

LONG    AppearTable[BLOCK_NUM] = { false };

BOOL HasDisappear()
{
    BOOL hasDisappear = FALSE;

    for (LONG index = 0; index < DATA_SIZE; index++) {
        if (0 == AppearTable[index]) {
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
        (AppearTable[index])++;
        if (1 < AppearTable[index]) {
            _tprintf(_T("DUPLICATED : %5d\n"), index);
            continue;
        }

        if (!HasDisappear()) {
            break;
        }
        loopCount++;
    } while (1);

    _tprintf(_T("LOOP COUNT = %5d\n"), loopCount);

    for (int index = 0; index < DATA_SIZE; index++) {
        if (0 == (index % 16)) {
            if (0 != index) {
                _tprintf(_T("\n"));
            }
            _tprintf(_T("%8d : "), index);
        }
        _tprintf(_T("0x%02X "), AppearTable[index]);
    }
    _tprintf(_T("\n"));

    std::cout << "Hello World!\n";
}
