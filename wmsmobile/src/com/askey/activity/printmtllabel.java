package com.askey.activity;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;

import org.json.JSONObject;

import zicox.print.StatusBox;
import zpSDK.zpSDK.zpSDK;

import android.annotation.SuppressLint;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Looper;
import android.provider.SyncStateContract.Constants;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.view.WindowManager;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.SimpleAdapter;
import android.widget.Toast;

import com.askey.Adapter.InsertSpitDataAdapter;
import com.askey.Adapter.LoginAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity_clear;
import com.askey.model.CommonUtil;
import com.askey.model.K_table;
import com.askey.wms.R;

public class printmtllabel extends BaseActivity_clear implements
		OnClickListener {

	private String SelectedBDAddress, barcodetype, printqty;
	private BluetoothAdapter myBluetoothAdapter;
	private boolean isPrintSingle = true;
	private boolean isLoop = false;
	private StatusBox statusBox;
	// private MessageBox megBox;
	private EditText edbarcode, edpn, edqty, eddc, edlotno, edvc, edemp;
	private ImageView ivGohome, ivcommit;
	private String pn_5in1, dc_5in1, qty_5in1, qty_input, lotno_5in1, VC_5in1,
			reel_5in1;
	private RadioGroup rg_HForRoHS, rg_PrintQty;
	private String HForRoHS;
	private RadioButton rb;

	//
	protected void onCreate(Bundle savedInstanceState) {
		// TODO 自動產生的方法 Stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.printmtllabel);
		//
		pn_5in1 = "";
		dc_5in1 = "";
		qty_5in1 = "";
		lotno_5in1 = "";
		VC_5in1 = "";
		reel_5in1 = "";
		qty_input = "";
		//
		SelectedBDAddress = "";
		barcodetype = "";
		printqty = "";
		//
		ivGohome = (ImageView) this.findViewById(R.id.printmtllab_gohome);
		ivGohome.setOnClickListener(this);
		//
		ivcommit = (ImageView) this.findViewById(R.id.printmtllab_commit);
		ivcommit.setOnClickListener(this);
		statusBox = new StatusBox(this, ivcommit);
		/*
		 * ImageView ivcommit = (ImageView)
		 * findViewById(R.id.printmtllab_commit); statusBox = new
		 * StatusBox(this,ivcommit); ivcommit.setOnClickListener(new
		 * Button.OnClickListener(){ public void onClick(View v) { if
		 * (!Printlab(SelectedBDAddress))finish(); } });
		 */
		//
		edbarcode = (EditText) findViewById(R.id.edt_originalbarcode);
		edpn = (EditText) findViewById(R.id.printmtllab_edtpn);
		edqty = (EditText) findViewById(R.id.printmtllab_edtqty);
		eddc = (EditText) findViewById(R.id.printmtllab_edtdc);
		edlotno = (EditText) findViewById(R.id.printmtllab_edtlotno);
		edvc = (EditText) findViewById(R.id.printmtllab_edtvc);
		edemp = (EditText) findViewById(R.id.printmtllab_edtemp);
		// edemp.setText(AppData.user_name);
		//
		rg_HForRoHS = (RadioGroup) findViewById(R.id.rg_barcodetype);
		rg_PrintQty = (RadioGroup) findViewById(R.id.rg_printqty);
		// 这个可以被用来控制键盘，直到使用者确实碰了编辑框
		getWindow().setSoftInputMode(
				WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
		//
		if (!ListBluetoothDevice()) {
			CommonUtil.WMSToast(this, "藍牙未配對或連接錯誤");
			finish();
		}
		//
/*		String ip = getIP();
		if (ip.equals(getString(R.string.myip))) {// 如果不是自己測試則清除賬號和密碼
			
		}*/
		//
		edemp.setText("016406");
		edbarcode
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity_clear.mCommReception = edbarcode;
							btnOpenFlag = true;
							Open("/dev/ttyHS1", 9600);
						} else {
							btnOpenFlag = false;
							Close();
						}
					}

				});
		//
		registerReadgoodReceiver();
	}

	public void trimandupperedt() {
		CommonUtil.trimandupper(edbarcode);
		CommonUtil.trimandupper(edqty);
		CommonUtil.trimandupper(edemp);
	}

	public boolean checkinput(boolean isClickcommit) {
		isPrintSingle = true;// 打印一張
		HForRoHS = "";
		return true;
		
	/*	try {
			
			 * if(isClickcommit==false){ ClearAll(); }
			 
			trimandupperedt();
			if (edemp.getText().toString().equals("")) {
				CommonUtil.WMSToast(this, "工號不能為空");
				return false;
			}
			//
			// ((RadioButton) rg_printqty.getChildAt(0)).setChecked(false);
			for (int i = 0; i < rg_PrintQty.getChildCount(); i++) {
				rb = (RadioButton) rg_PrintQty.getChildAt(i);
				if (rb.isChecked()) {
					if (i == 0) {
						isPrintSingle = true;// 打印一張
						break;
					} else {
						isPrintSingle = false;// 打印兩張
						break;
					}
				}
			}
			for (int i = 0; i < rg_HForRoHS.getChildCount(); i++) {
				rb = (RadioButton) rg_HForRoHS.getChildAt(i);
				if (rb.isChecked()) {
					if (i == 0) {
						HForRoHS = "";
						break;
					} else if (i == 1) {
						HForRoHS = "RoHS";
						break;
					} else {
						HForRoHS = "HF";
						break;
					}
				}
			}
			//
			pn_5in1 = "";
			dc_5in1 = "";
			qty_5in1 = "";
			lotno_5in1 = "";
			VC_5in1 = "";
			reel_5in1 = "";
			qty_input = "";
			//
			if (isClickcommit == false) {
				// String barcode = edbarcode.getText().toString();
				String barcode = AppData.scandata;
				if (!CommonUtil.Split5in1(printmtllabel.this, barcode)) {
					return false;
				}
			}
			pn_5in1 = AppData.pn_5in1;
			dc_5in1 = AppData.dc_5in1;
			qty_5in1 = AppData.qty_5in1;
			lotno_5in1 = AppData.lotno_5in1;
			VC_5in1 = AppData.vc_5in1;
			reel_5in1 = AppData.reel_5in1;
			//
			edpn.setText(pn_5in1);// 扫描框赋值为料号
			//
			if (isClickcommit == false) {
				edqty.setText(qty_5in1);//如果是點打印按鈕,則數量不從條碼裏帶出
			}
			//
			eddc.setText(dc_5in1);
			edlotno.setText(lotno_5in1);
			edvc.setText(VC_5in1);
			//
			if (isClickcommit == false) {
				return true;
			}
			//
			if (qty_5in1.equals("")) {
				CommonUtil.WMSToast(this, "請输入條碼");
				return false;
			}
			//
			qty_input = edqty.getText().toString();
			//
			if (qty_input.equals("")) {
				CommonUtil.WMSToast(this, "請输入数量");
				return false;
			}
			//
			if (!CommonUtil.isNumeric(qty_input)) {
				CommonUtil.WMSToast(this, "輸入的數量必須為整數型");
				return false;
			}
			//
			if (Integer.parseInt(qty_input) == 0) {
				CommonUtil.WMSToast(this, "輸入的數量應大於0");
				return false;
			}
			if (!isPrintSingle) {// 印兩張 //

				if (Integer.parseInt(qty_input) >= Integer.parseInt(qty_5in1)) {
					CommonUtil.WMSToast(this, "輸入的數量[" + qty_input
							+ "]應小於條碼中數量[" + qty_5in1 + "]");
					return false;
				}
			} else {// 印單張
				
				 * if (!(Integer.parseInt(qty_input) ==
				 * Integer.parseInt(qty_5in1))) { CommonUtil.WMSToast(this,
				 * "列印單張不能修改数量"); return false; }
				 
			}
			//
			if (!AppData.EmpnoIsValid) {
				CommonUtil.WMSToast(this, "工號[" + edemp.getText().toString()
						+ "]不存在");
				return false;
			}
			//
			if (AppData.PNDesc.equals("")) {
				CommonUtil.WMSToast(this, "料號描述不能為空");
				return false;
			}
			//
			if (AppData.BoxID_1.equals("")) {
				CommonUtil.WMSToast(this, "BoxID_1不能為空");
				return false;
			}
			if (AppData.BoxID_2.equals("")) {
				CommonUtil.WMSToast(this, "BoxID_2不能為空");
				return false;
			}
			//
			return true;
		} catch (Exception e) {
			CommonUtil.WMSToast(this, e.getMessage().toString());
			return false;
		}*/
	}

	public void ClearAll() {
		edbarcode.setText("");
		eddc.setText("");
		edlotno.setText("");
		edpn.setText("");
		edqty.setText("");
		edvc.setText("");
		//
		pn_5in1 = "";
		dc_5in1 = "";
		qty_5in1 = "";
		lotno_5in1 = "";
		VC_5in1 = "";
		reel_5in1 = "";
		qty_input = "";
		//
		AppData.VN = "";
		AppData.PNDesc = "";
		AppData.BoxID_1 = "";
		AppData.BoxID_2 = "";
		//
		edbarcode.requestFocus();
	}

	@Override
	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.printmtllab_gohome: {
			finish();
			break;
		}
		case R.id.printmtllab_commit: {
			if (checkinput(true)) {
				try {
					if (isPrintSingle) {// 補印
						if (!Printlab(SelectedBDAddress, AppData.pn_5in1,
								qty_input, AppData.lotno_5in1, AppData.dc_5in1,
								AppData.vc_5in1, AppData.BoxID_1)) {
							CommonUtil.WMSToast(printmtllabel.this,
									"print error");
						} else {
							InsertReprintData();
						}
					} else {// 分包裝
						// 打印第一张条码
						String balance_qty = String.valueOf(Integer
								.parseInt(qty_5in1)
								- Integer.parseInt(qty_input));
						//
						if (!Printlab(SelectedBDAddress, AppData.pn_5in1,
								balance_qty, AppData.lotno_5in1,
								AppData.dc_5in1, AppData.vc_5in1,
								AppData.BoxID_1)) {
							CommonUtil.WMSToast(printmtllabel.this,
									"print error");
						} else {
							// 打印第二张条码
							if (!Printlab(SelectedBDAddress, AppData.pn_5in1,
									qty_input, AppData.lotno_5in1,
									AppData.dc_5in1, AppData.vc_5in1,
									AppData.BoxID_2)) {
								CommonUtil.WMSToast(printmtllabel.this,
										"print error");
							} else {
								InsertSpitData();
							}
						}
					}
				} catch (Exception e) {

				} finally {
					ClearAll();// 清空
				}
				break;
			}
		}
		}

	}

	public boolean OpenPrinter(String BDAddress) {
		if (BDAddress == "" || BDAddress == null) {
			Toast.makeText(this, "没有选择打印机", Toast.LENGTH_LONG).show();
			return false;
		}
		BluetoothDevice myDevice;
		myBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
		if (myBluetoothAdapter == null) {
			Toast.makeText(this, "读取蓝牙设备错误", Toast.LENGTH_LONG).show();
			return false;
		}
		myDevice = myBluetoothAdapter.getRemoteDevice(BDAddress);
		if (myDevice == null) {
			Toast.makeText(this, "读取蓝牙设备错误", Toast.LENGTH_LONG).show();
			return false;
		}
		if (zpSDK.zp_open(myBluetoothAdapter, myDevice) == false) {
			Toast.makeText(this, zpSDK.ErrorMessage, Toast.LENGTH_LONG).show();
			return false;
		}
		return true;
	}

	@SuppressLint("DefaultLocale")
	private boolean Printlab(String BDAddress, String pn, String Qty,
			String lotno, String dc, String vendor, String BoxID)// 打印标签2
	// alvin develop
	{
		pn = "123";
		Qty = "123";
		lotno = "123";
		dc = "123";
		vendor = "123";
		BoxID = "123";
		String pndesc_1 = "123";
		String pndesc_2 = "123";
		Qty = String.format("%06d", Integer.parseInt(Qty));
		/*
		Log.e("VN length", String.valueOf(AppData.VN.length()));// length属性不分汉字还是英文都是算1个字符
		Integer VN_Max_length = 13;// Vendor name的最大长度
		if (AppData.VN.length() > VN_Max_length) {
			AppData.VN = AppData.VN.substring(0, VN_Max_length);// vendor name
		}
		//
		String pndesc_1, pndesc_2;// 品名规格里的第一和第二行
		Integer PNDesc_row1_Max_length = 13;// PN desc的第一行的最大长度
		Integer PNDesc_row2_Max_length = 20;// PN desc的第一行的最大长度
		if (AppData.PNDesc.length() > PNDesc_row1_Max_length) {
			pndesc_1 = AppData.PNDesc.substring(0, PNDesc_row1_Max_length);
			String s = AppData.PNDesc.substring(PNDesc_row1_Max_length,
					AppData.PNDesc.length());
			if (s.length() > PNDesc_row2_Max_length) {
				pndesc_2 = s.substring(0, PNDesc_row2_Max_length);
			} else
				pndesc_2 = s;
		} else {
			pndesc_1 = AppData.PNDesc;
			pndesc_2 = "";
		}*/
		
		
		if (!OpenPrinter(BDAddress)) {
			statusBox.Close();
			return false;
		}
		if (!zpSDK.zp_page_create(80, 0 + 40)) {
			Toast.makeText(this, "创建打印页面失败", Toast.LENGTH_LONG).show();
			statusBox.Close();
			return false;
		}
		zpSDK.TextPosWinStyle = true;
		zpSDK.zp_draw_rect(59, 0, 70, 6, 2);
		zpSDK.zp_draw_text_ex(59.5, 0.5, HForRoHS, "黑体", 3.5, 0, true, false,
				false);
		zpSDK.zp_draw_text_ex(18, 0, "亞旭進料標籤", "宋体", 3.5, 0, false, false,
				false);
		zpSDK.zp_draw_text_ex(2, 5, "Part No: " + pn, "宋体", 3, 0, true, false,
				false);
		zpSDK.zp_draw_text_ex(2, 8.5, "Q'ty: " + Qty, "宋体", 3, 0, true, false,
				false);
		zpSDK.zp_draw_text_ex(29, 8.5, "Lot No: " + lotno, "宋体", 3, 0, true,
				false, false);
		zpSDK.zp_draw_text_ex(2, 12, "D/C: " + dc, "宋体", 3, 0, true, false,
				false);
		zpSDK.zp_draw_text_ex(29, 12, "V/C: " + vendor, "宋体", 3, 0, true,
				false, false);
		zpSDK.zp_draw_text_ex(2, 15.5, "Serial ID: " + BoxID, "宋体", 3, 0, true,
				false, false);
		zpSDK.zp_draw_text_ex(2, 19, "V/N: " + AppData.VN, "宋体", 3, 0, true,
				false, false);
		zpSDK.zp_draw_text_ex(2, 22.5, "品名規格: " + pndesc_1, "宋体", 3, 0, true,
				false, false);
		zpSDK.zp_draw_text_ex(2, 26, pndesc_2, "宋体", 3, 0, true, false, false);
		String str_2D = pn + "#" + Qty + "#" + lotno + "#" + dc + "#" + vendor
				+ "#" + BoxID;
		zpSDK.zp_draw_barcode2d(56, 13, str_2D,
				zpSDK.BARCODE2D_TYPE.BARCODE2D_QRCODE, 0, 3, 0);
		zpSDK.zp_page_print(false);
		zpSDK.zp_printer_status_detect();
		if (zpSDK.zp_printer_status_get(8000) != 0) {
			Toast.makeText(this, zpSDK.ErrorMessage, Toast.LENGTH_LONG).show();
		}
		// 走到标签处
		zpSDK.zp_goto_mark_label(150);
		//
		zpSDK.zp_page_free();
		zpSDK.zp_close();
		statusBox.Close();
		return true;
	}

	// 異步處理補印
	public void InsertReprintData() {
		new InsertReprintDataAsyncTask().execute();
	}

	class InsertReprintDataAsyncTask extends AsyncTask<String, Void, Boolean> {
		@Override
		protected void onPreExecute() {
			// TODO Auto-generated method stub
			super.onPreExecute();
			showProgressDialog("", "加載中...",
					new DialogInterface.OnCancelListener() {
						public void onCancel(DialogInterface dialog) {
							// TODO Auto-generated method stub
						}
					});
		}

		@Override
		protected Boolean doInBackground(String... params) {
			try {
				String strResponse = InsertSpitDataAdapter.DoInsertSpitData(
						printmtllabel.this, edpn.getText().toString(), edlotno
								.getText().toString(), eddc.getText()
								.toString(), edvc.getText().toString(),
						AppData.reel_5in1, AppData.BoxID_1, "", qty_input, "0",
						AppData.user_id, edemp.getText().toString());
				//
				JSONObject jsobj = new JSONObject(strResponse);
				String res = jsobj.getString("result");
				K_table.excuteres = res;
				if (res.equals("0")) {
					return true;
				} else {
					CommonUtil.errorsound();
					return false;
				}
			} catch (Exception e) {
				return false;
			}
		}

		@Override
		protected void onPostExecute(Boolean result) {
			// TODO Auto-generated method stub
			super.onPostExecute(result);
			try {
				mAlertDialog.dismiss();
				CommonUtil
						.feedbackexcres(printmtllabel.this, K_table.excuteres);
			} catch (Exception ex1) {
				//
			}
		}
	}

	// 異步處理分包裝
	public void InsertSpitData() {
		new InsertSpitDataAsyncTask().execute();
	}

	class InsertSpitDataAsyncTask extends AsyncTask<String, Void, Boolean> {
		@Override
		protected void onPreExecute() {
			// TODO Auto-generated method stub
			super.onPreExecute();
			showProgressDialog("", "加載中...",
					new DialogInterface.OnCancelListener() {
						public void onCancel(DialogInterface dialog) {
							// TODO Auto-generated method stub
						}
					});
		}

		@Override
		protected Boolean doInBackground(String... params) {
			try {
				String balance_qty = String.valueOf(Integer.parseInt(qty_5in1)
						- Integer.parseInt(qty_input));
				//
				String strResponse = InsertSpitDataAdapter.DoInsertSpitData(
						printmtllabel.this, edpn.getText().toString(), edlotno
								.getText().toString(), eddc.getText()
								.toString(), edvc.getText().toString(),
						AppData.reel_5in1, AppData.BoxID_1, AppData.BoxID_2,
						qty_input, balance_qty, AppData.user_id, edemp
								.getText().toString());
				//
				JSONObject jsobj = new JSONObject(strResponse);
				String res = jsobj.getString("result");
				K_table.excuteres = res;
				if (res.equals("0")) {
					return true;
				} else {
					CommonUtil.errorsound();
					return false;
				}
			} catch (Exception e) {
				return false;
			}
		}

		@Override
		protected void onPostExecute(Boolean result) {
			// TODO Auto-generated method stub
			super.onPostExecute(result);
			try {
				mAlertDialog.dismiss();
				CommonUtil
						.feedbackexcres(printmtllabel.this, K_table.excuteres);
			} catch (Exception ex1) {
				//
			}
		}
	}

	public void GetInfoFromWMS() {
		new GetInfoFromWMSAsyncTask().execute();
	}

	class GetInfoFromWMSAsyncTask extends AsyncTask<String, Void, Boolean> {
		@Override
		protected void onPreExecute() {
			// TODO Auto-generated method stub
			super.onPreExecute();
			showProgressDialog("", "加載中...",
					new DialogInterface.OnCancelListener() {

						public void onCancel(DialogInterface dialog) {
							// TODO Auto-generated method stub
						}
					});
		}

		@Override
		protected Boolean doInBackground(String... params) {
			try {
				// get vendor name by vendor code from WMS database
				AppData.VN = "";
				//
				String strResponse = InsertSpitDataAdapter.CheckEMP_request(
						printmtllabel.this, edemp.getText().toString());
				JSONObject jsobj = new JSONObject(strResponse);
				String res = jsobj.getString("result");
				if (!res.equals("0")) {
					AppData.EmpnoIsValid = false;
					return false;
				} else
					AppData.EmpnoIsValid = true;
				//
				strResponse = InsertSpitDataAdapter.GetVNByVC_request(
						printmtllabel.this, String.valueOf(Integer
								.parseInt(edvc.getText().toString())));
				AppData.VN = InsertSpitDataAdapter
						.GetVNByVC_Resolve(strResponse);
				Log.d("VN", AppData.VN);
				// //get pn desc by pn from WMS database
				AppData.PNDesc = "";
				strResponse = InsertSpitDataAdapter.QueryDescByPN_request(
						printmtllabel.this, AppData.pn_5in1);
				AppData.PNDesc = InsertSpitDataAdapter
						.QueryDescByPN_Resolve(strResponse);

				Log.d("PNDesc", AppData.PNDesc);
				// get box id1 from WMS DB
				AppData.BoxID_1 = "";
				AppData.BoxID_2 = "";
				strResponse = InsertSpitDataAdapter
						.Get6in1ID_request(printmtllabel.this);
				AppData.BoxID_1 = InsertSpitDataAdapter
						.Get6in1ID_Resolve(strResponse);
				Log.d("BoxID_1", AppData.BoxID_1);
				// //get box id2 from WMS DB
				strResponse = InsertSpitDataAdapter
						.Get6in1ID_request(printmtllabel.this);
				AppData.BoxID_2 = InsertSpitDataAdapter
						.Get6in1ID_Resolve(strResponse);
				Log.d("BoxID_2", AppData.BoxID_2);
				//
				return true;
			} catch (Exception e) {
				return false;
			}
		}

		@Override
		protected void onPostExecute(Boolean result) {
			// TODO Auto-generated method stub
			super.onPostExecute(result);
			try {
				mAlertDialog.dismiss();
			} catch (Exception ex1) {
				//
			}
		}
	}

	@Override
	public boolean dispatchKeyEvent(KeyEvent event) {
		if (event.getAction() == KeyEvent.ACTION_DOWN) {
			//
			switch (event.getKeyCode()) {
			// case KeyEvent.KEYCODE_BARCODE_SCAN:
			case 223:
				if (btnOpenFlag == false) {
					return true;
				}
				if (btnScanFlag == false) {
					btnScanFlag = true;
					Scan(false);
				}
				return true;
			}
		} else if (event.getAction() == KeyEvent.ACTION_UP) {
			switch (event.getKeyCode()) {
			// case KeyEvent.KEYCODE_BARCODE_SCAN:
			case 223:
				if (btnOpenFlag == false)
					return true;
				btnScanFlag = false;
				StopScan();
				//
				if (edbarcode.hasFocus()) {
					// if (edbarcode.getText().toString().equals("")) {
					// CommonUtil.WMSShowmsg(this, AppData.scandata);
					if (AppData.scandata.equals("")) {
						break;
					}
					//
					if (!checkinput(false)) {
						// 清除刷入框
						CommonUtil.errorsound();
						break;
					}
					// 清除刷入框
					// edbarcode.setEnabled(false);
					// get vendor name & check emp no & get pn desc
					GetInfoFromWMS();
				}
			}
			//
			return true;
		}
		//
		return super.dispatchKeyEvent(event);
	}

	public boolean ListBluetoothDevice() {
		final List<Map<String, String>> list = new ArrayList<Map<String, String>>();
		ListView listView = (ListView) findViewById(R.id.lvmachine);
		SimpleAdapter m_adapter = new SimpleAdapter(this, list,
				android.R.layout.simple_list_item_2, new String[] {
						"DeviceName", "BDAddress" }, new int[] {
						android.R.id.text1, android.R.id.text2 });
		listView.setAdapter(m_adapter);

		if ((myBluetoothAdapter = BluetoothAdapter.getDefaultAdapter()) == null) {
			Toast.makeText(this, "没有找到蓝牙适配器", Toast.LENGTH_LONG).show();
			return false;
		}
		if (!myBluetoothAdapter.isEnabled()) {
			Intent enableBtIntent = new Intent(
					BluetoothAdapter.ACTION_REQUEST_ENABLE);
			startActivityForResult(enableBtIntent, 2);
		}

		Set<BluetoothDevice> pairedDevices = myBluetoothAdapter
				.getBondedDevices();
		if (pairedDevices.size() <= 0)
			return false;
		for (BluetoothDevice device : pairedDevices) {
			Map<String, String> map = new HashMap<String, String>();
			map.put("DeviceName", device.getName());
			map.put("BDAddress", device.getAddress());
			list.add(map);
		}
		//
		listView.setOnItemClickListener(new ListView.OnItemClickListener() {
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				SelectedBDAddress = list.get(position).get("BDAddress");
				if (((ListView) parent).getTag() != null) {
					((View) ((ListView) parent).getTag())
							.setBackgroundDrawable(null);
				}
				((ListView) parent).setTag(view);
				view.setBackgroundColor(Color.BLUE);
				
			}
		});
		//
		listView.setSelection(0);
		SelectedBDAddress = list.get(0).get("BDAddress");
		//
		return true;
	}// ListBluetoothDevice
}
