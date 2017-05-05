package com.askey.activity;

import org.json.JSONObject;

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

import com.askey.Adapter.SimulateTransAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.model.K_table;
import com.askey.wms.R;

public class simulateTrans extends BaseActivity implements OnClickListener {
	// Tissuequery founditem = new Tissuequery();
	//
	// private String pn_5in1, dc_5in1, qty_5in1;
	private ImageView ivCommit;
	// private ListView lvDisplay;
	private ImageView ivGohome;
	private EditText edtsimulateid;
	private SimulateTransAdapter mAdapter;

	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		if (AppData.orgid.equals("")) {
			CommonUtil.WMSToast(simulateTrans.this, "請先選擇ORG");
			finish();
		}
		setContentView(R.layout.simulatetrans);
		//
		ivGohome = (ImageView) this.findViewById(R.id.simulateTrans_gohome);
		ivGohome.setOnClickListener(this);
		ivCommit = (ImageView) this.findViewById(R.id.simulateTrans_commit);
		ivCommit.setOnClickListener(this);
		// lvDisplay = (ListView) findViewById(R.id.issue_display);
		//
		edtsimulateid = (EditText) findViewById(R.id.simulateTrans_simulateid);
		//
		mAdapter = new SimulateTransAdapter(this);
		//
		edtsimulateid
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = edtsimulateid;
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

	public void trimandupperedt() {
		CommonUtil.trimandupper(edtsimulateid);
	}

	public boolean checkinput() {
		trimandupperedt();
		//
		if(!CommonUtil.isNumeric(edtsimulateid.getText().toString())){
			CommonUtil.WMSShowmsg(this, "備料單應爲數字型");
			return false;
		}
		//
		return true;
	}

	@Override
	public boolean dispatchKeyEvent(KeyEvent event) {
		if (event.getAction() == KeyEvent.ACTION_DOWN) {
			Log.d("BarcodeActivity",
					"BarcodeActivity keycode = " + event.getKeyCode());
			//
			// edtlog.setText("");
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
				trimandupperedt();
				//
				if (edtsimulateid.hasFocus()) {
					if (!edtsimulateid.getText().toString().equals("")) {
						simulate_trans();
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

	public void cleardone() {
		// edtbarcode.setText("");
		// edtframe.setText("");
		// edtbarcode.requestFocus();
	}

	public void onClick(View v) {
		CommonUtil.trimandupper(edtsimulateid);
		//
		switch (v.getId()) {
		case R.id.simulateTrans_gohome: {
			finish();
			break;
		}
		case R.id.simulateTrans_commit: {
			if (checkinput()) {
				simulate_trans();
				break;
			}
		}
		}
	}

	// 處理模擬扣帳的線程
	public void simulate_trans() {
		new simulate_transAsyncTask().execute();
	}

	class simulate_transAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
			//
			CommonUtil.feedbackexcres(simulateTrans.this, K_table.excuteres);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strResponse = "";
				strResponse = SimulateTransAdapter.SimulateTrans(
						simulateTrans.this, edtsimulateid.getText().toString(),
						AppData.orgid, AppData.user_id);
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
				K_table.excuteres = e.getMessage().toString();
				CommonUtil.errorsound();
				return false;
			}
		}
	}

}
