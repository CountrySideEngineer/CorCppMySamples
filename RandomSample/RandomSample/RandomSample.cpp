#include <iostream>
#include <Windows.h>
#include <tchar.h>

#define BLOCK_NUM       (100)           //100block
#define DATA_SIZE       (BLOCK_NUM)     //Sample data size.
#define SAMPLING_NUM    (1000)         //Sampling count

BOOL HasDisappear(LONG appearTable[], LONG tableSize)
{
    BOOL hasDisappear = FALSE;

    for (LONG index = 0; index < tableSize; index++) {
        if (0 == appearTable[index]) {
            hasDisappear = TRUE;
            break;
        }
    }
    return hasDisappear;
}

ULONG Loop()
{
    LONG    appearTable[DATA_SIZE] = { 0 };
    ULONG   loopCount = 0;

    srand((unsigned int)time(NULL));

    do {
        loopCount++;

        ULONG index = ((ULONG)rand()) % DATA_SIZE;
        (appearTable[index])++;
        //if (1 < AppearTable[index]) {
        //    //_tprintf(_T("DUPLICATED : %5d\n"), index);
        //    continue;
        //}

        if (!HasDisappear(appearTable, DATA_SIZE)) {
            break;
        }
        loopCount++;
    } while (1);

#if 0
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
#endif
    return loopCount;
}

int main()
{
    ULONG   loopCounts[SAMPLING_NUM] = { 0 };

    for (LONG index = 0; index < SAMPLING_NUM; index++) {
        loopCounts[index] = Loop();
        _tprintf(_T("%8d - %8d\n"), index, loopCounts[index]);

        Sleep(1000);
    }

    //Output the number of loop in each sampling loop into csv file.
    CONST TCHAR* csvFilePath = _T(".\\data.csv");
    FILE* csvFile = NULL;
    fopen_s(&csvFile, csvFilePath, "w");
    if (NULL == csvFile) {
        return 0;
    }
    for (int index = 0; index < SAMPLING_NUM; index++) {
        _ftprintf_s(csvFile, _T("%d,"), loopCounts[index]);
    }
    fclose(csvFile);
}
