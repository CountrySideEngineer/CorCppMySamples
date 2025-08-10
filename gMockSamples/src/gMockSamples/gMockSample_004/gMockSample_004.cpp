#include <Windows.h>
#include <tchar.h>
#include <gtest/gtest.h>
#include <gmock/gmock.h>
#include <vector>
#include <algorithm>


using ::testing::_;
using ::testing::Invoke;
using ::testing::DoAll;
using ::testing::SaveArg;

class SubFunctionInterface {
public:
	virtual ~SubFunctionInterface() = default;
	virtual void SubFunc(int* ptr, size_t size) = 0;
};

class MockSubFunction : public SubFunctionInterface {
public:
	MOCK_METHOD(void, SubFunc, (int* ptr, size_t size), (override));
};

class TargetClass {
	SubFunctionInterface* subfunc_;
public:
	explicit TargetClass(SubFunctionInterface* subfunc) : subfunc_(subfunc) {}

	void DoSomething(int* ptr, size_t size)
	{
		subfunc_->SubFunc(ptr, size);
	}
};

TEST(GMockPointerArrayCaptureTest, CapturePointerArrayContents)
{
	MockSubFunction mock_subfunc;
	TargetClass target(&mock_subfunc);

	int actual_array[] = { 11, 22, 33, 44 };
	size_t actual_size = sizeof(actual_array) / sizeof(actual_array[0]);

	int* captured_ptr = nullptr;
	size_t captured_size = 0;
	std::vector<int> captured_values;

	EXPECT_CALL(mock_subfunc, SubFunc(_, _))
		.WillOnce(DoAll(
			SaveArg<0>(&captured_ptr),
			SaveArg<1>(&captured_size),
			Invoke([&captured_values](int* ptr, size_t size) {
				captured_values.assign(ptr, ptr + size);
				})
		));

	target.DoSomething(actual_array, actual_size);

	// ポインタの一致確認
	EXPECT_EQ(captured_ptr, actual_array);
	EXPECT_EQ(captured_size, actual_size);

	// 配列の中身確認
	ASSERT_EQ(captured_values.size(), actual_size);
	for (size_t i = 0; i < actual_size; ++i) {
		EXPECT_EQ(captured_values[i], actual_array[i]);
	}
}
