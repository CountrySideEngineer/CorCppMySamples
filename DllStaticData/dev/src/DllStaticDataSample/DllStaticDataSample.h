// DllStaticDataSample.h : DllStaticDataSample DLL のメイン ヘッダー ファイル
//

#pragma once

#ifndef __AFXWIN_H__
	#error "PCH に対してこのファイルをインクルードする前に 'pch.h' をインクルードしてください"
#endif

#include "resource.h"		// メイン シンボル


// CDllStaticDataSampleApp
// このクラスの実装に関しては DllStaticDataSample.cpp をご覧ください
//

class CDllStaticDataSampleApp : public CWinApp
{
public:
	CDllStaticDataSampleApp();

// オーバーライド
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
