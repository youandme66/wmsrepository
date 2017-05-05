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

import com.askey.Adapter.queryPoStatusByReceiptNoAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.wms.R;

public class queryPoStatusByReceiptNo extends BaseActivity implements OnClickListener {
	private ImageView ivGohome;
	private ListView lvdetail;
	private ImageView ivrefresh;
	private queryPoStatusByReceiptNoAdapter mAdapter;
	private EditText edReceiptno;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		if (AppData.orgid.equals("")) {
			CommonUtil.WMSToast(this, "請先選擇ORG");
			finish();
		}
		super.onCreate(savedInstanceState);
		setContentView(R.layout.postatusbyreceiptno);// 设置布局文件
		//
		ivGohome = (ImageView) this.findViewById(R.id.ivgohome);
		lvdetail = (ListView) this.findViewById(R.id.lvdetail);
		ivrefresh = (ImageView) this.findViewById(R.id.ivrefresh);
		edReceiptno = (EditText) this.findViewById(R.id.edReceiptno);
		//
		ivGohome.setOnClickListener(this);
		ivrefresh.setOnClickListener(this);
		//
		mAdapter = new queryPoStatusByReceiptNoAdapter(this);
		//
		//
		edReceiptno.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = edReceiptno;
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

	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.ivgohome: {
			finish();
			break;
		}
		case R.id.ivrefresh: {
			GetDetail();
			break;
		}
		}
	}
	
	@Override
	public boolean dispatchKeyEvent(KeyEvent event) {
		if (event.getAction() == KeyEvent.ACTION_DOWN) {
			Log.d("BarcodeActivity",
					"BarcodeActivity keycode = " + event.getKeyCode());
			//
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
			}
			if (edReceiptno.hasFocus()) {
				if(!edReceiptno.getText().toString().equals(""))
				{
					GetDetail();
				}
			
			}
			//
			return true;
		}
		//
		return super.dispatchKeyEvent(event);
	}

	public void GetDetail() {
		new GetDetailAsyncTask().execute();
	}

	// 参数1：传入doInBackground的参数类型
	// 参数2：传入onProgressUpdate的参数类型
	// 参数3：传入onPostExecute的参数类型，也是doInBackground的返回类型
	class GetDetailAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
			lvdetail.setAdapter(mAdapter);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strResponse = queryPoStatusByReceiptNoAdapter.Request(queryPoStatusByReceiptNo.this,
						edReceiptno.getText().toString(),AppData.orgid);
				queryPoStatusByReceiptNoAdapter.Resolve(strResponse);
				return true;
			} catch (Exception e) {
				return false;
			}
		}
	}
}
