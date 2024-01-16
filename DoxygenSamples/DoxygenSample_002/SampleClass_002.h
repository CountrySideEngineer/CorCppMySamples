#pragma once
#include "SampleClass_001.h"

class SampleClass_002 :
    public SampleClass_001
{
public:
	int SubFunction_001_01(int input1, int input2) override;
};

