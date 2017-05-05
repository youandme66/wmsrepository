package com.askey.model;

import java.io.File;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.SocketException;
import java.util.Enumeration;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.graphics.Color;
import android.media.AudioManager;
import android.media.ToneGenerator;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Environment;
import android.os.StatFs;
import android.util.Log;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.askey.wms.R;

public class CommonUtil {
	final static ToneGenerator tg = new ToneGenerator(
			AudioManager.STREAM_NOTIFICATION, 300);
	 //SharedPreferences工具类
	 public final static String SETTING = "Setting"; 
	    //set
	    public static void putValue(Context context,String key, int value) {  
	         Editor sp =  context.getSharedPreferences(SETTING, Context.MODE_PRIVATE).edit();  
	         sp.putInt(key, value);  
	         sp.commit();  
	    }  
	    public static void putValue(Context context,String key, boolean value) {  
	         Editor sp =  context.getSharedPreferences(SETTING, Context.MODE_PRIVATE).edit();  
	         sp.putBoolean(key, value);  
	         sp.commit();  
	    }  
	    public static void putValue(Context context,String key, String value) {  
	         Editor sp =  context.getSharedPreferences(SETTING, Context.MODE_PRIVATE).edit();  
	         sp.putString(key, value);  
	         sp.commit();  
	    }  
	    
	    //get
	    public static int getValue(Context context,String key, int defValue) {  
	        SharedPreferences sp =  context.getSharedPreferences(SETTING, Context.MODE_PRIVATE);  
	        int value = sp.getInt(key, defValue);  
	        return value;  
	    }  
	    public static boolean getValue(Context context,String key, boolean defValue) {  
	        SharedPreferences sp =  context.getSharedPreferences(SETTING, Context.MODE_PRIVATE);  
	        boolean value = sp.getBoolean(key, defValue);  
	        return value;  
	    }  
	    public static String getValue(Context context,String key, String defValue) {  
	        SharedPreferences sp =  context.getSharedPreferences(SETTING, Context.MODE_PRIVATE);  
	        String value = sp.getString(key, defValue);  
	        return value;  
	    }  
	
	//
	//{result:"0"}
	public static String GetStringFromResult(String strResponse) {
		try {
			JSONObject jsobj = new JSONObject(strResponse);
			String res = jsobj.getString("result");
			return res;
		} catch (JSONException e) {
			// TODO 自動產生的 catch 區塊
			return "";
		}
	}
	//{"result":[{"ORG":"26","ORGDESC":"CH1"},{"ORG":"4","ORGDESC":"TW1"}]}
	public static JSONArray GetArrayFromResult(String strResponse) {
		try {
			JSONObject jsobj = new JSONObject(strResponse);
			JSONArray array = jsobj.getJSONArray("result");
			return array;
		} catch (JSONException e) {
			// TODO 自動產生的 catch 區塊
			return null;
		}
	}
	//設置edittext爲只讀
    public static void setEdReadOnly(EditText view){  
    	  view.setKeyListener(null);//方法一
    	  //方法二
//        view.setTextColor(Color.GRAY);   //设置只读时的文字颜色  
//        if (view instanceof android.widget.EditText){  
//            view.setCursorVisible(false);      //设置输入框中的光标不可见  
//            view.setFocusable(false);           //无焦点  
//            view.setFocusableInTouchMode(false);//触摸时也得不到焦点  
//        }  
  }  
	// 彈出輸入框,返回值到界面的框中
	public static void Inputbox(Context context, String title, final EditText ed) {
		final EditText edview = new EditText(context);
		ed.setText("");// 清空框裏的值
		new AlertDialog.Builder(context).setTitle(title)
				.setIcon(android.R.drawable.ic_dialog_info).setView(edview)
				.setPositiveButton("确定", new OnClickListener() {
					@Override
					public void onClick(DialogInterface dialog, int which) {
						ed.setText(edview.getText().toString());
					}
					//
				}).setNegativeButton("取消", null).show();
	}

	// 彈出輸入框,返回值到全局變量
	public static void Inputbox_ret(Context context, String title,
			String defaultval) {
		final EditText edview = new EditText(context);
		AppData.dialog_ret = defaultval;// 框裏的初始值
		new AlertDialog.Builder(context).setTitle(title)
				.setIcon(android.R.drawable.ic_dialog_info).setView(edview)
				.setPositiveButton("确定", new OnClickListener() {
					@Override
					public void onClick(DialogInterface dialog, int which) {
						AppData.dialog_ret = edview.getText().toString();
					}
					//
				}).setNegativeButton("取消", null).show();
	}

