package com.askey.model;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.lang.reflect.Field;
import java.security.InvalidParameterException;
import java.util.Arrays;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.media.AudioManager;
import android.media.ToneGenerator;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.serialport.SerialPort;
import android.util.Log;
import android.view.View;
import android.view.Window;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.Toast;

import com.askey.wms.R;

public class BaseActivity extends Activity implements IBarcodeScan {

	private static final String TAG = "BaseActivity";

	protected AlertDialog mAlertDialog;
	protected AsyncTask mRunningTask;
	// ///////////////////////////////////////////////////
	public boolean btnOpenFlag = false;
	public boolean btnScanFlag = false;
	public boolean btnAimFlag = false;
	//
	protected SerialPort mSerialPort;
	protected OutputStream mOutputStream;
	private InputStream mInputStream;
	private ReadThread mReadThread;
	boolean mReadThreadflag = true;
	byte[] mBuffer = new byte[1024];
	public static EditText mCommReception = null;
	//
	private static final String BROADCAST_BARCODE_READGOOD = "org.broadcast.BarcodeReadgood";
	final ToneGenerator tg = new ToneGenerator(
			AudioManager.STREAM_NOTIFICATION, 100);

	private ReadgoodReceiver mReceiver;

	/******************************** 【Activity LifeCycle For Debug】 *******************************************/
	public class ReadThread extends Thread {
		@Override
		public void run() {
			super.run();
			int read = 0;
			int size = 0;
			//
			Log.d("SerialPortActivity", "SSSSSPPPPP SerialPortActivity.run()");
			while (mReadThreadflag == true) {
				try {
					Thread.sleep(100);
				} catch (InterruptedException e1) {
				}
				try {
					if (mInputStream == null)
						return;
					read = mInputStream.available();
					if (read == 0) {
						//
					} else {
						size = mInputStream.read(mBuffer);
						if (size > 0) {
							// mScanRet = 0;
							mReadThreadflag = false;
							onDataReceived(mCommReception, mBuffer, size);
							Log.d("SerialPortActivity",
									"SSSSSPPPPP SerialPortActivity.run() size = "
											+ size);
						}
						size = 0;
					}
				} catch (IOException e) {
					e.printStackTrace();
					return;
				}
			}
			Log.d("SerialPortActivity",
					"SSSSSPPPPP SerialPortActivity.run() thread exit");
		}
	}

	public int Open(String path, int baudrate) {
		Log.d("SerialPortActivity",
				"SSSSSPPPPP SerialPortActivity.open() path = " + path
						+ " baudrate = " + baudrate);
		try {
			// mSerialPort = new SerialPort(new File("/dev/ttyHS2"), 9600, 0);
			mSerialPort = new SerialPort(new File(path), baudrate, 0);
			mOutputStream = mSerialPort.getOutputStream();
			mInputStream = mSerialPort.getInputStream();
			mSerialPort.setScanPowerGPIO(1);
			mSerialPort.setScanTriggerGPIO(1);
			//
		} catch (SecurityException e) {
			// DisplayError(R.string.error_security);
			return -1;
		} catch (IOException e) {
			// DisplayError(R.string.error_unknown);
			return -1;
		} catch (InvalidParameterException e) {
			// DisplayError(R.string.error_configuration);
			return -1;
		}
		return 0;
	}

	public int Scan(boolean synchronous) {
		Log.d("SerialPortActivity",
				"SSSSSPPPPP SerialPortActivity.Scan() synchronous = "
						+ synchronous);

		if (mSerialPort != null) {
			Arrays.fill(mBuffer, (byte) 0);
			mSerialPort.setScanTriggerGPIO(0);
		}

		return 0;
	}

	public int StopScan() {
		if (mSerialPort != null) {
			Arrays.fill(mBuffer, (byte) 0);
			mSerialPort.setScanTriggerGPIO(1);
		}
		return 0;
	}

	public int AimOn() {
		if (mSerialPort != null)
			mSerialPort.setScanWakeupGPIO(0);
		return 0;
	}

	public int AimOff() {
		if (mSerialPort != null)
			mSerialPort.setScanWakeupGPIO(1);
		return 0;
	}

	public int checkReadgood() {
		int ret = 2;
		if (mSerialPort != null) {
			ret = mSerialPort.getReadGoodGPIO(0);
			Log.d("SerialPortActivity",
					"SSSSSPPPPP SerialPortActivity.getDataRead() ret = " + ret);
		}
		return ret;
	}

	public void getDataRead() {
		mReadThreadflag = true;
		mReadThread = new ReadThread();
		mReadThread.start();
	}

	public void Close() {
		Log.d("SerialPortActivity", "SSSSSPPPPP SerialPortActivity.close() ");
		if (mSerialPort != null) {
			mSerialPort.setScanTriggerGPIO(1);
			mSerialPort.setScanPowerGPIO(0);
			mSerialPort.setScanWakeupGPIO(1);
			mSerialPort.close();
			mSerialPort = null;
		}
		if (mReadThread != null) {
			mReadThreadflag = false;
			mReadThread = null;
		}
	}

