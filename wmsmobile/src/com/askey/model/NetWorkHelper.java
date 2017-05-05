package com.askey.model;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.Uri;
import android.telephony.TelephonyManager;
import android.util.Log;

public class NetWorkHelper {

	private static String LOG_TAG = "NetWorkHelper";

	public static Uri uri = Uri.parse("content://telephony/carriers");

	/**
	 * �斗�臬��蝏���	 */
	public static boolean isNetworkAvailable(Context context) {
		ConnectivityManager connectivity = (ConnectivityManager) context
				.getSystemService(Context.CONNECTIVITY_SERVICE);

		if (connectivity == null) {
			Log.w(LOG_TAG, "couldn't get connectivity manager");
		} else {
			NetworkInfo[] info = connectivity.getAllNetworkInfo();
			if (info != null) {
				for (int i = 0; i < info.length; i++) {
					if (info[i].isAvailable()) {
						Log.d(LOG_TAG, "network is available");
						return true;
					}
				}
			}
		}
		Log.d(LOG_TAG, "network is not available");
		return false;
	}
	
	public static boolean checkNetState(Context context){
    	boolean netstate = false;
		ConnectivityManager connectivity = (ConnectivityManager)context.getSystemService(Context.CONNECTIVITY_SERVICE);
		if(connectivity != null)
		{
			NetworkInfo[] info = connectivity.getAllNetworkInfo();
			if (info != null) {
				for (int i = 0; i < info.length; i++)
				{
					if (info[i].getState() == NetworkInfo.State.CONNECTED) 
					{
						netstate = true;
						break;
					}
				}
			}
		}
		return netstate;
    }

	/**
	 * �斗蝵��臬銝箸憤皜�	 */
	public static boolean isNetworkRoaming(Context context) {
		ConnectivityManager connectivity = (ConnectivityManager) context
				.getSystemService(Context.CONNECTIVITY_SERVICE);
		if (connectivity == null) {
			Log.w(LOG_TAG, "couldn't get connectivity manager");
		} else {
			NetworkInfo info = connectivity.getActiveNetworkInfo();
			if (info != null
					&& info.getType() == ConnectivityManager.TYPE_MOBILE) {
				TelephonyManager tm = (TelephonyManager) context
						.getSystemService(Context.TELEPHONY_SERVICE);
				if (tm != null && tm.isNetworkRoaming()) {
					Log.d(LOG_TAG, "network is roaming");
					return true;
				} else {
					Log.d(LOG_TAG, "network is not roaming");
				}
			} else {
				Log.d(LOG_TAG, "not using mobile network");
			}
		}
		return false;
	}

	/**
	 * �斗MOBILE蝵��臬�舐
	 * 
	 * @param context
	 * @return
	 * @throws Exception
	 */
	public static boolean isMobileDataEnable(Context context) throws Exception {
		ConnectivityManager connectivityManager = (ConnectivityManager) context
				.getSystemService(Context.CONNECTIVITY_SERVICE);
		boolean isMobileDataEnable = false;

		isMobileDataEnable = connectivityManager.getNetworkInfo(
				ConnectivityManager.TYPE_MOBILE).isConnectedOrConnecting();

		return isMobileDataEnable;
	}

	
	/**
	 * �斗wifi �臬�舐
	 * @param context
	 * @return
	 * @throws Exception
	 */
	public static boolean isWifiDataEnable(Context context) throws Exception {
		ConnectivityManager connectivityManager = (ConnectivityManager) context
				.getSystemService(Context.CONNECTIVITY_SERVICE);
		boolean isWifiDataEnable = false;
		isWifiDataEnable = connectivityManager.getNetworkInfo(
				ConnectivityManager.TYPE_WIFI).isConnectedOrConnecting();
		return isWifiDataEnable;
	}

	/**
	 * 霈曄蔭Mobile蝵�撘�
	 * 
	 * @param context
	 * @param enabled
	 * @throws Exception
	 */
//	public static void setMobileDataEnabled(Context context, boolean enabled)
//			throws Exception {
//		APNManager apnManager=APNManager.getInstance(context);
//		List<APN> list = apnManager.getAPNList();
//		if (enabled) {
//			for (APN apn : list) {
//				ContentValues cv = new ContentValues();
//				cv.put("apn", apnManager.matchAPN(apn.apn));
//				cv.put("type", apnManager.matchAPN(apn.type));
//				context.getContentResolver().update(uri, cv, "_id=?",
//						new String[] { apn.apnId });
//			}
//		} else {
//			for (APN apn : list) {
//				ContentValues cv = new ContentValues();
//				cv.put("apn", apnManager.matchAPN(apn.apn) + "mdev");
//				cv.put("type", apnManager.matchAPN(apn.type) + "mdev");
//				context.getContentResolver().update(uri, cv, "_id=?",
//						new String[] { apn.apnId });
//			}
//		}
//	}

}
