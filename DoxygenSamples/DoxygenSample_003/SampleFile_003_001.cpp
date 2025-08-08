#include <iostream>
#include <Windows.h>

typedef struct _SAMPLE_STRUCT_003_001_T {
	int		member_003_001_001;
	short	member_003_001_002;
	long	member_003_001_003;
} SAMPLE_STRUCT_003_001;

typedef struct _SAMPLE_STRUCT_003_002_T {
	int						member_003_002_001;
	short					member_003_002_002;
	long					member_003_002_003;
	SAMPLE_STRUCT_003_001	member_003_002_004;
} SAMPLE_STRUCT_003_002;

void StructCode_003_001(SAMPLE_STRUCT_003_001* sample)
{
	sample->member_003_001_001 = 0;
	sample->member_003_001_002 = 0;
	sample->member_003_001_003 = 0;
}

void StructCode_003_002(SAMPLE_STRUCT_003_002* sample)
{
	sample->member_003_002_001 = 0;
	sample->member_003_002_002 = 0;
	sample->member_003_002_003 = 0;

	StructCode_003_001(&(sample->member_003_002_004));
}
