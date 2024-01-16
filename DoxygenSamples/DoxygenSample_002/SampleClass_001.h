#pragma once
class SampleClass_001
{
public:
	virtual int SubFunction_001_01(int input1, int input2);
	virtual void SubFunction_001_02(void);

	virtual int GetSampleMember_001() const
	{
		return m_SampleMember_001;
	}

	virtual void SetSampleMember_001(int value)
	{
		m_SampleMember_001 = value;
	}
		
protected:
	int SubFunction_001_03(int input_001_003);
	int SubFunction_001_04(int input2, int input1);

private:
	int SubFunction_001_05(int input_001_05_01, int input_001_05_02);
	int SubFunction_001_06(int input_001_06_01, int input_001_06_02);
	static int SubFunction_001_07(int input_001_07_01, int input_001_07_02);

private:
	int m_SampleMember_001;
};

