package com.askey.model;

import org.apache.http.NameValuePair;

import android.content.Context;
import android.util.Log;

public class HttpUtils {
	// 蝵�餈�典�
	public static String postByHttpURLConnection(String strUrl,
			NameValuePair... nameValuePairs) {
		return CustomHttpURLConnection.PostFromWebByHttpURLConnection(strUrl,
				nameValuePairs);
	}

	public static String getByHttpURLConnection(String strUrl,
			NameValuePair... nameValuePairs) {
		return CustomHttpURLConnection.GetFromWebByHttpUrlConnection(strUrl,
				nameValuePairs);
	}

	public static String postByHttpClient(Context context, String strUrl,
			NameValuePair... nameValuePairs) {
		return CustomHttpClient.PostFromWebByHttpClient(context, strUrl,
				nameValuePairs);
	}

	public static String getByHttpClient(Context context, String strUrl,
			NameValuePair... nameValuePairs) throws Exception {
		return CustomHttpClient.getFromWebByHttpClient(context, strUrl,
				nameValuePairs);
	}

	// ------------------------------------------------------------------------------------------
	// 蝵�餈�斗
	// �斗�臬��蝏�	// public static boolean isNetworkAvailable(Context context) {
	// return NetWorkHelper.isNetworkAvailable(context);
	// }

	// �斗mobile蝵��臬�舐
	public static boolean isMobileDataEnable(Context context) {
		String TAG = "httpUtils.isMobileDataEnable()";
		try {
			return NetWorkHelper.isMobileDataEnable(context);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			Log.e(TAG, e.getMessage());
			e.printStackTrace();
			return false;
		}
	}

	// �斗wifi蝵��臬�舐
	public static boolean isWifiDataEnable(Context context) {
		String TAG = "httpUtils.isWifiDataEnable()";
		try {
			return NetWorkHelper.isWifiDataEnable(context);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			Log.e(TAG, e.getMessage());
			e.printStackTrace();
			return false;
		}
	}

	// 霈曄蔭Mobile蝵�撘�
	public static void setMobileDataEnabled(Context context, boolean enabled) {
		String TAG = "httpUtils.setMobileDataEnabled";
		try {
			// NetWorkHelper.setMobileDataEnabled(context, enabled);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			Log.e(TAG, e.getMessage());
			e.printStackTrace();
		}
	}

	// �斗�臬銝箸憤皜�	public static boolean isNetworkRoaming(Context context) {
	//	return NetWorkHelper.isNetworkRoaming(context);
	//}
}
