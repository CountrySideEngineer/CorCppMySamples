#include <gmock/gmock.h>  // gmock全機能
using ::testing::_;
using ::testing::Return;
using ::testing::SetArrayArgument;
using ::testing::DoAll;

// インターフェース
class IDataProvider {
public:
	virtual ~IDataProvider() = default;
	virtual bool GetValues(int* out, int count) = 0;
};

// モッククラス
class MockDataProvider : public IDataProvider {
public:
	MOCK_METHOD(bool, GetValues, (int* out, int count), (override));
};

// テスト対象クラス
class Processor {
public:
	Processor(IDataProvider& provider) : provider_(provider) {}

	int Process()
	{
		int buffer[3] = { 0 };
		if (provider_.GetValues(buffer, 3)) {
			return buffer[0] + buffer[1] + buffer[2];
		}
		return -1;
	}

private:
	IDataProvider& provider_;
};

// テスト
TEST(ProcessorTest, ReturnsSumOfProvidedArray)
{
	MockDataProvider mock;

	int arr[3] = { 1, 2, 3 };

	// GetValues 呼び出し時、out[0..2] に arr の内容をコピーし、true を返す
	EXPECT_CALL(mock, GetValues(_, 3))
		.WillOnce(DoAll(SetArrayArgument<0>(arr, arr + 3), Return(true)));

	Processor proc(mock);
	EXPECT_EQ(proc.Process(), 6); // 1+2+3=6
}
