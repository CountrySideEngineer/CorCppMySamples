#include "SampleClass_001.h"
#include "SampleClass_002.h"
#include "SampleClass_003.h"

void SampleClass_003::SubFunction_003_01(SampleClass_001 input1)
{
	input1.SetSampleMember_001(10);
}

void SampleClass_003::SubFunction_003_02(SampleClass_002* input1)
{
	input1->SetSampleMember_001(100);
}

void SampleClass_003::SubFunction_003_03(SampleClass_003& input1)
{
	input1.m_value_003_01 = 101;
}
