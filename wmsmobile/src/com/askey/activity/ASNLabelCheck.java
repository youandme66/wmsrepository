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

import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.model.CustomHttpClient;
import com.askey.model.K_table;
import com.askey.wms.R;
/*
第一步：輸入出貨單號從Oracle帶ASN、PO、80料號
第二步：刷入箱號（比對80料號帶出箱內數量）
第三步：刷出貨貼紙PO號
第四步：刷ASN貼紙ASN號
第五步：刷出貨貼紙數量
第六步：刷ASN貼紙數量
若比對OK則自動清空返回第二步作業
若比對NG則報錯，彈出對話框，手動確認后返回第一步作業
*/

public class ASNLabelCheck extends BaseActivity implements OnClickListener {
	//T6in1 founditem = new T6in1();
	//
	//private String pn_5in1, dc_5in1, qty_5in1, boxno_5in1;
	private ImageView ivCommit,ivdelete;
	private ListView lvDisplay;
	private ImageView ivGohome;
	private EditText etshipno,etcarton,etpo,etASN,etshiplabelqty,etASNlabelqty;
	//
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.asnlabelcheck);
		//
		ivGohome = (ImageView) this.findViewById(R.id.ivgohome);
		ivGohome.setOnClickListener(this);
		//
		ivCommit = (ImageView) this.findViewById(R.id.ivcommit);
		ivCommit.setOnClickListener(this);
		//
		ivdelete = (ImageView) this.findViewById(R.id.ivdelete);
		ivdelete.setOnClickListener(this);
		// lvDisplay = (ListView) findViewById(R.id.issue_display);
		//
		etshipno = (EditText) findViewById(R.id.etshipno);
		etcarton = (EditText) findViewById(R.id.etcarton);
		etpo = (EditText) findViewById(R.id.etpo);
		etASN = (EditText) findViewById(R.id.etASN);
		etshiplabelqty = (EditText) findViewById(R.id.etshiplabelqty);
		etASNlabelqty = (EditText) findViewById(R.id.etASNlabelqty);
		//
		etshipno
		.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = etshipno;
					btnOpenFlag = true;
					Open("/dev/ttyHS1", 9600);
				} else {
					btnOpenFlag = false;
					Close();
				}
			}
		});
        //
		etcarton
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = etcarton;
							btnOpenFlag = true;
							Open("/dev/ttyHS1", 9600);
						} else {
							btnOpenFlag = false;
							Close();
						}
					}
				});
		//
		etpo
				.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
					@Override
					public void onFocusChange(View v, boolean hasFocus) {
						if (hasFocus) {
							BaseActivity.mCommReception = etpo;
							btnOpenFlag = true;
							Open("/dev/ttyHS1", 9600);
						} else {
							btnOpenFlag = false;
							Close();
						}
					}

				});
		//
		etASN
		.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = etASN;
					btnOpenFlag = true;
					Open("/dev/ttyHS1", 9600);
				} else {
					btnOpenFlag = false;
					Close();
				}
			}

		});
		//
		etshiplabelqty
		.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = etshiplabelqty;
					btnOpenFlag = true;
					Open("/dev/ttyHS1", 9600);
				} else {
					btnOpenFlag = false;
					Close();
				}
			}

		});
		//
		etASNlabelqty
		.setOnFocusChangeListener(new android.view.View.OnFocusChangeListener() {
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				if (hasFocus) {
					BaseActivity.mCommReception = etASNlabelqty;
					btnOpenFlag = true;
					Open("/dev/ttyHS1", 9600);
				} else {
					btnOpenFlag = false;
					Close();
				}
			}

		});
		registerReadgoodReceiver();
		//
		clearAll();
	}
	//
	public void trimandupperedt() {
		CommonUtil.trimandupper(etshipno);
		CommonUtil.trimandupper(etcarton);
		CommonUtil.trimandupper(etpo);
		CommonUtil.trimandupper(etASN);
		CommonUtil.trimandupper(etshiplabelqty);
		CommonUtil.trimandupper(etASNlabelqty);
	}

	public boolean checkinput() {
		trimandupperedt();
		return true;
	}

	@Override
	public boolean dispatchKeyEvent(KeyEvent event) {
		if (event.getAction() == KeyEvent.ACTION_DOWN) {
			Log.d("BarcodeActivity",
					"BarcodeActivity keycode = " + event.getKeyCode());
			//
			//edtlog.setText("");
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
				if (etshipno.hasFocus()) {
					if (!etshipno.getText().toString().equals("")) {
						etcarton.requestFocus();
						break;
					}
				}
				//
				if (etcarton.hasFocus()) {
					if (!etcarton.getText().toString().equals("")) {
						etpo.requestFocus();
						break;
					}
				}
				//
				if (etpo.hasFocus()) {
					if (!etpo.getText().toString().equals("")) {
						etASN.requestFocus();
						break;
					}
				}
				//
				if (etASN.hasFocus()) {
					if (!etASN.getText().toString().equals("")) {
						etshiplabelqty.requestFocus();
						break;
					}
				}
				//
				if (etshiplabelqty.hasFocus()) {
					if (!etshiplabelqty.getText().toString().equals("")) {
						etASNlabelqty.requestFocus();
						break;
					}
				}
				//
				if (etASNlabelqty.hasFocus()) {
					if (!etASNlabelqty.getText().toString().equals("")) {
						//do check 
						docheck();
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

	public void clearAll() {
		etcarton.setText("");
		etpo.setText("");
		etASN.setText("");
		etshiplabelqty.setText("");
		etASNlabelqty.setText("");		
		//
		etcarton.requestFocus();
	}

	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.ivgohome: {
			finish();
			break;
		}
		case R.id.ivcommit: {
			//do check
			docheck();
			break;
		}
		case R.id.ivdelete:{
			clearAll();
		}
		}
	}

	// 根據單號查詢的線程
	public void docheck() {
		new myAsyncTask().execute();
	}

	class myAsyncTask extends AsyncTask<Void, Void, Boolean> {
		@Override
		protected void onPreExecute() {
			super.onPreExecute();
			showProgressDialog("", "加載中...",
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
			CommonUtil.feedbackqueryres(ASNLabelCheck.this, result);
			clearAll();
			if(!K_table.excuteres.equals("0"))
			{
				CommonUtil.WMSShowmsg(ASNLabelCheck.this,K_table.excuteres);
			}
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				    String strResponse="";
				    //
				    String strURL = String.format(K_table.checkASNAndPO, 
                            etshipno.getText().toString(), etpo.getText().toString(),
                            etcarton.getText().toString(),
                            etASN.getText().toString(),
                            etshiplabelqty.getText().toString(),
                            etASNlabelqty.getText().toString()).replaceAll(" ", "%20");
				    strResponse = CustomHttpClient.getFromWebByHttpClient(ASNLabelCheck.this,
							strURL);
					K_table.excuteres=CommonUtil.GetStringFromResult(strResponse);
					if (!K_table.excuteres.equals("0"))
					{
						return false;
					}
				return true;
			} catch (Exception e) {
				return false;
			}
		}
    }
}
