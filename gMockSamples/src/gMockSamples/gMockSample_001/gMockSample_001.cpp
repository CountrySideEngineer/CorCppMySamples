#include <gtest/gtest.h>
#include <gmock/gmock.h>

// 表示用インターフェース
class IDisplay {
public:
	virtual ~IDisplay() = default;
	virtual void ShowResult(int value) = 0;
};

// モッククラス
class MockDisplay : public IDisplay {
public:
	MOCK_METHOD(void, ShowResult, (int value), (override));
};

// テスト対象クラス
class Calculator {
public:
	Calculator(IDisplay& display) : display_(display) {}

	void AddAndShow(int a, int b)
	{
		int result = a + b;
		display_.ShowResult(result);
	}

private:
	IDisplay& display_;
};

// テストケース
TEST(CalculatorTest, CallsShowResultWithSum)
{
	MockDisplay mock;

	// 期待設定: ShowResult(3) が1回だけ呼ばれる
	EXPECT_CALL(mock, ShowResult(3))
		.Times(1);

	Calculator calc(mock);
	calc.AddAndShow(1, 2);
}
