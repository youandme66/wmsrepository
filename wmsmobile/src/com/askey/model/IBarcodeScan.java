package com.askey.model;

import android.widget.EditText;

public interface IBarcodeScan {
	
	int Open(String path, int baudrate);

	int Scan(boolean synchronous);

	int StopScan();

	int AimOn();

	int AimOff();

	int checkReadgood();

	void getDataRead();

	void Close();

	void onDataReceived(EditText editText, final byte[] buffer, final int size);

	void onDestroy();
}
