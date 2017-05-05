package com.askey.pda.activity;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
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
import android.widget.RadioGroup.OnCheckedChangeListener;
import android.widget.SimpleAdapter;
import android.widget.Toast;

import com.askey.Adapter.InsertSpitDataAdapter;
import com.askey.Adapter.LoginAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity_clear;
import com.askey.model.CommonUtil;
import com.askey.model.K_table;
import com.askey.pda.util.HttpUtils;
import com.askey.pda.util.UrlUtils;
import com.askey.wms.R;

public class PrintActivity extends BaseActivity_clear implements
		OnClickListener {
	
	//打印的类型
	private final static int TYPE_NEW_LABEL = 1;
	private final static int TYPE_RETURN = 2;
	private final static int TYPE_PRINT_AGAIN = 3;
	private final static int TYPE_PRINT_DOUBLE = 4;
	private int typePrint;
	
	private final static String RETURN = "return";
	private final static String PRINT_AGAIN = "again";
	private final static String PRINT_DOUBLE = "double";
	

	private String SelectedBDAddress, barcodetype, printqty;
	private BluetoothAdapter myBluetoothAdapter;
	private boolean isPrintSingle = true;
	private boolean isLoop = false;
	private StatusBox statusBox;
	// private MessageBox megBox;
	private EditText edBarcode, edPn, edQty, edDc, edLotno, edVc, edEmp;
	private ImageView ivGohome, ivCommit;
	private String pn_5in1, dc_5in1, qty_5in1, lotno_5in1, vc_5in1,qty_input,reel_5in1, emp;
	private RadioGroup rg_PrintQty;
	private String HForRoHS;
	private RadioButton rb;
	
	private Map<String,String> printData;
	private String requestUrl,oldID,newID;
	private int isDoubleLabel = 0; //0表示一张没打，1表示打了一张，2表示打了两张，打印完之后归0

	//
	protected void onCreate(Bundle savedInstanceState) {
		// TODO 自動產生的方法 Stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_print);

		initViews();
	
		initData();
		// 这个可以被用来控制键盘，直到使用者确实碰了编辑框
		getWindow().setSoftInputMode(
				WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);

		if (!ListBluetoothDevice()) {
			CommonUtil.WMSToast(this, "藍牙未配對或連接錯誤");
			finish();
		}
		
		edBarcode
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity_clear.mCommReception = edBarcode;
							btnOpenFlag = true;
							Open("/dev/ttyHS1", 9600);
						} else {
							btnOpenFlag = false;
							Close();
						}
					}

				});
		registerReadgoodReceiver();
	}

	private void initData() {
		emp = "";
		
		pn_5in1 = "";
		dc_5in1 = "";
		qty_5in1 = "";
		lotno_5in1 = "";
		vc_5in1 = "";
		
		reel_5in1 = "";
		qty_input = "";
		//
		SelectedBDAddress = "";
		barcodetype = "";
		printqty = "";
	}

	private void initViews() {
		ivGohome = (ImageView) this.findViewById(R.id.printmtllab_gohome);
		ivGohome.setOnClickListener(this);

		ivCommit = (ImageView) this.findViewById(R.id.printmtllab_commit);
		ivCommit.setOnClickListener(this);
		statusBox = new StatusBox(this, ivCommit);
		
		
		edBarcode = (EditText) findViewById(R.id.edt_originalbarcode);
		edPn = (EditText) findViewById(R.id.printmtllab_edtpn);
		edQty = (EditText) findViewById(R.id.printmtllab_edtqty);
		edDc = (EditText) findViewById(R.id.printmtllab_edtdc);
		edLotno = (EditText) findViewById(R.id.printmtllab_edtlotno);
		edVc = (EditText) findViewById(R.id.printmtllab_edtvc);
		edEmp = (EditText) findViewById(R.id.printmtllab_edtemp);
		
		rg_PrintQty = (RadioGroup) findViewById(R.id.rg_printqty);
		
		rg_PrintQty.setOnCheckedChangeListener(new OnCheckedChangeListener(){

			@Override
			public void onCheckedChanged(RadioGroup arg0, int arg1) {
				// TODO Auto-generated method stub
				if(arg0.getCheckedRadioButtonId() == R.id.rb_new){
					edPn.setEnabled(true);
					edDc.setEnabled(true);
					edLotno.setEnabled(true);
					edVc.setEnabled(true);
					edQty.setEnabled(true);
					//当打印类型为 新标签时，禁止扫描
					edBarcode.setEnabled(false);
				}else{
					edPn.setEnabled(false);
					edDc.setEnabled(false);
					edLotno.setEnabled(false);
					edVc.setEnabled(false);
					//当打印类型为 新标签时，允收扫描
					edBarcode.setEnabled(true);
					edQty.setEnabled(true);
					
					//若打印类型是 补印，则无需修改任何数据，重新点击打印即可
					if(arg0.getCheckedRadioButtonId() == R.id.rb_print){
						edQty.setEnabled(false);
					}
				}
			}
			
		});
	}


	@Override
	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.printmtllab_gohome: {
			finish();
			break;
		}
		case R.id.printmtllab_commit: {
			//一次检查输入
			if (checkInput(true)) {
				//二次检查：打印类型不为新类型时，扫描数据后，用户会再次修改，需要二次检查
				if(typePrint == TYPE_RETURN || typePrint == TYPE_PRINT_DOUBLE){
					if(!againCheckPrintData())
						break;
				}
				
				
				try {
					boolean flag = HttpUtils.isNetworkConnected(PrintActivity.this);
	                if(true) {
						switch (typePrint){
							case TYPE_NEW_LABEL://新标签
			                	newLabelRequestNet();
			                	break;
							case TYPE_RETURN://退料
			                	returnLabelRequestNet();
			                	break;
							case TYPE_PRINT_AGAIN://补印
								returnLabelRequestNet();
			                	break;
							case TYPE_PRINT_DOUBLE://补印
								returnLabelRequestNet();
			                	break;
			                }
					}
				/*	if (isPrintSingle) {// 補印
						if (!Printlab(SelectedBDAddress, AppData.pn_5in1,
								qty_input, AppData.lotno_5in1, AppData.dc_5in1,
								AppData.vc_5in1, AppData.BoxID_1)) {
							CommonUtil.WMSToast(print.this,
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
							CommonUtil.WMSToast(print.this,
									"print error");
						} else {
							// 打印第二张条码
							if (!Printlab(SelectedBDAddress, AppData.pn_5in1,
									qty_input, AppData.lotno_5in1,
									AppData.dc_5in1, AppData.vc_5in1,
									AppData.BoxID_2)) {
								CommonUtil.WMSToast(print.this,
										"print error");
							} else {
								InsertSpitData();
							}
						}
					}*/
				} catch (Exception e) {
					CommonUtil.WMSToast(PrintActivity.this,
							"print error");
				} finally {
					statusBox.Close();
					//此处不清空！！！，有的打印类型需要多个AsyncTask，在结束一个操作后再清空！
					//ClearAll();// 清空
				}
				break;
			}
		}
		}

	}
	
	/*
	 * 二次检查数据
	 * 	当打印类型不为新标签时，需要扫描，会走checkInput方法，但是扫描完后用户会二次修改QTY，所以再次判断
	 */
	public boolean againCheckPrintData(){
		qty_input = edQty.getText().toString();
		
		if (!CommonUtil.isNumeric(qty_input)) {
			CommonUtil.WMSToast(this, "輸入的數量必須為整數型");
			return false;
		}
		
		if (Integer.parseInt(qty_input) <= 0) {
			CommonUtil.WMSToast(this, "輸入的數量應大於0");
			return false;
		}
	
		if (Integer.parseInt(qty_input) >= Integer.parseInt(qty_5in1)) {
			CommonUtil.WMSToast(this, "輸入的數量[" + qty_input
						+ "]應小於條碼中數量[" + qty_5in1 + "]");
			return false;
		}
	// 印單張
			/*
			 * if (!(Integer.parseInt(qty_input) ==
			 * Integer.parseInt(qty_5in1))) { CommonUtil.WMSToast(this,
			 * "列印單張不能修改数量"); return false; }
			 */

/*			//
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
		
		//*/
		return true;	
	}

	public void setValueFromAppData() {
		pn_5in1 = AppData.pn_5in1;
		dc_5in1 = AppData.dc_5in1;
		qty_5in1 = AppData.qty_5in1;
		lotno_5in1 = AppData.lotno_5in1;
		vc_5in1 = AppData.vc_5in1;
	}
	
	
	public void trimandupperedt() {
		
		CommonUtil.trimandupper(edBarcode);
		CommonUtil.trimandupper(edQty);
		CommonUtil.trimandupper(edEmp);
		
		pn_5in1 = "";
		dc_5in1 = "";
		qty_5in1 = "";
		lotno_5in1 = "";
		vc_5in1 = "";
		
		reel_5in1 = "";
	}

	/*
	 * 一次检查
	 * 	只检查一些简单数据，因为扫描数据后会走此方法
	 */
	public boolean checkInput(Boolean isCommit) {
		typePrint = 0;
		emp = edEmp.getText().toString();
				
		if (edEmp.getText().toString().equals("")) {
			CommonUtil.WMSToast(this, "工號不能為空");
			return false;
		}
		try {
			
			//trimandupperedt();

			for (int i = 0; i < rg_PrintQty.getChildCount(); i++) {
				rb = (RadioButton) rg_PrintQty.getChildAt(i);
				if (rb.isChecked()) {
					switch(i){
					case 0:
						typePrint = TYPE_NEW_LABEL;
						break;
					case 1:
						typePrint = TYPE_RETURN;
						break;
					case 2:
						typePrint = TYPE_PRINT_AGAIN;
						break;
					case 3:
						typePrint = TYPE_PRINT_DOUBLE;
						break;
					}
				}
			}
			

				//如果打印类型不为1，则需要扫码来获取数据
				if(typePrint != 1){	
					if(!isCommit){
						
						String barcode = AppData.scandata;
						if (!CommonUtil.Split5in1(PrintActivity.this, barcode)) {
							return false;
						}
						
						setValueFromAppData();
						
						// 扫描框赋值
						edPn.setText(pn_5in1);
						edDc.setText(dc_5in1);
						edLotno.setText(lotno_5in1);
						edVc.setText(vc_5in1);
						edQty.setText(qty_5in1);
						
						//获取旧id
						oldID = AppData.reel_5in1;
					}
					
				}else{
					//新标签
					AppData.pn_5in1 = edPn.getText().toString();
					AppData.dc_5in1 = edDc.getText().toString();
					AppData.qty_5in1 = edQty.getText().toString();
					AppData.lotno_5in1 = edLotno.getText().toString();
					AppData.vc_5in1 = edVc.getText().toString();
					
					setValueFromAppData();
				}
			
			if (pn_5in1.equals("")) {
				CommonUtil.WMSToast(this, "pn不可为空");
				return false;
			}
			if (dc_5in1.equals("")) {
				CommonUtil.WMSToast(this, "dc不可为空");
				return false;
			}
			if (lotno_5in1.equals("")) {
				CommonUtil.WMSToast(this, "lotno不可为空");
				return false;
			}
			if (vc_5in1.equals("")) {
				CommonUtil.WMSToast(this, "vc不可为空");
				return false;
			}
			if (qty_5in1.equals("")) {
				CommonUtil.WMSToast(this, "qty不可为空");
				return false;
			}
			
			if (!CommonUtil.isNumeric(qty_5in1)) {
				CommonUtil.WMSToast(this, "輸入的數量必須為整數型");
				return false;
			}
			
			if (Integer.parseInt(qty_5in1) <= 0) {
				CommonUtil.WMSToast(this, "輸入的數量應大於0");
				return false;
			}
		
			
		} catch (Exception e) {
			CommonUtil.WMSToast(this, e.getMessage().toString());
			return false;
		}
		return true;
	}


	
	//-----------------------------------------------------------------------------
	
	/*
	 * 将参数值  赋值到  map中，方便构建URl
	 *  QTY值比较特殊，需要另外赋值
	 */
	public HashMap<String, String> setPrintData(){
		printData = new HashMap<String, String>();
    	printData.put("lot_no", lotno_5in1);
    	printData.put("datecode", dc_5in1);
    	printData.put("vendor_code", vc_5in1);
 
    	return (HashMap<String, String>) printData;
	}
	
	/*
	 * 打印类型为 新标签  时的网络请湫业务逻辑
	 */
	public void newLabelRequestNet(){
		//获取Url
		setPrintData();
		printData.put("pn", pn_5in1);
		printData.put("user_name", emp);
		printData.put("qty", qty_5in1);
    	requestUrl = UrlUtils.getPrintUrl((printData), UrlUtils.DETAIL_TABLE);
    		
    	//启动异步任务
		InsertDataToDetailTableAsyncTask insertDataToDetailTableAsyncTask = new InsertDataToDetailTableAsyncTask();
		insertDataToDetailTableAsyncTask.execute();	
		
		ClearAll();
	}
	
	
	
	/*
	 *  打印类型为 退料  时的网络请湫业务逻辑
	 */
	public void returnLabelRequestNet(){
		/*获取Url
		 * 
		 * 注意：此处的Qty 并非qty_5in1，而是二次输入过后的qty_input
		 */
		setPrintData();
		printData.put("pn", pn_5in1);
		printData.put("user_name", emp);
		//退料和补印共用此方法，只是两者的 qty不同
		if(typePrint == TYPE_PRINT_AGAIN){
			printData.put("qty", qty_5in1);
		}else{
			printData.put("qty", qty_input);	
		}
		requestUrl = UrlUtils.getPrintUrl(printData, UrlUtils.DETAIL_TABLE);
		
		//启动异步任务
		InsertDataToDetailTableAsyncTask insertDataToDetailTableAsyncTask = new InsertDataToDetailTableAsyncTask();
		insertDataToDetailTableAsyncTask.execute();		
	
	}
	
	
	/*
	 * 后续只能写在AsyncTask成功之后，才会被执行
	 * 		不然将以下逻辑写在returnLabelRequestNet中，在onPostExecute之前就会执行，因此 newID为空
	 */
	public void returnLabelSecond(){
		/* 	
		 * wms_6in1_detail表插入完毕后，还需要将新旧ID和相关数据插入到 wms_6in1_id_relation表中
		*	逻辑为退料时，需要将新、旧的id参数也传入
		*/
		setPrintData();
		//退料和补印共用此方法，只是两者的 qty不同
		if(typePrint == TYPE_PRINT_AGAIN){
			printData.put("qty", qty_5in1);
			printData.put("type", PRINT_AGAIN);
		}else{
			printData.put("qty", qty_input);
			if(typePrint == TYPE_RETURN){
				printData.put("type", RETURN);
			}else{
				printData.put("type", PRINT_DOUBLE);
			}
		}
		printData.put("create_by", emp);
		printData.put("prior_id", oldID);
		printData.put("current_id", newID);
		requestUrl = UrlUtils.getPrintUrl(printData, UrlUtils.RELATION_TABLE);
		//启动异步任务
		InsertDataToRelationTableAsyncTask insertDataToRelationTableAsyncTask = new InsertDataToRelationTableAsyncTask();
		insertDataToRelationTableAsyncTask.execute();	
		
		if(typePrint != TYPE_PRINT_DOUBLE){
			ClearAll();
		}
	}
	
	
	/*
	 * 分包装类型：  第一次打印已经结束，第二次打印开始，存入detail表
	 */
	public void doubleLabelRequestNet(){
		setPrintData();
		printData.put("pn", pn_5in1);
		printData.put("user_name", emp);
		//退料和补印共用此方法，只是两者的 qty不同
		String restQty = Integer.parseInt(qty_5in1) - Integer.parseInt(qty_input)+"";
		printData.put("qty", restQty);
		
		requestUrl = UrlUtils.getPrintUrl(printData, UrlUtils.DETAIL_TABLE);
		
		//启动异步任务
		InsertDataToDetailTableAsyncTask insertDataToDetailTableAsyncTask = new InsertDataToDetailTableAsyncTask();
		insertDataToDetailTableAsyncTask.execute();		
		
	}
	
	/*
	 * 分包装类型：  第一次打印已经结束，第二次打印,存入relation表
	 */
	public void doubleLabelSecond(){
		setPrintData();
		String restQty = Integer.parseInt(qty_5in1) - Integer.parseInt(qty_input)+"";
		printData.put("qty", restQty);
		printData.put("create_by", emp);
		printData.put("prior_id", oldID);
		printData.put("current_id", newID);
		printData.put("type", PRINT_DOUBLE);
		requestUrl = UrlUtils.getPrintUrl(printData, UrlUtils.RELATION_TABLE);
		//启动异步任务
		InsertDataToRelationTableAsyncTask insertDataToRelationTableAsyncTask = new InsertDataToRelationTableAsyncTask();
		insertDataToRelationTableAsyncTask.execute();
		
		//分包装逻辑结束
		ClearAll();
	}
	
	
	
	
	
	/*
	 * 打印类型为新标签、  退印  的AsyncTask
	 */
	class InsertDataToDetailTableAsyncTask extends AsyncTask<Void, String, String> {
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
		protected String doInBackground(Void... params) {
			 String str;
			try {
				//调用方法进行GET方式的网络请求
				str = HttpUtils.getMethodConnectNet(requestUrl);
				return str;
			} catch (Exception e) {
				return null;
			}
		}

		@Override
		protected void onPostExecute(String result) {
			// TODO Auto-generated method stub
			super.onPostExecute(result);
			try {
				mAlertDialog.dismiss();

				//数据库添加成功，获取到自增id，可以开始打印
				newID = result;
				boolean flag;
				if(Integer.parseInt(result) == -1 || Integer.parseInt(result) == 404){
					CommonUtil.WMSToast(PrintActivity.this,"save error");
					return;
				}
				
				if(typePrint == TYPE_NEW_LABEL || typePrint == TYPE_PRINT_AGAIN){
					flag = Printlab(SelectedBDAddress, AppData.pn_5in1,
							AppData.qty_5in1, AppData.lotno_5in1, AppData.dc_5in1,
							AppData.vc_5in1, result);
				}else{
					//打印类型不为 新标签 时，数量会二次输入，存入的值为qty_input，不是AppData.qty_5in1
					//打印类型为新包装时的第二次打印，数量为 qty_5in1 - qty_input
					if(isDoubleLabel == 1 && typePrint == TYPE_PRINT_DOUBLE){
						String restQty = Integer.parseInt(qty_5in1) - Integer.parseInt(qty_input)+"";
						flag = Printlab(SelectedBDAddress, AppData.pn_5in1,
								restQty, AppData.lotno_5in1, AppData.dc_5in1,
								AppData.vc_5in1, result);	
					}else{
						flag = Printlab(SelectedBDAddress, AppData.pn_5in1,
								qty_input, AppData.lotno_5in1, AppData.dc_5in1,
								AppData.vc_5in1, result);
					 	}
					}
				
				if (flag) {
					CommonUtil.WMSToast(PrintActivity.this,"print success");
					//退料类型的后续操作
					returnLabelSecond();
				}else{
					CommonUtil.WMSToast(PrintActivity.this,"print error");
				}
				
				//分包装类型的第二次打印
				if(isDoubleLabel == 1){
					doubleLabelSecond();
				}
				
			} catch (Exception ex1) {
				mAlertDialog.dismiss();
			}
		}
	}
	
	
	
	/*
	 * 
	 */
	class InsertDataToRelationTableAsyncTask extends AsyncTask<Void, String, String> {
		@Override
		protected void onPreExecute() {
			// TODO Auto-generated method stub
			super.onPreExecute();
			/*showProgressDialog("", "加載中...aaaaaaa",
					new DialogInterface.OnCancelListener() {
						public void onCancel(DialogInterface dialog) {
							// TODO Auto-generated method stub
						}
					});*/
		}

		@Override
		protected String doInBackground(Void... params) {
			 String str, readerline;
			try {
				//调用方法进行GET方式的网络请求
				str = HttpUtils.getMethodConnectNet(requestUrl);
				return str;
			} catch (Exception e) {
				return null;
			}
		}

		@Override
		protected void onPostExecute(String result) {
			// TODO Auto-generated method stub
			super.onPostExecute(result);
			try {
				int flag = Integer.parseInt(result);
				if (flag == 1) {
					CommonUtil.WMSToast(PrintActivity.this,"save success");
				}else 
					CommonUtil.WMSToast(PrintActivity.this,"save error");
	
				
				if(typePrint == TYPE_PRINT_DOUBLE){
					if(isDoubleLabel == 0){
						//分包装第一次打印结束，开始剩余数量的第二次打印
						doubleLabelRequestNet();
						isDoubleLabel = 1;
					}else if(isDoubleLabel == 1)
						isDoubleLabel = 0; 
				}
				
			} catch (Exception ex1) {
			}
		}
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	//-----------------------------------------------------------------------------------
	
	public void ClearAll() {
		edBarcode.setText("");
		edDc.setText("");
		edLotno.setText("");
		edPn.setText("");
		edQty.setText("");
		edVc.setText("");
		//
		pn_5in1 = "";
		dc_5in1 = "";
		qty_5in1 = "";
		lotno_5in1 = "";
		vc_5in1 = "";
		reel_5in1 = "";
		qty_input = "";
		//
		AppData.VN = "";
		AppData.PNDesc = "";
		AppData.BoxID_1 = "";
		AppData.BoxID_2 = "";
		//
		edBarcode.requestFocus();
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
		
		Qty = String.format("%06d", Integer.parseInt(Qty));
		vendor = String.format("%05d", Integer.parseInt(vendor));
		//
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
		}
		//
		statusBox.Show("正在打印...");
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
/*		zpSDK.zp_draw_text_ex(59.5, 0.5, HForRoHS, "黑体", 3.5, 0, true, false,
				false);*/
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
/*		zpSDK.zp_draw_text_ex(2, 19, "V/N: " + AppData.VN, "宋体", 3, 0, true,
				false, false);
		zpSDK.zp_draw_text_ex(2, 22.5, "品名規格: " + pndesc_1, "宋体", 3, 0, true,
				false, false);*/
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

	
/*	public void GetInfoFromWMS() {
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
				
				String strResponse = InsertSpitDataAdapter.CheckEMP_request(
						PrintActivity.this, edEmp.getText().toString());
				JSONObject jsobj = new JSONObject(strResponse);
				String res = jsobj.getString("result");
				if (!res.equals("0")) {
					AppData.EmpnoIsValid = false;
					return false;
				} else
					AppData.EmpnoIsValid = true;
				// 
				strResponse = InsertSpitDataAdapter.GetVNByVC_request(
						PrintActivity.this, String.valueOf(Integer
								.parseInt(edVc.getText().toString())));
				AppData.VN = InsertSpitDataAdapter
						.GetVNByVC_Resolve(strResponse);
				Log.d("VN", AppData.VN);
				// //get pn desc by pn from WMS database
				AppData.PNDesc = "";
				strResponse = InsertSpitDataAdapter.QueryDescByPN_request(
						PrintActivity.this, AppData.pn_5in1);
				AppData.PNDesc = InsertSpitDataAdapter
						.QueryDescByPN_Resolve(strResponse);

				Log.d("PNDesc", AppData.PNDesc);
				// get box id1 from WMS DB
				AppData.BoxID_1 = "";
				AppData.BoxID_2 = "";
				strResponse = InsertSpitDataAdapter
						.Get6in1ID_request(PrintActivity.this);
				AppData.BoxID_1 = InsertSpitDataAdapter
						.Get6in1ID_Resolve(strResponse);
				Log.d("BoxID_1", AppData.BoxID_1);
				// //get box id2 from WMS DB
				strResponse = InsertSpitDataAdapter
						.Get6in1ID_request(PrintActivity.this);
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
*/
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
				if (edBarcode.hasFocus()) {
					// if (edbarcode.getText().toString().equals("")) {
					// CommonUtil.WMSShowmsg(this, AppData.scandata);
					if (AppData.scandata.equals("")) {
						break;
					}
					/*
					 * 传递false,代表此检查时为了扫描后赋值，Qty数据可以二次修改
					 */
					
					if (!checkInput(false)) {
						// 清除刷入框
						CommonUtil.errorsound();
						break;
					}
					// 清除刷入框
					// edbarcode.setEnabled(false);
					// get vendor name & check emp no & get pn desc
					
					
					//GetInfoFromWMS();
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