	public void onDataReceived(EditText ed, final byte[] buffer, final int size) {
		runOnUiThread(new Runnable() {
			public void run() {
				Log.d("BarcodeActivity", "BarcodeActivity onDataReceived");
				if (mCommReception != null) {
					// btnScanFlag = false; //20140903, mark here!
					String s = new String(buffer, 0, size);
					s = s.replace("�", "");// 替換亂碼
					//mCommReception.append(s);//內容會累加
					mCommReception.setText(s);//清空後重新獲取內容
					StopScan();
				}
			}
		});
	}

	public class ReadgoodReceiver extends BroadcastReceiver {
		@Override
		public void onReceive(Context context, Intent arg1) {
			// TODO Auto-generated method stub
			int ret;
			Log.d("BarcodeActivity", "BarcodeActivity get Readgood event !! ");
			ret = checkReadgood();
			if (ret == 0) {
				getDataRead();
				// tg.startTone(ToneGenerator.TONE_CDMA_ONE_MIN_BEEP);
				tg.startTone(ToneGenerator.TONE_PROP_PROMPT);//設置掃描時聲音
			}
		}
	}

	public void registerReadgoodReceiver() {
		IntentFilter filter = new IntentFilter(BROADCAST_BARCODE_READGOOD);
		mReceiver = new ReadgoodReceiver();
		registerReceiver(mReceiver, filter);
	}

