#pragma once
class SampleClass_003
{
public:
	virtual void SubFunction_003_01(SampleClass_001 input1);
	virtual void SubFunction_003_02(SampleClass_002* input1);
	virtual void SubFunction_003_03(SampleClass_003& input1);

protected:
	int m_value_003_01 = 0;
};

