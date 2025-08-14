#include <gtest/gtest.h>
#include <gmock/gmock.h>

using ::testing::_;
using ::testing::Sequence;
using ::testing::SetArgPointee;
using ::testing::DoAll;

// インターフェース
class IDataProvider {
public:
	virtual ~IDataProvider() = default;
	virtual bool GetValue(int* out) = 0;
};

// モッククラス
class MockDataProvider : public IDataProvider {
public:
	MOCK_METHOD(bool, GetValue, (int* out), (override));
};

// テスト対象
class Processor {
public:
	Processor(IDataProvider& provider) : provider_(provider) {}

	int Process()
	{
		int value = 0;
		int value1 = 0;
		int value2 = 0;
		if (provider_.GetValue(&value1)) {
			value += (value1 * 2); // 取得値を2倍して返す
		}
		if (provider_.GetValue(&value2)) {
			value += (value2 * 2); // 取得値を2倍して返す
		}

		return value;
	}

private:
	IDataProvider& provider_;
};

// テスト
TEST(ProcessorTest, ReturnsDoubleOfProvidedValue)
{
	MockDataProvider mock;
	Sequence seq;

	// GetValue が複数回呼び出された場合の動作をセット
	// GetValue の呼び出し時、引数(out)に 1 をセットし、true を返す
	EXPECT_CALL(mock, GetValue(_))
		.Times(2)
		.WillRepeatedly(DoAll(SetArgPointee<0>(1), testing::Return(true)));

	Processor proc(mock);
	EXPECT_EQ(proc.Process(), 4);
}
