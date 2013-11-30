// WebSpiderService.cpp : Defines the entry point for the console application.
//

#include <Windows.h>
#include "stdafx.h"

#define SVCNAME TEXT("SvcName")

void ServiceMain(int argc, char** argv) {
	return;
}

int _tmain(int argc, _TCHAR* argv[]) {
	SERVICE_TABLE_ENTRY ServiceTable[1];
	ServiceTable[0].lpServiceName = SVCNAME;
	ServiceTable[0].lpServiceProc = (LPSERVICE_MAIN_FUNCTION)ServiceMain;

	return 0;
}

