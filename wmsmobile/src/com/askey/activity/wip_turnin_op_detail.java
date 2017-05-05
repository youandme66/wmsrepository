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
import android.widget.TextView;

import com.askey.Adapter.WipTurnInAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.model.K_table;
import com.askey.wms.R;

public class wip_turnin_op_detail extends BaseActivity implements
		OnClickListener {
	private ImageView ivgohome, ivcommit;
	private WipTurnInAdapter mAdapter;
	private EditText tunique_id, tinvoice_no, two_no, tregion_name, tpart_no,
			trequired_qty, tbalance_qty, tsubinventory_name, tlocator_name,
			tversion, tframe_name,tInput_qty;
	private String unique_id, invoice_no, wo_no, region_name, part_no,
			required_qty, balance_qty, subinventory_name, locator_name,
			version, frame_name,inputqty;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.wip_turnin_op_detail);// 设置布局文件
		//
		mAdapter = new WipTurnInAdapter(this);
		//
		ivgohome = (ImageView) findViewById(R.id.wip_trurnin_detail_gohome);
		ivgohome.setOnClickListener(this);
		//
		ivcommit = (ImageView) findViewById(R.id.wip_trurnin_detail_commit);
		ivcommit.setOnClickListener(this);
		//
		tunique_id = (EditText) findViewById(R.id.wip_trurnin_detail_edtunique_id);
		tinvoice_no = (EditText) findViewById(R.id.wip_trurnin_detail_edtinvoice_no);
		two_no = (EditText) findViewById(R.id.wip_trurnin_detail_edtwo_no);
		tregion_name = (EditText) findViewById(R.id.wip_trurnin_detail_edtRegion_name);
		tpart_no = (EditText) findViewById(R.id.wip_trurnin_detail_edtPart_no);
		trequired_qty = (EditText) findViewById(R.id.wip_trurnin_detail_edtRequired_qty);
		tbalance_qty = (EditText) findViewById(R.id.wip_trurnin_detail_edtBalance_qty);
		tsubinventory_name = (EditText) findViewById(R.id.wip_trurnin_detail_edtSubinventory_name);
		tlocator_name = (EditText) findViewById(R.id.wip_trurnin_detail_edtLocator_name);
		tversion = (EditText) findViewById(R.id.wip_trurnin_detail_edtVersion);
		tframe_name = (EditText) findViewById(R.id.wip_trurnin_detail_edtFrame_name);
		tInput_qty=(EditText) findViewById(R.id.wip_trurnin_detail_edt_qty);
		//
		Intent intent = this.getIntent();
		//
		tunique_id.setText(intent.getExtras().getString("unique_id").trim());
		tinvoice_no.setText(intent.getExtras().getString("invoice_no").trim());
		two_no.setText(intent.getExtras().getString("wo_no").trim());
		tregion_name
				.setText(intent.getExtras().getString("region_name").trim());
		tpart_no.setText(intent.getExtras().getString("part_no").trim());
		trequired_qty.setText(intent.getExtras().getString("required_qty")
				.trim());
		tbalance_qty
				.setText(intent.getExtras().getString("balance_qty").trim());
		tsubinventory_name.setText(intent.getExtras()
				.getString("subinventory_name").trim());
		tlocator_name.setText(intent.getExtras().getString("locator_name")
				.trim());
		tversion.setText(intent.getExtras().getString("version").trim());
		tInput_qty.setText(intent.getExtras().getString("balance_qty").trim());
		tframe_name.setText(intent.getExtras().getString("frame_name").trim());
		
		//
		tunique_id.setKeyListener(null); // 設置為只讀
		tinvoice_no.setKeyListener(null);
		two_no.setKeyListener(null);
		tregion_name.setKeyListener(null);
		tpart_no.setKeyListener(null);
		trequired_qty.setKeyListener(null);
		tbalance_qty.setKeyListener(null);
		tsubinventory_name.setKeyListener(null);
		tlocator_name.setKeyListener(null);
		tversion.setKeyListener(null);
		//
		tinvoice_no.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = tinvoice_no;
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
		//
		tframe_name.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = tframe_name;
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
	
	public boolean checkinput(){		
		inputqty=tInput_qty.getText().toString().trim().toUpperCase();
		balance_qty=tbalance_qty.getText().toString().trim().toUpperCase();
		frame_name=tframe_name.getText().toString().trim().toUpperCase();
		//
		if(frame_name.equals("")){
			CommonUtil.WMSToast(this, "料架不能爲空");
			return false;
		}
		if (CommonUtil.isNumeric(inputqty)==false){
			CommonUtil.WMSToast(this, "入庫量必須數字");
			return false;
		}
		if(Integer.parseInt(inputqty)>Integer.parseInt(balance_qty)){
			CommonUtil.WMSToast(this, "入庫量不能大於剩餘量");
			return false;
		}
		return true;
	}

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
				return true;
			}
		}
		//
		return super.dispatchKeyEvent(event);
	}

	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.wip_trurnin_detail_gohome: {
			Intent intent = new Intent(wip_turnin_op_detail.this, wip_turnin_op_head.class);
			setResult(RESULT_OK,intent);
			finish();
			break;
		}
		case R.id.wip_trurnin_detail_commit: {
			if (checkinput()==false){
				break;
			}
			wip_turnintrans();
			//
			break;
		}
		}
	}

	public void wip_turnintrans() {
		new wip_turnintransAsyncTask().execute();
	}

	class wip_turnintransAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
			CommonUtil.feedbackexcres(wip_turnin_op_detail.this,
					K_table.excuteres);
			if (K_table.excuteres.equals("0")) {
				Intent intent = new Intent(wip_turnin_op_detail.this, wip_turnin_op_head.class);
				setResult(RESULT_OK,intent);
				finish();//關閉
			}			
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			String strResponse = "";
			try {				
				strResponse = WipTurnInAdapter.CommitWipTurnIn(
						wip_turnin_op_detail.this, 
						tunique_id.getText().toString(), 
						tinvoice_no.getText().toString(), 
						two_no.getText().toString(),
						tregion_name.getText().toString(), 
						tpart_no.getText().toString(),
						trequired_qty.getText().toString(), 
						tbalance_qty.getText().toString(), 
						tsubinventory_name.getText().toString(),
						tlocator_name.getText().toString(), 
						tversion.getText().toString(),
						inputqty,
						frame_name,
						AppData.user_id,
						AppData.orgid);
				
				JSONObject jsobj = new JSONObject(strResponse);
				String res = jsobj.getString("result");
				K_table.excuteres = res;
				if (res.equals("0")) {
					return true;
				} else {
					return false;
				}
			} catch (Exception e) {
				K_table.excuteres = e.getMessage().toString();
				return false;
			}
		}
	}

}