	// 彈出輸入框
	// public static void Inputbox_ret(Context context, String title,final
	// StringBuffer sb) {
	// final EditText edview = new EditText(context);
	// new AlertDialog.Builder(context).setTitle(title)
	// .setIcon(android.R.drawable.ic_dialog_info).setView(edview)
	// .setPositiveButton("确定", new OnClickListener() {
	// @Override
	// public void onClick(DialogInterface dialog, int which) {
	// sb.append(edview.getText().toString());
	// }
	// //
	// }).setNegativeButton("取消", null).show();
	// }
	// 檢查是否爲大於等於0的整數
	public static boolean isNumeric(String str) {
		for (int i = str.length(); --i >= 0;) {
			if (!Character.isDigit(str.charAt(i))) {
				return false;
			}
		}
		return true;
	}

	//
	public static void errorsound() {
		// tg.startTone(ToneGenerator.TONE_CDMA_ONE_MIN_BEEP);
		tg.startTone(ToneGenerator.TONE_SUP_ERROR, 300);
	}

	//
	public static void feedbackexcres(Context context, String excuteres) {
		//
		if (excuteres.equals("0")) {
			Toast.makeText(context, "數據操作完成", Toast.LENGTH_SHORT).show();
		} else {
			// Toast.makeText(Context, excuteres, Toast.LENGTH_SHORT).show();
			CommonUtil.WMSShowmsg(context, excuteres);
		}
	}

	public static void trimandupper(EditText edt) {
		edt.setText(edt.getText().toString().trim().toUpperCase());
	}

	public static void feedbackqueryres(Context Context, boolean queryres) {
		//
		if (queryres) {
			Toast.makeText(Context, "數據查詢完成", Toast.LENGTH_SHORT).show();
		} else {
			Toast.makeText(Context, "數據查詢錯誤", Toast.LENGTH_SHORT).show();
		}
	}

	public static void WMSToast(Context context, String msg) {
		Toast.makeText(context, msg, Toast.LENGTH_SHORT).show();
	}

