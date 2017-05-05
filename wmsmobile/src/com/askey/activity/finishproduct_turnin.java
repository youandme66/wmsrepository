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

import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;
import com.askey.wms.R;

public class finishproduct_turnin extends BaseActivity implements
		OnClickListener {
	private ImageView ivgohome, ivcommit;
	private EditText edbarcode;

	protected void onCreate(Bundle savedInstanceState) {
		if (AppData.orgid.equals("")) {
			CommonUtil.WMSToast(finishproduct_turnin.this, "請先選擇ORG");
			finish();
		}
		//
		super.onCreate(savedInstanceState);
		setContentView(R.layout.finishproduct_turnin);// 設置佈局
		//
		ivgohome = (ImageView) this.findViewById(R.id.ivgohome);
		ivgohome.setOnClickListener(this);
		//
		ivcommit = (ImageView) this.findViewById(R.id.ivcommit);
		ivcommit.setOnClickListener(this);
		//
		edbarcode = (EditText) findViewById(R.id.edbarcode);
		//
		edbarcode
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = edbarcode;
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
				CommonUtil.trimandupper(edbarcode);
				//
				if (edbarcode.hasFocus()) {
					if (!edbarcode.getText().toString().equals("")) {
						finishProductTurnIn();
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
		CommonUtil.trimandupper(edbarcode);
		switch (v.getId()) {
		case R.id.ivgohome: {// 返回主界面
			finish();
			break;
		}
		case R.id.ivcommit: {// 查詢
			finishProductTurnIn();
			break;
		}
		}
	}

	public void finishProductTurnIn() {
		new finishProductTurnIn().execute();
	}

	// 参数1：传入doInBackground的参数类型
	// 参数2：传入onProgressUpdate的参数类型
	// 参数3：传入onPostExecute的参数类型，也是doInBackground的返回类型
	class finishProductTurnIn extends AsyncTask<Void, Void, Boolean> {
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
			if(K_table.excuteres.equals("0"))
			{
				edbarcode.setText("");
			}else
			{
				CommonUtil.WMSShowmsg(finishproduct_turnin.this, K_table.excuteres);
			}
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strURL = String
				.format(K_table.finishproductturnin, edbarcode.getText().toString(),
						AppData.user_id,AppData.orgid).replaceAll(" ", "%20");
				String strData = CustomHttpClient.getFromWebByHttpClient(finishproduct_turnin.this,
				strURL);
				K_table.excuteres=CommonUtil.GetStringFromResult(strData);				
				return true;
			} catch (Exception e) {
				K_table.excuteres=e.toString();				
				return false;
			}
		}
	}

}
