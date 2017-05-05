package android.serialport;

import java.io.File;
import java.io.FileDescriptor;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

import android.util.Log;

public class SerialPort {

	private static final String TAG = "SerialPort";

	private FileDescriptor mFd;
	private FileInputStream mFileInputStream;
	private FileOutputStream mFileOutputStream;

	public SerialPort()  {
	
	}

	public SerialPort(File device, int baudrate, int flags) throws SecurityException, IOException {
        
		mFd = open(device.getAbsolutePath(), baudrate, flags);
		if (mFd == null) {
			Log.e(TAG, "native open returns null");
			throw new IOException();
		}
		mFileInputStream = new FileInputStream(mFd);
		mFileOutputStream = new FileOutputStream(mFd);
	}

	// Getters and setters
	public InputStream getInputStream() {
		return mFileInputStream;
	}

	public OutputStream getOutputStream() {
		return mFileOutputStream;
	}

	// JNI
	private native static FileDescriptor open(String path, int baudrate, int flags);
	public native void close();
	public native int setScanTriggerGPIO(int pin_val);
	public native int setScanWakeupGPIO(int pin_val);
	public native int setScanPowerGPIO(int pin_val);
	//
	public native int getReadGoodGPIO(int pin_val);
	static 
	{
		try 
		{
			Log.i("JNI","Trying to load serial_port.so");
			System.loadLibrary("serial_port");
		} 
		catch (UnsatisfiedLinkError ule) 
		{
			System.out.println("Got expected ULE");
			Log.d("JNI","WARNING: Could not load serial_port.so");
		}
			
	}
}
