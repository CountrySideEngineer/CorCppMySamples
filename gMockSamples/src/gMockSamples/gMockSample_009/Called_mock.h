#pragma once

class ICalled {
public:
	virtual ~ICalled() = default;

	virtual int DoWhenCalled(int x) = 0;
};

class Called : public ICalled {
public:
	virtual ~Called() = default;

	MOCK_METHOD(int, DoWhenCalled, (int x), (override));
};

extern ICalled* calledMock;