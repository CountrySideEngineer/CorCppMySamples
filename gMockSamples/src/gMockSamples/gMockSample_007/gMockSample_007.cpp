#include <gmock/gmock.h>
#include <gtest/gtest.h>
#include <memory>

using ::testing::_;
using ::testing::DoAll;
using ::testing::SetArgPointee;

// ==== インターフェース ====
class IAllocator {
public:
	virtual ~IAllocator() = default;
	virtual void AllocateBuffer(int** out_ptr) = 0;
};

// ==== モック ====
class MockAllocator : public IAllocator {
public:
	MOCK_METHOD(void, AllocateBuffer, (int** out_ptr), (override));
};

// ==== テスト対象クラス ====
class BufferUser {
public:
	BufferUser(IAllocator& alloc) : alloc_(alloc) {}
	int UseBuffer()
	{
		int* buffer = nullptr;
		alloc_.AllocateBuffer(&buffer); // モックがここでセット
		if (!buffer) return -1;
		int sum = 0;
		for (int i = 0; i < 3; ++i) {
			sum += buffer[i];
		}
		return sum;
	}
private:
	IAllocator& alloc_;
};

// ==== テスト ====
TEST(BufferUserTest, DoublePointerArgument)
{
	MockAllocator mock;

	// モックが返す配列データ（静的領域でOK）
	static int test_data[3] = { 1, 2, 3 };

	// out_ptr（int**）の指す先に test_data のアドレスをセット
	EXPECT_CALL(mock, AllocateBuffer(_))
		.WillOnce(DoAll(
			SetArgPointee<0>(test_data)  // 0番目の引数（int** out_ptr）に test_data のアドレスを代入
		));

	BufferUser user(mock);
	EXPECT_EQ(user.UseBuffer(), 6); // 1+2+3=6
}
