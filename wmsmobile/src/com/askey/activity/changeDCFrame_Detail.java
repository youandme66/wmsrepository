package com.askey.activity;

import org.json.JSONObject;

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

import com.askey.Adapter.ChangedcframeHelper;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.model.K_table;
import com.askey.wms.R;

public class changeDCFrame_Detail extends BaseActivity implements
		OnClickListener {
	public String sUniqueID;
	public String sItem_name;
	public String ssub_inv;
	public String sonhand_qty;
	public String sSim_qty;
	public String sDC;
	public String sFrame;
	private ChangedcframeHelper changedcframeHelper = new ChangedcframeHelper();
	//
	private ImageView ivgohome, ivcommit;
	private EditText edtpn, edtsub_inv, edtonhand_qty, edtsim_qty, edtdc,
			edtframe;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.changedcframedetail);// 设置布局文件
		//
		ivgohome = (ImageView) findViewById(R.id.changedcframedetail_gohome);
		ivgohome.setOnClickListener(this);
		//
		ivcommit = (ImageView) findViewById(R.id.changedcframedetail_commit);
		ivcommit.setOnClickListener(this);

		//
		edtpn = (EditText) findViewById(R.id.edtPN);
		edtsub_inv = (EditText) findViewById(R.id.edtsubinv);
		edtonhand_qty = (EditText) findViewById(R.id.edtonhandqty);
		edtsim_qty = (EditText) findViewById(R.id.edtsimqty);
		edtdc = (EditText) findViewById(R.id.edtdc);
		edtframe = (EditText) findViewById(R.id.edtframe);
		//
		Intent intent = this.getIntent();
		sUniqueID = intent.getExtras().getString("unique_id");
		sUniqueID = sUniqueID.trim();
		sItem_name = intent.getExtras().getString("item_name");
		sItem_name = sItem_name.trim();
		ssub_inv = intent.getExtras().getString("subinventory_name");
		ssub_inv = ssub_inv.trim();
		sonhand_qty = intent.getExtras().getString("onhand_qty");
		sonhand_qty = sonhand_qty.trim();
		sSim_qty = intent.getExtras().getString("simulated_qty");
		sSim_qty = sSim_qty.trim();
		sDC = intent.getExtras().getString("datecode");
		sDC = sDC.trim();
		sFrame = intent.getExtras().getString("frame");
		sFrame = sFrame.trim();
		//
		edtpn.setText(sItem_name);
		edtsub_inv.setText(ssub_inv);
		edtonhand_qty.setText(sonhand_qty);
		edtsim_qty.setText(sSim_qty);
		edtdc.setText(sDC);
		edtframe.setText(sFrame);
		//
		edtpn.setKeyListener(null); // 設置為只讀
		edtsub_inv.setKeyListener(null);
		//
		edtframe.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = edtframe;
					// Log.d("BarcodeActivity_2", "3");
					// Log.d("BarcodeActivity_2", "BarcodeActivity OPEN");
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
				if (edtframe.hasFocus()) {
					trimedt();
                    //
					if (!edtframe.getText().toString().equals("")) {
						if (checkinput()) {
							Updatedcframe();
						} else {
							CommonUtil.errorsound();
						}
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
		if (edtdc.getText().toString().equals("")) {
			CommonUtil.WMSToast(changeDCFrame_Detail.this, "D/C不能為空");
			return false;
		}
		if (edtonhand_qty.getText().toString().equals("")) {
			CommonUtil.WMSToast(changeDCFrame_Detail.this, "在手量不能為空");
			return false;
		}
		if (edtsim_qty.getText().toString().equals("")) {
			CommonUtil.WMSToast(changeDCFrame_Detail.this, "模擬量不能為空");
			return false;
		}
		if (edtframe.getText().toString().equals("")) {
			CommonUtil.WMSToast(changeDCFrame_Detail.this, "料架不能為空");
			return false;
		}
		//
		return true;
	}
	
	public void trimedt(){
		CommonUtil.trimandupper(edtonhand_qty);
		CommonUtil.trimandupper(edtsim_qty);
		CommonUtil.trimandupper(edtdc);
		CommonUtil.trimandupper(edtframe);		
	}

	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.changedcframedetail_gohome: {
			finish();
			break;
		}
		case R.id.changedcframedetail_commit: {
			trimedt();
			//
			if (checkinput() == false) {
				CommonUtil.errorsound();
				break;
			}
			Updatedcframe();
			//
			break;
		}
		}
	}

	public void Updatedcframe() {
		new UpdatedcframeAsyncTask().execute();
	}

	// 参数1：传入doInBackground的参数类型
	// 参数2：传入onProgressUpdate的参数类型
	// 参数3：传入onPostExecute的参数类型，也是doInBackground的返回类型
	class UpdatedcframeAsyncTask extends AsyncTask<Void, Void, Boolean> {
		@Override
		protected void onPreExecute() {
			super.onPreExecute();
			showProgressDialog("", "執行中...",
					new DialogInterface.OnCancelListener() {
						public void onCancel(DialogInterface dialog) {
						}
					});
		}

		@Override
		protected void onPostExecute(Boolean result) {
			super.onPostExecute(result);
			mAlertDialog.dismiss();
			CommonUtil.feedbackexcres(changeDCFrame_Detail.this,
					K_table.excuteres);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			String strResponse = "";
			try {
				strResponse = changedcframeHelper.Updatedcframe(
						changeDCFrame_Detail.this, sUniqueID, edtsub_inv
								.getText().toString(), edtonhand_qty.getText()
								.toString(), edtsim_qty.getText().toString(),
						edtdc.getText().toString(), edtframe.getText()
								.toString());
				JSONObject jsobj = new JSONObject(strResponse);
				String res = jsobj.getString("result");
				K_table.excuteres = res;
				if (res.equals("0")) {
					return true;
				} else {
					CommonUtil.errorsound();
					return false;
				}
				// return true;
			} catch (Exception e) {
				K_table.excuteres = strResponse;
				return false;
			}
		}
	}
}
