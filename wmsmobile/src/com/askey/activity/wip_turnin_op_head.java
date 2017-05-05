package com.askey.activity;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ListView;

import com.askey.Adapter.WipTurnInAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.wms.R;

public class wip_turnin_op_head extends BaseActivity implements
		OnClickListener {
	private ImageView ivgohome, ivrefresh;
	private ListView lvdisplay;
	private EditText edwip_turnin_rcvnosearch;
	private WipTurnInAdapter mAdapter;

	protected void onCreate(Bundle savedInstanceState) {
		if (AppData.orgid.equals("")) {
			CommonUtil.WMSToast(wip_turnin_op_head.this, "請先選擇ORG");
			finish();
		}
		//
		super.onCreate(savedInstanceState);
		setContentView(R.layout.wip_turnin_op_main);// 設置佈局
		//
		ivgohome = (ImageView) this.findViewById(R.id.wip_turnin_gohome);
		ivgohome.setOnClickListener(this);
		//
		ivrefresh = (ImageView) this.findViewById(R.id.wip_turnin_refresh);
		ivrefresh.setOnClickListener(this);
		//
		lvdisplay = (ListView) findViewById(R.id.wip_turnin_display);
		//
		edwip_turnin_rcvnosearch = (EditText) findViewById(R.id.edbarcode);

		mAdapter = new WipTurnInAdapter(this);
		//
		if (!edwip_turnin_rcvnosearch.getText().toString().equals("")) {
			WipTurnInQuery();
		}
		//
		edwip_turnin_rcvnosearch
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = edwip_turnin_rcvnosearch;
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
	
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		switch (resultCode) { // resultCode为回传的标记，我在B中回传的是RESULT_OK
		case RESULT_OK:
			//Bundle b = data.getExtras(); // data为B中回传的Intent
			//String str = b.getString("ListenB");// str即为回传的值"Hello, this is B speaking"
			/* 得到B回传的数据后做什么... 略 */
			WipTurnInQuery();//從detail頁面返回後刷新數據
			break;
		default:
			break;
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
				//
				CommonUtil.trimandupper(edwip_turnin_rcvnosearch);
				//
				if (edwip_turnin_rcvnosearch.hasFocus()) {
					if (!edwip_turnin_rcvnosearch.getText().toString().equals("")) {
						WipTurnInQuery();
						break;
					}
				}
			}
			//
			return true;
		}
		//
		return super.dispatchKeyEvent(event);
	}

	@Override
	public void onClick(View v) {
		// TODO 自動產生的方法 Stub
		CommonUtil.trimandupper(edwip_turnin_rcvnosearch);
		switch (v.getId()) {
		case R.id.wip_turnin_gohome: {// 返回主界面
			finish();
			break;
		}
		case R.id.wip_turnin_refresh: {// 查詢
			WipTurnInQuery();
			break;
		}
		}
	}

	public void WipTurnInQuery() {
		new WipTurnInQueryAsyncTask().execute();
	}

	// 参数1：传入doInBackground的参数类型
	// 参数2：传入onProgressUpdate的参数类型
	// 参数3：传入onPostExecute的参数类型，也是doInBackground的返回类型
	class WipTurnInQueryAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
			lvdisplay.setAdapter(mAdapter);// 顯示資料
			if(AppData.mwipturninquery.size()==1)
				edwip_turnin_rcvnosearch.setText("");//如果沒有數據則清空單號的輸入框
			CommonUtil.feedbackqueryres(wip_turnin_op_head.this, result);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strResponse = WipTurnInAdapter.Request(
						wip_turnin_op_head.this, edwip_turnin_rcvnosearch
								.getText().toString(), AppData.orgid);
				WipTurnInAdapter.Resolve(strResponse);
				return true;
			} catch (Exception e) {
				String s=e.toString();				
				return false;
			}
		}
	}

}
