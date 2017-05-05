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

import com.askey.Adapter.QueryStockAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.wms.R;

public class queryStock extends BaseActivity implements OnClickListener {

	private ImageView ivRefresh;
	private ListView lvDisplay;
	private ImageView ivGohome;
	private EditText edtpn_search,edtsubinv_search, edtdc_search; 
	//
	private QueryStockAdapter mAdapter;

	@Override
	protected void onCreate(Bundle savedInstanceState) {

		if (AppData.orgid.equals("")) {
			CommonUtil.WMSToast(queryStock.this, "請先選擇ORG");
			finish();
		}

		super.onCreate(savedInstanceState);
		setContentView(R.layout.querystock);// 设置布局文件
		//
		ivGohome = (ImageView) this.findViewById(R.id.querystock_gohome);
		ivGohome.setOnClickListener(this);
		//
		ivRefresh = (ImageView) this.findViewById(R.id.querystock_refresh);
		lvDisplay = (ListView) findViewById(R.id.querystock_display);
		ivRefresh.setOnClickListener(this);
		//
		edtpn_search = (EditText) this.findViewById(R.id.querystock_pnsearch);
		edtdc_search = (EditText) this.findViewById(R.id.querystock_dcsearch);
		edtsubinv_search = (EditText) this
				.findViewById(R.id.querystock_subinvsearch);
		//
		mAdapter = new QueryStockAdapter(this);
		//
		edtpn_search
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = edtpn_search;
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
				if(edtpn_search.hasFocus()){
					if(!edtpn_search.getText().toString().equals("")){
						edtdc_search.requestFocus();
					}
				}
				//
				return true;
			}
		}
		//
		return super.dispatchKeyEvent(event);
	}

	public Boolean checkinput() {
		if ((edtpn_search.getText().toString().equals(""))
				&& (edtsubinv_search.getText().toString().equals(""))) {
			CommonUtil.WMSToast(queryStock.this, "料號和廠庫不能都為空");
			return false;
		}
		//
		return true;
	}
	
	public void trimedt(){
		CommonUtil.trimandupper(edtpn_search);
		CommonUtil.trimandupper(edtdc_search);
		CommonUtil.trimandupper(edtsubinv_search);
	}

	public void onClick(View v) {
		trimedt();
		//
		switch (v.getId()) {
		case R.id.querystock_gohome: {
			finish();
			break;
		}
		case R.id.querystock_refresh: {
			if (checkinput() == false) {
				break;
			}
			String pn = edtpn_search.getText().toString();
			if (pn.indexOf("#") > 0) {
				CommonUtil.Split5in1(queryStock.this, pn);
				edtpn_search.setText(AppData.pn_5in1);
			}
			GetData();
			break;
		}
		}
	}

	public void GetData() {
		new QueryStockAsyncTask().execute();
	}

	// 参数1：传入doInBackground的参数类型
	// 参数2：传入onProgressUpdate的参数类型
	// 参数3：传入onPostExecute的参数类型，也是doInBackground的返回类型
	class QueryStockAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
			lvDisplay.setAdapter(mAdapter);// 顯示資料
			CommonUtil.feedbackqueryres(queryStock.this, result);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String pn_search = edtpn_search.getText().toString();
				String dc_search = edtdc_search.getText().toString();
				String subinv = edtsubinv_search.getText().toString();
				//
				// pn_search="0315-00M4000";
				// dc_search="201202";
				// subinv="531";
				// AppData.orgid="26";
				//
				String strResponse = QueryStockAdapter.Request(queryStock.this,
						pn_search, dc_search, subinv, AppData.orgid);
				//
				QueryStockAdapter.Resolve(strResponse);
				return true;
			} catch (Exception e) {
				return false;
			}
		}
	}
}