	// /////////////////////////////////////////////////////////////////////////
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		LogUtil.d(TAG, this.getClass().getSimpleName()
				+ " onCreate() invoked!!");
		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_NO_TITLE);

	}

	@Override
	protected void onStart() {
		LogUtil.d(TAG, this.getClass().getSimpleName() + " onStart() invoked!!");
		super.onStart();
	}

	@Override
	protected void onRestart() {
		LogUtil.d(TAG, this.getClass().getSimpleName()
				+ " onRestart() invoked!!");
		super.onRestart();
	}

	@Override
	protected void onResume() {
		LogUtil.d(TAG, this.getClass().getSimpleName()
				+ " onResume() invoked!!");
		super.onResume();
	}

	@Override
	protected void onPause() {
		LogUtil.d(TAG, this.getClass().getSimpleName() + " onPause() invoked!!");
		super.onPause();
		try {
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	@Override
	protected void onStop() {
		LogUtil.d(TAG, this.getClass().getSimpleName() + " onStop() invoked!!");
		super.onStop();
	}

	@Override
	public void onDestroy() {
		LogUtil.d(TAG, this.getClass().getSimpleName()
				+ " onDestroy() invoked!!");

		if (mRunningTask != null && mRunningTask.isCancelled() == false) {
			mRunningTask.cancel(false);
			mRunningTask = null;
		}
		if (mAlertDialog != null) {
			mAlertDialog.dismiss();
			mAlertDialog = null;
		}
		//
		Log.d("SerialPortActivity", "SSSSSPPPPP SerialPortActivity.onDestroy()");
		if (mReadThread != null)
			mReadThread.interrupt();

		if (mSerialPort != null) {
			mSerialPort.close();
			mSerialPort = null;
		}
		super.onDestroy();
	}

	// get ip address
	public String getIP() {
		// 获取wifi服务
		WifiManager wifiManager = (WifiManager) getSystemService(Context.WIFI_SERVICE);
		// 判断wifi是否开启
		if (!wifiManager.isWifiEnabled()) {
			wifiManager.setWifiEnabled(true);
		}
		WifiInfo wifiInfo = wifiManager.getConnectionInfo();
		int ipAddress = wifiInfo.getIpAddress();
		String ip = intToIp(ipAddress);
		return ip;
	}

	private String intToIp(int i) {
		return (i & 0xFF) + "." + ((i >> 8) & 0xFF) + "." + ((i >> 16) & 0xFF)
				+ "." + (i >> 24 & 0xFF);
	}

	//

	/******************************** 【Activity LifeCycle For Debug】 *******************************************/

	protected void showShortToast(int pResId) {
		showShortToast(getString(pResId));
	}

	protected void showLongToast(String pMsg) {
		Toast.makeText(this, pMsg, Toast.LENGTH_LONG).show();
	}

	protected void showShortToast(String pMsg) {
		Toast.makeText(this, pMsg, Toast.LENGTH_SHORT).show();
	}

	protected boolean hasExtra(String pExtraKey) {
		if (getIntent() != null) {
			return getIntent().hasExtra(pExtraKey);
		}
		return false;
	}

	protected void openActivity(Class<?> pClass) {
		openActivity(pClass, null);
	}

	protected void openActivity(Class<?> pClass, Bundle pBundle) {
		Intent intent = new Intent(this, pClass);
		if (pBundle != null) {
			intent.putExtras(pBundle);
		}
		startActivity(intent);
	}

	protected void openActivity(String pAction) {
		openActivity(pAction, null);
	}

	protected void openActivity(String pAction, Bundle pBundle) {
		Intent intent = new Intent(pAction);
		if (pBundle != null) {
			intent.putExtras(pBundle);
		}
		startActivity(intent);
	}

	/**
	 * 通过反射来设置对话框是否要关闭，在表单校验时很管用， 因为在用户填写出错时点确定时默认Dialog会消失， 所以达不到校验的效果
	 * 而mShowing字段就是用来控制是否要消失的，而它在Dialog中是私有变量， 所有只有通过反射去解决此问题
	 * 
	 * @param pDialog
	 * @param pIsClose
	 */
	public void setAlertDialogIsClose(DialogInterface pDialog, Boolean pIsClose) {
		try {
			Field field = pDialog.getClass().getSuperclass()
					.getDeclaredField("mShowing");
			field.setAccessible(true);
			field.set(pDialog, pIsClose);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	protected AlertDialog showAlertDialog(String TitleID, String Message) {
		mAlertDialog = new AlertDialog.Builder(this).setTitle(TitleID)
				.setMessage(Message).show();
		return mAlertDialog;
	}

	protected AlertDialog showAlertDialog(int pTitelResID, String pMessage,
			DialogInterface.OnClickListener pOkClickListener) {
		String title = getResources().getString(pTitelResID);
		return showAlertDialog(title, pMessage, pOkClickListener, null, null);
	}

	protected AlertDialog showAlertDialog(String pTitle, String pMessage,
			DialogInterface.OnClickListener pOkClickListener,
			DialogInterface.OnClickListener pCancelClickListener,
			DialogInterface.OnDismissListener pDismissListener) {
		mAlertDialog = new AlertDialog.Builder(this)
				.setTitle(pTitle)
				.setMessage(pMessage)
				.setPositiveButton(android.R.string.ok, pOkClickListener)
				.setNegativeButton(android.R.string.cancel,
						pCancelClickListener).show();
		if (pDismissListener != null) {
			mAlertDialog.setOnDismissListener(pDismissListener);
		}
		return mAlertDialog;
	}

	protected AlertDialog showAlertDialog(String pTitle, String pMessage,
			String pPositiveButtonLabel, String pNegativeButtonLabel,
			DialogInterface.OnClickListener pOkClickListener,
			DialogInterface.OnClickListener pCancelClickListener,
			DialogInterface.OnDismissListener pDismissListener) {
		mAlertDialog = new AlertDialog.Builder(this).setTitle(pTitle)
				.setMessage(pMessage)
				.setPositiveButton(pPositiveButtonLabel, pOkClickListener)
				.setNegativeButton(pNegativeButtonLabel, pCancelClickListener)
				.show();
		if (pDismissListener != null) {
			mAlertDialog.setOnDismissListener(pDismissListener);
		}
		return mAlertDialog;
	}

	protected ProgressDialog showProgressDialog(int pTitelResID,
			String pMessage,
			DialogInterface.OnCancelListener pCancelClickListener) {
		String title = getResources().getString(pTitelResID);
		return showProgressDialog(title, pMessage, pCancelClickListener);
	}

	protected ProgressDialog showProgressDialog(String pTitle, String pMessage,
			DialogInterface.OnCancelListener pCancelClickListener) {
		mAlertDialog = ProgressDialog.show(this, pTitle, pMessage, true, true);
		mAlertDialog.setOnCancelListener(pCancelClickListener);
		return (ProgressDialog) mAlertDialog;
	}

	protected void hideKeyboard(View view) {
		InputMethodManager imm = (InputMethodManager) this
				.getSystemService(Context.INPUT_METHOD_SERVICE);
		imm.hideSoftInputFromWindow(view.getWindowToken(),
				InputMethodManager.HIDE_NOT_ALWAYS);
	}

	protected void handleOutmemoryError() {
		runOnUiThread(new Runnable() {
			// @Override
			public void run() {
				Toast.makeText(BaseActivity.this, "内存空间不足！", Toast.LENGTH_SHORT)
						.show();
				// finish();
			}
		});
	}

	private int network_err_count = 0;

	protected void handleNetworkError() {
		network_err_count++;
		runOnUiThread(new Runnable() {
			// @Override
			public void run() {
				if (network_err_count < 3) {
					Toast.makeText(BaseActivity.this, "网速好像不怎么给力啊！",
							Toast.LENGTH_SHORT).show();
				} else if (network_err_count < 5) {
					Toast.makeText(BaseActivity.this, "网速真的不给力！",
							Toast.LENGTH_SHORT).show();
				} else {
					Toast.makeText(BaseActivity.this, "唉，今天的网络怎么这么差劲！",
							Toast.LENGTH_SHORT).show();
				}
				// finish();
			}
		});
	}

	protected void handleMalformError() {
		runOnUiThread(new Runnable() {
			// @Override
			public void run() {
				Toast.makeText(BaseActivity.this, "数据格式错误！", Toast.LENGTH_SHORT)
						.show();
				// finish();
			}
		});
	}

	protected void handleFatalError() {
		runOnUiThread(new Runnable() {
			// @Override
			public void run() {
				Toast.makeText(BaseActivity.this, "发生了一点意外，程序终止！",
						Toast.LENGTH_SHORT).show();
				finish();
			}
		});
	}

	public void finish() {
		super.finish();
		overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);
	}

	public void defaultFinish() {
		super.finish();
	}

}
