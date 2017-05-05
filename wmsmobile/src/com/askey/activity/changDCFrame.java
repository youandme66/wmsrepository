package com.askey.activity;

import android.content.DialogInterface;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ListView;

import com.askey.Adapter.ChangedcframeAdapter;
import com.askey.Adapter.ChangedcframeHelper;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.wms.R;

public class changDCFrame extends BaseActivity implements OnClickListener {

	private ImageView ivRefresh;
	private ListView lvTodolistDisplay;
	private ImageView ivGohome;
	private EditText edtpn_search;
	private EditText edtvsub_search;
	//
	private ChangedcframeAdapter mAdapter;
	private ChangedcframeHelper changedcframeHelper = new ChangedcframeHelper();

	@Override
	protected void onCreate(Bundle savedInstanceState) {

		if (AppData.orgid.equals("")) {
			CommonUtil.WMSToast(changDCFrame.this, "請先選擇ORG");
			finish();
		}

		super.onCreate(savedInstanceState);
		setContentView(R.layout.changdcframe);// 设置布局文件
		//
		ivGohome = (ImageView) this.findViewById(R.id.changedcframe_gohome);
		ivGohome.setOnClickListener(this);
		//
		ivRefresh = (ImageView) this.findViewById(R.id.changedcframe_refresh);
		lvTodolistDisplay = (ListView) findViewById(R.id.todolist_display);
		ivRefresh.setOnClickListener(this);
		//
		edtpn_search = (EditText) this.findViewById(R.id.pn_search);
		edtvsub_search = (EditText) this.findViewById(R.id.subinv_search);
		//
		mAdapter = new ChangedcframeAdapter(this);
		// lvTodolistDisplay.setAdapter(mAdapter);
		// GetTodolist();
		/*
		 * lvTodolistItem = (ListView) findViewById(R.id.todolist_display);
		 * lvTodolistItem.setOnItemClickListener(new OnItemClickListener() {//
		 * 为ListView添加项单击事件 // 覆写onItemClick方法
		 * 
		 * @Override public void onItemClick(AdapterView<?> parent, View view,
		 * int position, long id) { //String strInfo =
		 * String.valueOf(((TextView) view).getText());// 记录收入信息 //String strid
		 * = strInfo.substring(0, strInfo.indexOf('|'));// 从收入信息中截取收入编号 Intent
		 * intent = new Intent(changDCFrame.this, changeDCFrame_Detail.class);//
		 * 创建Intent对象 startActivity(intent);// 执行Intent操作 } });
		 */
		edtpn_search
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = edtpn_search;
							// Log.d("BarcodeActivity_2", "3");
							// Log.d("BarcodeActivity_2",
							// "BarcodeActivity OPEN");
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

	@Override
	public boolean dispatchKeyEvent(KeyEvent event) {
		if (event.getAction() == KeyEvent.ACTION_DOWN) {
			Log.d("BarcodeActivity",
					"BarcodeActivity keycode = " + event.getKeyCode());
			switch (event.getKeyCode()) {
			// case KeyEvent.KEYCODE_BARCODE_SCAN:
			case 223:
				if (btnOpenFlag == false)
					return true;
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
				trimedt();
				//
				if(edtpn_search.hasFocus()){
					if(!edtpn_search.getText().toString().equals("")){
						edtvsub_search.requestFocus();
					}
				}
                //
				return true;
			}
		}
		//
		return super.dispatchKeyEvent(event);
	}

    public void trimedt(){
		CommonUtil.trimandupper(edtpn_search);
		CommonUtil.trimandupper(edtvsub_search);
    }
	
	public void onClick(View v) {
		trimedt();
		//
		switch (v.getId()) {
		case R.id.changedcframe_gohome: {
			finish();
			break;
		}
		case R.id.changedcframe_refresh: {
			trimedt();
			//
			String pn = edtpn_search.getText().toString();
			if (pn.indexOf("#") > 0) {
				CommonUtil.Split5in1(changDCFrame.this, pn);
				edtpn_search.setText(AppData.pn_5in1);
			}
			GetTodolist();
			break;
		}
		}
	}

	public void GetTodolist() {
		new TodolistAsyncTask().execute();
	}

	// 参数1：传入doInBackground的参数类型
	// 参数2：传入onProgressUpdate的参数类型
	// 参数3：传入onPostExecute的参数类型，也是doInBackground的返回类型
	class TodolistAsyncTask extends AsyncTask<Void, Void, Boolean> {
		@Override
		protected void onPreExecute() {
			super.onPreExecute();
			showProgressDialog("", "加载中...",
					new DialogInterface.OnCancelListener() {
						public void onCancel(DialogInterface dialog) {
						}
					});
		}

		@Override
		protected void onPostExecute(Boolean result) {
			super.onPostExecute(result);
			mAlertDialog.dismiss();
			//
			lvTodolistDisplay.setAdapter(mAdapter);// 顯示資料
			CommonUtil.feedbackqueryres(changDCFrame.this, result);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strResponse = changedcframeHelper.Request(
						changDCFrame.this, edtpn_search.getText().toString(),
						edtvsub_search.getText().toString(), AppData.orgid);
				changedcframeHelper.Resolve(strResponse);
				return true;
			} catch (Exception e) {
				return false;
			}
		}
	}
}
