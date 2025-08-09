#include <gtest/gtest.h>
#include <gmock/gmock.h>

using ::testing::_;
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
		if (provider_.GetValue(&value)) {
			return value * 2; // 取得値を2倍して返す
		}
		return -1;
	}

private:
	IDataProvider& provider_;
};

// テスト
TEST(ProcessorTest, ReturnsDoubleOfProvidedValue)
{
	MockDataProvider mock;

	// GetValue の呼び出し時、引数(out)に 42 をセットし、true を返す
	EXPECT_CALL(mock, GetValue(_))
		.WillOnce(DoAll(SetArgPointee<0>(42), testing::Return(true)));

	Processor proc(mock);
	EXPECT_EQ(proc.Process(), 84);
}