	public static void WMSShowmsg_YesNo(Context context, String msg) {
		AlertDialog.Builder builder = new AlertDialog.Builder(context);
		builder.setMessage(msg)
				.setCancelable(false)
				.setPositiveButton("Yes",
						new DialogInterface.OnClickListener() {
							public void onClick(DialogInterface dialog, int id) {
								//
								AppData.dialogresult = AppData.dialogresult_yes;
							}
						})
				.setNegativeButton("No", new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int id) {
						AppData.dialogresult = AppData.dialogresult_no;
						dialog.cancel();
					}
				});
		AlertDialog alert = builder.create();
		alert.show();
	}

	public static void WMSShowmsg(Context context, String msg) {
		AlertDialog.Builder builder = new AlertDialog.Builder(context);
		builder.setIcon(R.drawable.error)
				.setMessage(msg)
				.setCancelable(false)
				.setPositiveButton("Yes",
						new DialogInterface.OnClickListener() {
							public void onClick(DialogInterface dialog, int id) {
								//
							}
						});
		AlertDialog alert = builder.create();
		alert.show();
	}

	public static int getcharcnt(String str) {
		int cnt = 0;
		for (int i = 0; i < str.length(); i++) {
			if (str.charAt(i) == '#') {
				cnt = cnt + 1;
			}
		}
		return cnt;
	}

	public static boolean Split5in1(Context context, String barcode) {
		AppData.pn_5in1 = "";
		AppData.qty_5in1 = "";
		AppData.dc_5in1 = "";
		AppData.lotno_5in1 = "";
		AppData.vc_5in1 = "";
		AppData.reel_5in1 = "";
		//
		barcode = barcode.toString().trim();
		//
		int count, index;
		if (barcode.equals("")) {
			CommonUtil.WMSToast(context, "條碼不能為空");
			return false;
		}
		count = getcharcnt(barcode);
		if (count == 3) {// 四合一: 料號 # 數量 # D/C # Vendor
			index = barcode.indexOf("#");
			AppData.pn_5in1 = barcode.substring(0, index);
			barcode = barcode.substring(index + 1).toString().trim();

			index = barcode.indexOf("#");
			AppData.qty_5in1 = barcode.substring(0, index).toString().trim();
			barcode = barcode.substring(index + 1);

			index = barcode.indexOf("#");
			AppData.dc_5in1 = barcode.substring(0, index).toString().trim();
			barcode = barcode.substring(index + 1);

			AppData.vc_5in1 = barcode;

		} else if (count == 4) {// 五合一 料號 # 數量 # Lot # D/C # Vendor
			index = barcode.indexOf("#");
			AppData.pn_5in1 = barcode.substring(0, index).toString().trim();
			barcode = barcode.substring(index + 1);

			index = barcode.indexOf("#");
			AppData.qty_5in1 = barcode.substring(0, index).toString().trim();
			barcode = barcode.substring(index + 1);

			index = barcode.indexOf("#");
			AppData.lotno_5in1 = barcode.substring(0, index).toString().trim();
			barcode = barcode.substring(index + 1);

			index = barcode.indexOf("#");
			AppData.dc_5in1 = barcode.substring(0, index).toString().trim();
			barcode = barcode.substring(index + 1);

			AppData.vc_5in1 = barcode;

		} else if (count == 5) {// 六合一 料號 # 數量 # Lot # D/C # Vendor # Reel
			index = barcode.indexOf("#");
			AppData.pn_5in1 = barcode.substring(0, index).toString().trim();
			barcode = barcode.substring(index + 1);

			index = barcode.indexOf("#");
			AppData.qty_5in1 = barcode.substring(0, index).toString().trim();
			barcode = barcode.substring(index + 1);

			index = barcode.indexOf("#");
			AppData.lotno_5in1 = barcode.substring(0, index).toString().trim();
			barcode = barcode.substring(index + 1);

			index = barcode.indexOf("#");
			AppData.dc_5in1 = barcode.substring(0, index).toString().trim();
			barcode = barcode.substring(index + 1);

			index = barcode.indexOf("#");
			AppData.vc_5in1 = barcode.substring(0, index).toString().trim();
			barcode = barcode.substring(index + 1);

			AppData.reel_5in1 = barcode;

		} else {
			CommonUtil.WMSToast(context, "條碼格式不正確[#數量有誤]");
			CommonUtil.errorsound();
			return false;
		}
		if (AppData.pn_5in1.equals("") || AppData.qty_5in1.equals("")) {
			CommonUtil.WMSToast(context, "條碼格式不正確[PN&QTY不能為空]");
			CommonUtil.errorsound();
			return false;
		}
		//
		if (count != 3 && AppData.dc_5in1.equals("")) {// 如果爲4合一條碼,D/C可以爲空
			CommonUtil.WMSToast(context, "條碼格式不正確[D/C不能為空]");
			CommonUtil.errorsound();
			return false;
		}
		//
		if (!CommonUtil.isNumeric(AppData.qty_5in1)) {
			CommonUtil.WMSToast(context, "條碼格式不正確[QTY必須爲數字]");
			CommonUtil.errorsound();
			return false;
		}
		//
		return true;
	}

	public static boolean sdCardIsAvailable() {
		String status = Environment.getExternalStorageState();
		if (!status.equals(Environment.MEDIA_MOUNTED))
			return false;
		return true;
	}

	/**
	 * Checks if there is enough Space on SDCard
	 * 
	 * @param updateSize
	 *            Size to Check
	 * @return True if the Update will fit on SDCard, false if not enough space
	 *         on SDCard Will also return false, if the SDCard is not mounted as
	 *         read/write
	 */
	public static boolean enoughSpaceOnSdCard(long updateSize) {
		String status = Environment.getExternalStorageState();
		if (!status.equals(Environment.MEDIA_MOUNTED))
			return false;
		return (updateSize < getRealSizeOnSdcard());
	}

	public static long getRealSizeOnSdcard() {
		File path = new File(Environment.getExternalStorageDirectory()
				.getAbsolutePath());
		StatFs stat = new StatFs(path.getPath());
		long blockSize = stat.getBlockSize();
		long availableBlocks = stat.getAvailableBlocks();
		return availableBlocks * blockSize;
	}

	public static boolean enoughSpaceOnPhone(long updateSize) {
		return getRealSizeOnPhone() > updateSize;
	}

	public static long getRealSizeOnPhone() {
		File path = Environment.getDataDirectory();
		StatFs stat = new StatFs(path.getPath());
		long blockSize = stat.getBlockSize();
		long availableBlocks = stat.getAvailableBlocks();
		long realSize = blockSize * availableBlocks;
		return realSize;
	}

	public static int dip2px(Context context, float dpValue) {
		final float scale = context.getResources().getDisplayMetrics().density;
		return (int) (dpValue * scale + 0.5f);
	}

	public static int px2dip(Context context, float pxValue) {
		final float scale = context.getResources().getDisplayMetrics().density;
		return (int) (pxValue / scale + 0.5f) - 15;
	}

}
