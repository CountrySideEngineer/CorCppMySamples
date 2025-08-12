#include <gmock/gmock.h>
#include <gtest/gtest.h>
using ::testing::_;
using ::testing::Return;
using ::testing::Sequence;

// インターフェース
class ICalculator {
public:
	virtual ~ICalculator() = default;
	virtual int Add(int a, int b) = 0;
};

// モッククラス
class MockCalculator : public ICalculator {
public:
	MOCK_METHOD(int, Add, (int a, int b), (override));
};

// テスト対象
class MathClient {
public:
	MathClient(ICalculator& calc) : calc_(calc) {}

	int Run()
	{
		int r1 = calc_.Add(1, 2);  // 1回目
		int r2 = calc_.Add(5, 7);  // 2回目
		return r1 + r2;
	}

private:
	ICalculator& calc_;
};

// テスト
TEST(MathClientTest, MultipleCallsWithDifferentArgs)
{
	MockCalculator mock;
	Sequence s; // 呼び出し順を保証するためのシーケンス

	EXPECT_CALL(mock, Add(1, 2))
		.InSequence(s)
		.WillOnce(Return(3));

	EXPECT_CALL(mock, Add(5, 7))
		.InSequence(s)
		.WillOnce(Return(12));

	MathClient client(mock);
	EXPECT_EQ(client.Run(), 15); // 3 + 12 = 15
}
